using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moderator_Server
{
    static class Program
    {
        public static MainForm Gui;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            string DebugFilePath = Application.StartupPath + "\\DebugFile\\";
            if(!Directory.Exists(DebugFilePath))
            {
                Directory.CreateDirectory(DebugFilePath);
            }
            TraceListener listener = new DelimitedListTraceListener(DebugFilePath+$"\\debugfile_{DateTime.Now.ToString("ddMMMyyyy")}.txt");
            Debug.Listeners.Add(listener);
            Debug.WriteLine("***********************DEBUG*************************");
            Debug.AutoFlush = true;

            try
            {
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomainUnhandledException);
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(ApplicationThreadException);

                string appGuid = GetAppguid();

                using (Mutex mutex = new Mutex(false, "Global\\" + appGuid))
                {
                    if (!mutex.WaitOne(0, false))
                    {
                        MessageBox.Show("Instance already running");
                    }
                    else
                    {
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new MainForm());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while Loading Application :  " + ex);
            }
            Debug.WriteLine("***********************End*************************");
            Debug.Flush();
            Debug.Close();
        }

        public static void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.ExceptionObject;

                MessageBox.Show("Whoops! Please contact the developers with the following"
                    + " information:\n\n" + ex.Message + ex.StackTrace,
                    "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                Application.Exit();
            }
        }

        public static void ApplicationThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            DialogResult result = DialogResult.Abort;
            try
            {
                result = MessageBox.Show("Whoops! Please contact the developers with the"
                    + " following information:\n\n" + e.Exception.Message + e.Exception.StackTrace,
                    "Application Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
            }
            finally
            {
                if (result == DialogResult.Abort)
                {
                    Application.Exit();
                }
            }
        }

        private static string GetAppguid()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            var attribute = (GuidAttribute)assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0];
            string appGuid = attribute.Value.ToString();
            return appGuid;
        }

        public static string GetAssamblyInfo()
        {
            string info = "NeatReplica Unknown";

            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                info = assembly.GetName().Name;
                var version = assembly.GetName().Version;

                var title = assembly
                        .GetCustomAttributes(typeof(AssemblyTitleAttribute), false)
                        .Cast<AssemblyTitleAttribute>().FirstOrDefault();
                if (title != null)
                {
                    info = string.Format("{0} Ver-{1}.{2}.{3}", title.Title, version.Major, version.Minor, version.Build);
                }
            }
            catch { }
            return info;
        }
    }
}
