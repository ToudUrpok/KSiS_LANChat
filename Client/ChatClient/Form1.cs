using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;
using LANnode;

namespace ChatClient
{
    public partial class Form1 : Form
    {
        const int CommonChatDialogId = 0;

        Client client;
        Dictionary<int, DialogInformation> chatDialogsInfo;
        List<Endpoints> clientsList;
        int CurrentDialogId = CommonChatDialogId;
        int selectedIndex = 0;
        public Form1()
        {
            InitializeComponent();
            clientsList = new List<Endpoints>();
            clientsList.Add(new Endpoints() { Key = CommonChatDialogId, Name = "Common chat" });
            chatDialogsInfo = new Dictionary<int, DialogInformation>();
            chatDialogsInfo.Add(CommonChatDialogId, new DialogInformation("Common chat"));
            client = new Client();
            client.ReceiveMessageHandler += ShowReceivedMessages;
        }

        public void ShowReceivedMessages(LANnode.Message message)
        {
            switch (message.messageType)
            {
                case MessageType.Common:
                    chatDialogsInfo[CommonChatDialogId].AddMessage(message.Name + ": "
                        + message.Content + "  " + message.Time + "  " + message.IPAdress);
                    break;
                case MessageType.Private:
                    chatDialogsInfo[message.SenderID].AddMessage(message.Content + "  " +
                        message.Time + "  " + message.IPAdress);
                    break;
                case MessageType.ClientsList:
                    {
                        Action action = delegate
                        {
                            clientsList.Clear();
                            clientsList.Add(new Endpoints() { Key = CommonChatDialogId, Name = "Common chat" });
                            foreach (Endpoints nameClient in message.clientsNames)
                            {
                                clientsList.Add(nameClient);
                                if (!chatDialogsInfo.ContainsKey(nameClient.Key))
                                {
                                    chatDialogsInfo.Add(nameClient.Key, new DialogInformation(nameClient.Name));
                                }
                            }
                        };
                        if (InvokeRequired)
                            Invoke(action);
                        else
                            action();
                    }
                    break;
                case MessageType.SearchResponse:
                    {
                        Action action = delegate
                        {
                            tbIPAdress.Text = message.IPAdress;
                            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(message.IPAdress), message.Port);
                            cbIsConnected.Checked = client.ConnectToServer(endPoint, tbName.Text);
                        };
                        if (InvokeRequired)
                            Invoke(action);
                        else
                            action();
                    }
                    break;
                case MessageType.History:
                    chatDialogsInfo[message.ReceiverID].Messages = message.messageHistory;
                    break;
            }
            UpdateView();
        }

        public void UpdateView()
        {
            Action action = delegate
            {
                if (CurrentDialogId == CommonChatDialogId)
                {
                    lbCurrentDialog.Text = "Common chat";
                }
                else
                {
                    lbCurrentDialog.Text = chatDialogsInfo[CurrentDialogId].Name;
                }
                tbChatContent.Clear();
                if (chatDialogsInfo != null)
                {
                    string[] messages = new string[chatDialogsInfo[CurrentDialogId].Messages.Count];
                    chatDialogsInfo[CurrentDialogId].Messages.CopyTo(messages);
                    foreach (string messageContent in messages)
                    {
                        tbChatContent.Text += messageContent + "\r\n";
                    }
                }
                lbParticipants.Items.Clear();
                foreach (Endpoints clientName in clientsList)
                {
                    lbParticipants.Items.Add(clientName.Name + " " + ((chatDialogsInfo[clientName.Key].UnreadMessagesCounter != 0) ?
                        chatDialogsInfo[clientName.Key].UnreadMessagesCounter.ToString() : ""));
                }
            };
            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }

        private void btSendMessage_Click(object sender, EventArgs e)
        {
            if (selectedIndex != -1)
            {
                LANnode.Message message;
                if (CurrentDialogId == CommonChatDialogId)
                {
                    message = new LANnode.Message(tbMessageContent.Text);
                }
                else
                {
                    message = new LANnode.Message(clientsList[selectedIndex].Key, tbMessageContent.Text);
                    chatDialogsInfo[message.ReceiverID].Messages.Add("Me: " + message.Content + "  " + DateTime.Now);
                }
                if (cbIsConnected.Checked)
                {
                    client.SendMessage(message);
                    UpdateView();
                }
            }
        }

        private void btConnect_Click(object sender, EventArgs e)
        {
            client.UdpBroadcastRequest();
            
        }

        private void btDisconnect_Click(object sender, EventArgs e)
        {
            client.Disconnect();
            cbIsConnected.Checked = client.IsConnected;
        }

        private void lbParticipants_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbParticipants.SelectedIndex != -1)
            {
                selectedIndex = lbParticipants.SelectedIndex;
                CurrentDialogId = clientsList[selectedIndex].Key;
                chatDialogsInfo[CurrentDialogId].UnreadMessagesCounter = 0;
                UpdateView();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.CloseAllThreads();
        }

        private void btGetHistory_Click(object sender, EventArgs e)
        {
            lbParticipants.SelectedIndex = 0;
            LANnode.Message message = new LANnode.Message(new List<string>(), clientsList[selectedIndex].Key);
            client.SendMessage(message);
            UpdateView();
        }

    }
}
