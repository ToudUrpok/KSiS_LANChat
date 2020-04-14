using System.Collections.Generic;

namespace ChatClient
{
    class DialogInformation
    {
        public string Name;
        public List<string> Messages;
        public int UnreadMessagesCounter;

        public DialogInformation(string name)
        {
            Name = name;
            Messages = new List<string>();
            UnreadMessagesCounter = 0;
        }
        public void AddMessage(string messageContent)
        {
            Messages.Add(messageContent);
            UnreadMessagesCounter++;
        }
    }
}
