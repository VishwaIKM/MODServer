using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;

namespace Moderator_Server.Backend
{
    public class Server
    {
        public bool _IsConnected = false;
        public byte[] ReceivingBuffer = null;
        public const int maxBufferSize = 512;
        //TradeWriter tradeWriter;

        public Thread handlerThread;
        private Thread Pinger;
        
        public TcpClient instance;
        public string ipAddress, serverName;
        public int port, userId, passWord,clientVersion;
        public const int LoginResponse = 1001;
        private readonly object lock1 = new object();
        private readonly object lock2 = new object();

        //    public const int NeatIDDetails = 1099;

        public void StopServer()
        {
            if (instance != null && instance.Client != null && instance.Client.Connected)
            {
                instance.Client.Close();
                instance = null;
                _IsConnected = false;
                Program.Gui.updateServerStatus();
                TradeServer.logger.WriteLine(userId + "Logged Out");
            }

            if (handlerThread != null && handlerThread.IsAlive)
            {
                handlerThread.Abort();
                handlerThread = null;
            }
            
            if (Pinger != null && Pinger.IsAlive)
            {
                Pinger.Abort();
                Pinger = null;
            }
        }
        public bool Connect()
        {
            lock (lock1)
            {
                try
                {
                    if (instance == null || instance.Client == null)
                    {
                        instance = new TcpClient();
                        instance.Connect(this.ipAddress, this.port);
                        //  instance.Connect("198.168.1.152", 1998);

                        if (instance.Connected)
                        {
                            HandleBackendResponses();
                            SendLogin();
                            _IsConnected = true;
                            Pinger = new System.Threading.Thread(SendPing);
                            Pinger.Start();

                            // TradeServer.logger.WriteLine(userId + "Logged In");
                        }

                        return instance.Connected;
                    }
                    else
                        return true;
                }
                catch (Exception ex)
                {
                    instance.Client.Dispose();
                    instance = null;
                    // MessageBox.Show("Error in Connecting to " + this.ipAddress + ":" + this.port + ex);
                    return false;
                }
            }

        }
        private void SendPing()
        {
            try
            {
                while (_IsConnected)
                {
                    if (instance != null && instance.Connected)
                    {
                        Ping();
                        Thread.Sleep(3000);
                    }
                }
            }
            catch { }
            
        }
        public bool Connected
        {
            get
            {
                lock (lock2)
                {
                    if (instance != null && instance.Client != null)
                        return instance.Connected;
                    else
                        return false;
                }
            }
        }
        public void HandleBackendResponses()
        {
            if (handlerThread != null)
            {
                handlerThread.Abort();
                handlerThread = null;
            }

            if (instance.Connected)
            {
                ReceivingBuffer = new byte[maxBufferSize];
                handlerThread = new Thread(ReceivingLoop);
                handlerThread.Start();
              
            }
        }
        public bool SendLogin()
        {
            if (instance != null && instance.Connected)
            {
               // LoginRequest request = new LoginRequest() { password = passWord, userId = userId, version = TradeServer.Version };
                NewLoginRequest request = new NewLoginRequest() { password = passWord, userId = userId,version=clientVersion};
                var data = request.GetBytes();
                return this.Send(data, data.Length);
            }
            return false;
        }
        private void Ping()
        {
            try
            {
                Header hdr = new Header();
                hdr.errorCode = 0;
                hdr.MessageLength = 8;
                hdr.transCode = 2010;
                var data = hdr.GetBytes();
                this.Send(data, data.Length);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error at Ping :" + ex);
                
            }

        }
        public bool Send(byte[] data, int length)
        {
            if (instance != null && instance.Connected)
            {
                int bytesend = 0;
                while (bytesend < length)
                {
                    bytesend += instance.Client.Send(data, bytesend, length - bytesend, 0);
                }
                return true;
            }

            return false;
        }
        public  bool Receive(ref byte[] data, int length)
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
            catch
            {
                Program.Gui.updateServerStatus();
                Program.Gui.tradeServer.serverController.UpdateLogedOutNeatID(userId, 0);
                TradeServer.logger.WriteLine(userId + " " + serverName+ "Logged Out");
            }

            if (bytesend == length)
                return true;
            else
            {
                if (instance != null)
                {
                    instance.Close();
                   // TradeServer.logger.WriteLine(userId + " " +serverName + $"  Socket has closed because recieved length is not equal to expected ");
                }
                return false;
            }
        }
        public void ReceivingLoop()
        {
            ReceivingBuffer = new byte[8];
            while(this.Connected)
            {
                try
                {
                    int trans = 0;
                    int len = 0;
                    if (Receive(ref ReceivingBuffer, 8))
                    {
                        short tcode = BitConverter.ToInt16(ReceivingBuffer, 0);
                        trans = (int)tcode;
                        short lens = BitConverter.ToInt16(ReceivingBuffer, 2);
                        len = (int)lens;
                        int err = BitConverter.ToInt32(ReceivingBuffer, 4);
                        
                       // TradeServer.logger.WriteLine("Tcode :" + tcode + "Len :" + len + "Err :" + err);
                      //  Debug.WriteLine(userId + "," + serverName + "," + "Tcode :" + tcode + "Len :" + len + "Err :" + err);
                        
                        ReceivingBuffer = new byte[len - 8];

                        switch (trans)
                        {
                            case LoginResponse:
                                {
                                    if (Receive(ref ReceivingBuffer, 5))
                                    {
                                        string code = Encoding.ASCII.GetString(ReceivingBuffer, 0, 5);
                                        if (err == 100)
                                        {
                                            TradeServer.logger.WriteLine(userId + "Invalid User Id, could not Login");

                                        }
                                        else if (err == 101)
                                        {
                                            TradeServer.logger.WriteLine(userId + "Invalid Password, could not Login");

                                        }
                                        else if (err == 102)
                                        {
                                            TradeServer.logger.WriteLine(userId + "Invalid TransCode, could Not Login");

                                        }
                                        else if(err==98)
                                        {
                                            TradeServer.logger.WriteLine(userId + "Invalid Version, could Not Login");
                                        }
                                        else
                                            TradeServer.logger.WriteLine(userId + "_" + serverName + "Logged In");
                                    }
                                    break;
                                }
                            case Constant.Flag.InhouseTrade:
                                {
                                    if (Receive(ref ReceivingBuffer, 102))
                                    {
                                        Program.Gui.tradeServer.AddTradeToModeratorQueue(ReceivingBuffer);
                                    }
                                    break;
                                }
                            case Constant.Flag.NeatIdDetails:
                                {
                                    if(Receive(ref ReceivingBuffer,4))
                                    {
                                        int NeatId = BitConverter.ToInt32(ReceivingBuffer, 0);
                                        Program.Gui.tradeServer.serverController.UpdateLogedInNeatID(userId, NeatId, serverName);
                                    }
                                    break;
                                }
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
                catch(Exception ex)
                {
                    Debug.WriteLine("Error in ReceiveLoop "+ ex);
                  //  TradeServer.logger.WriteError("Error in Receiving loop "+ex.Message);
                    Program.Gui.updateServerStatus();
                    StopServer();
                    break;
                }
                
            }
            Program.Gui.updateServerStatus();
            Program.Gui.tradeServer.serverController.UpdateLogedOutNeatID(userId, 0);

        }

    }
}
