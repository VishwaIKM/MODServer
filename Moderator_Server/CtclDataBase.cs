using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderator_Server
{
    public class CtclDataBase
    {
        ConcurrentDictionary<int, string> dicCtclDB = new ConcurrentDictionary<int, string>();
        string path = "";
        public CtclDataBase(string path)
        {
            this.path = path;
        }
        public void LoadCtclFile()
        {
            try
            {
                if (File.Exists(path))
                {
                    string[] lines = File.ReadAllLines(path);
                    for (int i = 1; i < lines.Length; i++)
                    {
                        string[] data = lines[i].Split(',');
                        string nt = data[2].Replace('"',' ');
                        int neat = Convert.ToInt32(nt);
                        string userName = data[4];
                        if (!dicCtclDB.ContainsKey(neat))
                        {
                            dicCtclDB.TryAdd(neat, userName);
                        }
                        else
                        {
                            dicCtclDB[neat] = userName;
                        }
                    }
                }
                else
                {
                    TradeServer.logger.WriteError("Ctcl DataBase file is not existed");
                }
            }
            catch(Exception ex)
            {
                TradeServer.logger.WriteError("Error while Loading CTCLDB " + ex);
            }
        }

        public string GetUserName(int neat)
        {
            if(dicCtclDB.ContainsKey(neat))
            {
                return dicCtclDB[neat];
            }
            return "Not Available";
        }
    }
}
