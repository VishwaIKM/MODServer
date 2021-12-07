using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Moderator_Server.Backend
{
    public class ServerController
    {
        private Dictionary<int, Server> Servers;

        public ServerController()
        {
            Servers = new Dictionary<int, Server>();
        }

        public void LoadServerDetails(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    StreamReader sr = new StreamReader(fs);

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
                            string[] ln = arr[2].Split(':');
                            string ip = ln[0];
                            int port = Convert.ToInt32(ln[1]);
                            if (!Servers.ContainsKey(userId))
                            {
                                Server srvr = new Server() { ipAddress = ip, port = port, userId = userId, serverName = mod, passWord = passWord };
                                this.Servers.Add(userId, srvr);
                            }
                        }
                    }
                    TradeServer.logger.WriteLine("Moderator Detils Loaded");
                    sr.Close();
                    fs.Close();

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
    }
}
