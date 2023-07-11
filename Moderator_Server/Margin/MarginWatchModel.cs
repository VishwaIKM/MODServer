using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderator_Server.Margin
{
    public class MarginWatchModel
    {
        public string TCODE { get; set; }
        public string TNAME { get; set; }
        public double TotalAllottedAmount { get; set; }
        public double NetMargin { get; set; }
        public bool TradingStatus { get; set; }
        public string Restrictions { get; set; }
    }

    public class MARGIN_WATCH_TABLE
    {
        public string TCODE { get; set; }
        public string TNAME { get; set; }
        public double TotalAllottedAmount { get; set; }

        //DayOfWeek 0=> Disable 1=> Enable
        public int Monday { get; set; }
        public int Tuesday { get; set; }
        public int Wednesday { get; set; }
        public int Thursday { get; set; }
        public int Friday { get; set; }
    }


    public enum EnumWeekdays
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday
    }

    public enum EnumRestriction
    {
        Applicable,
        Not_Applicable
    }


}
