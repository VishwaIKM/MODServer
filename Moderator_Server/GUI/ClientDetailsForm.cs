using Moderator_Server.Constant;
using System;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Moderator_Server.GUI
{
    public partial class ClientDetailsForm : DockContent
    {
        public ClientDetailsForm()
        {
            InitializeComponent();
            lvclientdetails.View = View.Details;
            lvclientdetails.Columns.Add("User ID", 100);
            lvclientdetails.Columns.Add("Client", 200);
            lvclientdetails.Columns.Add("Status", 200);
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
                    foreach (int id in General.tradeServer.clntManager.ClientDataBase.Keys)
                    {
                        ListViewItem itm = new ListViewItem(id.ToString().Trim());
                        itm.Name = id.ToString().Trim();
                        itm.SubItems.Add(General.tradeServer.clntManager.ClientDataBase[id].ClientName);
                        itm.SubItems.Add("DISCONNECTED");
                        itm.ForeColor = Color.Red;
                        lvclientdetails.Items.Add(itm);
                    }
                }
                catch (Exception ex) { TradeServer.logger.WriteLine(ex.ToString()); };
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

                    int count = General.tradeServer.clntManager.ClientDataBase.Count;
                    foreach (int id in General.tradeServer.clntManager.ClientDataBase.Keys)
                    {
                        if (lvclientdetails.Items.ContainsKey(id.ToString().Trim()))
                        {

                            string servername = General.tradeServer.clntManager.ClientDataBase[id].ClientName + '(' + id + ')';
                            if (General.tradeServer.clntManager.Checkconnection(id))
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
                catch (Exception ex)
                {
                    TradeServer.logger.WriteLine(ex.ToString());
                }
            }
        }
    }
}
