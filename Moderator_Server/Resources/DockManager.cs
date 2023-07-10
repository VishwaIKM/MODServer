using Moderator_Server.Constant;
using Moderator_Server.GUI;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Moderator_Server.Resources
{
    internal class DockManager
    {
        #region Fields
        private static DeserializeDockContent m_deserializeDockContent;
        static string DockPath = path.startUpPath + "\\DockPanel.config";
        private delegate void Dockyarddel();
        #endregion


        /// <summary>
        /// IDOCK INFORMATION ABOUT DOCK FORM
        /// </summary>
        /// <param name="persistString"></param>
        /// <returns></returns>
        private static IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(LogForm).ToString())
                return General.LogForm;
            else if (persistString == typeof(ClientDetailsForm).ToString())
                return General.ClientDetailsForm;
            else if (persistString == typeof(ServerForm).ToString())
                return General.ServerForm;
            else if (persistString == typeof(UserForm).ToString())
                return General.UserForm;
            else if (persistString == typeof(ClientMargin).ToString())
                return General.ClientMargin;
            else
            {
                return null;
            }
        }

        internal static void InitialDockFormLoad()
        {
            General.DockForm.WindowState = FormWindowState.Maximized;
            m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);

            if (File.Exists(DockPath))
            {
                try
                {
                    if (General.DockForm.InvokeRequired)
                    {
                        Dockyarddel d = new Dockyarddel(InitialDockFormLoad);
                        General.DockForm.Invoke(d);
                    }
                    else
                    {
                        General.DockForm.dockyardPanel.LoadFromXml(DockPath, m_deserializeDockContent);
                    }
                }
                catch (Exception ex) { TradeServer.logger.WriteLine(ex.ToString()); }
            }

            ShowOtherWindows();
        }

        /// <summary>
        /// docked window display
        /// </summary>
        private static void ShowOtherWindows()
        {
            try
            {
                if (General.DockForm.InvokeRequired)
                {
                    Dockyarddel d = new Dockyarddel(ShowOtherWindows);
                    General.DockForm.Invoke(d);
                }
                else
                {
                    General.LogForm.Show(General.DockForm.dockyardPanel);
                    General.ClientDetailsForm.Show(General.DockForm.dockyardPanel);
                    General.ServerForm.Show(General.DockForm.dockyardPanel);
                    General.UserForm.Show(General.DockForm.dockyardPanel);
                    General.ClientMargin.Show(General.DockForm.dockyardPanel);
                }
            }
            catch (Exception ex)
            {
                TradeServer.logger.WriteLine(ex.ToString());
            }

        }

        public static void SaveDockPanelSetting()
        {
            General.DockForm.dockyardPanel.SaveAsXml(DockPath);
        }
    }
}
