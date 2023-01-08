using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CustomerAPI.Model;
using CustomerAPI.Helpers;


namespace CustomerAPI.Repository.General
{
    public class CustomerRepository : ICustomerRepository
    {
        public DataTable dtTransactions;
        public DataTable dtFinalRewards;
        public DataTable dtRewards;
        public int TrID = 0;

        public CustomerRepository()
        {
            AddTransColumns();
            AddRewardColumns();
            AddFinalRewardColumns();
            AddPurchaseData();
        }

        public RetDataTable GetTransactions()
        {
            RetDataTable ret = new RetDataTable();            

            try
            {
                if (dtTransactions.Rows.Count > 0)
                {
                    ret.dataTable = dtTransactions;
                    ret.Status = true;
                    ret.ErrorDescription = string.Empty;
                }
                else
                {
                    ret.dataTable = dtRewards;
                    ret.Status = false;
                    ret.ErrorDescription = "No Trasaction Records Exist";
                }
            }
            catch (Exception ex)
            {
                ret.Status = false;
                ret.ErrorDescription = ex.Message;
                ret.dataTable = null;
            }

            return ret;
        }


        public RetDataTable GetRewards()
        {
            RetDataTable ret = new RetDataTable();

            try
            {
                if (dtTransactions.Rows.Count > 0)
                {

                    dtRewards = new DataTable();
                    AddRewardColumns();
                    DataRow dr;

                    double Rewards = 0;
                    double Price = 0;

                    for (int i = 0; i < dtTransactions.Rows.Count; i++)
                    {

                        Price = Convert.ToDouble(dtTransactions.Rows[i]["Price"]);
                        if (Price > 100)
                        {
                            Rewards += ((Price - 100) * 2);
                        }
                        if (Price > 50)
                        {
                            if (Price > 100)
                            {
                                Rewards += 50;
                            }
                            else
                            {
                                Rewards += (Price - 50);
                            }
                        }

                        dr = dtRewards.NewRow();
                        dr["TransactionID"] = dtTransactions.Rows[i]["TransactionID"];
                        dr["PurchaseDate"] = dtTransactions.Rows[i]["PurchaseDate"];
                        dr["CustomerID"] = dtTransactions.Rows[i]["CustomerID"];
                        dr["Price"] = dtTransactions.Rows[i]["Price"];
                        dr["Rewards"] = Rewards;
                        dtRewards.Rows.Add(dr);
                    }

                    ret.dataTable = dtRewards;
                    ret.Status = true;
                    ret.ErrorDescription = string.Empty;
                }
                else
                {
                    ret.dataTable = dtRewards;
                    ret.Status = false;
                    ret.ErrorDescription = "No Trasaction Records Exist";
                }
            }
            catch (Exception ex)
            {
                ret.Status = false;
                ret.ErrorDescription = ex.Message;
                ret.dataTable = null;
            }

            return ret;
        }
        public RetDataTable GetFinalRewards(string CustomerID)
        {
            RetDataTable ret = new RetDataTable();

            try
            {

                dtFinalRewards = new DataTable();
                AddFinalRewardColumns();
                DataRow dr;

                DataTable dtData = dtTransactions.Copy();
                dtData.DefaultView.RowFilter = "CustomerID = '" + CustomerID + "'";
                dtData = dtData.DefaultView.ToTable("Data");

                if (dtData.Rows.Count > 0)
                {

                    dtData.DefaultView.Sort = "PurchaseDate ASC";
                    dtData = dtData.DefaultView.ToTable("Data");

                    int pos = 0;
                    double MonthTot = 0;
                    double TotRew = 0;
                    double Price = 0;

                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        if (pos > 0)
                        {
                            if ((Convert.ToDateTime(dtData.Rows[i]["PurchaseDate"]).Month > Convert.ToDateTime(dtData.Rows[i - 1]["PurchaseDate"]).Month))
                            {
                                TotRew += MonthTot;
                                dr = dtFinalRewards.NewRow();
                                dr["Month"] = Convert.ToDateTime(dtData.Rows[i - 1]["PurchaseDate"]).ToString("MMMM");
                                dr["CustomerID"] = CustomerID;
                                dr["Rewards"] = MonthTot;
                                dr["TotalRewards"] = TotRew;
                                dtFinalRewards.Rows.Add(dr);

                                MonthTot = 0;
                            }
                        }
                        else
                        {
                            pos += 1;
                        }

                        Price = Convert.ToDouble(dtData.Rows[i]["Price"]);
                        if (Price > 100)
                        {
                            MonthTot += ((Price - 100) * 2);
                        }
                        if (Price > 50)
                        {
                            if (Price > 100)
                            {
                                MonthTot += 50;
                            }
                            else
                            {
                                MonthTot += (Price - 50);
                            }
                        }

                        if (i == dtData.Rows.Count - 1)
                        {
                            TotRew += MonthTot;
                            dr = dtFinalRewards.NewRow();
                            dr["Month"] = Convert.ToDateTime(dtData.Rows[i]["PurchaseDate"]).ToString("MMMM");
                            dr["CustomerID"] = CustomerID;
                            dr["Rewards"] = MonthTot;
                            dr["TotalRewards"] = TotRew;
                            dtFinalRewards.Rows.Add(dr);
                        }
                    }


                    foreach (DataRow drFinal in dtFinalRewards.Rows)
                    {
                        drFinal["TotalRewards"] = TotRew;
                        dtFinalRewards.AcceptChanges();
                    }

                    ret.dataTable = dtFinalRewards;
                    ret.Status = true;
                    ret.ErrorDescription = string.Empty;
                }
                else
                {
                    ret.dataTable = dtRewards;
                    ret.Status = false;
                    ret.ErrorDescription = "No Customer Records Exist";
                }
            }
            catch (Exception ex)
            {
                ret.Status = false;
                ret.ErrorDescription = ex.Message;
                ret.dataTable = null;
            }

