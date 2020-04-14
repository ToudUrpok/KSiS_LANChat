using System.Xml.Serialization;
using System.IO;

namespace LANnode
{
    public class Serializer
    {
        public byte[] Serialize(Message message)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Message));
            MemoryStream messageStorage = new MemoryStream();
            serializer.Serialize(messageStorage, message);
            return messageStorage.GetBuffer();
        }

        public Message Deserialize(byte[] data, int amount)
        {
            MemoryStream messageStorage = new MemoryStream();
            messageStorage.Write(data, 0, amount);  // 0 - смещение
            XmlSerializer serializer = new XmlSerializer(typeof(Message));
            messageStorage.Position = 0;
            Message message = (Message)serializer.Deserialize(messageStorage);
            return message;
        }
    }
}
