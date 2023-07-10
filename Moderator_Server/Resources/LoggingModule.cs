using Moderator_Server.Constant;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderator_Server
{
    public interface ILogger
    {
        void WriteLine(string msg);
        void WriteError(string errorMessage);
    }

    public class Logger : ILogger
    {
        BlockingCollection<Param> bc = new BlockingCollection<Param>();
        private StreamWriter logWriter;

        // Constructor create the thread that wait for work on .GetConsumingEnumerable()
        public Logger()
        {
            
        }

        public bool Start(string LogFileDirectory, string FileName)
        {
            if (!Directory.Exists(LogFileDirectory))
                Directory.CreateDirectory(LogFileDirectory);

            logWriter = new StreamWriter(LogFileDirectory + "\\" + FileName, true);
            logWriter.AutoFlush = true;

            Task.Factory.StartNew(() =>
            {
                foreach (Param p in bc.GetConsumingEnumerable())
                {
                    DateTime dateTime = DateTime.Now;

                    switch (p.Ltype)
                    {
                        case Param.LogType.Info:
                            const string LINE_MSG = "[{0}] {1}";
                            logWriter.WriteLine(String.Format(LINE_MSG, dateTime.ToString(Constant.path.TimeFormat), p.Msg));
                            System.Diagnostics.Debug.WriteLine("[Info]" + p.Msg);
                            break;
                        case Param.LogType.Error:
                            const string ERROR_MSG = "[{1}] [Error] {0}";
                            logWriter.WriteLine(String.Format(ERROR_MSG, p.Msg, dateTime.ToString(Constant.path.TimeFormat)));
                            System.Diagnostics.Debug.WriteLine("[Error]" + p.Msg);
                            break;
                        default:
                            logWriter.WriteLine(String.Format(LINE_MSG, dateTime.ToString(Constant.path.TimeFormat), p.Msg));
                            break;
                    }

                    if(p.Msg.Contains("Test:"))
                    {
                        continue;
                    }

                    if(General.LogForm != null)
                        General.LogForm.DisplayLog(dateTime, p.Msg, p.Ltype);
                }

                logWriter.Flush();
                logWriter.Close();
            });

            return true;
        }

        ~Logger()
        {
            // Free the writing thread
            bc.CompleteAdding();
        }

        // Just call this method to log something (it will return quickly because it just queue the work with bc.Add(p))
        public void WriteLine(string msg)
        {
            Param p = new Param(Param.LogType.Info, msg);
            bc.Add(p);
        }

        public void WriteError(string errorMsg)
        {
            Param p = new Param(Param.LogType.Error, errorMsg);
            bc.Add(p);
        }
    }

    public class Param
    {
        public enum LogType { Info, Error};

        public LogType Ltype { get; set; }  // Type of log
        internal string Msg { get; set; }     // Message

        internal Param()
        {
            Ltype = LogType.Info;
            Msg = "";
        }
        internal Param(LogType logType, string logMsg)
        {
            Ltype = logType;
            Msg = logMsg;
        }
    }
}
