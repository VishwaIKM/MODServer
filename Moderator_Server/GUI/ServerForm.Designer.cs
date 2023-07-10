namespace Moderator_Server.GUI
{
    partial class ServerForm
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
            this.lvServerDetail = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lvServerDetail
            // 
            this.lvServerDetail.BackColor = System.Drawing.Color.White;
            this.lvServerDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvServerDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvServerDetail.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvServerDetail.ForeColor = System.Drawing.Color.Black;
            this.lvServerDetail.GridLines = true;
            this.lvServerDetail.HideSelection = false;
            this.lvServerDetail.Location = new System.Drawing.Point(0, 0);
            this.lvServerDetail.Name = "lvServerDetail";
            this.lvServerDetail.Size = new System.Drawing.Size(800, 450);
            this.lvServerDetail.TabIndex = 1;
            this.lvServerDetail.UseCompatibleStateImageBehavior = false;
            this.lvServerDetail.View = System.Windows.Forms.View.Details;
            this.lvServerDetail.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvServerDetail_MouseDoubleClick);
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lvServerDetail);
            this.HideOnClose = true;
            this.Name = "ServerForm";
            this.Text = "ServerForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvServerDetail;
    }
}