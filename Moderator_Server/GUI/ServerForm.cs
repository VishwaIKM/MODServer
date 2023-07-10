using Moderator_Server.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Moderator_Server.GUI
{
    public partial class ServerForm : DockContent
    {
        #region Fileds & MEB

       

        #endregion
        public ServerForm()
        {
            InitializeComponent();

            lvServerDetail.View = View.Details;

            lvServerDetail.Columns.Add(" Server ", 160);
            lvServerDetail.Columns.Add("User Id", 100);
            lvServerDetail.Columns.Add(" IP : Port", 160);
            //lvServerDetail.Columns.Add("NeatId");
            //lvServerDetail.Columns.Add("ServerId");
            lvServerDetail.Columns.Add("Status", 190);
            lvServerDetail.FullRowSelect = true;
            lvServerDetail.MultiSelect = false;

          
        }
        public void AddServerToGui(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    StreamReader sr = new StreamReader(fs);
                    sr.ReadLine();
                    while (sr.Peek() > 0)
                    {
                        string line = sr.ReadLine();
                        if (!string.IsNullOrEmpty(line))
                        {
                            string[] arr = line.Split(',');
                            int userId = Convert.ToInt32(arr[0]);
                            string mod = arr[1];
                            string ipPort = arr[2];
                            int passWord = Convert.ToInt32(arr[3]);
                            string[] ln = arr[2].Split(':');
                            string ip = ln[0];
                            int port = Convert.ToInt32(ln[1]);
                            if (!General.DockForm.ServerGuiInstance.ContainsKey(userId))
                            {
                                ListViewItem itm = new ListViewItem(mod);
                                itm.SubItems.Add(userId.ToString());
                                itm.SubItems.Add(ipPort);
                                itm.SubItems.Add("DISCONNETED");
                                lvServerDetail.Items.Add(itm);
                                General.DockForm.ServerGuiInstance.Add(userId, itm);
                            }
                        }
                    }
                    General.DockForm.updateServerStatus();
                    sr.Close();
                    fs.Close();
                }
                else
                {
                    TradeServer.logger.WriteLine("Moderator Detail File does not exists");
                }
            }
            catch (Exception ex)
            {
                TradeServer.logger.WriteLine("Error in Loading Server Detail :" + ex);
            }
        }

        private void lvServerDetail_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                ListViewHitTestInfo listViewHitTestInfo = lvServerDetail.HitTest(e.X, e.Y);
                // Index of the clicked ListView column
                int columnIndex = listViewHitTestInfo.Item.SubItems.IndexOf(listViewHitTestInfo.SubItem);
                if (columnIndex > -1)
                {
                    int serverId = int.Parse(listViewHitTestInfo.Item.SubItems[1].Text);
                    if (General.tradeServer.serverController.Connected(serverId))
                    {
                        if (DialogResult.OK == MessageBox.Show("Do you really want to disconnect Server " + serverId, "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2))
                        {
                            General.tradeServer.serverController.DisconnectServer(serverId);
                            listViewHitTestInfo.Item.ForeColor = Color.Red;
                            listViewHitTestInfo.Item.SubItems[3].Text = "DISCONNECTED";
                        }
                    }
                    else
                    {
                        if (General.tradeServer.serverController.ConnectToBackend(serverId))
                        {
                            listViewHitTestInfo.Item.ForeColor = Color.Green;
                            listViewHitTestInfo.Item.SubItems[3].Text = "CONNECTED";
                        }

                    }
                    //updateDashboard();
                }
            }
            catch (Exception ex)
            {
                TradeServer.logger.WriteLine(ex.ToString());
            }
        }
    }
}
