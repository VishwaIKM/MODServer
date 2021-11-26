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
        public int tradeTime;
        public int tradeid;

        public void GetData(byte[] data)
        {
            try
            { 
                neatId = BitConverter.ToInt32(data, 0);
                userCode = Encoding.UTF8.GetString(data, 4, 6).ToString();
                token = BitConverter.ToInt32(data, 10);
                tradeQnty = BitConverter.ToInt32(data, 14);
                tradePrice = BitConverter.ToSingle(data, 18);
                tradeTime = BitConverter.ToInt32(data, 22);
                tradeid = BitConverter.ToInt32(data, 26);
            }
            catch
            {
                
            }
        }
    }

    public class HedgerPreviousTrades
    {
        public int len = 110;
        public int trnsCode = 95;
        public int tradeId;
        public string userCode;
        public int neatId;
        public long ordNo;
        public int trdQnty;
        public float trdPrice;
        public int token;
        public int pfId;
        public int stgType;
        public int stgId;
        public int pfBuySell;
        public int legNo;
        public string ratios;
        public string tokens;
        public int UserId;
        public int tradeTime;
        public int Expiry;

        public HedgerPreviousTrades()
        {
            
        }
        public byte[] GetBytes()
        {
            byte[] data = new byte[110];

            BitConverter.GetBytes(len).CopyTo(data, 0);
            BitConverter.GetBytes(trnsCode).CopyTo(data, 4);
            Encoding.UTF8.GetBytes(userCode.ToString().PadRight(6)).CopyTo(data, 8);
            BitConverter.GetBytes(neatId).CopyTo(data, 14);
            BitConverter.GetBytes(ordNo).CopyTo(data, 18);
            BitConverter.GetBytes(token).CopyTo(data, 26);
            BitConverter.GetBytes(trdQnty).CopyTo(data, 30);
            BitConverter.GetBytes(trdPrice).CopyTo(data, 34);
            BitConverter.GetBytes(tradeTime).CopyTo(data, 38);
            BitConverter.GetBytes(tradeId).CopyTo(data, 42);
            BitConverter.GetBytes(pfId).CopyTo(data, 46);
            BitConverter.GetBytes(stgType).CopyTo(data, 50);
            BitConverter.GetBytes(pfBuySell).CopyTo(data, 54);
            BitConverter.GetBytes(legNo).CopyTo(data, 58);
            BitConverter.GetBytes(stgId).CopyTo(data, 62);
            BitConverter.GetBytes(Expiry).CopyTo(data, 66);
            Encoding.UTF8.GetBytes(ratios.ToString().PadRight(15)).CopyTo(data, 70);
            Encoding.UTF8.GetBytes(tokens.ToString().PadRight(25)).CopyTo(data, 85);
            return data;

        }

    }
}
