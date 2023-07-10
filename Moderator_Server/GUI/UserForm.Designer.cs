namespace Moderator_Server.GUI
{
    partial class UserForm
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
            this.lvUserDetails = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lvUserDetails
            // 
            this.lvUserDetails.AllowColumnReorder = true;
            this.lvUserDetails.BackColor = System.Drawing.Color.White;
            this.lvUserDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvUserDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvUserDetails.ForeColor = System.Drawing.Color.Black;
            this.lvUserDetails.GridLines = true;
            this.lvUserDetails.HideSelection = false;
            this.lvUserDetails.Location = new System.Drawing.Point(0, 0);
            this.lvUserDetails.Name = "lvUserDetails";
            this.lvUserDetails.Size = new System.Drawing.Size(906, 497);
            this.lvUserDetails.TabIndex = 2;
            this.lvUserDetails.UseCompatibleStateImageBehavior = false;
            this.lvUserDetails.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvUserDetails_MouseDown);
            // 
            // UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 497);
            this.Controls.Add(this.lvUserDetails);
            this.HideOnClose = true;
            this.Name = "UserForm";
            this.Text = "UserForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvUserDetails;
    }
}