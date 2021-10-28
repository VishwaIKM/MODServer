using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Concurrent;
using System.Threading;
using System.Collections.Generic;

namespace Moderator_Server
{
    public class TradeServer
    {
        //Queue<Trade> for moderators   << match_Engine, hedger, Trade_Manager <Queue_match>
        //Queue<Trade> for dropcopy     << match_Engine,
        //Instances

        ConcurrentQueue<byte[]> ModeratorTardeQueue = new ConcurrentQueue<byte[]>();
        List<int> TradeIdList = new List<int>();

        private Thread DequeuThreadMod;
        bool dequBool = true;
        public static Logger logger;
        public Contract contract;
        long ModTradeCount = 0;
        private readonly object portfolioMainSync = new object();
        public int Port { get; set; }
        public string Interface { get; set; }
        public string OutPath { get; set; }
        public string ModeratorTradeFile { get; set; }
        public string ContractPath { get; set; }
        
        public TradeWriter ModeratorTradeWriter;

        public Backend.ServerController serverController;
        public ClientManager.Manager clntManager;

        public void LoadTradeServer()
        {   
            string LogPath = Constant.path.startUpPath + "\\Logs";
            DateTime dttm = DateTime.Today;
            string DayFolder = LogPath + "\\" + dttm.Year.ToString("0000") + dttm.Month.ToString("00") + dttm.Day.ToString("00");

            if (!Directory.Exists(LogPath))
                Directory.CreateDirectory(LogPath);
            if (!Directory.Exists(DayFolder))
            {
                Directory.CreateDirectory(DayFolder);
            }
            logger = new Logger();
            if (!logger.Start(DayFolder, "LogFile.txt"))
            {
                MessageBox.Show("Error in initializing logging Module");
                Environment.Exit(0);
            }
            else
            {
                logger.WriteLine(DateTime.Now.ToString("ddMMMyyyy hh:mm:ss"));
                logger.WriteLine("Logging started.");
            }
            LoadTradeServerdetails();
            contract = new Contract();
            string pathcntr = ContractPath + "\\contract.txt";
            if (!contract.CheckContractFile(pathcntr))
            {
                MessageBox.Show("Update Contract.txt");
                Environment.Exit(0);
            }
            contract.LoadTokenDeatils(pathcntr);


            serverController = new Backend.ServerController();
            string path = Constant.path.startUpPath + "\\ModeratorDetail.txt";
            string clnPath = Constant.path.startUpPath + "\\UserDb.csv";
            serverController.LoadServerDetails(path);
            clntManager = new ClientManager.Manager();
            clntManager.LoadClientDetails(clnPath);
            ModeratorTradeFile = OutPath + $"Out_{DateTime.Now.ToString("d_M")}.txt";
            ModeratorTradeWriter = new TradeWriter(ModeratorTradeFile);
            ModeratorTradeWriter.Start();

            if (DequeuThreadMod != null && DequeuThreadMod.IsAlive)
            {
                DequeuThreadMod.Abort();
                DequeuThreadMod = new Thread(EmptyModeratorQueue);
                DequeuThreadMod.Start();
            }
            else
            {
                DequeuThreadMod = new Thread(EmptyModeratorQueue);
                DequeuThreadMod.Start();
            }
            
            clntManager.StartClienthandling(Interface, Port);
             
        }
        public void LoadTradeServerdetails()
        {
            if (File.Exists(Constant.path.IniPath))
            {
                Ini ini = new Ini(Constant.path.IniPath);
                Interface = ini.Read("TRADESERVER", "IP");
                Port = ini.Read_int("TRADESERVER", "PORT");
                OutPath = ini.Read("TRADESERVER", "OUTPATH");
                ContractPath = ini.Read("TRADESERVER", "CONTRACT");

                if (!Directory.Exists(OutPath))
                    TradeServer.logger.WriteError("OutPath does not exists");
                logger.WriteLine("Moderator Server details Loaded");
            }
            else
            {
                TradeServer.logger.WriteLine("Config file des not exists");
            }
        }
        
