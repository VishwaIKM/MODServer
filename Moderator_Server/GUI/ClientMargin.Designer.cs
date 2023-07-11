namespace Moderator_Server.GUI
{
    partial class ClientMargin
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnUpdateConfig = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.RestrictionSetting = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.MarginWatchDataGrid = new System.Windows.Forms.DataGridView();
            this.head_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.head_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.header_totalamount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.head_netmargin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.head_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.head_isapplicable = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MarginWatchDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.RestrictionSetting);
            this.panel1.Controls.Add(this.btnUpdateConfig);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1430, 34);
            this.panel1.TabIndex = 0;
            // 
            // btnUpdateConfig
            // 
            this.btnUpdateConfig.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnUpdateConfig.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnUpdateConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateConfig.Location = new System.Drawing.Point(1269, 0);
            this.btnUpdateConfig.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnUpdateConfig.Name = "btnUpdateConfig";
            this.btnUpdateConfig.Size = new System.Drawing.Size(161, 34);
            this.btnUpdateConfig.TabIndex = 16;
            this.btnUpdateConfig.Text = "UPDATE";
            this.btnUpdateConfig.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 34);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1430, 15);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.MarginWatchDataGrid);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 49);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1430, 595);
            this.panel3.TabIndex = 2;
            // 
            // RestrictionSetting
            // 
            this.RestrictionSetting.BackColor = System.Drawing.Color.White;
            this.RestrictionSetting.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.RestrictionSetting.Dock = System.Windows.Forms.DockStyle.Right;
            this.RestrictionSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RestrictionSetting.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RestrictionSetting.ForeColor = System.Drawing.Color.Gray;
            this.RestrictionSetting.FormattingEnabled = true;
            this.RestrictionSetting.Items.AddRange(new object[] {
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday"});
            this.RestrictionSetting.Location = new System.Drawing.Point(1093, 0);
            this.RestrictionSetting.Name = "RestrictionSetting";
            this.RestrictionSetting.Size = new System.Drawing.Size(176, 32);
            this.RestrictionSetting.TabIndex = 17;
            this.RestrictionSetting.Text = "Day";
            this.RestrictionSetting.SelectedIndexChanged += new System.EventHandler(this.RestrictionSetting_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(741, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(352, 27);
            this.label1.TabIndex = 18;
            this.label1.Text = "Restriction Setting Applied for :      ";
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(16, 595);
            this.panel4.TabIndex = 0;
            // 
            // MarginWatchDataGrid
            // 
            this.MarginWatchDataGrid.AllowUserToAddRows = false;
            this.MarginWatchDataGrid.AllowUserToDeleteRows = false;
            this.MarginWatchDataGrid.BackgroundColor = System.Drawing.Color.White;
            this.MarginWatchDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MarginWatchDataGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MarginWatchDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.MarginWatchDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MarginWatchDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.head_code,
            this.head_name,
            this.header_totalamount,
            this.head_netmargin,
            this.head_status,
            this.head_isapplicable});
            this.MarginWatchDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MarginWatchDataGrid.GridColor = System.Drawing.Color.Silver;
            this.MarginWatchDataGrid.Location = new System.Drawing.Point(16, 0);
            this.MarginWatchDataGrid.Name = "MarginWatchDataGrid";
            this.MarginWatchDataGrid.RowHeadersVisible = false;
            this.MarginWatchDataGrid.RowTemplate.Height = 30;
            this.MarginWatchDataGrid.Size = new System.Drawing.Size(1414, 595);
            this.MarginWatchDataGrid.TabIndex = 1;
            // 
            // head_code
            // 
            this.head_code.HeaderText = "Code";
            this.head_code.Name = "head_code";
            this.head_code.Width = 110;
            // 
            // head_name
            // 
            this.head_name.HeaderText = "Name";
            this.head_name.Name = "head_name";
            this.head_name.Width = 160;
            // 
            // header_totalamount
            // 
            this.header_totalamount.HeaderText = "Allotted Amount";
            this.header_totalamount.Name = "header_totalamount";
            this.header_totalamount.Width = 150;
            // 
            // head_netmargin
            // 
            this.head_netmargin.HeaderText = "Margin";
            this.head_netmargin.Name = "head_netmargin";
            this.head_netmargin.Width = 150;
            // 
            // head_status
            // 
            this.head_status.HeaderText = "Status";
            this.head_status.Name = "head_status";
            this.head_status.Width = 150;
            // 
            // head_isapplicable
            // 
            this.head_isapplicable.HeaderText = "Restrictions ";
            this.head_isapplicable.Items.AddRange(new object[] {
            "Applicable",
            "Not Applicable"});
            this.head_isapplicable.Name = "head_isapplicable";
            this.head_isapplicable.Width = 150;
            // 
            // ClientMargin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1430, 644);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideOnClose = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ClientMargin";
            this.Text = "Margin Watch";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MarginWatchDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnUpdateConfig;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.ComboBox RestrictionSetting;
        internal System.Windows.Forms.DataGridView MarginWatchDataGrid;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridViewTextBoxColumn head_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn head_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn header_totalamount;
        private System.Windows.Forms.DataGridViewTextBoxColumn head_netmargin;
        private System.Windows.Forms.DataGridViewTextBoxColumn head_status;
        private System.Windows.Forms.DataGridViewComboBoxColumn head_isapplicable;
    }
}