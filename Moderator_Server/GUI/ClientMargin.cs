using Moderator_Server.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Moderator_Server.GUI
{
    public partial class ClientMargin : DockContent
    {
        public ClientMargin()
        {
            InitializeComponent();
          
        }

        private void RestrictionSetting_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(General.marginWatchManager != null) 
                General.marginWatchManager.UpdateRestrictionsDataForALLTrader();
        }
    }
}
