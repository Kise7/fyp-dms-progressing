using fyp_dms.DB;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace fyp_dms.Class
{
    public class Faculty
    {
        private int facultyID;
        private string facultyName;

        private MySqlCommand cmd = null;
        HttpContext context = HttpContext.Current;

        public Faculty()
        {

        }

        public int GetFacultyID()
        {
            return facultyID;
        }

        public string GetFacultyName()
        {
            return facultyName;
        }

        public bool SetFacultyID(int facultyID)
        {
            try
            {
                this.facultyID = facultyID;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetFacultyName(string facultyName)
        {
            try
            {
                this.facultyName = facultyName;
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

            cmd.CommandText = "SELECT facultyID, facultyName FROM Faculty ORDER BY facultyID";
            DataSet ds = db.GetDataSet(cmd, "Faculty");

            return ds;
        }

        public bool Add(string facultyName)
        {
            try
            {
                DatabaseManager db = new DatabaseManager();
                cmd = new MySqlCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM Faculty WHERE facultyName=@FacultyName"; //authenticate
                cmd.Parameters.AddWithValue("@FacultyName", facultyName);

                int reader = 0;
                reader = Convert.ToInt32(db.GetDataScalar(cmd));

                if (reader == 1)
                {
                    return false;
                }
                else
                {
                    if (this.SetFacultyName(facultyName))
                    {
                        cmd = new MySqlCommand();
                        cmd.CommandText = "INSERT INTO Faculty(facultyID,facultyName) " +
                                          "VALUES(@P1, @P2)";
                        cmd.Parameters.AddWithValue("@P1", "");
                        cmd.Parameters.AddWithValue("@P2", this.GetFacultyName());
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

        public bool Update(string facultyID, string facultyName)
        {
            try
            {
                DatabaseManager db = new DatabaseManager();
                cmd = new MySqlCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM Faculty WHERE facultyName=@FacultyName"; //authenticate
                cmd.Parameters.AddWithValue("@FacultyName", facultyName);

                int reader = 0;
                reader = Convert.ToInt32(db.GetDataScalar(cmd));

                if (reader == 1)
                {
                    return false;
                }
                else
                {
                    if (this.SetFacultyID(Convert.ToInt32(facultyID)) && this.SetFacultyName(facultyName))
                    {
                        cmd = new MySqlCommand();
                        cmd.CommandText = "UPDATE Faculty " +
                                          "SET facultyName ='" + this.GetFacultyName() + "' " +
                                          "WHERE facultyID ='" + this.GetFacultyID() + "'";
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

        public bool DeleteFaculty(string facultyID)
        {
            try
            {
                DatabaseManager db = new DatabaseManager();
                if (this.SetFacultyID(Convert.ToInt32(facultyID)))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = "DELETE FROM Faculty WHERE facultyID =" + this.GetFacultyID();    //delete with the faculty ID
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