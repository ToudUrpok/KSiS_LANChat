namespace ChatClient
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btGetHistory = new System.Windows.Forms.Button();
            this.lbCurrentDialog = new System.Windows.Forms.Label();
            this.tbChatContent = new System.Windows.Forms.RichTextBox();
            this.lbParticipants = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.btDisconnect = new System.Windows.Forms.Button();
            this.cbIsConnected = new System.Windows.Forms.CheckBox();
            this.btSendMessage = new System.Windows.Forms.Button();
            this.btConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbIPAdress = new System.Windows.Forms.TextBox();
            this.tbMessageContent = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btGetHistory
            // 
            this.btGetHistory.Location = new System.Drawing.Point(597, 335);
            this.btGetHistory.Name = "btGetHistory";
            this.btGetHistory.Size = new System.Drawing.Size(139, 49);
            this.btGetHistory.TabIndex = 34;
            this.btGetHistory.Text = "Load dialog history";
            this.btGetHistory.UseVisualStyleBackColor = true;
            this.btGetHistory.Click += new System.EventHandler(this.btGetHistory_Click);
            // 
            // lbCurrentDialog
            // 
            this.lbCurrentDialog.AutoSize = true;
            this.lbCurrentDialog.Location = new System.Drawing.Point(224, 130);
            this.lbCurrentDialog.Name = "lbCurrentDialog";
            this.lbCurrentDialog.Size = new System.Drawing.Size(100, 17);
            this.lbCurrentDialog.TabIndex = 33;
            this.lbCurrentDialog.Text = "Not connected";
            // 
            // tbChatContent
            // 
            this.tbChatContent.Location = new System.Drawing.Point(26, 159);
            this.tbChatContent.Name = "tbChatContent";
            this.tbChatContent.ReadOnly = true;
            this.tbChatContent.Size = new System.Drawing.Size(491, 174);
            this.tbChatContent.TabIndex = 31;
            this.tbChatContent.Text = "";
            // 
            // lbParticipants
            // 
            this.lbParticipants.FormattingEnabled = true;
            this.lbParticipants.ItemHeight = 16;
            this.lbParticipants.Location = new System.Drawing.Point(597, 159);
            this.lbParticipants.Name = "lbParticipants";
            this.lbParticipants.Size = new System.Drawing.Size(139, 164);
            this.lbParticipants.TabIndex = 30;
            this.lbParticipants.SelectedIndexChanged += new System.EventHandler(this.lbParticipants_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(630, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 17);
            this.label3.TabIndex = 29;
            this.label3.Text = "Participants";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(98, 28);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(122, 22);
            this.tbName.TabIndex = 28;
            // 
            // btDisconnect
            // 
            this.btDisconnect.Location = new System.Drawing.Point(346, 28);
            this.btDisconnect.Name = "btDisconnect";
            this.btDisconnect.Size = new System.Drawing.Size(88, 23);
            this.btDisconnect.TabIndex = 27;
            this.btDisconnect.Text = "Disconnect";
            this.btDisconnect.UseVisualStyleBackColor = true;
            this.btDisconnect.Click += new System.EventHandler(this.btDisconnect_Click);
            // 
            // cbIsConnected
            // 
            this.cbIsConnected.AutoSize = true;
            this.cbIsConnected.Location = new System.Drawing.Point(42, 72);
            this.cbIsConnected.Name = "cbIsConnected";
            this.cbIsConnected.Size = new System.Drawing.Size(110, 21);
            this.cbIsConnected.TabIndex = 26;
            this.cbIsConnected.Text = "Is connected";
            this.cbIsConnected.UseVisualStyleBackColor = true;
            // 
            // btSendMessage
            // 
            this.btSendMessage.Location = new System.Drawing.Point(458, 329);
            this.btSendMessage.Name = "btSendMessage";
            this.btSendMessage.Size = new System.Drawing.Size(59, 55);
            this.btSendMessage.TabIndex = 25;
            this.btSendMessage.Text = "Send";
            this.btSendMessage.UseVisualStyleBackColor = true;
            this.btSendMessage.Click += new System.EventHandler(this.btSendMessage_Click);
            // 
            // btConnect
            // 
            this.btConnect.Location = new System.Drawing.Point(243, 28);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(88, 23);
            this.btConnect.TabIndex = 24;
            this.btConnect.Text = "Connect";
            this.btConnect.UseVisualStyleBackColor = true;
            this.btConnect.Click += new System.EventHandler(this.btConnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(509, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 17);
            this.label1.TabIndex = 22;
            this.label1.Text = "Server IP";
            // 
            // tbIPAdress
            // 
            this.tbIPAdress.Location = new System.Drawing.Point(581, 29);
            this.tbIPAdress.Name = "tbIPAdress";
            this.tbIPAdress.ReadOnly = true;
            this.tbIPAdress.Size = new System.Drawing.Size(122, 22);
            this.tbIPAdress.TabIndex = 20;
            // 
            // tbMessageContent
            // 
            this.tbMessageContent.Location = new System.Drawing.Point(26, 329);
            this.tbMessageContent.Name = "tbMessageContent";
            this.tbMessageContent.Size = new System.Drawing.Size(435, 55);
            this.tbMessageContent.TabIndex = 19;
            this.tbMessageContent.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 17);
            this.label4.TabIndex = 35;
            this.label4.Text = "name";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 424);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btGetHistory);
            this.Controls.Add(this.lbCurrentDialog);
            this.Controls.Add(this.tbChatContent);
            this.Controls.Add(this.lbParticipants);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.btDisconnect);
            this.Controls.Add(this.cbIsConnected);
            this.Controls.Add(this.btSendMessage);
            this.Controls.Add(this.btConnect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbIPAdress);
            this.Controls.Add(this.tbMessageContent);
            this.Name = "Form1";
            this.Text = "Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btGetHistory;
        private System.Windows.Forms.Label lbCurrentDialog;
        private System.Windows.Forms.RichTextBox tbChatContent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Button btDisconnect;
        private System.Windows.Forms.CheckBox cbIsConnected;
        private System.Windows.Forms.Button btSendMessage;
        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbIPAdress;
        private System.Windows.Forms.RichTextBox tbMessageContent;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lbParticipants;
    }
}

