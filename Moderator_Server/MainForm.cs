using Moderator_Server.Backend;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;


namespace Moderator_Server
{
    public partial class MainForm : Form
    {
        private Dictionary<int, ListViewItem> ServerGuiInstance;
        public TradeServer tradeServer;
        private readonly object lock1 = new object();
        public MainForm()
        {
            InitializeComponent();

            this.Text = Program.GetAssamblyInfo();

            lvLogs.View = View.Details;
            lvLogs.Dock = DockStyle.Fill;
            lvLogs.Columns.Add("Time", 100);
            lvLogs.Columns.Add("Type", 80);
            lvLogs.Columns.Add("Message", 500);

            lvclientdetails.View = View.Details;
            lvclientdetails.Columns.Add("UserID", 100);
            lvclientdetails.Columns.Add("Client", 200);
            lvclientdetails.Columns.Add("Status", 200);

            lvServerDetail.View = View.Details;

            lvServerDetail.Columns.Add("Server", 100);
            lvServerDetail.Columns.Add("UserId", 60);
            lvServerDetail.Columns.Add("Ip:Port", 100);
            //lvServerDetail.Columns.Add("NeatId");
            //lvServerDetail.Columns.Add("ServerId");
            lvServerDetail.Columns.Add("Status",100);
            lvServerDetail.FullRowSelect = true;
            lvServerDetail.MultiSelect = false;
            // lvServerDetail.GridLines.

            lvUserDetails.Columns.Add("Neat ID", 65);
            lvUserDetails.Columns.Add("User ID", 65);
            lvUserDetails.Columns.Add("User Name", 245);
            lvUserDetails.Columns.Add("Status", 110);
            lvUserDetails.Columns.Add("CST", 75);
            lvUserDetails.Columns.Add("NNF  Code", 110);
            lvUserDetails.Columns.Add("Login ID", 65);
            lvUserDetails.Columns.Add("Server Name", 120);


            lvUserDetails.View = View.Details;

            //groupBoxModeratorInfo.BackColor = Color.LightCyan;
            lvServerDetail.BackColor = Color.LightGray;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            Program.Gui = this;
            tradeServer = new TradeServer();
            tradeServer.LoadTradeServer();
            lblModeratorCredentials.Text = tradeServer.ToString();
            timer1.Enabled = true;
           
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
                            if (!ServerGuiInstance.ContainsKey(userId))
                            {
                                ListViewItem itm = new ListViewItem(mod);
                                itm.SubItems.Add(userId.ToString());
                                itm.SubItems.Add(ipPort);
                                itm.SubItems.Add("DISCONNETED");
                                lvServerDetail.Items.Add(itm);
                                ServerGuiInstance.Add(userId, itm);
                            }
                        }
                    }
                    updateServerStatus();
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error in Connect.");
                btnConnect.Enabled = true;
                btnDisconnect.Enabled = false;
            }
            updateServerStatus();
        }
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("Are you really want to disconnect All Servers ", "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2))
            {
                btnConnect.Enabled = false;
                btnDisconnect.Enabled = false;

                tradeServer.serverController.DisconnectAllServer();
                btnConnect.Enabled = true;
                btnDisconnect.Enabled = false;

                updateServerStatus();
            }
        }

        public delegate void delWithString(DateTime stamp, string param1, Param.LogType param2);
        public delegate void delFortradeCount(long tradeCount);
        public void DisplayLog(DateTime stamp, string message, Param.LogType logType)
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
            catch(Exception ex) { TradeServer.logger.WriteLine(ex.ToString()); }
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
            try
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
                            listViewHitTestInfo.Item.ForeColor = Color.Red;
                            listViewHitTestInfo.Item.SubItems[3].Text = "DISCONNECTED";
                        }
                    }
                    else
                    {
                        if (tradeServer.serverController.ConnectToBackend(serverId))
                        {
                            listViewHitTestInfo.Item.ForeColor = Color.Green;
                            listViewHitTestInfo.Item.SubItems[3].Text = "CONNECTED";
                        }

                    }
                    //updateDashboard();
                }
            }
            catch(Exception ex)
            {
                TradeServer.logger.WriteLine(ex.ToString());
            }
        }
        public void updateServerStatus()
        {
            try
            {
                if (this.InvokeRequired && !this.IsDisposed)
                {
                    Action action = new Action(updateServerStatus);
                    this.Invoke(action);
                }
                else
                {
                    lock (lock1)
                    {
                        foreach (int i in ServerGuiInstance.Keys)
                        {
                            if (tradeServer.serverController.Connected(i))
                            {
                                ServerGuiInstance[i].ForeColor = Color.Green;
                                ServerGuiInstance[i].SubItems[3].Text = "CONNECTED";
                            }
                            else
                            {
                                ServerGuiInstance[i].ForeColor = Color.Red;
                                ServerGuiInstance[i].SubItems[3].Text = "DISCONNECTED";

                            }
                        }
                    }
                   // updateDashboard();
                }
            }
            catch(Exception ex) { TradeServer.logger.WriteLine(ex.ToString()); }
        }

        private void MainForm_MaximumSizeChanged(object sender, EventArgs e)
        {
            //groupBox2.Visible = true;
            //groupBox2.Size = new System.Drawing.Size(400, 418);

        }
        delegate void updatest();

        public void loadclientdetails()
        {
            if (this.InvokeRequired)
            {
                updatest up = new updatest(loadclientdetails);
                this.Invoke(up);
            }
            else
            {
                try
                {
                    foreach (int id in tradeServer.clntManager.ClientDataBase.Keys)
                    {
                        ListViewItem itm = new ListViewItem(id.ToString().Trim());
                        itm.Name = id.ToString().Trim();
                        itm.SubItems.Add(tradeServer.clntManager.ClientDataBase[id].ClientName);
                        itm.SubItems.Add("DISCONNECTED");
                        itm.ForeColor = Color.Red;
                        lvclientdetails.Items.Add(itm);
                    }
                }
                catch(Exception ex) { TradeServer.logger.WriteLine(ex.ToString()); };
            }


        }
        public void UpdatestatusServercon()
        {
            if (this.InvokeRequired)
            {
                updatest up = new updatest(UpdatestatusServercon);
                this.Invoke(up);
            }
            else
            {
                try
                {
                  
                        int count = tradeServer.clntManager.ClientDataBase.Count;
                    foreach (int id in tradeServer.clntManager.ClientDataBase.Keys)
                    {
                        if (lvclientdetails.Items.ContainsKey(id.ToString().Trim()))
                        {

                            string servername = tradeServer.clntManager.ClientDataBase[id].ClientName + '(' + id + ')';
                            if (tradeServer.clntManager.Checkconnection(id))
                            {
                                lvclientdetails.Items[id.ToString()].SubItems[2].Text = "CONNECTED";
                                lvclientdetails.Items[id.ToString()].ForeColor = Color.Green;

                            }
                            else
                            {
                                lvclientdetails.Items[id.ToString()].SubItems[2].Text = "DISCONNECTED";
                                lvclientdetails.Items[id.ToString()].ForeColor = Color.Red;
                            }
                        }
                    }
                     
                }
                catch(Exception ex)
                {
                    TradeServer.logger.WriteLine(ex.ToString());
                }
            }
        }

        delegate void updatercv();
        /*public void updateDashboard()
        {
            if (this.InvokeRequired)
            {
                updatercv up = new updatercv(updateDashboard);
                this.Invoke(up);
            }
            else
            {
                try
                {
                    // if (WindowState == FormWindowState.Maximized)
                    {
                        //groupBox3.Controls.Clear();
                        RichTextBox richtext = new RichTextBox();
                        richtext.Dock = DockStyle.Fill;

                        foreach (int i in ServerGuiInstance.Keys)
                        {
                                string servername = ServerGuiInstance[i].SubItems[0].Text;
                            if (tradeServer.serverController.Connected(i))
                            {
                                Addtext(richtext, servername.PadRight(20) + ":\tCONNECTED\n", Color.Green, true);
                            }
                            else
                            {
                                Addtext(richtext, servername.PadRight(20) + ":\tDISCONNECTED\n", Color.Red, true);

                            }
                        }
                        //groupBox3.Controls.Add(richtext);
                    }
                }
                catch
                {

                }
            }
        }
        */

        public static void Addtext(RichTextBox box, string text, Color color, bool Addnewline)
        {
            try
            {
                if (Addnewline)
                {
                    text += Environment.NewLine;
                }
                box.SelectionStart = box.TextLength;
                box.SelectionLength = 0;              
                box.SelectionColor = color;
                box.SelectionFont = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                box.AppendText(text);
                box.SelectionColor = box.ForeColor;
            }
            catch { }
        }
      
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            try
            {

                //if (WindowState == FormWindowState.Maximized)
                //{
                //    groupBox2.Visible = true;
                //    groupBox2.Size = new System.Drawing.Size(400, 418);
                //    UpdatestatusServercon();
                //}
                //if (WindowState == FormWindowState.Normal)
                //{
                //    groupBox2.Visible = false;
                //    groupBox2.Size = new System.Drawing.Size(0, 418);

                //}
            }
            catch { }
        }

        private void Reload_btn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to update Moderator Details", "Reload",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2,MessageBoxOptions.DefaultDesktopOnly)==DialogResult.Yes)
            {
                if (MessageBox.Show("Have you Updated the Following Details ? \n TradeMatch :- NeatID And DB Files" +
                    "\n Hedger :- Strategy File And Neat File" +
                    "\n TradeManager :- Database Files(Neat,Ctcl..etc)","Cofirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly) == DialogResult.Yes)
                {
                    try
                    {
                        string path = Constant.path.startUpPath + "\\ModeratorDetail.txt";
                        tradeServer.serverController.LoadServerDetails(path);
                        AddServerToGui(path);
                        tradeServer.ctclDataBase.LoadCtclFile();
                    }
                    catch(Exception ex)
                    {
                        TradeServer.logger.WriteLine(ex.ToString());
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                ModTrade.Text = tradeServer.GetTotalTradeCount().ToString();
            }
            catch { }
        }

     
        //private void lvServerDetail_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Right)
        //    {
        //        // if (lvQtyPlus.FocusedItem.Bounds.Contains(e.Location))
        //        {
        //            ContextMenuStrip ContextMenu = new ContextMenuStrip();
        //            ToolStripMenuItem mnuUserDetails = new ToolStripMenuItem("User Details");
        //            mnuUserDetails.Click += UserDetails_Click;
        //            ContextMenu.Items.Add(mnuUserDetails);
        //            lvServerDetail.ContextMenuStrip = ContextMenu;
        //        }
        //    }
        //}

        public void UpdateNeatDetails(ConcurrentDictionary<int,UserDtStruct>dicNeatData)
        {
           
            UpdateList(dicNeatData);
            
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
                    foreach (int neat in dicNeatData.Keys.ToArray())
                    {
                        UserDtStruct dt = dicNeatData[neat];
                        string st = dt.Status == true ? "Connected" : "DisConnected";
                        ListViewItem item = new ListViewItem(dt.NeatID.ToString());
                        if (dt.Status) { item.ForeColor = Color.Green; } else { item.ForeColor = Color.Red; }
                        item.SubItems.Add(dt.UserID.ToString());
                        item.SubItems.Add(dt.UserName);
                        item.SubItems.Add(st);
                        item.SubItems.Add(dt.dateTime.ToString("HH:mm:ss"));
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
            Setting setting = new Setting(lvUserDetails,this.Top+lvUserDetails.Top,this.Left+lvUserDetails.Left);
            setting.ShowDialog();
        }
    }
}
