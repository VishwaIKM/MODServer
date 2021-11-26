using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Moderator_Server
{
    public class TradeWriter
    {
        private readonly object locker;
        string OutputFilePath = string.Empty;
        FileStream wfl_RiskTrades;
        StreamWriter sw_trades;
        
        public TradeWriter(string FilePath)
        {
            this.OutputFilePath = FilePath;
            locker = new object();
        }

        public bool Start()
        {
            try
            {
                if (!File.Exists(OutputFilePath))
                {
                    wfl_RiskTrades = new FileStream(OutputFilePath, FileMode.Create, FileAccess.Write, FileShare.Read);
                    sw_trades = new StreamWriter(wfl_RiskTrades);
                }
                else
                {
                    wfl_RiskTrades = new FileStream(OutputFilePath, FileMode.Open, FileAccess.Write, FileShare.Read);
                    sw_trades = new StreamWriter(wfl_RiskTrades);
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("[Error][TradeWriter.cs]" + ex.StackTrace.ToString());
            }
            return false;
        }

        public void WriteLine(string line)
        {      
            lock (locker)
            {
                if (sw_trades != null)
                {
                    sw_trades.WriteLine(line);
                    sw_trades.Flush();
                }
            }
        }

        public void Close()
        {
            lock (locker)
            {
                try
                {
                    if (sw_trades != null)
                    {
                        sw_trades.Flush();
                        sw_trades.Close();
                    }
                }
                catch { }
            }
            
        }

    }
}
