using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;

namespace Moderator_Server.ClientManager
{
    public class Manager
    {
        TcpListener clientListner;
        Thread clientSessionChecker;
        bool ConnectivityBool;
        private readonly object locker = new object();
        private static readonly object clientLock = new object();
        private Dictionary<int, Client> ClientDataBase = new Dictionary<int, Client>();
        public Manager()
        {
            //clnt = new Client();
            
        }
        public void LoadClientDetails(string filePath)
        {
            lock (clientLock)
            {
                try
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        StreamReader sr = new StreamReader(fs);
                        sr.ReadLine();
                        string line_data = string.Empty;
                        while ((line_data = sr.ReadLine()) != null)
                        {
                            string[] arr_line_data = line_data.Split(',');
                            int user_id = Convert.ToInt32(arr_line_data[0].Trim());

                            if (ClientDataBase.ContainsKey(user_id))
                            {
                                throw new Exception("Client Already exists");
                            }
                            else
                            {
                                Client cli_det = new Client();
                                cli_det.ClientID = user_id;
                                cli_det.ClientName = arr_line_data[1].Trim();
                                ClientDataBase.Add(user_id, cli_det);
                            }
                        }
                        TradeServer.logger.WriteLine("Client Details Loaded");
                    }
                    clientSessionChecker = new System.Threading.Thread(CheckConnection);
                    clientSessionChecker.Start();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error in Loading Client Details :" + ex);
                }
            }
        }

        private void CheckConnection()
        {
            try
            {
                ConnectivityBool = true;
                do
                {
                    Thread.Sleep(5000);

                   // lock (clientLock)
                    {
                        foreach(var client in ClientDataBase.Values)
                        {
                            if (!client.IsConnected())
                            {
                                client.Disconnect();
                            }
                        }
                         
                    }

                    // System.Threading.Thread.Sleep(50000);
                }
                while (ConnectivityBool);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Erro in CheckCnnection :" + ex);
                TradeServer.logger.WriteLine("TradeFetcher Diconnected");
            }

        }
        public void Dispose()
        {
            if (clientSessionChecker != null && clientSessionChecker.IsAlive)
            {
                clientSessionChecker.Abort();
            }
        }
        //public void SendTradeToFetcher(byte[] data)
        //{
        //    try
        //    {  
        //        if (data != null)
        //        {
        //            if(clnt.Reply(data, 119))
        //                TradeServer.logger.WriteLine($"[TradeFetcher RS] Trade Send");
        //            else
        //                TradeServer.logger.WriteLine($"[TradeFetcher RS] Trade Not Send");
        //        }
        //        else
        //        {
        //            //Client has been disconnected
        //            //Client is not connected
        //            TradeServer.logger.WriteLine($"[Client RS][ERROR] Cant send Trade");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine("Error at SendTradeToServer :" + ex);
        //    }
        //}
        public bool StartClienthandling(string ip, int port)
        {
            try
            {
                //int id = Program.Gui.tradeServer.TradeManager;
                System.Net.IPEndPoint iPEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Parse(ip), port);
                clientListner = new TcpListener(iPEndPoint);
                clientListner.Start(5);
                clientListner.BeginAcceptTcpClient(AcceptClientConnection, new object());
                return true;
            }
            catch { return false; }
        }
        private void AcceptClientConnection(IAsyncResult asyncResult)
        {
            var client = clientListner.EndAcceptTcpClient(asyncResult);
            clientListner.BeginAcceptTcpClient(AcceptClientConnection, new object());
            TradeServer.logger.WriteLine("Client Connection Requested");
            HandleClientConnection(client);
            
        }
        private void HandleClientConnection(TcpClient newClient)
        {
            byte[] data = new byte[4];
            int size = 4;
            int received = 0;
            try
            {
                while (received < size)
                {
                    received += newClient.Client.Receive(data, received, size - received, 0);
                }
            }
            catch { }

            ClientDetails detail = new ClientDetails();
            detail.GetValue(data, 0);

            if (ClientDataBase.ContainsKey(detail.userId))
            {
                var client = ClientDataBase[detail.userId];
                client.SetSocket(newClient);
                TradeServer.logger.WriteLine("User Id : " + detail.userId + " " + client.ClientName + " " + "Connected");
                #region
                // client._IsEmptyQueAllowed = false;
                // client.StartClientDequThread();

                //if (client.ClientID == Program.Gui.tradeServer.RMSHedger || client.ClientID == Program.Gui.tradeServer.TradeManager || client.ClientID == Program.Gui.tradeServer.TradeMatch)
                //{
                //    Program.Gui.tradeServer._ISDequAllowed = false;
                //    string path = Program.Gui.tradeServer.GetModeratorPreviousTradesFile();
                //    if (path != null)
                //    {
                //        string tempPath = Constant.path.startUpPath + "_trd.tmp";
                //        File.Copy(path, tempPath, true);
                //        Program.Gui.tradeServer._ISDequAllowed = true;
                //        // Using(StreamReader sr = new StreamReader(tempPath))
                //        using (StreamReader sr = new StreamReader(tempPath))
                //        {
                //           // sr.ReadLine();
                //            if(client.ClientID == Program.Gui.tradeServer.TradeMatch)
                //            {
                //                while (!sr.EndOfStream)
                //                {
                //                    string[] input = sr.ReadLine().Split(',');
                //                    if (input.Length > 7)
                //                    {
                //                        TradeMatchResponse trd = new TradeMatchResponse();
                //                        trd.NeatId = Convert.ToInt32(input[1]);
                //                        trd.Token = Convert.ToInt32(input[3]);
                //                        trd.TradePrice = Convert.ToSingle(input[5]);
                //                        trd.TradeQnty = Convert.ToInt32(input[4]);
                //                        trd.exp = Convert.ToInt64(input[6]);
                //                        byte[] arr = trd.GetBytes();
                //                        client.Reply(arr, arr.Length);
                //                    }
                //                }
                //            }
                //            else if (client.ClientID == Program.Gui.tradeServer.RMSHedger)
                //            {
                //                while (!sr.EndOfStream)
                //                {
                //                    string[] input = sr.ReadLine().Split(',');
                //                    if (input.Length > 13)
                //                    {
                //                        HedgerTradeResponse hdgr = new HedgerTradeResponse();
                //                        hdgr.userCode = input[0];
                //                        hdgr.neatId = Convert.ToInt32(input[1]);
                //                        hdgr.ordNo = Convert.ToInt64(input[2]);
                //                        hdgr.token = Convert.ToInt32(input[3]);
                //                        hdgr.trdQnty = Convert.ToInt32(input[4]);
                //                        hdgr.trdPrice = Convert.ToSingle(input[5]);
                //                        hdgr.exp = Convert.ToInt64(input[6]);
                //                        hdgr.tradeId = Convert.ToInt32(input[7]);
                //                        hdgr.pfId = Convert.ToInt32(input[8]);
                //                        hdgr.stgType = Convert.ToInt32(input[9]);
                //                        hdgr.pfBuySell = Convert.ToInt32(input[10]);
                //                        hdgr.legNo = Convert.ToInt32(input[11]);
                //                        hdgr.ratios = input[12];
                //                        hdgr.tokens = input[13];

                //                        byte[] arr = hdgr.GetBytes();
                //                        client.Reply(arr, arr.Length);
                //                    }
                //                }
                //            }

                //        }
                //        File.Delete(tempPath); 
                //    }
                //    else
                //    {
                //        TradeServer.logger.WriteLine("Previous Moderator TradeFile File Does not exists");
                //    }
                //}
                //if (client.ClientID == Program.Gui.tradeServer.TradeMatch)
                //{
                //    Program.Gui.tradeServer._ISDequAllowed = false;
                //    string path = Program.Gui.tradeServer.GetDropCopyPreviousTradesFile();
                //    if (path != null)
                //    {
                //        string tempPath = Constant.path.startUpPath + "_droptrd.tmp";
                //        File.Copy(path, tempPath, true);
                //        Program.Gui.tradeServer._ISDequAllowed = true;
                //        // Using(StreamReader sr = new StreamReader(tempPath))
                //        using (StreamReader sr = new StreamReader(tempPath))
                //        {
                //            sr.ReadLine();
                //            while (!sr.EndOfStream)
                //            {
                //                string[] input = sr.ReadLine().Split(',');
                //                if (input.Length > 10)
                //                {

                //                }

                //            }
                //        }
                //        File.Delete(tempPath);
                //    }
                //    else
                //    {
                //        TradeServer.logger.WriteLine("Previous Drop Copy File Does not exists");
                //    }
                //}

                //client._IsEmptyQueAllowed = true;
                //Program.Gui.tradeServer._ISDequAllowed = true;
                #endregion

                client.InitiateReceiveLoop();
                //HedgerTradeResponse respn = new HedgerTradeResponse { exp = 1309617000, legNo = 2, neatId = 34561, ordNo = 1987642, pfBuySell = 1, pfId = 5, ratios = "1:2:1:2", StgId = 12, stgType = 1, token = 35000, tokens = "35000:56771:0:0", tradeId = 1234987, trdPrice = 23.5F, trdQnty = 75, userCode = "Ad007" };
                //SendTradesToClient(5001, respn.GetBytes());
            }
            else
            {
                TradeServer.logger.WriteLine("ClientId :" + detail.userId + "Did not Match with DataBase");
            }
        }
        public void StopClient()
        {
            ConnectivityBool = false;
            foreach (var client in ClientDataBase.Values)
            {
                if (client.IsConnected())
                {
                    client.Disconnect();
                }
            }
        }
        public void SendTradesToClient(string clientName, byte[] data)
        {
            lock(locker)
            {
                foreach (int clId in ClientDataBase.Keys)
                {
                    if (ClientDataBase[clId].ClientName.Contains(clientName))
                    {
                        ClientDataBase[clId].Reply(data, data.Length);
                    }
                }
            }
        }
        public void SendTradesToRmsHedger(string clientName, int neat, int stgCode, byte[] data)
        {
            foreach (int clId in ClientDataBase.Keys)
            {
                if (ClientDataBase[clId].ClientName.Contains(clientName))
                {
                    var detail = ClientDataBase[clId];
                    if(detail.NeatIdList.Contains(neat) && detail.StrategyIdList.Contains(stgCode))
                        ClientDataBase[clId].Reply(data, data.Length);
                }
            }
        }
    }
}
