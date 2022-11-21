using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderator_Server.Constant
{
    class Flag
    {
        
        public const int InhouseTrade = 100;
        public const int RmsHedger = 501;
        public const int HedgerTrdResponse = 502;
        public const int NeatIdDetails = 1098;

        public const string TradeMatch = "TRADEMATCH";
        public const string TradeManager = "TRADEMANAGER";
        public const string Hedger = "HEDGER";

        public static DateTime DateFrom1980 = new DateTime(1980, 1, 1, 0, 0, 1);
        public static DateTime GetDateFromSeconds(int seconds)
        {
            return DateFrom1980.AddSeconds(seconds);
        }
    }
}
