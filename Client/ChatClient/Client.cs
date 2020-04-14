﻿using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using LANnode;
using System.Threading;

namespace ChatClient
{
    public class Client
    {
        const int ServerPort = 8005;

        Socket socketServerHandler;
        Socket socketUdpHandler;
        Serializer messageSerializer;
        public bool IsConnected = false;
        List<Thread> threads;

        public delegate void MessageHandler(Message message);
        public event MessageHandler ReceiveMessageHandler;
        public Client()
        {
            messageSerializer = new Serializer();
            SetUdpEndPoint();
            socketServerHandler = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            threads = new List<Thread>();
        }

        public void CloseAllThreads()
        {
            foreach (Thread elem in threads)
            {
                elem.Abort();
            }
        }
        public bool Connect(IPEndPoint IPEnd)
        {
            try
            {
                socketServerHandler = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socketServerHandler.Connect(IPEnd);
            }
            catch
            {
                socketServerHandler.Close();
                return false;
            }
            return IsConnected = true;
        }

        public void Disconnect()
        {
            socketServerHandler.Close();
            IsConnected = false;
        }

        public void OnMessageReceive(Message message)
        {
            ReceiveMessageHandler(message);
        }

        public bool ConnectToServer(IPEndPoint endPoint, string name)
        {
            if (Connect(endPoint))
            {
                SendMessage(new Message(name, MessageType.Registration));

                Thread threadServerConnection = new Thread(ReceiveMessages);
                threads.Add(threadServerConnection);
                threadServerConnection.Start();
                return true;
            }
            else
                return false;
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
                        amount = socketServerHandler.Receive(data);
                        messageContainer.Write(data, 0, amount);
                    } while (socketServerHandler.Available > 0);
                    Message recievedMessage = messageSerializer.Deserialize(messageContainer.GetBuffer(),
                        messageContainer.GetBuffer().Length);
                    OnMessageReceive(recievedMessage);
                }
                catch
                {
                    //
                }
            } while (IsConnected);
        }

        public void SetUdpEndPoint()
        {
            socketUdpHandler = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 0);
            socketUdpHandler.Bind(localEndPoint);
        }

        public void ReceiveMessagesUdp()
        {

            byte[] data = new byte[1024];
            EndPoint endPoint = socketUdpHandler.LocalEndPoint;
            while (true)
            {
                int amount = socketUdpHandler.ReceiveFrom(data, ref endPoint);
                Message message = messageSerializer.Deserialize(data, amount);
                if (message.messageType == MessageType.SearchResponse)
                {
                    OnMessageReceive(message);
                    threads.Remove(Thread.CurrentThread);
                    return;
                }
            }
        }

        public void UdpBroadcastRequest()
        {
            Message message = new Message(MessageType.SearchRequest);
            message.Port = ((IPEndPoint)socketUdpHandler.LocalEndPoint).Port;
            message.IPAdress = NodeInformation.GetCurrentIP().ToString();
            Socket sendRequest = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sendRequest.EnableBroadcast = true;
            sendRequest.SendTo(messageSerializer.Serialize(message), new IPEndPoint(IPAddress.Broadcast, ServerPort));
            Thread threadReceiveUdp = new Thread(ReceiveMessagesUdp);
            threads.Add(threadReceiveUdp);
            threadReceiveUdp.Start();
        }

        public bool SendMessage(Message message)
        {
            byte[] buffer = messageSerializer.Serialize(message);
            try
            {
                socketServerHandler.Send(buffer);
            }
            catch
            {
                Disconnect();
            }
            return true;
        }

        public override int GetHashCode()
        {
            return socketServerHandler.LocalEndPoint.GetHashCode();
        }
    }
}
