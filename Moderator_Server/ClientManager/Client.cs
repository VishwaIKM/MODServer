using System;
using System.Threading;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;

namespace Moderator_Server.ClientManager
{
    public class Client
    {
        public const int maxBufferSize = 512;
        public const int headerLength = 8;

        public List<int> NeatIdList = new List<int>();
        public List<int> StrategyIdList = new List<int>();
        Thread clientThread;
        public byte[] ReceivingBuffer;
        //public int clientId;
        public string ClientName;
        public int ClientID;
        TcpClient instance;

        public void SetSocket(TcpClient client)
        {
            if (instance != null)
                instance = null;

            instance = client;
            
        }
        
        public void InitiateReceiveLoop()
        {
            if (clientThread != null)
            {
                clientThread.Abort();
                clientThread = null;
            }

            if (instance.Connected)
            {
                ReceivingBuffer = new byte[maxBufferSize];

                clientThread = new Thread(ReceivingLoop);
                clientThread.Start();
            }
        }
        private bool Receive(ref byte[] data, int length)
        {
            int bytesend = 0;
            try
            {
                data = null;

                if (instance.Connected)
                {
                    data = new byte[length];

                    while (bytesend < length)
                    {
                        int received = instance.Client.Receive(data, bytesend, length - bytesend, 0);
                        if (received == 0)
                            break;
                        bytesend += received;
                    }
                }
            }
            catch(Exception ex)
            {
                Program.Gui.UpdatestatusServercon();
                //OnClientDisconnected();
               // TradeServer.logger.WriteLine(ClientID + " " + ClientName + $"  Disconnected. Reason:{ex.Message} ");
            }

            if (bytesend == length)
                return true;
            else
            {
                if (instance != null)
                {
                    instance.Close();
                }
                return false;
            }
        }
        void ReceivingLoop()
        {
            ReceivingBuffer = new byte[8];
            NeatIdList.Clear();
            StrategyIdList.Clear();
            while (instance.Connected)
            {
                if (Receive(ref ReceivingBuffer, 8))
                {
                    int len = BitConverter.ToInt32(ReceivingBuffer, 0);
                    int tcode = BitConverter.ToInt32(ReceivingBuffer, 4);
                    
                    ReceivingBuffer = new byte[len - 8];
                    switch (tcode)
                    {
                        case Constant.Flag.RmsHedger :
                            Receive(ref ReceivingBuffer, len - 8);
                            string data = Encoding.ASCII.GetString(ReceivingBuffer);
                            FillNeatStrategyList(data);
                            break;

                        case Constant.Flag.HedgerTrdResponse :
                           Receive(ref ReceivingBuffer, len - 8);
                            HedgerTradeResponse resp = new HedgerTradeResponse();
                            resp.GetData(ReceivingBuffer);

                            //long tm = DateTime.Now.Ticks;
                            //DateTime now = DateTime.Now;
                           // DateTime dtss = new DateTime();
                           // dtss = Convert.ToDateTime("1/1/1980 12:00:00 AM");
                            //TimeSpan diffn = (now - dtss);
                           // int secn = (int)diffn.TotalSeconds;


                            TradeMatchResponse match = new TradeMatchResponse { NeatId = resp.neatId, Time = resp.tradeTime, Token = resp.token, TradePrice = resp.tradePrice, TradeQnty = resp.tradeQnty,tradeId=resp.tradeid };
                            Program.Gui.tradeServer.clntManager.SendTradesToClient(Constant.Flag.TradeMatch, match.GetBytes());

                            TradeManagerResponse mngr = new TradeManagerResponse { UserCode = resp.userCode, TradeTime = resp.tradeTime, Token = resp.token, TradePrice = resp.tradePrice, TradeQnty = resp.tradeQnty,Tradeid=resp.tradeid};
                            Program.Gui.tradeServer.clntManager.SendTradesToClient(Constant.Flag.TradeManager, mngr.GetBytes());

                            break;

                        default:
                            break;
                    }
                    ReceivingBuffer = new byte[8];
                }
                else
                {
                    //Error
                    break;
                }
            }
        }
        public void FillNeatStrategyList(string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                if (data.Contains(","))
                {
                    string[] arr = data.Split(',');

                    if (NeatIdList.Count == 0)
                    {
                        for (int id = 0; id < arr.Length; id++)
                        {
                            if (!NeatIdList.Contains(Convert.ToInt32(arr[id])))
                            {
                                NeatIdList.Add(Convert.ToInt32(arr[id]));
                            }
                        }
                        TradeServer.logger.WriteLine("Neat Id Received from Hedger");
                    }
                    else
                    {
                        //for (int id = 0; id < arr.Length; id++)
                        //{
                        //    if (!StrategyIdList.Contains(Convert.ToInt32(arr[id])))
                        //    {
                        //        StrategyIdList.Add(Convert.ToInt32(arr[id]));
                        //    }
                        //}
                        //TradeServer.logger.WriteLine("strategy Id Received from Hedger");
                    }
                }
                else // for single NeatId or single Strategy Code
                {
                    if (NeatIdList.Count == 0)
                    {
                        if (!NeatIdList.Contains(Convert.ToInt32(data)))
                        {
                            NeatIdList.Add(Convert.ToInt32(data));
                        }
                        TradeServer.logger.WriteLine("Neat Id Received from Hedger");
                    }
                    else
                    {
                        //if (!StrategyIdList.Contains(Convert.ToInt32(data)))
                        //{
                        //    StrategyIdList.Add(Convert.ToInt32(data));
                        //}
                        TradeServer.logger.WriteLine("strategy Id Received from Hedger");
                    }
                }
            }
        }
        public bool IsConnected()
        {
            try
            {
                if (instance != null && instance.Client != null)
                {
                    return instance.Connected;
                }
                else
                    return false;
            }
            catch { return false; }
        }
        public bool Reply(byte[] data, int length)
        {
            if (IsConnected())
            {
                try
                {
                    int bytesend = 0;
                    while (bytesend < length)
                    {
                        bytesend += instance.Client.Send(data, bytesend, length - bytesend, 0);
                    }
                    return true;
                }
                catch
                {      
                    OnClientDisconnected();
                    return false;
                }
            }
            else
            {
                //TradeServer.logger.WriteLine("Client Disconnected , Coul Not Send Trades:" +ClientName);
                return false;
            }
        }

        private void OnClientDisconnected(string reason = "NRML")
        {
            Program.Gui.UpdatestatusServercon();
            TradeServer.logger.WriteLine(ClientID + " " + ClientName + $"  Disconnected. Reason:{reason} ");
        }
      
        public void Disconnect(string reason = "NRML")
        {
            try
            {
                if(instance != null && instance.Client!=null)
                {
                    //if (instance.Client != null && instance.Connected)
                    {

                        this.instance.Client.Shutdown(SocketShutdown.Both);
                        instance = null;
                        OnClientDisconnected(reason);
                    }
                }
                if(instance!=null)
                {
                    instance = null;
                }
                
                if (clientThread != null && clientThread.IsAlive)
                {
                    clientThread.Abort();
                    clientThread = null;
                }
                
            }
            catch 
            {
                //this.instance.Client.Shutdown(SocketShutdown.Both);
                instance = null;
                OnClientDisconnected(reason);
            }
        }
    }
}
