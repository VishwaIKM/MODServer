using System;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;


namespace Moderator_Server.GUI
{
    public partial class LogForm : DockContent
    {
        public LogForm()
        {
            InitializeComponent();

            lvLogs.View = View.Details;
            lvLogs.Dock = DockStyle.Fill;
            lvLogs.Columns.Add("Time", 110);
            lvLogs.Columns.Add("Type", 110);
            lvLogs.Columns.Add("Message", 1100);
        }
        public delegate void delWithString(DateTime stamp, string param1, Param.LogType param2);
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
            catch (Exception ex) { TradeServer.logger.WriteLine(ex.ToString()); }
        }
    }
}
