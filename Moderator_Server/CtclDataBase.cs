using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderator_Server
{
    public struct CtclDetails
    {
        public string UserName;
        public string CtclID;
        public string LoginID;
    }
    public class CtclDataBase
    {
        ConcurrentDictionary<int, CtclDetails> dicCtclDB = new ConcurrentDictionary<int, CtclDetails>();
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
                        string ctctID= data[3].Replace('"', ' ');
                        string loginID = data[0].Replace('"', ' ');
                        CtclDetails ct = new CtclDetails() { UserName = userName, CtclID = ctctID.Trim(),LoginID=loginID.Trim() };
                        if (!dicCtclDB.ContainsKey(neat))
                        {
                            dicCtclDB.TryAdd(neat, ct);
                        }
                        else
                        {
                            dicCtclDB[neat] = ct;
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
                return dicCtclDB[neat].UserName;
            }
            return "Not Available";
        }
        public string GetCtclID(int neat)
        {
            if (dicCtclDB.ContainsKey(neat))
            {
                return dicCtclDB[neat].CtclID;
            }
            return "Not Available";
        }
        public string GetLoginID(int neat)
        {
            if (dicCtclDB.ContainsKey(neat))
            {
                return dicCtclDB[neat].LoginID;
            }
            return "Not Available";
        }
    }
}
