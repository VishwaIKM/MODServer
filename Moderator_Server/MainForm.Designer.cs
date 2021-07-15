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
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.ModTrade = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lvServerDetail = new System.Windows.Forms.ListView();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.groupBoxModeratorInfo = new System.Windows.Forms.GroupBox();
            this.lblModeratorCredentials = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lvLogs = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.groupBoxModeratorInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.splitContainerMain.Panel1.Controls.Add(this.ModTrade);
            this.splitContainerMain.Panel1.Controls.Add(this.label2);
            this.splitContainerMain.Panel1.Controls.Add(this.lvServerDetail);
            this.splitContainerMain.Panel1.Controls.Add(this.btnDisconnect);
            this.splitContainerMain.Panel1.Controls.Add(this.groupBoxModeratorInfo);
            this.splitContainerMain.Panel1.Controls.Add(this.btnConnect);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.lvLogs);
            this.splitContainerMain.Size = new System.Drawing.Size(900, 362);
            this.splitContainerMain.SplitterDistance = 135;
            this.splitContainerMain.TabIndex = 0;
            // 
            // ModTrade
            // 
            this.ModTrade.AutoSize = true;
            this.ModTrade.Location = new System.Drawing.Point(636, 96);
            this.ModTrade.Name = "ModTrade";
            this.ModTrade.Size = new System.Drawing.Size(13, 13);
            this.ModTrade.TabIndex = 8;
            this.ModTrade.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(626, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "ModeratorTrade";
            // 
            // lvServerDetail
            // 
            this.lvServerDetail.HideSelection = false;
            this.lvServerDetail.Location = new System.Drawing.Point(187, 12);
            this.lvServerDetail.Name = "lvServerDetail";
            this.lvServerDetail.Size = new System.Drawing.Size(415, 121);
            this.lvServerDetail.TabIndex = 4;
            this.lvServerDetail.UseCompatibleStateImageBehavior = false;
            this.lvServerDetail.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvServerDetail_MouseDoubleClick_1);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(769, 12);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(119, 27);
            this.btnDisconnect.TabIndex = 3;
            this.btnDisconnect.Text = "Disconnect All";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // groupBoxModeratorInfo
            // 
            this.groupBoxModeratorInfo.Controls.Add(this.lblModeratorCredentials);
            this.groupBoxModeratorInfo.Location = new System.Drawing.Point(6, 12);
            this.groupBoxModeratorInfo.Name = "groupBoxModeratorInfo";
            this.groupBoxModeratorInfo.Size = new System.Drawing.Size(175, 121);
            this.groupBoxModeratorInfo.TabIndex = 2;
            this.groupBoxModeratorInfo.TabStop = false;
            this.groupBoxModeratorInfo.Text = "ModeratorServer Details";
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
            this.btnConnect.Location = new System.Drawing.Point(618, 12);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(119, 27);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect All";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lvLogs
            // 
            this.lvLogs.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lvLogs.HideSelection = false;
            this.lvLogs.Location = new System.Drawing.Point(6, 3);
            this.lvLogs.Name = "lvLogs";
            this.lvLogs.Size = new System.Drawing.Size(891, 212);
            this.lvLogs.TabIndex = 0;
            this.lvLogs.UseCompatibleStateImageBehavior = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 362);
            this.Controls.Add(this.splitContainerMain);
            this.Name = "MainForm";
            this.Text = "Moderator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel1.PerformLayout();
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.groupBoxModeratorInfo.ResumeLayout(false);
            this.groupBoxModeratorInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.GroupBox groupBoxModeratorInfo;
        private System.Windows.Forms.Label lblModeratorCredentials;
        private System.Windows.Forms.ListView lvLogs;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.ListView lvServerDetail;
        private System.Windows.Forms.Label ModTrade;
        private System.Windows.Forms.Label label2;
    }
}

