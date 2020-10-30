using fyp_dms.DB;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace fyp_dms.Class
{
    public class Intake
    {
        private int intakeID;
        private string intakeMonth;

        private MySqlCommand cmd = null;
        HttpContext context = HttpContext.Current;

        public Intake()
        {

        }

        public int GetIntakeID()
        {
            return intakeID;
        }

        public string GetIntakeMonth()
        {
            return intakeMonth;
        }

        public bool SetIntakeID(int intakeID)
        {
            try
            {
                this.intakeID = intakeID;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SetIntakeMonth(string intakeMonth)
        {
            try
            {
                this.intakeMonth = intakeMonth;
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

            cmd.CommandText = "SELECT intakeID, intakeMonth FROM Intake ORDER BY intakeID";

            DataSet ds = db.GetDataSet(cmd, "Intake");

            return ds;
        }

        public bool Add(string intakeMonth)
        {
            try
            {
                DatabaseManager db = new DatabaseManager();
                cmd = new MySqlCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM Intake WHERE intakeMonth=@IntakeMonth"; //authenticate
                cmd.Parameters.AddWithValue("@IntakeMonth", intakeMonth);

                int reader = 0;
                reader = Convert.ToInt32(db.GetDataScalar(cmd));

                if (reader == 1)
                {
                    return false;
                }
                else
                {
                    if (this.SetIntakeID(Convert.ToInt32(intakeID)) && this.SetIntakeMonth(intakeMonth))
                    {
                        cmd = new MySqlCommand();
                        cmd.CommandText = "INSERT INTO Intake(intakeID, intakeMonth) " +
                                          "VALUES(@P1, @P2)";
                        cmd.Parameters.AddWithValue("@P1", "");
                        cmd.Parameters.AddWithValue("@P2", this.GetIntakeMonth());
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

        public bool Update(string intakeID, string intakeMonth)
        {
            try
            {
                DatabaseManager db = new DatabaseManager();
                cmd = new MySqlCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM Intake WHERE intakeMonth=@IntakeMonth"; //authenticate
                cmd.Parameters.AddWithValue("@IntakeMonth", intakeMonth);

                int reader = 0;
                reader = Convert.ToInt32(db.GetDataScalar(cmd));

                if (reader == 1)
                {
                    return false;
                }
                else
                {
                    if (this.SetIntakeID(Convert.ToInt32(intakeID)) && this.SetIntakeMonth(intakeMonth))
                    {
                        cmd = new MySqlCommand();
                        cmd.CommandText = "UPDATE Intake " +
                                          "SET intakeMonth ='" + this.GetIntakeMonth() + "' " +
                                          "WHERE intakeID ='" + this.GetIntakeID() + "'";
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

        public bool DeleteIntake(string intakeID)
        {
            try
            {
                DatabaseManager db = new DatabaseManager();
                if (this.SetIntakeID(Convert.ToInt32(intakeID)))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = "DELETE FROM Intake WHERE intakeID =" + this.GetIntakeID();//delete with the ID
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
    }
}