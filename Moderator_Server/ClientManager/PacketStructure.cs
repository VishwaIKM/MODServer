using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderator_Server.ClientManager
{
    class ClientDetails
    {
        public int userId;// 4bytes
       // public char[] clientName;// 

        public void GetValue(byte[] data, int offset)
        {
            this.userId = BitConverter.ToInt32(data, offset);
           // this.clientName = Encoding.UTF8.GetString(data, 4, 15).ToCharArray();
        }
    }

    class HedgerTradeResponse
    {
        public int neatId;
        public string userCode;
        public int token;
        public int tradeQnty;
        public float tradePrice;

        public void GetData(byte[] data)
        {
            try
            { 
                neatId = BitConverter.ToInt32(data, 0);
                userCode = Encoding.UTF8.GetString(data, 4, 6).ToString();
                token = BitConverter.ToInt32(data, 10);
                tradeQnty = BitConverter.ToInt32(data, 14);
                tradePrice = BitConverter.ToSingle(data, 18);
            }
            catch
            {
                
            }
        }
    }
}
