using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.IO;
using LANnode;
using System.Threading;

namespace ChatServer
{
    class ConnectionHandler
    {
        public delegate void DisconnectClient();
        public event DisconnectClient EventDisconnectClient;
        public string Name;
        Serializer messageSerializer;
        Socket socketClientHandler;
        Thread threadHandleClient;
        public bool IsConnected;
        public ConnectionHandler(Socket socketClientHandler)
        {
            Name = "";
            messageSerializer = new Serializer();
            IsConnected = true;
            EventDisconnectClient += RemoveClient;
            EventDisconnectClient += UpdateClientsList;
            this.socketClientHandler = socketClientHandler;
            socketClientHandler.ReceiveTimeout = 1000;
            socketClientHandler.SendTimeout = 1000;
            Server.clients.Add(socketClientHandler.RemoteEndPoint.GetHashCode(), socketClientHandler);
            threadHandleClient = new Thread(HandleClient);
            threadHandleClient.Start();
        }

        public void RemoveClient()
        {
            Console.WriteLine(Server.clientNames[socketClientHandler.RemoteEndPoint.GetHashCode()] + " has left the chat");
            Server.RemoveClient(socketClientHandler.RemoteEndPoint.GetHashCode());
            socketClientHandler.Close();
        }

        public void UpdateClientsList()
        {
            List<Endpoints> CurList = GetClientsList();
            Server.SendToAll(new Message(CurList));
        }
        public void OnDisconnectClient()
        {
            EventDisconnectClient();
        }
        bool IsClientConnected()
        {
            bool IsConnected = true;
            try
            {
                socketClientHandler.Send(new byte[] { 1, 1, 1 });
                if (!socketClientHandler.Connected)
                    IsConnected = false;
            }
            catch
            {
                IsConnected = false;
            }
            return IsConnected;
        }

        public List<Endpoints> GetClientsList()
        {
            List<Endpoints> info = new List<Endpoints>();
            foreach (KeyValuePair<int, string> pair in Server.clientNames)
            {
                info.Add(new Endpoints() { Key = pair.Key.GetHashCode(), Name = pair.Value });
            }
            return info;
        }

        void HandleRegistrationMessage(Message message)
        {
            Name = message.Name;
            Server.clientNames.Add(socketClientHandler.RemoteEndPoint.GetHashCode(), message.Name);
            Console.WriteLine(message.Name + " joined the chat");
            List<Endpoints> CurList = GetClientsList();
            Server.SendToAll(new Message(CurList));
        }

        void HandleCommonMesssage(Message message)
        {
            Console.WriteLine((string)Server.clientNames[socketClientHandler.RemoteEndPoint.GetHashCode()]
                       + " : " + message.Content);
            message.Name = Server.clientNames[socketClientHandler.RemoteEndPoint.GetHashCode()];
            Server.SendToAll(message);
            Server.MessageHistory.Add(message.Name + ": " + message.Content + "  " + message.Time + "  " + message.IPAdress);
        }

        void HandlePrivateMessage(Message message)
        {
            message.Name = Server.clientNames[socketClientHandler.RemoteEndPoint.GetHashCode()];
            if (Server.clients.ContainsKey(message.ReceiverID))
            {
                message.SenderID = socketClientHandler.RemoteEndPoint.GetHashCode();
                message.Content = Server.clientNames[socketClientHandler.RemoteEndPoint.GetHashCode()]
                    + ": " + message.Content;
                Server.SendMessage(message, Server.clients[message.ReceiverID]);
            }

        }

        void HandleHistoryRequest(Message message)
        {
            Message responseMessage = new Message(Server.MessageHistory, message.ReceiverID);
            Server.SendMessage(responseMessage, socketClientHandler);
        }

        void HandleMessage(Message message)
        {
            message.Time = DateTime.Now;
            message.IPAdress = ((IPEndPoint)socketClientHandler.RemoteEndPoint).Address.ToString();
            switch (message.messageType)
            {
                case MessageType.Common:
                    HandleCommonMesssage(message);
                    break;
                case MessageType.Registration:
                    HandleRegistrationMessage(message);
                    break;
                case MessageType.Private:
                    HandlePrivateMessage(message);
                    break;
                case MessageType.History:
                    HandleHistoryRequest(message);
                    break;
            }
        }

        public void ReceiveMessages()
        {
            byte[] data = new byte[1024];
            int amount;
            do
            {
                MemoryStream messageContainer = new MemoryStream();
                try
                {
                    do
                    {
                        amount = socketClientHandler.Receive(data);
                        messageContainer.Write(data, 0, amount);
                    } while (socketClientHandler.Available > 0);
                    Message recievedMessage = messageSerializer.Deserialize(messageContainer.GetBuffer(),
                        messageContainer.GetBuffer().Length);
                    HandleMessage(recievedMessage);
                }
                catch
                {
                    if (!IsClientConnected())
                    {
                        IsConnected = false;
                    }
                }
            } while (IsConnected);
        }

        public void HandleClient()
        {
            ReceiveMessages();
            OnDisconnectClient();
        }
    }
}
