using System;
using System.Collections.Generic;

namespace LANnode
{
    public enum MessageType { Common, Registration, ClientsList, Private, SearchRequest, SearchResponse, History };

    public struct Endpoints
    {
        public int Key;   
        public string Name { get; set; }
    }
    public class Message
    {
        public string Content;
        public string Name;
        public MessageType messageType;
        public List<Endpoints> clientsNames;
        public int ReceiverID;
        public int SenderID;
        public string IPAdress;
        public int Port;
        public int Hash;
        public List<string> messageHistory;  
        public DateTime Time;

        public Message(int ReceiverID, string Content)
        {
            this.ReceiverID = ReceiverID;
            this.Content = Content;
            messageType = MessageType.Private;
        }
        public Message(List<string> MessageHistory, int ReceiverID)
        {
            this.ReceiverID = ReceiverID;
            messageHistory = MessageHistory;
            messageType = MessageType.History;
        }
        public Message(MessageType MessType)
        {
            messageType = MessType;
        }
        public Message(List<Endpoints> ClientsNames)
        {
            clientsNames = ClientsNames;
            messageType = MessageType.ClientsList;
        }
        public Message(string IPAdress, int Port, MessageType MessType)
        {
            messageType = MessType;
            this.Port = Port;
            this.IPAdress = IPAdress;
        }
        public Message(string Data, MessageType MessType)
        {
            messageType = MessType;
            switch (messageType)
            {
                case MessageType.Common:
                    Content = Data;
                    Name = "";
                    break;
                case MessageType.Registration:     
                    Content = "";
                    Name = Data;
                    break;
            }
        }
        public Message(string Content)
        {
            messageType = MessageType.Common;
            this.Content = Content;
            Name = "Unknown";
        }
        public Message()
        {
            Content = "No content";
            Name = "Unknown";
        }
    }
}
