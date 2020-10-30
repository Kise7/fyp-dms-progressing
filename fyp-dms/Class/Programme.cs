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
    public class Programme
    {
        private int programmeID;
        private string programmeCode;
        private int facultyID;
        private string programmeName;

        private MySqlCommand cmd = null;
        HttpContext context = HttpContext.Current;

        public Programme()
        {

        }

        public int GetProgrammeID()
        {
            return programmeID;
        }

        public string GetProgrammeCode()
        {
            return programmeCode;
        }

        public int GetFacultyID()
        {
            return facultyID;
        }

        public string GetProgrammeName()
        {
            return programmeName;
        }

        public bool SetProgrammeID(int programmeID)
        {
            try
            {
                this.programmeID = programmeID;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetProgrammeCode(string programmeCode)
        {
            try
            {
                this.programmeCode = programmeCode;
                return true;
            }
            catch
            {
                return false;
            }
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
        public bool SetProgrammeName(string programmeName)
        {
            try
            {
                this.programmeName = programmeName;
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
            cmd.CommandText = "SELECT P.programmeID 'ProgrammeID', P.programmeCode 'ProgrammeCode', F.facultyName 'FacultyName', P.programmeName 'ProgrammeName' " + 
                              "FROM Programme P " +
                              "INNER JOIN Faculty F ON P.facultyID=F.facultyID " + 
                              "ORDER BY P.programmeID";
            DataSet ds = db.GetDataSet(cmd, "Programme");
            return ds;
        }

        public bool Add(string programmeCode, string facultyID, string programmeName)
        {
            try
            {
                DatabaseManager db = new DatabaseManager();
                cmd = new MySqlCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM Programme WHERE programmeCode=@ProgrammeCode"; //authenticate
                cmd.Parameters.AddWithValue("@ProgrammeCode", programmeCode);

                int reader = 0;
                reader = Convert.ToInt32(db.GetDataScalar(cmd));

                if (reader == 1)
                {
                    return false;
                }
                else
                {
                    if (this.SetProgrammeCode(programmeCode) && this.SetFacultyID(Convert.ToInt32(facultyID)) && this.SetProgrammeName(programmeName))
                    {
                        cmd = new MySqlCommand();
                        cmd.CommandText = "INSERT INTO Programme(programmeID, programmeCode, facultyID, programmeName) " +
                                          "VALUES(@P1, @P2, @P3, @P4)";
                        cmd.Parameters.AddWithValue("@P1", "");
                        cmd.Parameters.AddWithValue("@P2", this.GetProgrammeCode());
                        cmd.Parameters.AddWithValue("@P3", this.GetFacultyID());
                        cmd.Parameters.AddWithValue("@P4", this.GetProgrammeName());
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

        public bool Update(string programmeID, string programmeCode, string facultyID, string programmeName)
        {
            try
            {
                DatabaseManager db = new DatabaseManager();
                cmd = new MySqlCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM Programme WHERE programmeCode=@ProgrammeCode"; //authenticate
                cmd.Parameters.AddWithValue("@ProgrammeCode", programmeCode);

                int reader = 0;
                reader = Convert.ToInt32(db.GetDataScalar(cmd));

                if (reader == 1)
                {
                    return false;
                }
                else
                {
                    if (this.SetProgrammeID(Convert.ToInt32(programmeID)) && this.SetProgrammeCode(programmeCode) && this.SetFacultyID(Convert.ToInt32(facultyID)) && this.SetProgrammeName(programmeName))
                    {
                        cmd = new MySqlCommand();
                        cmd.CommandText = "UPDATE Programme " +
                                          "SET programmeCode ='" + this.GetProgrammeCode() + "', facultyID ='" + this.GetFacultyID() + "', programmeName ='" + this.GetProgrammeName() + "' " +
                                          "WHERE programmeID ='" + this.GetProgrammeID() + "'";
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

        public bool DeleteProgramme(string id)
        {
            try
            {
                DatabaseManager db = new DatabaseManager();
                if (this.SetProgrammeID(Convert.ToInt32(id)))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = "DELETE FROM Programme WHERE programmeID =" + this.GetProgrammeID();  //delete with the programme ID
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

        public ArrayList SelectFacultyID()
        {
            ArrayList alTempResult = null;

            try
            {
                DatabaseManager db = new DatabaseManager();
                cmd = new MySqlCommand();
                cmd.CommandText = "SELECT facultyID, facultyName FROM Faculty"; 
                alTempResult = (ArrayList)db.GetArrayList(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return alTempResult;
        }

        public ArrayList SelectFacultyID(string strKey, string strValue)
        {
            ArrayList groupList = null;
            DatabaseManager db = new DatabaseManager();
            try
            {
                ArrayList arList = SelectFacultyID();
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