        public override string ToString()
        {
            return string.Format("IP {0}\nPort {1}", this.Interface, this.Port);
        }
        public void CloseAll()
        {
            serverController.DisconnectAllServer();
            dequBool = false;
            if (DequeuThreadMod != null && DequeuThreadMod.IsAlive)
            {
                DequeuThreadMod.Abort();
                DequeuThreadMod = null;
            }
            
            clntManager.StopClient();
            clntManager.Dispose();
            if (ModeratorTradeWriter != null)
                ModeratorTradeWriter.Close();
        }
        public void AddTradeToModeratorQueue(byte[] data)
        {
            // Need Locking as multiple threads will add request to queue
            lock(portfolioMainSync)
            {
                if (data != null)
                    ModeratorTardeQueue.Enqueue(data);
            }
        }
        private void EmptyModeratorQueue()
        {
            try
            {
                while (dequBool)
                {
                    if (ModeratorTardeQueue.Count > 0)
                    {
                        byte[] data = null;
                        if (ModeratorTardeQueue.TryDequeue(out data))
                        {
                            if (data != null)
                            {
                                HedgerTradeResponse resp = new HedgerTradeResponse();
                                resp.GetData(data);
                                

                                if (!TradeIdList.Contains(resp.tradeId))
                                {
                                    TradeIdList.Add(resp.tradeId);

                                    byte[] HedgerData = new byte[110];
                                    BitConverter.GetBytes(110).CopyTo(HedgerData, 0);
                                    BitConverter.GetBytes(95).CopyTo(HedgerData, 4);
                                    data.CopyTo(HedgerData, 8);


                                    clntManager.SendTradesToRmsHedger(Constant.Flag.Hedger, resp.neatId, resp.stgType, HedgerData);

                                    TradeMatchResponse matchResp = new TradeMatchResponse { Time = resp.tradeTime, NeatId = resp.neatId, Token = resp.token, TradeQnty = resp.trdQnty, TradePrice = resp.trdPrice, tradeId = resp.tradeId };

                                    clntManager.SendTradesToClient(Constant.Flag.TradeMatch, matchResp.GetBytes());

                                    TradeManagerResponse mngr = new TradeManagerResponse
                                    { TradeTime = resp.tradeTime, UserCode = resp.userCode, Token = resp.token, TradePrice = resp.trdPrice, TradeQnty = resp.trdQnty,Tradeid=resp.tradeId};

                                    clntManager.SendTradesToClient(Constant.Flag.TradeManager, mngr.GetBytes());

                                    DateTime dt = Constant.Flag.GetDateFromSeconds(resp.Expiry);
                                    DateTime tradeTime = Constant.Flag.GetDateFromSeconds(resp.tradeTime);

                                    string script = "";
                                    string instrument = "";
                                    string option = "";
                                    string expr = "";
                                    int strike = 0;
                                    if (resp.token > 0)
                                    {
                                        var info = contract.GetTokenDetails(resp.token);
                                        {
                                            if (info != null)
                                            {
                                                script = info.script;
                                                instrument = info.instrument.ToString();
                                                option = info.option.ToString();
                                                DateTime dts = Constant.Flag.GetDateFromSeconds(info.exp);
                                                expr = dt.ToString("dd MMM yyyy").ToUpper();
                                                strike = info.strike;

                                            }
                                            else
                                            {
                                                logger.WriteLine("Token Detail Not Found in Contract file :" + resp.token);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        logger.WriteLine("Token : " + resp.token);
                                    }

                                    //string tradeLog = $"{resp.tradeId},{resp.userCode.Trim()},{resp.neatId},{resp.ordNo.ToString()},{resp.token},{resp.trdQnty},{(resp.trdPrice).ToString("0.00")},{tradeTime.ToString("yyyy-MM-dd HH:mm:ss")},{ dt.ToString("dd MMM yyyy").ToUpper()},{resp.pfId},{resp.stgType},{resp.pfBuySell},{resp.legNo},{resp.StgId},{resp.ratios.Trim()},{resp.tokens.Trim()}";

                                    string tradeLog = $"{resp.tradeId},{resp.userCode.Trim()},{resp.neatId},{resp.ordNo.ToString()},{script},{instrument},{(strike / 100.0).ToString()},{option},{expr},{resp.trdQnty},{(resp.trdPrice).ToString("0.00")},{Constant.Flag.GetDateFromSeconds(resp.tradeTime).ToString("yyyy-MM-dd HH:mm:ss")},{resp.tradeId},{resp.token},{resp.StgId},{resp.pfId},{resp.stgType},{resp.pfBuySell},{resp.legNo},{resp.ratios.Trim()},{resp.tokens.Trim()}";


                                    ModTradeCount++;
                                    Program.Gui.tradeServer.ModeratorTradeWriter.WriteLine(tradeLog);
                                    //if(ModTradeCount % 50 == 0)
                                    Program.Gui.UpdateTradeCount(ModTradeCount);

                                }
                            }
                        }
                    }
                    else
                        Thread.Sleep(100);
                }
            }
            catch { }
        }
    }
}
