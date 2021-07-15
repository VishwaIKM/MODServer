﻿using System;
using System.Text;


namespace Moderator_Server
{
    class Header
    {
        public Int16 transCode;
        public Int16 MessageLength;
        public int errorCode;

        public byte[] GetBytes()
        {
            byte[] data = new byte[8];
            BitConverter.GetBytes(this.transCode).CopyTo(data, 0);
            BitConverter.GetBytes(this.MessageLength).CopyTo(data, 2);
            BitConverter.GetBytes(this.errorCode).CopyTo(data, 4);
            return data;
        }

        public void GetValue(byte[] data, int offset)
        {
            if (data.Length < offset + 8)
            {
                throw new Exception("Incomplete Buffer received");
            }
            else
            {
                this.transCode = BitConverter.ToInt16(data, offset);
                this.MessageLength = BitConverter.ToInt16(data, offset + 2);
                this.errorCode = BitConverter.ToInt32(data, offset + 4);
            }
        }
    }
    class LoginRequest
    {
        public int userId;
        public int password;
        public int version;
        public char[] pswd;

        public LoginRequest()
        {
            pswd = new char[8];
        }

        public byte[] GetBytes()
        {
            byte[] data = new byte[24];

            BitConverter.GetBytes((int)1001).CopyTo(data, 0);
            BitConverter.GetBytes((int)24).CopyTo(data, 2);
            BitConverter.GetBytes((int)0).CopyTo(data, 4);
            BitConverter.GetBytes(userId).CopyTo(data, 8);
            BitConverter.GetBytes(version).CopyTo(data, 12);
            Encoding.UTF8.GetBytes(password.ToString().PadRight(8)).CopyTo(data, 16);
            

            return data;
        }

        public void GetValue(byte[] data, int offset)
        {
            if (data.Length < offset + 16)
            {
                throw new Exception("Incomplete Buffer received");
            }
            else
            {
                this.userId = BitConverter.ToInt32(data, offset);
                this.version = BitConverter.ToInt32(data, offset + 4);
                this.pswd = Encoding.UTF8.GetString(data, offset + 8, 8).ToCharArray();
            }
        }
    }
    class LoginResponse
    {
        public Header header;
        int _traderId;

        public int traderId
        {
            get
            {
                return _traderId;
            }
            set
            {
                _traderId = value;
            }
        }


        public LoginResponse(int errorCode)
        {
            this.header = new Header();
            this.header.errorCode = errorCode;
            this.header.transCode = 1001;
            this.header.MessageLength = 12;
            
        }

        public byte[] GetBytes()
        {
            byte[] data = new byte[this.header.MessageLength];
            header.GetBytes().CopyTo(data, 0);
            BitConverter.GetBytes(this.traderId).CopyTo(data, 8);
            return data;
        }
    }
    class HedgerTradeResponse
    {
        public int tradeId;
        public string userCode;
        public int neatId;
        public long ordNo;
        public int trdQnty;
        public float trdPrice;
        public int token;
        public int pfId;
        public int stgType;
        public int pfBuySell;
        public int legNo;
        public int StgId;
        public string ratios;
        public string tokens;
        public long tradeTime;
        public int Expiry;

        public HedgerTradeResponse()
        {
        }
        public void GetData(byte[] data)
        {

            userCode = Encoding.ASCII.GetString(data, 0, 6);
            neatId = BitConverter.ToInt32(data, 6);
            ordNo = BitConverter.ToInt64(data, 10);
            token = BitConverter.ToInt32(data, 18);
            trdQnty = BitConverter.ToInt32(data, 22);
            trdPrice = BitConverter.ToSingle(data, 26);
            tradeTime = BitConverter.ToInt64(data, 30);
            tradeId = BitConverter.ToInt32(data, 38);
            pfId = BitConverter.ToInt32(data, 42);
            stgType = BitConverter.ToInt32(data, 46);
            pfBuySell = BitConverter.ToInt32(data, 50);
            legNo = BitConverter.ToInt32(data, 54);
            StgId = BitConverter.ToInt32(data, 58);
            Expiry = BitConverter.ToInt32(data, 62);
            ratios = Encoding.ASCII.GetString(data, 66, 15);
            tokens = Encoding.ASCII.GetString(data, 81, 25);
        }
    }
    class TradeMatchResponse
    {
        public int NeatId;
        public int Token;
        public int TradeQnty;
        public float TradePrice;
        public Int64 Time;

        public byte[] GetBytes()
        {
            byte[] data = new byte[24];

            BitConverter.GetBytes(this.NeatId).CopyTo(data, 0);
            BitConverter.GetBytes(this.Token).CopyTo(data, 4);
            BitConverter.GetBytes(this.TradeQnty).CopyTo(data, 8);
            BitConverter.GetBytes(this.TradePrice).CopyTo(data, 12);
            BitConverter.GetBytes(this.Time).CopyTo(data, 16);
            return data;
        }
    }
    class TradeManagerResponse
    {
        public char[] ClientCode;
        public int Token;
        public int TradeQnty;
        public float TradePrice;
        public Int64 Time;

        public byte[] GetBytes()
        {
            byte[] data = new byte[26];

            Encoding.UTF8.GetBytes(ClientCode.ToString().PadRight(6)).CopyTo(data, 0);
            BitConverter.GetBytes(this.Token).CopyTo(data, 6);
            BitConverter.GetBytes(this.TradeQnty).CopyTo(data, 10);
            BitConverter.GetBytes(this.TradePrice).CopyTo(data, 14);
            BitConverter.GetBytes(this.Time).CopyTo(data, 18);
            return data;
        }
    }
}
