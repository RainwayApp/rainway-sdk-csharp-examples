namespace host_example_gui
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.linkLabelManageApiKeys = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxApiKey = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.textBoxLogs = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxPeerId = new System.Windows.Forms.TextBox();
            this.linkLabelWebDemo = new System.Windows.Forms.LinkLabel();
            this.groupBoxSettings = new System.Windows.Forms.GroupBox();
            this.checkBoxKeyboard = new System.Windows.Forms.CheckBox();
            this.checkBoxMouse = new System.Windows.Forms.CheckBox();
            this.checkBoxAcceptIncoming = new System.Windows.Forms.CheckBox();
            this.groupBoxSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // linkLabelManageApiKeys
            // 
            this.linkLabelManageApiKeys.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelManageApiKeys.AutoSize = true;
            this.linkLabelManageApiKeys.Location = new System.Drawing.Point(292, 17);
            this.linkLabelManageApiKeys.Name = "linkLabelManageApiKeys";
            this.linkLabelManageApiKeys.Size = new System.Drawing.Size(97, 15);
            this.linkLabelManageApiKeys.TabIndex = 2;
            this.linkLabelManageApiKeys.TabStop = true;
            this.linkLabelManageApiKeys.Text = "Manage API keys";
            this.linkLabelManageApiKeys.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "API key";
            // 
            // textBoxApiKey
            // 
            this.textBoxApiKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxApiKey.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxApiKey.Location = new System.Drawing.Point(64, 14);
            this.textBoxApiKey.Name = "textBoxApiKey";
            this.textBoxApiKey.PlaceholderText = "pk_live_xxxxxxxxxx";
            this.textBoxApiKey.Size = new System.Drawing.Size(222, 23);
            this.textBoxApiKey.TabIndex = 0;
            this.textBoxApiKey.Text = "pk_live_AOc6GRfwJfxzNCT3lIR7zePo";
            this.textBoxApiKey.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // buttonConnect
            // 
            this.buttonConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonConnect.Location = new System.Drawing.Point(395, 12);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(113, 24);
            this.buttonConnect.TabIndex = 1;
            this.buttonConnect.Text = "&Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDisconnect.Enabled = false;
            this.buttonDisconnect.Location = new System.Drawing.Point(395, 42);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(113, 24);
            this.buttonDisconnect.TabIndex = 1;
            this.buttonDisconnect.Text = "&Disconnect";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // textBoxLogs
            // 
            this.textBoxLogs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLogs.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxLogs.HideSelection = false;
            this.textBoxLogs.Location = new System.Drawing.Point(12, 239);
            this.textBoxLogs.Multiline = true;
            this.textBoxLogs.Name = "textBoxLogs";
            this.textBoxLogs.ReadOnly = true;
            this.textBoxLogs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLogs.Size = new System.Drawing.Size(496, 184);
            this.textBoxLogs.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Peer ID";
            // 
            // textBoxPeerId
            // 
            this.textBoxPeerId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPeerId.Location = new System.Drawing.Point(64, 43);
            this.textBoxPeerId.Name = "textBoxPeerId";
            this.textBoxPeerId.PlaceholderText = "Disconnected";
            this.textBoxPeerId.ReadOnly = true;
            this.textBoxPeerId.Size = new System.Drawing.Size(222, 23);
            this.textBoxPeerId.TabIndex = 4;
            // 
            // linkLabelWebDemo
            // 
            this.linkLabelWebDemo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelWebDemo.LinkArea = new System.Windows.Forms.LinkArea(39, 8);
            this.linkLabelWebDemo.Location = new System.Drawing.Point(12, 69);
            this.linkLabelWebDemo.Name = "linkLabelWebDemo";
            this.linkLabelWebDemo.Size = new System.Drawing.Size(496, 41);
            this.linkLabelWebDemo.TabIndex = 5;
            this.linkLabelWebDemo.TabStop = true;
            this.linkLabelWebDemo.Text = "Now try connecting to this peer in the Web Demo from another device.";
            this.linkLabelWebDemo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabelWebDemo.UseCompatibleTextRendering = true;
            this.linkLabelWebDemo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelWebDemo_LinkClicked);
            // 
            // groupBoxSettings
            // 
            this.groupBoxSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSettings.Controls.Add(this.checkBoxKeyboard);
            this.groupBoxSettings.Controls.Add(this.checkBoxMouse);
            this.groupBoxSettings.Controls.Add(this.checkBoxAcceptIncoming);
            this.groupBoxSettings.Enabled = false;
            this.groupBoxSettings.Location = new System.Drawing.Point(12, 138);
            this.groupBoxSettings.Name = "groupBoxSettings";
            this.groupBoxSettings.Size = new System.Drawing.Size(496, 95);
            this.groupBoxSettings.TabIndex = 6;
            this.groupBoxSettings.TabStop = false;
            this.groupBoxSettings.Text = "Settings";
            // 
            // checkBoxKeyboard
            // 
            this.checkBoxKeyboard.AutoSize = true;
            this.checkBoxKeyboard.Checked = true;
            this.checkBoxKeyboard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxKeyboard.Location = new System.Drawing.Point(19, 70);
            this.checkBoxKeyboard.Name = "checkBoxKeyboard";
            this.checkBoxKeyboard.Size = new System.Drawing.Size(190, 19);
            this.checkBoxKeyboard.TabIndex = 0;
            this.checkBoxKeyboard.Text = "Allow remote keyboard control";
            this.checkBoxKeyboard.UseVisualStyleBackColor = true;
            this.checkBoxKeyboard.CheckedChanged += new System.EventHandler(this.checkBoxKeyboard_CheckedChanged);
            // 
            // checkBoxMouse
            // 
            this.checkBoxMouse.AutoSize = true;
            this.checkBoxMouse.Checked = true;
            this.checkBoxMouse.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMouse.Location = new System.Drawing.Point(19, 45);
            this.checkBoxMouse.Name = "checkBoxMouse";
            this.checkBoxMouse.Size = new System.Drawing.Size(177, 19);
            this.checkBoxMouse.TabIndex = 0;
            this.checkBoxMouse.Text = "Allow remote mouse control";
            this.checkBoxMouse.UseVisualStyleBackColor = true;
            this.checkBoxMouse.CheckedChanged += new System.EventHandler(this.checkBoxMouse_CheckedChanged);
            // 
            // checkBoxAcceptIncoming
            // 
            this.checkBoxAcceptIncoming.AutoSize = true;
            this.checkBoxAcceptIncoming.Checked = true;
            this.checkBoxAcceptIncoming.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAcceptIncoming.Location = new System.Drawing.Point(19, 20);
            this.checkBoxAcceptIncoming.Name = "checkBoxAcceptIncoming";
            this.checkBoxAcceptIncoming.Size = new System.Drawing.Size(227, 19);
            this.checkBoxAcceptIncoming.TabIndex = 0;
            this.checkBoxAcceptIncoming.Text = "Accept incoming connection requests";
            this.checkBoxAcceptIncoming.UseVisualStyleBackColor = true;
            this.checkBoxAcceptIncoming.CheckedChanged += new System.EventHandler(this.checkBoxAcceptIncoming_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 435);
            this.Controls.Add(this.groupBoxSettings);
            this.Controls.Add(this.linkLabelWebDemo);
            this.Controls.Add(this.textBoxPeerId);
            this.Controls.Add(this.linkLabelManageApiKeys);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxLogs);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.textBoxApiKey);
            this.Controls.Add(this.buttonConnect);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Rainway C# Host Example";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxSettings.ResumeLayout(false);
            this.groupBoxSettings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private LinkLabel linkLabelManageApiKeys;
        private Label label1;
        private TextBox textBoxApiKey;
        private Button buttonConnect;
        private Button buttonDisconnect;
        private TextBox textBoxLogs;
        private Label label3;
        private TextBox textBoxPeerId;
        private LinkLabel linkLabelWebDemo;
        private GroupBox groupBoxSettings;
        private CheckBox checkBoxAcceptIncoming;
        private CheckBox checkBoxKeyboard;
        private CheckBox checkBoxMouse;
    }
}