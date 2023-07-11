using Dapper;
using Moderator_Server.Constant;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moderator_Server.Margin
{
    public class MarginWatchManager
    {
        #region fil. & var & DIC
        public ConcurrentDictionary<string,MarginWatchModel> MarginWatch_Dicc = new ConcurrentDictionary<string,MarginWatchModel>();

        #endregion

        public MarginWatchManager()
        {
            LoadMarginConfiguration();
            
        }

        #region Private Section
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
        private void LoadMarginConfiguration()
        {
            try
            {
                string DayofWeek = currentDay();
                General.ClientMargin.RestrictionSetting.Text = DayofWeek;//GUI Update

                MarginWatch_Dicc.Clear();//Clear DICC
                List<MARGIN_WATCH_TABLE> MarginWatch_List = new List<MARGIN_WATCH_TABLE>();//List



                using (IDbConnection con = new SQLiteConnection(LoadConnectionString()))
                {
                    var OutputData = con.Query<MARGIN_WATCH_TABLE>("Select * from MARGIN_WATCH_TABLE;", new DynamicParameters());
                    MarginWatch_List = OutputData.ToList();
                }


                foreach (MARGIN_WATCH_TABLE MarginWatch_Table in MarginWatch_List)
                {
                    if (!MarginWatch_Dicc.ContainsKey(MarginWatch_Table.TCODE))
                    {
                        MarginWatchModel marginWatchModel = new MarginWatchModel
                        {
                            TCODE = MarginWatch_Table.TCODE,
                            TNAME = MarginWatch_Table.TNAME,
                            TotalAllottedAmount = MarginWatch_Table.TotalAllottedAmount,
                            Restrictions = ValueAccordingToDay(DayofWeek, MarginWatch_Table)
                        };
                        AddRowToDataGridView(marginWatchModel);
                    }
                }

                //DataLoad 
            }
            catch (Exception ex)
            {
                TradeServer.logger.WriteError(ex.Message);
            }
        }

        private string currentDay()
        {
            return DateTime.Today.DayOfWeek.ToString();
        }
       
        private string ValueAccordingToDay(string day, MARGIN_WATCH_TABLE MarginWatch_Table)
        {
            try
            {
                switch (day)
                {
                    case "Monday":
                        return MarginWatch_Table.Monday == 0 ? "Not Applicable" : "Applicable";
                    case "Tuesday":
                        return MarginWatch_Table.Tuesday == 0 ? "Not Applicable" : "Applicable";
                    case "Wednesday":
                        return MarginWatch_Table.Wednesday == 0 ? "Not Applicable" : "Applicable";
                    case "Thursday":
                        return MarginWatch_Table.Thursday == 0 ? "Not Applicable" : "Applicable";
                    case "Friday":
                        return MarginWatch_Table.Friday == 0 ? "Not Applicable" : "Applicable";
                    default:
                        TradeServer.logger.WriteLine("Current Day is not implemented");
                        break;
                }
            }
            catch (Exception ex)
            {
                TradeServer.logger.WriteError(ex.Message);
            }
            return "Not Applicable";
        }
        private void AddRowToDataGridView(MarginWatchModel marginWatchModel)
        {
            try
            {
                General.ClientMargin.MarginWatchDataGrid.Rows.Add(marginWatchModel.TCODE, marginWatchModel.TNAME, marginWatchModel.TotalAllottedAmount, "", "", marginWatchModel.Restrictions);
            }
            catch (Exception ex)
            {
                TradeServer.logger.WriteError(ex.Message);
            }
        }

        private void DeleteDataGridView(string TCODE)
        {
            //Remove from 
        }
        #endregion



        #region Accessable  methods 



        public void UpdateDB() //MakeSure THE dicc is UpToDate before Updating DataBase
        {
            try
            {
                string DaySelected = General.ClientMargin.RestrictionSetting.Text;
                using (IDbConnection con = new SQLiteConnection(LoadConnectionString()))
                {

                    foreach(MarginWatchModel MWM in MarginWatch_Dicc.Values)
                    {
                        int Enable = MWM.Restrictions == "Not Applicable" ? 0 : 1;
                        var TransactionStatus = con.Execute("UPDATE MARGIN_WATCH_TABLE SET TNAME = "+MWM.TNAME+ ", TotalAllottedAmount = "+Math.Round(MWM.TotalAllottedAmount,2)+","+DaySelected+" = "+Enable+ " WHERE TCODE=" + MWM.TCODE+ ";");
                        if(TransactionStatus<0)
                        {
                            TradeServer.logger.WriteError("Data Updation Failed for " + MWM.TCODE);
                        }
                    } 
                }
            }
            catch (Exception ex)
            {
                TradeServer.logger.WriteError(ex.Message);
            }
        }

        public void UpdateRestrictionsDataForALLTrader()
        {
            //Code Here

            try
            {
                string DaySelected = General.ClientMargin.RestrictionSetting.Text;
                List<MARGIN_WATCH_TABLE> MarginWatch_List = new List<MARGIN_WATCH_TABLE>();//List
                using (IDbConnection con = new SQLiteConnection(LoadConnectionString()))
                {
                    var OutputData = con.Query<MARGIN_WATCH_TABLE>("Select * from MARGIN_WATCH_TABLE;", new DynamicParameters());
                    MarginWatch_List = OutputData.ToList();
                }
                foreach (MARGIN_WATCH_TABLE MarginWatch_Table in MarginWatch_List)
                {
                    if (MarginWatch_Dicc.ContainsKey(MarginWatch_Table.TCODE))
                    {
                        var data = MarginWatch_Dicc[MarginWatch_Table.TCODE];
                        data.Restrictions = ValueAccordingToDay(DaySelected, MarginWatch_Table);
                    }
                }
                foreach(DataRow dataRow in General.ClientMargin.MarginWatchDataGrid.Rows)
                {
                    if(dataRow != null)
                    {

                    }
                }
            }

            catch (Exception ex)
            {
                TradeServer.logger.WriteError(ex.Message);
            }
        }
        #endregion

    }
}