            return ret;
        }

        public void AddTransColumns()
        {
            dtTransactions = new DataTable("Data");
            dtTransactions.Columns.Add("TransactionID", typeof(int));
            dtTransactions.Columns.Add("PurchaseDate", typeof(string));
            dtTransactions.Columns.Add("CustomerID", typeof(string));
            dtTransactions.Columns.Add("Price", typeof(string));
        }

        public void AddFinalRewardColumns()
        {
            dtFinalRewards = new DataTable("Data");
            dtFinalRewards.Columns.Add("Month", typeof(string));
            dtFinalRewards.Columns.Add("CustomerID", typeof(string));
            dtFinalRewards.Columns.Add("Rewards", typeof(double));
            dtFinalRewards.Columns.Add("TotalRewards", typeof(double));
        }

        public void AddRewardColumns()
        {
            dtRewards = new DataTable("Data");
            dtRewards.Columns.Add("TransactionID", typeof(int));
            dtRewards.Columns.Add("PurchaseDate", typeof(string));
            dtRewards.Columns.Add("CustomerID", typeof(string));
            dtRewards.Columns.Add("Price", typeof(string));
            dtRewards.Columns.Add("Rewards", typeof(double));
        }

        public void AddPurchaseData()
        {
            DataRow dr;

            dr = dtTransactions.NewRow();
            dr["TransactionID"] = TrID;
            dr["PurchaseDate"] = "03/01/2022";
            dr["CustomerID"] = "Anand";
            dr["Price"] = 300;
            dtTransactions.Rows.Add(dr);
            TrID += 1;

            dr = dtTransactions.NewRow();
            dr["TransactionID"] = TrID;
            dr["PurchaseDate"] = "03/15/2022";
            dr["CustomerID"] = "Anand";
            dr["Price"] = 185;
            dtTransactions.Rows.Add(dr);
            TrID += 1;

            dr = dtTransactions.NewRow();
            dr["TransactionID"] = TrID;
            dr["PurchaseDate"] = "03/22/2022";
            dr["CustomerID"] = "Anand";
            dr["Price"] = 500;
            dtTransactions.Rows.Add(dr);
            TrID += 1;

            dr = dtTransactions.NewRow();
            dr["TransactionID"] = TrID;
            dr["PurchaseDate"] = "04/04/2022";
            dr["CustomerID"] = "Anand";
            dr["Price"] = 125;
            dtTransactions.Rows.Add(dr);
            TrID += 1;

            dr = dtTransactions.NewRow();
            dr["TransactionID"] = TrID;
            dr["PurchaseDate"] = "05/04/2022";
            dr["CustomerID"] = "Anand";
            dr["Price"] = 450;
            dtTransactions.Rows.Add(dr);
            TrID += 1;

            dr = dtTransactions.NewRow();
            dr["TransactionID"] = TrID;
            dr["PurchaseDate"] = "03/01/2022";
            dr["CustomerID"] = "Vineeth";
            dr["Price"] = 340;
            dtTransactions.Rows.Add(dr);
            TrID += 1;

            dr = dtTransactions.NewRow();
            dr["TransactionID"] = TrID;
            dr["PurchaseDate"] = "04/04/2022";
            dr["CustomerID"] = "Vineeth";
            dr["Price"] = 100;
            dtTransactions.Rows.Add(dr);
            TrID += 1;

            dr = dtTransactions.NewRow();
            dr["TransactionID"] = TrID;
            dr["PurchaseDate"] = "04/12/2022";
            dr["CustomerID"] = "Vineeth";
            dr["Price"] = 80;
            dtTransactions.Rows.Add(dr);
            TrID += 1;

            dr = dtTransactions.NewRow();
            dr["TransactionID"] = TrID;
            dr["PurchaseDate"] = "05/23/2022";
            dr["CustomerID"] = "Deepak";
            dr["Price"] = 210;
            dtTransactions.Rows.Add(dr);
            TrID += 1;

            dr = dtTransactions.NewRow();
            dr["TransactionID"] = TrID;
            dr["PurchaseDate"] = "04/04/2022";
            dr["CustomerID"] = "Deepak";
            dr["Price"] = 325;
            dtTransactions.Rows.Add(dr);
            TrID += 1;

            dr = dtTransactions.NewRow();
            dr["TransactionID"] = TrID;
            dr["PurchaseDate"] = "05/10/2022";
            dr["CustomerID"] = "Deepak";
            dr["Price"] = 800;
            dtTransactions.Rows.Add(dr);
            TrID += 1;
        }

               
    }
}
