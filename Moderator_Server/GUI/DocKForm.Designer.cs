namespace Moderator_Server.GUI
{
    partial class DocKForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ModTrade = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.Reload_btn = new System.Windows.Forms.Button();
            this.lblModeratorCredentials = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.marginWatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loggerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dockyardPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.vS2005Theme1 = new WeifenLuo.WinFormsUI.Docking.VS2005Theme();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ModTrade);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Controls.Add(this.btnDisconnect);
            this.groupBox1.Controls.Add(this.Reload_btn);
            this.groupBox1.Controls.Add(this.lblModeratorCredentials);
            this.groupBox1.Controls.Add(this.menuStrip1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1268, 88);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Moderator Details";
            // 
            // ModTrade
            // 
            this.ModTrade.AutoSize = true;
            this.ModTrade.Dock = System.Windows.Forms.DockStyle.Left;
            this.ModTrade.Font = new System.Drawing.Font("Times New Roman", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ModTrade.Location = new System.Drawing.Point(671, 46);
            this.ModTrade.Name = "ModTrade";
            this.ModTrade.Size = new System.Drawing.Size(28, 31);
            this.ModTrade.TabIndex = 20;
            this.ModTrade.Text = "0";
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(643, 46);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(28, 39);
            this.panel2.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.ForestGreen;
            this.label2.Location = new System.Drawing.Point(490, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 22);
            this.label2.TabIndex = 17;
            this.label2.Text = "Moderator Trade";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(290, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 39);
            this.panel1.TabIndex = 16;
            // 
            // btnConnect
            // 
            this.btnConnect.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnConnect.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.Location = new System.Drawing.Point(755, 46);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(165, 39);
            this.btnConnect.TabIndex = 15;
            this.btnConnect.Text = "Connect All";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnDisconnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDisconnect.Location = new System.Drawing.Point(920, 46);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(146, 39);
            this.btnDisconnect.TabIndex = 14;
            this.btnDisconnect.Text = "Disconnect All";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // Reload_btn
            // 
            this.Reload_btn.Dock = System.Windows.Forms.DockStyle.Right;
            this.Reload_btn.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.Reload_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Reload_btn.Location = new System.Drawing.Point(1066, 46);
            this.Reload_btn.Name = "Reload_btn";
            this.Reload_btn.Size = new System.Drawing.Size(199, 39);
            this.Reload_btn.TabIndex = 13;
            this.Reload_btn.Text = "Update Moderator Details";
            this.Reload_btn.UseVisualStyleBackColor = true;
            this.Reload_btn.Click += new System.EventHandler(this.Reload_btn_Click);
            // 
            // lblModeratorCredentials
            // 
            this.lblModeratorCredentials.AutoSize = true;
            this.lblModeratorCredentials.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblModeratorCredentials.ForeColor = System.Drawing.Color.Gray;
            this.lblModeratorCredentials.Location = new System.Drawing.Point(3, 46);
            this.lblModeratorCredentials.Name = "lblModeratorCredentials";
            this.lblModeratorCredentials.Size = new System.Drawing.Size(287, 19);
            this.lblModeratorCredentials.TabIndex = 3;
            this.lblModeratorCredentials.Text = "TradeServer Details are not assigned yet.";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(3, 22);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1262, 24);
            this.menuStrip1.TabIndex = 21;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.marginWatchToolStripMenuItem,
            this.loggerToolStripMenuItem,
            this.clientDetailsToolStripMenuItem,
            this.serverDetailsToolStripMenuItem,
            this.userDetailsToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // marginWatchToolStripMenuItem
            // 
            this.marginWatchToolStripMenuItem.Name = "marginWatchToolStripMenuItem";
            this.marginWatchToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.marginWatchToolStripMenuItem.Text = "Margin Watch";
            this.marginWatchToolStripMenuItem.Click += new System.EventHandler(this.marginWatchToolStripMenuItem_Click);
            // 
            // loggerToolStripMenuItem
            // 
            this.loggerToolStripMenuItem.Name = "loggerToolStripMenuItem";
            this.loggerToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.loggerToolStripMenuItem.Text = "Logger";
            this.loggerToolStripMenuItem.Click += new System.EventHandler(this.loggerToolStripMenuItem_Click);
            // 
            // clientDetailsToolStripMenuItem
            // 
            this.clientDetailsToolStripMenuItem.Name = "clientDetailsToolStripMenuItem";
            this.clientDetailsToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.clientDetailsToolStripMenuItem.Text = "Client Details";
            this.clientDetailsToolStripMenuItem.Click += new System.EventHandler(this.clientDetailsToolStripMenuItem_Click);
            // 
            // serverDetailsToolStripMenuItem
            // 
            this.serverDetailsToolStripMenuItem.Name = "serverDetailsToolStripMenuItem";
            this.serverDetailsToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.serverDetailsToolStripMenuItem.Text = "Server Details";
            this.serverDetailsToolStripMenuItem.Click += new System.EventHandler(this.serverDetailsToolStripMenuItem_Click);
            // 
            // userDetailsToolStripMenuItem
            // 
            this.userDetailsToolStripMenuItem.Name = "userDetailsToolStripMenuItem";
            this.userDetailsToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.userDetailsToolStripMenuItem.Text = "User Details";
            this.userDetailsToolStripMenuItem.Click += new System.EventHandler(this.userDetailsToolStripMenuItem_Click);
            // 
            // dockyardPanel
            // 
            this.dockyardPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockyardPanel.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.dockyardPanel.Location = new System.Drawing.Point(0, 88);
            this.dockyardPanel.Name = "dockyardPanel";
            this.dockyardPanel.Size = new System.Drawing.Size(1268, 558);
            this.dockyardPanel.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // DocKForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1268, 646);
            this.Controls.Add(this.dockyardPanel);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DocKForm";
            this.Text = "MOD SERVER";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DocKForm_FormClosing);
            this.Load += new System.EventHandler(this.DocKForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblModeratorCredentials;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button Reload_btn;
        private WeifenLuo.WinFormsUI.Docking.VS2005Theme vS2005Theme1;
        internal WeifenLuo.WinFormsUI.Docking.DockPanel dockyardPanel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label ModTrade;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem marginWatchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loggerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serverDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userDetailsToolStripMenuItem;
    }
}