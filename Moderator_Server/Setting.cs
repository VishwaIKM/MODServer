using Moderator_Server.Backend;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Moderator_Server
{
    public partial class Setting : Form
    {
        ListView userDt;
        int x;
        int y;
        public Setting(ListView UserDt,int x,int y)
        {
            InitializeComponent();
            this.x = x + 200;
            this.y = y + 150;
            this.userDt = UserDt;
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            this.Top = x;
            this.Left = y;
            Point p = new Point(panel1.Location.X + 10, panel1.Location.Y + 10);

            foreach (ColumnHeader col in userDt.Columns)
            {
                string colName = col.Text;
                if (colName == "User ID"|| colName== "Server Name"|| colName== "CST")
                {
                    CheckBox chk = new CheckBox();
                    chk.Text = colName;
                    chk.Name = col.Index.ToString();

                    chk.Checked = col.Width > 0 ? true : false;

                    chk.Location = p;
                    chk.CheckedChanged += delegate
                    {
                        int index = Convert.ToInt32(chk.Name);
                        ColumnHeader colm = userDt.Columns[index];
                        if (colm.Width == 0)
                        {
                            if (colm.Text == "User ID")
                            {
                                userDt.Columns[index].Width = 80;
                            }
                            if (colm.Text == "Server Name")
                            {
                                userDt.Columns[index].Width = 120;
                            }
                            if(colm.Text== "CST")
                            {
                                userDt.Columns[index].Width = 80;
                            }
                        }
                        else
                        {
                            userDt.Columns[index].Width = 0;
                        }
                    };
                    panel1.Controls.Add(chk);
                    p = new Point(p.X, p.Y + chk.Height);
                }

            }
        }

        private void Setting_FormClosing(object sender, FormClosingEventArgs e)
        {
          
        }
    }
}
