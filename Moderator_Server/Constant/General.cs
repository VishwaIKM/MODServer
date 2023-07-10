using Moderator_Server.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderator_Server.Constant
{
    internal class General
    {
        #region Ver& Prop


        // Static GUI Filed
        public static LogForm LogForm = new LogForm();
        public static ClientDetailsForm ClientDetailsForm = new ClientDetailsForm();
        public static ServerForm ServerForm = new ServerForm();
        public static UserForm UserForm = new UserForm();
        public static ClientMargin ClientMargin = new ClientMargin();
        public static DocKForm DockForm;

        public static TradeServer tradeServer = new TradeServer();
        #endregion
    }
}
