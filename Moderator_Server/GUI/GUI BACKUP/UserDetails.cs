using Moderator_Server.Backend;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moderator_Server
{
    public partial class UserDetails : Form
    {
        public UserDetails()
        {
            InitializeComponent();

            lvUserDetails.Columns.Add("Neat ID", 100);
            lvUserDetails.Columns.Add("User ID", 100);
            lvUserDetails.Columns.Add("Status", 100);

            lvUserDetails.View = View.Details;
        }

        public void SetSize(int x,int y,int a,int b)
        {
            this.Width = x;
            this.Height = y;
            this.Top = a;
            this.Left = b;
        }

        private void UserDetails_FormClosed(object sender, FormClosedEventArgs e)
        {
           // this.Hide()
        }

        private void UserDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
        delegate void UpdateListdata(ConcurrentDictionary<int, UserDtStruct> dicNeatData);
        public void UpdateList(ConcurrentDictionary<int,UserDtStruct> dicNeatData)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    UpdateListdata update = new UpdateListdata(UpdateList);
                    this.Invoke(update, dicNeatData);
                }
                else
                {
                    lvUserDetails.Items.Clear();
                    foreach (int neat in dicNeatData.Keys.ToArray())
                    {
                        UserDtStruct dt = dicNeatData[neat];

                        ListViewItem item = new ListViewItem(dt.NeatID.ToString());
                        item.SubItems.Add(dt.UserID.ToString());
                        item.SubItems.Add(dt.Status.ToString());

                        lvUserDetails.Items.Insert(0, item);
                    }
                }
            }
            catch(Exception ex)
            {
                TradeServer.logger.WriteError("Error while Updting NeatID Details"+ex);
            }

        }
        
    }
}
