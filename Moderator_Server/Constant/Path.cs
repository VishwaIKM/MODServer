using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moderator_Server.Constant
{
    public class path
    {
        public static string startUpPath = Application.StartupPath;
        public const string TimeFormat = "HH:mm:ss";
        public static readonly string IniPath = startUpPath + "\\Configuration.ini";
        
    }
}
