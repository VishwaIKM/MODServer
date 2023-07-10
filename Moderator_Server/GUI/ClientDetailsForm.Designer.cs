namespace Moderator_Server.GUI
{
    partial class ClientDetailsForm
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
            this.lvclientdetails = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lvclientdetails
            // 
            this.lvclientdetails.BackColor = System.Drawing.Color.White;
            this.lvclientdetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvclientdetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvclientdetails.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvclientdetails.ForeColor = System.Drawing.Color.Black;
            this.lvclientdetails.FullRowSelect = true;
            this.lvclientdetails.GridLines = true;
            this.lvclientdetails.HideSelection = false;
            this.lvclientdetails.Location = new System.Drawing.Point(0, 0);
            this.lvclientdetails.Name = "lvclientdetails";
            this.lvclientdetails.Size = new System.Drawing.Size(800, 450);
            this.lvclientdetails.TabIndex = 1;
            this.lvclientdetails.UseCompatibleStateImageBehavior = false;
            this.lvclientdetails.View = System.Windows.Forms.View.Details;
            // 
            // ClientDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lvclientdetails);
            this.HideOnClose = true;
            this.Name = "ClientDetailsForm";
            this.Text = "ClientDetailsForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvclientdetails;
    }
}