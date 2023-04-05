using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moderator_Server
{
    public class ContractInfo
    {
        public uint lotSize;
        public string script, tradingSymbol;
        public int exp, lastClosingPrice, Position, UnderLyingToken, token;
        public Int16 streamId;
        public int strike, freezeQty, tickSize;
        public Constant.OptionType option; // strore the index of option type for given contract
        public Constant.InstrumentType instrument;

    }
    public class Contract
    {
        Dictionary<int, ContractInfo> SecurityDataBase = new Dictionary<int, ContractInfo>();
        private int _niftyFutToken;

        public bool CheckContractFile(string path)
        {
            try
            {
                if (!File.Exists(path))
                    return false;

                bool ContractOK = true;

                StreamReader rfl = new StreamReader(path);
                string row = rfl.ReadLine();
                DateTime dt = new DateTime();

                Dictionary<string, List<DateTime>> DcExpiry = new Dictionary<string, List<DateTime>>();

                while (!rfl.EndOfStream)
                {
                    row = rfl.ReadLine();
                    string[] cols = row.Split(',');
                    if (cols[3] == "NIFTY")
                    {
                        dt = Convert.ToDateTime("1/1/1980 12:00:00 AM");//.Add(diff);
                        dt = dt.AddSeconds(Convert.ToInt32(cols[4]));//cols[28]));//

                        if (!DcExpiry.ContainsKey(cols[3]))
                        {
                            List<DateTime> lstExpiry = new List<DateTime>();

                            DateTime expdt = dt;
                            lstExpiry.Add(expdt);
                            DcExpiry.Add(cols[3], lstExpiry);
                        }
                        else
                        {
                            bool ExpExist = false;
                            List<DateTime> lstTmp = DcExpiry[cols[3]];
                            foreach (DateTime ldt in lstTmp)
                            {
                                if (ldt == dt)
                                {
                                    ExpExist = true;
                                }
                            }

                            if (ExpExist == false)
                            {
                                DateTime expdt = dt;
                                lstTmp.Add(expdt);
                                lstTmp.Sort();
                                DcExpiry[cols[3]] = lstTmp;
                            }
                        }
                    }
                }
                rfl.Close();

                foreach (string key in DcExpiry.Keys)
                {
                    if (DcExpiry[key][0] < DateTime.Today)
                    {
                        ContractOK = false;
                        break;
                    }
                }
                return ContractOK;
            }
            catch
            {
                MessageBox.Show("Error in Checking Contract File in Contarct Manager");
                return false;
            }

        }
        public void LoadTokenDeatils(string contractPath)
        {
            try
            {
                if (File.Exists(contractPath))
                {
                    using (FileStream fs = new FileStream(contractPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        StreamReader sr = new StreamReader(fs);
                        SecurityDataBase = new Dictionary<int, ContractInfo>();
                        sr.ReadLine();
                        string line;

                        string niftyFut = string.Format("NIFTY{0}FUT", DateTime.Now.ToString("yyMMM").ToUpper());
                        string bankNiftyFut = "BANK" + niftyFut;
                        string relianceFut = string.Format("RELIANCE{0}FUT", DateTime.Now.ToString("yyMMM").ToUpper());


                        while ((line = sr.ReadLine()) != null)
                        {
                            DateTime dt = new DateTime();
                            string exp = "";
                            string[] arrline = line.Split(',');

                            if (string.IsNullOrEmpty(arrline[18]) || string.IsNullOrWhiteSpace(arrline[18]))
                                continue;

                            int token = Convert.ToInt32(arrline[0].Trim());
                            if (token < 1)
                            {
                                continue;
                            }
                            string script = arrline[3].Trim();
                            dt = Convert.ToDateTime("1/1/1980 12:00:00 AM");//.Add(diff);
                            dt = dt.AddSeconds(Convert.ToInt32(arrline[4]));//cols[28]));//
                            exp = dt.ToString("dd-MMM-yy").ToUpper();
                            string expr = dt.ToString("yyMMM").ToUpper();
                            //string exprs = dt.ToString("ddMMMyyyy").ToUpper();//
                            //string ex = script + "_" + dt.ToString("ddMMMyyyy");
                            ContractInfo cntrInfo = new ContractInfo();


                            int tok = token;

                            int strike = Convert.ToInt32(arrline[5].Trim());
                            uint lotSize = Convert.ToUInt32(arrline[8].Trim());

                            int freezeQnty = (int)Convert.ToDouble(arrline[40]);
                            string tradingSymbol = arrline[18];

                            if (niftyFut == tradingSymbol)
                                _niftyFutToken = token;
                            //if (bankNiftyFut == tradingSymbol)
                            //    BankNiftyFutToken = token;

                            Constant.OptionType otp = 0;
                            if (arrline[6].Trim() == Constant.OptionType.CE.ToString())
                                otp = Constant.OptionType.CE;
                            if (arrline[6].Trim() == Constant.OptionType.PE.ToString())
                                otp = Constant.OptionType.PE;
                            if (arrline[6].Trim() == Constant.OptionType.XX.ToString())
                            {
                                otp = Constant.OptionType.XX;
                                strike = 0;
                                //continue;
                            }

                            Constant.InstrumentType inst = 0;
                            int len = Enum.GetNames(typeof(Constant.InstrumentType)).Length;
                            for (int i = 0; i < len; i++)
                            {
                                if (arrline[2].Trim() == ((Constant.InstrumentType)i).ToString())
                                {
                                    inst = (Constant.InstrumentType)i;
                                    break;
                                }
                            }

                            int tickSize = Convert.ToInt32(arrline[10]);

                            cntrInfo.token = token;
                            cntrInfo.script = script;
                            cntrInfo.strike = strike;
                            cntrInfo.lotSize = lotSize;
                            cntrInfo.exp = Convert.ToInt32(arrline[4]);
                            cntrInfo.tradingSymbol = tradingSymbol;
                            cntrInfo.tickSize = tickSize;
                            cntrInfo.freezeQty = freezeQnty;
                            cntrInfo.option = otp;
                            cntrInfo.instrument = inst;
                            cntrInfo.lastClosingPrice = Convert.ToInt32(arrline[20].Trim());
                            cntrInfo.tickSize = tickSize;
                            if (!SecurityDataBase.ContainsKey(token))
                                SecurityDataBase.Add(token, cntrInfo);
                        }
                    }
                    TradeServer.logger.WriteLine("Contract Details Loaded");
                }
                else
                {
                    MessageBox.Show(contractPath + " not found.Error!!!");
                    Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Loading TokenDetails" + ex);

            }
        }
        public ContractInfo GetTokenDetails(int token)
        {
            if (SecurityDataBase != null && SecurityDataBase.ContainsKey(token))
                return SecurityDataBase[token];
            else
                return null;

        }

    }
}
