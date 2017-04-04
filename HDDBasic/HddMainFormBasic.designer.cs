namespace HDD
{
    partial class HddMainFormBasic
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HddMainFormBasic));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.myConnection = new System.Windows.Forms.ComboBox();
            this.myPassword = new System.Windows.Forms.TextBox();
            this.myUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.myLogoutButton = new System.Windows.Forms.Button();
            this.myCurrency = new System.Windows.Forms.ComboBox();
            this.myDataParameterGroupBox = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.myEndTime = new System.Windows.Forms.DateTimePicker();
            this.myStartTime = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.myStartDownloadButton = new System.Windows.Forms.Button();
            this.myOutputDirectory = new System.Windows.Forms.TextBox();
            this.myBrowseButton = new System.Windows.Forms.Button();
            this.myTimeInterval = new System.Windows.Forms.ComboBox();
            this.myTypeOfData = new System.Windows.Forms.ComboBox();
            this.myEndDate = new System.Windows.Forms.DateTimePicker();
            this.myStartDate = new System.Windows.Forms.DateTimePicker();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.myStatusType = new System.Windows.Forms.ToolStripStatusLabel();
            this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.myProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.myPercentComplete = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.mVersionLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1.SuspendLayout();
            this.myDataParameterGroupBox.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.myConnection);
            this.groupBox1.Controls.Add(this.myPassword);
            this.groupBox1.Controls.Add(this.myUsername);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(7, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(420, 104);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Login/Logout";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(251, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Connection:";
            // 
            // myConnection
            // 
            this.myConnection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.myConnection.FormattingEnabled = true;
            this.myConnection.Items.AddRange(new object[] {
            "Demo",
            "Real"});
            this.myConnection.Location = new System.Drawing.Point(321, 28);
            this.myConnection.Name = "myConnection";
            this.myConnection.Size = new System.Drawing.Size(72, 21);
            this.myConnection.TabIndex = 2;
            // 
            // myPassword
            // 
            this.myPassword.Location = new System.Drawing.Point(88, 61);
            this.myPassword.Name = "myPassword";
            this.myPassword.PasswordChar = '*';
            this.myPassword.Size = new System.Drawing.Size(132, 20);
            this.myPassword.TabIndex = 1;
            // 
            // myUsername
            // 
            this.myUsername.Location = new System.Drawing.Point(88, 29);
            this.myUsername.Name = "myUsername";
            this.myUsername.Size = new System.Drawing.Size(132, 20);
            this.myUsername.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username:";
            // 
            // myLogoutButton
            // 
            this.myLogoutButton.Enabled = false;
            this.myLogoutButton.Location = new System.Drawing.Point(519, 12);
            this.myLogoutButton.Name = "myLogoutButton";
            this.myLogoutButton.Size = new System.Drawing.Size(75, 23);
            this.myLogoutButton.TabIndex = 5;
            this.myLogoutButton.Text = "Logout";
            this.myLogoutButton.Click += new System.EventHandler(this.myLogoutButton_Click);
            // 
            // myCurrency
            // 
            this.myCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.myCurrency.Enabled = false;
            this.myCurrency.FormattingEnabled = true;
            this.myCurrency.Location = new System.Drawing.Point(88, 112);
            this.myCurrency.Name = "myCurrency";
            this.myCurrency.Size = new System.Drawing.Size(99, 21);
            this.myCurrency.TabIndex = 4;
            // 
            // myDataParameterGroupBox
            // 
            this.myDataParameterGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.myDataParameterGroupBox.Controls.Add(this.label12);
            this.myDataParameterGroupBox.Controls.Add(this.label11);
            this.myDataParameterGroupBox.Controls.Add(this.myEndTime);
            this.myDataParameterGroupBox.Controls.Add(this.myStartTime);
            this.myDataParameterGroupBox.Controls.Add(this.label10);
            this.myDataParameterGroupBox.Controls.Add(this.label8);
            this.myDataParameterGroupBox.Controls.Add(this.label7);
            this.myDataParameterGroupBox.Controls.Add(this.label6);
            this.myDataParameterGroupBox.Controls.Add(this.label5);
            this.myDataParameterGroupBox.Controls.Add(this.label4);
            this.myDataParameterGroupBox.Controls.Add(this.myStartDownloadButton);
            this.myDataParameterGroupBox.Controls.Add(this.myOutputDirectory);
            this.myDataParameterGroupBox.Controls.Add(this.myBrowseButton);
            this.myDataParameterGroupBox.Controls.Add(this.myTimeInterval);
            this.myDataParameterGroupBox.Controls.Add(this.myTypeOfData);
            this.myDataParameterGroupBox.Controls.Add(this.myEndDate);
            this.myDataParameterGroupBox.Controls.Add(this.myStartDate);
            this.myDataParameterGroupBox.Controls.Add(this.myCurrency);
            this.myDataParameterGroupBox.Location = new System.Drawing.Point(7, 121);
            this.myDataParameterGroupBox.Name = "myDataParameterGroupBox";
            this.myDataParameterGroupBox.Size = new System.Drawing.Size(613, 241);
            this.myDataParameterGroupBox.TabIndex = 2;
            this.myDataParameterGroupBox.TabStop = false;
            this.myDataParameterGroupBox.Text = "Data Parameters";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(239, 78);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(55, 13);
            this.label12.TabIndex = 22;
            this.label12.Text = "End Time:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(235, 39);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Start Time:";
            // 
            // myEndTime
            // 
            this.myEndTime.Enabled = false;
            this.myEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.myEndTime.Location = new System.Drawing.Point(301, 74);
            this.myEndTime.Name = "myEndTime";
            this.myEndTime.ShowUpDown = true;
            this.myEndTime.Size = new System.Drawing.Size(99, 20);
            this.myEndTime.TabIndex = 3;
            // 
            // myStartTime
            // 
            this.myStartTime.Enabled = false;
            this.myStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.myStartTime.Location = new System.Drawing.Point(301, 35);
            this.myStartTime.Name = "myStartTime";
            this.myStartTime.ShowUpDown = true;
            this.myStartTime.Size = new System.Drawing.Size(99, 20);
            this.myStartTime.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(198, 115);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(87, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "Output Directory:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 199);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Time Interval:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 157);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Type of Data:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Instrument:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "End Date:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Start Date:";
            // 
            // myStartDownloadButton
            // 
            this.myStartDownloadButton.Enabled = false;
            this.myStartDownloadButton.Location = new System.Drawing.Point(461, 168);
            this.myStartDownloadButton.Name = "myStartDownloadButton";
            this.myStartDownloadButton.Size = new System.Drawing.Size(126, 53);
            this.myStartDownloadButton.TabIndex = 10;
            this.myStartDownloadButton.Text = "Start Download";
            this.myStartDownloadButton.Click += new System.EventHandler(this.myStartDownloadButton_Click);
            // 
            // myOutputDirectory
            // 
            this.myOutputDirectory.Enabled = false;
            this.myOutputDirectory.Location = new System.Drawing.Point(301, 111);
            this.myOutputDirectory.Name = "myOutputDirectory";
            this.myOutputDirectory.Size = new System.Drawing.Size(216, 20);
            this.myOutputDirectory.TabIndex = 8;
            // 
            // myBrowseButton
            // 
            this.myBrowseButton.Enabled = false;
            this.myBrowseButton.Location = new System.Drawing.Point(523, 110);
            this.myBrowseButton.Name = "myBrowseButton";
            this.myBrowseButton.Size = new System.Drawing.Size(64, 23);
            this.myBrowseButton.TabIndex = 9;
            this.myBrowseButton.Text = "Browse";
            this.myBrowseButton.Click += new System.EventHandler(this.myBrowseButton_Click);
            // 
            // myTimeInterval
            // 
            this.myTimeInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.myTimeInterval.Enabled = false;
            this.myTimeInterval.FormattingEnabled = true;
            this.myTimeInterval.IntegralHeight = false;
            this.myTimeInterval.Items.AddRange(new object[] {
            "1 Minute",
            "5 Minute",
            "15 Minute",
            "30 Minute",
            "1 Hour",
            "4 Hour",
            "1 Day",
            "1 Week",
            "1 Month"});
            this.myTimeInterval.Location = new System.Drawing.Point(88, 195);
            this.myTimeInterval.MaxDropDownItems = 9;
            this.myTimeInterval.Name = "myTimeInterval";
            this.myTimeInterval.Size = new System.Drawing.Size(99, 21);
            this.myTimeInterval.TabIndex = 6;
            // 
            // myTypeOfData
            // 
            this.myTypeOfData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.myTypeOfData.Enabled = false;
            this.myTypeOfData.FormattingEnabled = true;
            this.myTypeOfData.IntegralHeight = false;
            this.myTypeOfData.Items.AddRange(new object[] {
            "Bid",
            "Ask",
            "BidAndAsk"});
            this.myTypeOfData.Location = new System.Drawing.Point(88, 153);
            this.myTypeOfData.Name = "myTypeOfData";
            this.myTypeOfData.Size = new System.Drawing.Size(99, 21);
            this.myTypeOfData.TabIndex = 5;
            // 
            // myEndDate
            // 
            this.myEndDate.Enabled = false;
            this.myEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.myEndDate.Location = new System.Drawing.Point(88, 74);
            this.myEndDate.Name = "myEndDate";
            this.myEndDate.Size = new System.Drawing.Size(99, 20);
            this.myEndDate.TabIndex = 2;
            // 
            // myStartDate
            // 
            this.myStartDate.Enabled = false;
            this.myStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.myStartDate.Location = new System.Drawing.Point(88, 35);
            this.myStartDate.Name = "myStartDate";
            this.myStartDate.Size = new System.Drawing.Size(99, 20);
            this.myStartDate.TabIndex = 0;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusType,
            this.myStatusLabel,
            this.myProgressBar,
            this.myPercentComplete,
            this.toolStripStatusLabel5,
            this.mVersionLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 448);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(632, 22);
            this.statusStrip1.TabIndex = 49;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // myStatusType
            // 
            this.myStatusType.Name = "myStatusType";
            this.myStatusType.Size = new System.Drawing.Size(42, 17);
            this.myStatusType.Text = "Status:";
            // 
            // myStatusLabel
            // 
            this.myStatusLabel.BackColor = System.Drawing.Color.White;
            this.myStatusLabel.ForeColor = System.Drawing.Color.Firebrick;
            this.myStatusLabel.Name = "myStatusLabel";
            this.myStatusLabel.Size = new System.Drawing.Size(79, 17);
            this.myStatusLabel.Text = "Disconnected";
            this.myStatusLabel.VisitedLinkColor = System.Drawing.Color.Red;
            // 
            // myProgressBar
            // 
            this.myProgressBar.Name = "myProgressBar";
            this.myProgressBar.Size = new System.Drawing.Size(200, 16);
            this.myProgressBar.Visible = false;
            // 
            // myPercentComplete
            // 
            this.myPercentComplete.Name = "myPercentComplete";
            this.myPercentComplete.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(450, 17);
            this.toolStripStatusLabel5.Spring = true;
            // 
            // mVersionLabel
            // 
            this.mVersionLabel.Name = "mVersionLabel";
            this.mVersionLabel.Size = new System.Drawing.Size(46, 17);
            this.mVersionLabel.Text = "Version";
            // 
            // HddMainFormBasic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(632, 470);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.myLogoutButton);
            this.Controls.Add(this.myDataParameterGroupBox);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(640, 500);
            this.Name = "HddMainFormBasic";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FXCM Historical Data Downloader Basic";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.HddMainFormBasic_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.myDataParameterGroupBox.ResumeLayout(false);
            this.myDataParameterGroupBox.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox myConnection;
        private System.Windows.Forms.TextBox myPassword;
        private System.Windows.Forms.TextBox myUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button myLogoutButton;
        private System.Windows.Forms.ComboBox myCurrency;
        private System.Windows.Forms.GroupBox myDataParameterGroupBox;
        private System.Windows.Forms.DateTimePicker myEndDate;
        private System.Windows.Forms.DateTimePicker myStartDate;
        private System.Windows.Forms.TextBox myOutputDirectory;
        private System.Windows.Forms.Button myBrowseButton;
        private System.Windows.Forms.ComboBox myTimeInterval;
        private System.Windows.Forms.ComboBox myTypeOfData;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button myStartDownloadButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker myEndTime;
        private System.Windows.Forms.DateTimePicker myStartTime;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel myStatusType;
        private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel mVersionLabel;
        private System.Windows.Forms.ToolStripProgressBar myProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel myPercentComplete;
    }
}

