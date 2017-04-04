namespace HDD
{
    partial class LoginForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.loginLogoutButton = new System.Windows.Forms.Button();
            this.accountTypeDropdownList = new System.Windows.Forms.ComboBox();
            this.register = new System.Windows.Forms.LinkLabel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatusLabel1 = new System.Windows.Forms.Label();
            this.myStatusLabel = new System.Windows.Forms.Label();
            this.myStatusType = new System.Windows.Forms.Label();
            this.myPercentComplete = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.usernameDropdownList = new System.Windows.Forms.ComboBox();
            this.passwordTextbox = new System.Windows.Forms.TextBox();
            this.saveLoginButton = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.mVersionLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // loginLogoutButton
            // 
            this.loginLogoutButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.loginLogoutButton.Location = new System.Drawing.Point(151, 236);
            this.loginLogoutButton.Name = "loginLogoutButton";
            this.loginLogoutButton.Size = new System.Drawing.Size(75, 23);
            this.loginLogoutButton.TabIndex = 43;
            this.loginLogoutButton.Text = "Login";
            this.loginLogoutButton.UseVisualStyleBackColor = false;
            this.loginLogoutButton.Click += new System.EventHandler(this.loginLogoutButton_Click);
            // 
            // accountTypeDropdownList
            // 
            this.accountTypeDropdownList.FormattingEnabled = true;
            this.accountTypeDropdownList.Location = new System.Drawing.Point(151, 186);
            this.accountTypeDropdownList.Name = "accountTypeDropdownList";
            this.accountTypeDropdownList.Size = new System.Drawing.Size(177, 21);
            this.accountTypeDropdownList.TabIndex = 4;
            this.accountTypeDropdownList.Text = "Real";
            // 
            // register
            // 
            this.register.AutoSize = true;
            this.register.Location = new System.Drawing.Point(282, 217);
            this.register.Name = "register";
            this.register.Size = new System.Drawing.Size(46, 13);
            this.register.TabIndex = 6;
            this.register.TabStop = true;
            this.register.Text = "Register";
            this.register.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(104, 48);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click_2);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AccessibleDescription = "Status: ";
            this.toolStripStatusLabel1.AccessibleName = "Status: ";
            this.toolStripStatusLabel1.AutoSize = true;
            this.toolStripStatusLabel1.Location = new System.Drawing.Point(0, 0);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(100, 23);
            this.toolStripStatusLabel1.TabIndex = 0;
            this.toolStripStatusLabel1.Text = "Status: ";
            // 
            // myStatusLabel
            // 
            this.myStatusLabel.AccessibleDescription = "Disconnected";
            this.myStatusLabel.AccessibleName = "Disconnected";
            this.myStatusLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myStatusLabel.ForeColor = System.Drawing.Color.Red;
            this.myStatusLabel.Location = new System.Drawing.Point(-1, 1);
            this.myStatusLabel.Name = "myStatusLabel";
            this.myStatusLabel.Size = new System.Drawing.Size(83, 18);
            this.myStatusLabel.TabIndex = 0;
            this.myStatusLabel.Text = "Disconnected";
            // 
            // myStatusType
            // 
            this.myStatusType.Location = new System.Drawing.Point(0, 0);
            this.myStatusType.Name = "myStatusType";
            this.myStatusType.Size = new System.Drawing.Size(100, 23);
            this.myStatusType.TabIndex = 0;
            // 
            // myPercentComplete
            // 
            this.myPercentComplete.Location = new System.Drawing.Point(0, 0);
            this.myPercentComplete.Name = "myPercentComplete";
            this.myPercentComplete.Size = new System.Drawing.Size(100, 23);
            this.myPercentComplete.TabIndex = 0;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox4.Image = global::HDD.Properties.Resources.connection;
            this.pictureBox4.Location = new System.Drawing.Point(131, 186);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(21, 21);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox4.TabIndex = 37;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox3.Image = global::HDD.Properties.Resources.password1;
            this.pictureBox3.Location = new System.Drawing.Point(131, 160);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(21, 20);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 32;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.White;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Image = global::HDD.Properties.Resources.user1;
            this.pictureBox2.Location = new System.Drawing.Point(131, 134);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(21, 20);
            this.pictureBox2.TabIndex = 31;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(131, 53);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(197, 55);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // usernameDropdownList
            // 
            this.usernameDropdownList.FormattingEnabled = true;
            this.usernameDropdownList.Location = new System.Drawing.Point(151, 134);
            this.usernameDropdownList.Name = "usernameDropdownList";
            this.usernameDropdownList.Size = new System.Drawing.Size(177, 21);
            this.usernameDropdownList.TabIndex = 45;
            this.usernameDropdownList.SelectedIndexChanged += new System.EventHandler(this.usernameDropdownList_SelectedIndexChanged);
            // 
            // passwordTextbox
            // 
            this.passwordTextbox.Location = new System.Drawing.Point(151, 160);
            this.passwordTextbox.Name = "passwordTextbox";
            this.passwordTextbox.PasswordChar = '*';
            this.passwordTextbox.Size = new System.Drawing.Size(177, 20);
            this.passwordTextbox.TabIndex = 46;
            // 
            // saveLoginButton
            // 
            this.saveLoginButton.AutoSize = true;
            this.saveLoginButton.Location = new System.Drawing.Point(151, 213);
            this.saveLoginButton.Name = "saveLoginButton";
            this.saveLoginButton.Size = new System.Drawing.Size(80, 17);
            this.saveLoginButton.TabIndex = 47;
            this.saveLoginButton.Text = "Save Login";
            this.saveLoginButton.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel4,
            this.toolStripStatusLabel5,
            this.mVersionLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 298);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(451, 22);
            this.statusStrip1.TabIndex = 48;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel2.Text = "Status:";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.BackColor = System.Drawing.Color.White;
            this.toolStripStatusLabel4.ForeColor = System.Drawing.Color.Firebrick;
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(79, 17);
            this.toolStripStatusLabel4.Text = "Disconnected";
            this.toolStripStatusLabel4.VisitedLinkColor = System.Drawing.Color.Red;
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(269, 17);
            this.toolStripStatusLabel5.Spring = true;
            // 
            // mVersionLabel
            // 
            this.mVersionLabel.Name = "mVersionLabel";
            this.mVersionLabel.Size = new System.Drawing.Size(46, 17);
            this.mVersionLabel.Text = "Version";
            // 
            // LoginForm
            // 
            this.AcceptButton = this.loginLogoutButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(451, 320);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.saveLoginButton);
            this.Controls.Add(this.passwordTextbox);
            this.Controls.Add(this.usernameDropdownList);
            this.Controls.Add(this.register);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.accountTypeDropdownList);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.loginLogoutButton);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoginForm_FormClosing);
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button loginLogoutButton;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.ComboBox accountTypeDropdownList;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.LinkLabel register;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.Label toolStripStatusLabel1;
        private System.Windows.Forms.Label myStatusLabel;
        private System.Windows.Forms.Label myStatusType;
        private System.Windows.Forms.Label myPercentComplete;
        private System.Windows.Forms.ComboBox usernameDropdownList;
        private System.Windows.Forms.TextBox passwordTextbox;
        private System.Windows.Forms.CheckBox saveLoginButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel mVersionLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
    }
}