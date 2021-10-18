using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace Moderator_Server
{
    public partial class MainForm : Form
    {
        private Dictionary<int, ListViewItem> ServerGuiInstance;
        public TradeServer tradeServer;
        public MainForm()
        {
            InitializeComponent();

            this.Text = Program.GetAssamblyInfo();

            lvLogs.View = View.Details;
            lvLogs.Dock = DockStyle.Fill;
            lvLogs.Columns.Add("Time",100);
            lvLogs.Columns.Add("Type", 100);
            lvLogs.Columns.Add("Message", 500);

            lvServerDetail.View = View.Details;
            
            lvServerDetail.Columns.Add("Moderator", 100);
            lvServerDetail.Columns.Add("UserId", 60);
            lvServerDetail.Columns.Add("Ip:Port", 150);
            //lvServerDetail.Columns.Add("NeatId");
            //lvServerDetail.Columns.Add("ServerId");
            lvServerDetail.Columns.Add("Status");
            lvServerDetail.FullRowSelect = true;
            lvServerDetail.MultiSelect = false;

            //groupBoxModeratorInfo.BackColor = Color.LightCyan;
            lvServerDetail.BackColor = Color.LightPink;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            Program.Gui = this;
            tradeServer = new TradeServer();
            tradeServer.LoadTradeServer();
            lblModeratorCredentials.Text = tradeServer.ToString();

            ServerGuiInstance = new Dictionary<int, ListViewItem>();
            string path = Constant.path.startUpPath + "\\ModeratorDetail.txt";
            
            AddServerToGui(path);
            
        }
        private void AddServerToGui(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                    StreamReader sr = new StreamReader(fs);
                    
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
                            ListViewItem itm = new ListViewItem(mod);
                            itm.SubItems.Add(userId.ToString());
                            itm.SubItems.Add(ipPort);
                            itm.SubItems.Add("False");
                            lvServerDetail.Items.Add(itm);
                            ServerGuiInstance.Add(userId, itm);
                        }
                    }
                    fs.Close();
                    sr.Close();
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
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you really wan't to close TradeServer??", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                tradeServer.CloseAll();
            }
            else
            {
                e.Cancel = true;
            }            
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                btnConnect.Enabled = false;
                btnDisconnect.Enabled = false;

                if (tradeServer.serverController.ConnectAll())
                {
                    btnConnect.Enabled = false;
                    btnDisconnect.Enabled = true;
                }
                else
                {
                    btnConnect.Enabled = true;
                    btnDisconnect.Enabled = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(),"Error in Connect.");
                btnConnect.Enabled = true;
                btnDisconnect.Enabled = false;
            }
            UpdateServerStatus();
        }
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("Are you really want to disconnect All Servers " , "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2))
            {
                btnConnect.Enabled = false;
                btnDisconnect.Enabled = false;

                tradeServer.serverController.DisconnectAllServer();
                btnConnect.Enabled = true;
                btnDisconnect.Enabled = false;

                UpdateServerStatus();
            }
        }

        public delegate void delWithString(DateTime stamp, string param1, Param.LogType param2);
        public delegate void delFortradeCount(long tradeCount);
        public void DisplayLog(DateTime stamp, string message,Param.LogType logType)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    delWithString d = new delWithString(DisplayLog);
                    this.Invoke(d, new object[] { stamp, message, logType });
                }
                else
                {
                    ListViewItem itm = new ListViewItem(stamp.ToString("HH:mm:ss"));
                    if (logType == Param.LogType.Error)
                    {
                        itm.ForeColor = Color.Red;
                    }

                    itm.SubItems.Add(logType.ToString());
                    itm.SubItems.Add(message);
                    this.lvLogs.Items.Insert(0, itm);
                }
            }
            catch { }
        }
        public void UpdateTradeCount(long tradeCount)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    delFortradeCount d = new delFortradeCount(UpdateTradeCount);
                    this.Invoke(d, new object[] { tradeCount });
                }
                else
                {
                        ModTrade.Text = tradeCount.ToString();
                }
            }
            catch { }
        }
        private void lvServerDetail_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo listViewHitTestInfo = lvServerDetail.HitTest(e.X, e.Y);
            // Index of the clicked ListView column
            int columnIndex = listViewHitTestInfo.Item.SubItems.IndexOf(listViewHitTestInfo.SubItem);
            if (columnIndex > -1)
            {
                int serverId = int.Parse(listViewHitTestInfo.Item.SubItems[1].Text);
                if (tradeServer.serverController.Connected(serverId))
                {
                    if (DialogResult.OK == MessageBox.Show("Do you really want to disconnect Server " + serverId, "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2))
                    {
                        tradeServer.serverController.DisconnectServer(serverId);
                        listViewHitTestInfo.Item.SubItems[3].Text = false.ToString();
                    }
                }
                else
                {
                    listViewHitTestInfo.Item.SubItems[3].Text = tradeServer.serverController.ConnectToBackend(serverId).ToString();
                }
            }
        }
        public void UpdateServerStatus()
        {
            try
            {
                if (this.InvokeRequired && !this.IsDisposed)
                {
                    Action action = new Action(UpdateServerStatus);
                    this.Invoke(action);
                }
                else
                {
                    foreach (int i in ServerGuiInstance.Keys)
                    {
                        ServerGuiInstance[i].SubItems[3].Text = tradeServer.serverController.Connected(i).ToString();
                    } 
                }
            }
            catch { }
        }

    }
}
