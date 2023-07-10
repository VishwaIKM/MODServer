using Moderator_Server.Backend;
using Moderator_Server.ClientManager;
using Moderator_Server.Constant;
using Moderator_Server.Resources;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moderator_Server.GUI
{
    public partial class DocKForm : Form
    {
        public Dictionary<int, ListViewItem> ServerGuiInstance;
        private readonly object lock1 = new object();
        public DocKForm()
        {
            InitializeComponent();
            General.DockForm = this;
        }

        private void DocKForm_Load(object sender, EventArgs e)
        {
            DockManager.InitialDockFormLoad();

            General.tradeServer.LoadTradeServer();
            lblModeratorCredentials.Text = General.tradeServer.ToString();
            timer1.Enabled = true;

            ServerGuiInstance = new Dictionary<int, ListViewItem>();
            string path = Constant.path.startUpPath + "\\ModeratorDetail.txt";

            General.ServerForm.AddServerToGui(path);
            General.UserForm.UserDetailsDisplaIndex();
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                btnConnect.Enabled = false;
                btnDisconnect.Enabled = false;

                if (General.tradeServer.serverController.ConnectAll())
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

                General.tradeServer.serverController.DisconnectAllServer();
                btnConnect.Enabled = true;
                btnDisconnect.Enabled = false;

                updateServerStatus();
            }
        }

        private void Reload_btn_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Do you really want to update Moderator Details", "Reload", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly) == DialogResult.Yes)
            {
                if (MessageBox.Show("Have you Updated the Following Details ? \n TradeMatch :- NeatID And DB Files" +
                    "\n Hedger :- Strategy File And Neat File" +
                    "\n TradeManager :- Database Files(Neat,Ctcl..etc)", "Cofirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly) == DialogResult.Yes)
                {
                    try
                    {
                        string path = Constant.path.startUpPath + "\\ModeratorDetail.txt";
                        General.tradeServer.serverController.LoadServerDetails(path);
                        General.ServerForm.AddServerToGui(path);
                        General.tradeServer.ctclDataBase.LoadCtclFile();
                    }
                    catch (Exception ex)
                    {
                        TradeServer.logger.WriteLine(ex.ToString());
                    }
                }
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
                            if (General.tradeServer.serverController.Connected(i))
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
            catch (Exception ex) { TradeServer.logger.WriteLine(ex.ToString()); }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                ModTrade.Text = General.tradeServer.GetTotalTradeCount().ToString();
                General.tradeServer.serverController.UpdateNeatLTTAfterATime();
            }
            catch { }
        }

        private void DocKForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you really wan't to close TradeServer??", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                DockManager.SaveDockPanelSetting();
                General.UserForm.SaveUserdetailsDisplayIndex();
                General.tradeServer.CloseAll();

            }
            else
            {
                e.Cancel = true;
            }
        }

        public delegate void delFortradeCount(long tradeCount);
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
        public void UpdateNeatDetails(ConcurrentDictionary<int, UserDtStruct> dicNeatData)
        {

            General.UserForm.UpdateList(dicNeatData);

        }

        private void marginWatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            General.ClientMargin.Show(dockyardPanel);
        }

        private void loggerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            General.LogForm.Show(dockyardPanel);
        }

        private void serverDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            General.ServerForm.Show(dockyardPanel);
        }

        private void clientDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            General.ClientDetailsForm.Show(dockyardPanel);
        }

        private void userDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            General.UserForm.Show(dockyardPanel);
        }
    }
}
