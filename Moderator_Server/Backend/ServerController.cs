using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Concurrent;
using System.Data.Common;
using System.Threading;

namespace Moderator_Server.Backend
{
    public struct UserDtStruct
    {
        public int UserID;
        public int NeatID;
        public string UserName;
        public Boolean Status;
        public DateTime dateTime;
        public string ServerName;
        public string CtclID;
        public string LoginID;
        public DateTime LastTradedTime;
    }
    public class ServerController
    {
        private ConcurrentDictionary<int, Server> Servers;
        public ConcurrentDictionary<int, UserDtStruct> dicNeatIDDetails = new ConcurrentDictionary<int, UserDtStruct>();
        public ConcurrentDictionary<int, DateTime> dicNeatIDLTT = new ConcurrentDictionary<int, DateTime>();

        public bool ReConnect = true;

        public Thread thReconnect;

        private readonly object lock1 = new object();
        public ServerController()
        {
            Servers = new ConcurrentDictionary<int, Server>();
        }

        public void LoadServerDetails(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    StreamReader sr = new StreamReader(fs);
                    sr.ReadLine();
                    while (sr.Peek() > 0)
                    {   
                        string line = sr.ReadLine();
                        if (!string.IsNullOrEmpty(line))
                        {
                            string[] arr = line.Split(',');
                            int userId = Convert.ToInt32(arr[0]);
                            string mod = arr[1];
                            string ipPort = arr[2];
                            int passWord = Convert.ToInt32(arr[3]);
                            int clientVersion;
                            if (arr.Length >= 5)
                            {
                                clientVersion = Convert.ToInt32(arr[4]);
                            }
                            else
                            {
                                clientVersion=0;
                            }
                            string[] ln = arr[2].Split(':');
                            string ip = ln[0];
                            int port = Convert.ToInt32(ln[1]);
                            if (!Servers.ContainsKey(userId))
                            {
                                Server srvr = new Server() { ipAddress = ip, port = port, userId = userId, serverName = mod, passWord = passWord,clientVersion=clientVersion };
                                this.Servers.TryAdd(userId, srvr);
                            }
                        }
                    }
                    TradeServer.logger.WriteLine("Moderator Detils Loaded");
                    sr.Close();
                    fs.Close();
                    if (thReconnect == null || !thReconnect.IsAlive)
                    {
                        thReconnect = new Thread(ConnectToModerator);
                        thReconnect.Start();
                    }

                }
                else
                {
                    TradeServer.logger.WriteLine("Moderator Detail File does not exists");
                }
            }
            catch (Exception ex)
            {
                TradeServer.logger.WriteLine("Error in Loading Server Detail :" + ex);

            }
               
        }

        public void ConnectToModerator()
        {
            while(ReConnect)
            {
                try
                {
                    bool isRequiedToUpdateGui = false;
                    foreach (int userId in Servers.Keys.ToArray())
                    {
                        var server = Servers[userId];
                        if (!server.Connected)
                        {
                            if (server.Connect())
                            {
                                isRequiedToUpdateGui = true;
                            }
                        }
                    }
                    if (isRequiedToUpdateGui)
                    {
                        Program.Gui.updateServerStatus();
                    }
                    Thread.Sleep(3000);
                }
                catch(Exception ex)
                {
                    TradeServer.logger.WriteError(ex.ToString());
                }
            }
        }
        public bool ConnectAll()
        {
            foreach (var server in this.Servers.Keys)
            {
                ConnectToBackend(server);
            }
            return true;
        }

        public bool ConnectToBackend(int serverId)
        {
            if (this.Servers.ContainsKey(serverId))
            {
                //if(Servers[serverId].DropCopy)
                //{
                //    return this.Servers[serverId].ConnectToDropCopyServer();
                //}
                //else
                    return this.Servers[serverId].Connect();
            }
            return false;
        }

        public void DisconnectAllServer()
        {
            try
            {
                ReConnect = false;
                if (thReconnect != null && thReconnect.IsAlive)
                {
                    thReconnect.Abort();
                }
            }
            catch { }
            foreach (var id in this.Servers.Keys)
            {
                DisconnectServer(id);
            }
        }

        public void DisconnectServer(int serverId)
        {
            if (this.Servers.ContainsKey(serverId))
            {
                this.Servers[serverId].StopServer();
            }
        }

        public bool Connected(int ServerID)
        {
            if (Servers.ContainsKey(ServerID))
            {
                return Servers[ServerID].Connected;
            }
            else
            {
                TradeServer.logger.WriteLine("Server Id did not Match With strategy");
                return false;
            }
        }

        public void UpdateLogedInNeatID(int useriD,int neatID,string serverName)
        {
            lock (lock1)
            {
                UserDtStruct userDt = new UserDtStruct() { NeatID = neatID, UserID = useriD, Status = true,UserName=Program.Gui.tradeServer.ctclDataBase.GetUserName(neatID),dateTime=DateTime.Now,ServerName=serverName,CtclID= Program.Gui.tradeServer.ctclDataBase.GetCtclID(neatID),LoginID= Program.Gui.tradeServer.ctclDataBase.GetLoginID(neatID) };
                if (!dicNeatIDDetails.ContainsKey(neatID))
                {
                    dicNeatIDDetails.TryAdd(neatID, userDt);
                }
                else
                {
                    dicNeatIDDetails[neatID] = userDt;
                }
                Program.Gui.UpdateNeatDetails(dicNeatIDDetails);
            }
        }
        public void UpdateNeatLastTradedTime(int neatID,DateTime LTT)
        {
            try
            {
                if (dicNeatIDDetails.ContainsKey(neatID))
                {
                    if (!dicNeatIDLTT.ContainsKey(neatID))
                    {
                        dicNeatIDLTT.TryAdd(neatID, LTT);
                    }
                    else
                    {
                        dicNeatIDLTT[neatID] = LTT;
                    }
                }
            }
            catch { }
        }
        public void UpdateNeatLTTAfterATime()
        {
            lock (lock1)
            {
                try
                {
                    foreach (int neatID in dicNeatIDLTT.Keys.ToArray())
                    {
                        var data = dicNeatIDDetails[(int)neatID];
                        data.LastTradedTime = dicNeatIDLTT[neatID];
                        dicNeatIDDetails[neatID] = data;
                    }
                    Program.Gui.UpdateNeatDetails(dicNeatIDDetails);
                }
                catch { }
            }
        }

        public void UpdateLogedOutNeatID(int userID,int neatID)
        {
            lock (lock1)
            {
                foreach (int neat in dicNeatIDDetails.Keys.ToArray())
                {
                    var data = dicNeatIDDetails[neat];
                    if (userID == data.UserID)
                    {
                        data.Status = false;
                        dicNeatIDDetails[neat] = data;
                    }
                }
                Program.Gui.UpdateNeatDetails(dicNeatIDDetails);
            }
        }
    }
}
