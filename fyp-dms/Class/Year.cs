using fyp_dms.DB;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace fyp_dms.Class
{
    public class Year
    {
        private int yearID;
        private int yearValue;

        private MySqlCommand cmd = null;
        HttpContext context = HttpContext.Current;

        private string strConn;
        private MySqlConnection sqlConn;
        private MySqlCommand sqlCmd;

        public Year()
        {
        }

        public int GetYearID()
        {
            return yearID;
        }
        public int GetYearValue()
        {
            return yearValue;
        }

        public bool SetYearID(int yearID)
        {
            try
            {
                this.yearID = yearID;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetYearValue(int yearValue)
        {
            try
            {
                this.yearValue = yearValue;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public DataSet GetDisplay()
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();

            cmd.CommandText = "SELECT yearID, yearValue FROM Year ORDER BY yearID";

            DataSet ds = db.GetDataSet(cmd, "YEAR");

            return ds;
        }

        public bool Add(string yearValue)
        {
            try
            {
                DatabaseManager db = new DatabaseManager();
                cmd = new MySqlCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM Year WHERE yearValue=@YearValue"; //authenticate
                cmd.Parameters.AddWithValue("@YearValue", yearValue);

                int reader = 0;
                reader = Convert.ToInt32(db.GetDataScalar(cmd));

                if (reader == 1)
                {
                    return false;
                }
                else
                {
                    if (this.SetYearValue(Convert.ToInt32(yearValue)))
                    {
                        cmd = new MySqlCommand();
                        cmd.CommandText = "INSERT INTO Year(yearID, yearValue) " +
                                          "VALUES(@P1, @P2)";
                        cmd.Parameters.AddWithValue("@P1", "");
                        cmd.Parameters.AddWithValue("@P2", this.GetYearValue());
                        db.ExecuteNonQuery(cmd);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public bool Update(string ID, string yearValue)
        {
            try
            {
                DatabaseManager db = new DatabaseManager();
                cmd = new MySqlCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM Year WHERE yearValue=@YearValue"; //authenticate
                cmd.Parameters.AddWithValue("@YearValue", yearValue);

                int reader = 0;
                reader = Convert.ToInt32(db.GetDataScalar(cmd));

                if (reader == 1)
                {
                    return false;
                }
                else
                {
                    if (this.SetYearID(Convert.ToInt32(ID)) && this.SetYearValue(Convert.ToInt32(yearValue)))
                    {
                        cmd = new MySqlCommand();
                        cmd.CommandText = "UPDATE Year " +
                                          "SET yearValue ='" + this.GetYearValue() + "' " +
                                          "WHERE yearID ='" + this.GetYearID() + "'";
                        db.ExecuteNonQuery(cmd);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteYear(string yearID)
        {
            try
            {
                DatabaseManager db = new DatabaseManager();
                if (this.SetYearID(Convert.ToInt32(yearID)))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = "DELETE FROM Year WHERE yearID =" + this.GetYearID(); //delete with the year ID
                    db.ExecuteNonQuery(cmd);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public string GetYear(string yearID)
        {
            strConn = "server = localhost; user = root; database = document; port = 3306; password =;CharSet=ascii";
            sqlConn = new MySqlConnection(strConn);
            sqlCmd = new MySqlCommand();
            sqlCmd.Connection = sqlConn;

            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT yearValue FROM Year WHERE yearID ='" + Convert.ToInt32("2") + "'";
            sqlConn.Open();
            MySqlDataReader dr = sqlCmd.ExecuteReader();
            dr.Read();
            string year = dr.GetString(0);
            sqlConn.Close();

            return year;
        }

        public ArrayList SelectYear()
        {
            ArrayList alTempResult = null;
            try
            {
                DatabaseManager db = new DatabaseManager();
                cmd = new MySqlCommand();
                cmd.CommandText = "SELECT yearValue 'Year', yearValue FROM Year";
                alTempResult = (ArrayList)db.GetArrayList(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return alTempResult;
        }

        public ArrayList SelectYear(string strKey, string strValue)
        {
            ArrayList groupList = null;
            DatabaseManager db = new DatabaseManager();
            try
            {
                ArrayList arList = SelectYear();
                groupList = db.GetConcatCode(arList, strKey, strValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return groupList;
        }

    }
}