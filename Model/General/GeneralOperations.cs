using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class GeneralOperations
{
    
    public static string ConnectionString = string.Empty;
    MySqlConnection con;
    public GeneralOperations()
    {
        var configuration = GetConfiguration();
        con = new MySqlConnection(configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
        ConnectionString = con.ConnectionString;
    }

    public IConfiguration GetConfiguration()
    {
        
        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        return builder.Build();
    }
    //public void SomeMethod()
    //{
    //    var jwtPass = Configuration["Jwt:SecretKey"];
    //    var connStr = Configuration["ConnectionStrings:DefaultConnection"];
    //}

    public RetDataSet GetDbDetails(string CustID)
    {
        RetDataSet ret = new RetDataSet();

        string SQL = string.Empty;
        string DBNAME = string.Empty;

        try
        {
            if (CustID != "")
            {
                using (MySqlConnection Conn = new MySqlConnection(GeneralOperations.ConnectionString))
                {
                    Conn.Open();

                    SQL = "SELECT * FROM wmsrfidsetup WHERE SubscriptionStatus=1 AND CustID=@CustID";

                    using (MySqlCommand CMD = new MySqlCommand(SQL, Conn))
                    {
                        CMD.CommandType = CommandType.Text;
                        CMD.Parameters.AddWithValue("@CustID", CustID);
                        ret.dataSet = new DataSet();
                        GeneralOperations.ConnectionString = "";

                        using (MySqlDataAdapter DA = new MySqlDataAdapter(CMD))
                        {
                            DA.Fill(ret.dataSet, "Data");
                        }
                        if (ret.dataSet != null && ret.dataSet.Tables[0].Rows.Count > 0)
                        {
                            string SQLConn = string.Empty;
                            if (ret.dataSet.Tables[0].Rows[0]["DBName"].ToString() != "" && ret.dataSet.Tables[0].Rows[0]["DBName"] != null)
                            {
                                DBNAME = ret.dataSet.Tables[0].Rows[0]["DBName"].ToString();
                                SQLConn = con.ConnectionString;
                                MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder(SQLConn);
                                SQLConn = builder.ConnectionString;
                                GeneralOperations.ConnectionString = SQLConn.Replace("Initial Catalog=WMSRFIDSETUP", "Initial Catalog=" + DBNAME);
                                //GeneralOperations.ConnectionString = SQLConn.Replace("RDS_DB_NAME=WMSRFIDSETUP", "RDS_DB_NAME=" + DBNAME);
                            }
                            else
                            {
                                SQLConn = con.ConnectionString;
                                MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder(SQLConn);
                                SQLConn = builder.ConnectionString;
                                GeneralOperations.ConnectionString = SQLConn;
                            }

                            ret.Status = true;
                            ret.ErrorDescription = string.Empty;
                        }
                        else
                        {
                            ret.Status = false;
                            ret.ErrorDescription = "CustID Missing ";
                            ret.dataSet = null;
                        }
                    }
                }
            }
            else
            {
                ret.Status = false;
                ret.ErrorDescription = "CustID Missing ";
                ret.dataSet = null;
            }
        }
        catch (Exception ex)
        {
            ret.Status = false;
            ret.ErrorDescription = ex.Message;
            ret.dataSet = null;
        }

        return ret;
    }
}

