using Moderator_Server.Backend;
using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Moderator_Server.GUI
{
    public partial class UserForm : DockContent
    {
        public UserForm()
        {
            InitializeComponent();

            lvUserDetails.Columns.Add("Neat ID", 100);
            lvUserDetails.Columns.Add("User ID", 100);
            lvUserDetails.Columns.Add("User Name", 275);
            lvUserDetails.Columns.Add("Status", 140);
            lvUserDetails.Columns.Add("CST", 100);
            lvUserDetails.Columns.Add("LTT", 100);
            lvUserDetails.Columns.Add("NNF  Code", 150);
            lvUserDetails.Columns.Add("Login ID", 100);
            lvUserDetails.Columns.Add("Server Name", 180);


            lvUserDetails.View = View.Details;
        }
        public void UserDetailsDisplaIndex()
        {
            try
            {
                if (File.Exists(Constant.path.IniPath))
                {
                    Ini ini = new Ini(Constant.path.IniPath);
                    int i = 0;
                    foreach (ColumnHeader col in lvUserDetails.Columns)
                    {
                        col.DisplayIndex = ini.Read_int("USERDETAILS", i.ToString());
                        i++;
                    }
                }
                else
                {
                    TradeServer.logger.WriteLine("Config file des not exists");
                }
            }
            catch (Exception ex)
            {
                TradeServer.logger.WriteLine(ex.ToString());
            }
        }

        public void SaveUserdetailsDisplayIndex()
        {
            try
            {
                if (File.Exists(Constant.path.IniPath))
                {
                    Ini ini = new Ini(Constant.path.IniPath);
                    int i = 0;
                    foreach (ColumnHeader col in lvUserDetails.Columns)
                    {
                        ini.Write("USERDETAILS", i.ToString(), col.DisplayIndex.ToString());
                        i++;
                    }
                }
                else
                {
                    TradeServer.logger.WriteLine("Config file des not exists");
                }
            }
            catch (Exception ex)
            {
                TradeServer.logger.WriteLine(ex.ToString());
            }
        }
        delegate void UpdateListdata(ConcurrentDictionary<int, UserDtStruct> dicNeatData);
        public void UpdateList(ConcurrentDictionary<int, UserDtStruct> dicNeatData)
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
                    int[] keys = dicNeatData.Keys.ToArray();
                    Array.Sort(keys);
                    foreach (int neat in keys)
                    {
                        UserDtStruct dt = dicNeatData[neat];
                        string st = dt.Status == true ? "Connected" : "DisConnected";
                        ListViewItem item = new ListViewItem(dt.NeatID.ToString());
                        if (dt.Status) { item.ForeColor = Color.Green; } else { item.ForeColor = Color.Red; }
                        item.SubItems.Add(dt.UserID.ToString());
                        item.SubItems.Add(dt.UserName);
                        item.SubItems.Add(st);
                        item.SubItems.Add(dt.dateTime.ToString("HH:mm:ss"));
                        item.SubItems.Add(dt.LastTradedTime.ToString("HH:mm:ss"));
                        item.SubItems.Add(dt.CtclID);
                        item.SubItems.Add(dt.LoginID);
                        item.SubItems.Add(dt.ServerName);
                        lvUserDetails.Items.Insert(0, item);
                    }
                }
            }
            catch (Exception ex)
            {
                TradeServer.logger.WriteError("Error while Updting NeatID Details" + ex);
            }

        }

        private void lvUserDetails_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // if (lvUserDetails.FocusedItem.Bounds.Contains(e.Location))
                {
                    ContextMenuStrip ContextMenu = new ContextMenuStrip();
                    ToolStripMenuItem mnuUserDetails = new ToolStripMenuItem("Show/Hide Column");
                    mnuUserDetails.Click += UserDetails_Click;
                    ContextMenu.Items.Add(mnuUserDetails);
                    lvUserDetails.ContextMenuStrip = ContextMenu;
                }
            }
        }
        private void UserDetails_Click(object sender, EventArgs e)
        {
            Setting setting = new Setting(lvUserDetails, this.Top + lvUserDetails.Top, this.Left + lvUserDetails.Left);
            setting.ShowDialog();
        }
    }
}
