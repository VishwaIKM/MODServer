namespace Moderator_Server
{
    partial class MainForm
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
            this.ModTrade = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.lblModeratorCredentials = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lvServerDetail = new System.Windows.Forms.ListView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvclientdetails = new System.Windows.Forms.ListView();
            this.lvLogs = new System.Windows.Forms.ListView();
            this.Reload_btn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ModTrade
            // 
            this.ModTrade.AutoSize = true;
            this.ModTrade.Location = new System.Drawing.Point(260, 30);
            this.ModTrade.Name = "ModTrade";
            this.ModTrade.Size = new System.Drawing.Size(13, 13);
            this.ModTrade.TabIndex = 8;
            this.ModTrade.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(220, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "ModeratorTrade";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(487, 9);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(105, 27);
            this.btnDisconnect.TabIndex = 3;
            this.btnDisconnect.Text = "Disconnect All";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // lblModeratorCredentials
            // 
            this.lblModeratorCredentials.AutoSize = true;
            this.lblModeratorCredentials.Location = new System.Drawing.Point(6, 16);
            this.lblModeratorCredentials.Name = "lblModeratorCredentials";
            this.lblModeratorCredentials.Size = new System.Drawing.Size(202, 13);
            this.lblModeratorCredentials.TabIndex = 2;
            this.lblModeratorCredentials.Text = "TradeServer Details are not assigned yet.";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(380, 9);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(98, 27);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect All";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.groupBox1.Controls.Add(this.Reload_btn);
            this.groupBox1.Controls.Add(this.ModTrade);
            this.groupBox1.Controls.Add(this.btnDisconnect);
            this.groupBox1.Controls.Add(this.lblModeratorCredentials);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(762, 54);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Moderator details";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lvServerDetail);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(355, 490);
            this.panel1.TabIndex = 2;
            // 
            // lvServerDetail
            // 
            this.lvServerDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvServerDetail.GridLines = true;
            this.lvServerDetail.HideSelection = false;
            this.lvServerDetail.Location = new System.Drawing.Point(0, 0);
            this.lvServerDetail.Name = "lvServerDetail";
            this.lvServerDetail.Size = new System.Drawing.Size(355, 490);
            this.lvServerDetail.TabIndex = 0;
            this.lvServerDetail.UseCompatibleStateImageBehavior = false;
            this.lvServerDetail.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvServerDetail_MouseDoubleClick_1);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(355, 54);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lvLogs);
            this.splitContainer1.Size = new System.Drawing.Size(407, 490);
            this.splitContainer1.SplitterDistance = 199;
            this.splitContainer1.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lvclientdetails);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(407, 199);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Client_Status";
            // 
            // lvclientdetails
            // 
            this.lvclientdetails.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lvclientdetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvclientdetails.GridLines = true;
            this.lvclientdetails.HideSelection = false;
            this.lvclientdetails.Location = new System.Drawing.Point(3, 16);
            this.lvclientdetails.Name = "lvclientdetails";
            this.lvclientdetails.Size = new System.Drawing.Size(401, 180);
            this.lvclientdetails.TabIndex = 0;
            this.lvclientdetails.UseCompatibleStateImageBehavior = false;
            // 
            // lvLogs
            // 
            this.lvLogs.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lvLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLogs.HideSelection = false;
            this.lvLogs.Location = new System.Drawing.Point(0, 0);
            this.lvLogs.Name = "lvLogs";
            this.lvLogs.Size = new System.Drawing.Size(407, 287);
            this.lvLogs.TabIndex = 0;
            this.lvLogs.UseCompatibleStateImageBehavior = false;
            // 
            // Reload_btn
            // 
            this.Reload_btn.Location = new System.Drawing.Point(595, 9);
            this.Reload_btn.Name = "Reload_btn";
            this.Reload_btn.Size = new System.Drawing.Size(150, 27);
            this.Reload_btn.TabIndex = 9;
            this.Reload_btn.Text = "Update Moderator Details";
            this.Reload_btn.UseVisualStyleBackColor = true;
            this.Reload_btn.Click += new System.EventHandler(this.Reload_btn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 544);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "Moderator";
            this.MaximumSizeChanged += new System.EventHandler(this.MainForm_MaximumSizeChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblModeratorCredentials;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Label ModTrade;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView lvServerDetail;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lvLogs;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lvclientdetails;
        private System.Windows.Forms.Button Reload_btn;
    }
}

