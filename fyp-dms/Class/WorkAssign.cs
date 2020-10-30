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
    public class WorkAssign
    {
        private int workAssignID;
        private int courseSectionID;
        private int lecturerID;
        private string position;
        private bool status;

        private MySqlCommand cmd = null;
        HttpContext context = HttpContext.Current;

        public WorkAssign()
        {

        }
        public int GetWorkAssignID()
        {
            return workAssignID;
        }
        public int GetCourseSectionID()
        {
            return courseSectionID;
        }
        public int GetLecturerID()
        {
            return lecturerID;
        }
        public string GetPosition()
        {
            return position;
        }
        public bool GetStatus()
        {
            return status;
        }
        public bool SetWorkAssignID(int workAssignID)
        {
            try
            {
                this.workAssignID = workAssignID;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SetCourseSectionID(int courseSectionID)
        {
            try
            {
                this.courseSectionID = courseSectionID;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SetLecturerID(int lecturerID)
        {
            try
            {
                this.lecturerID = lecturerID;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetPosition(string position)
        {
            try
            {
                this.position = position;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetStatus(bool status)
        {
            try
            {
                this.status = status;
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
            cmd.CommandText = "SELECT W.workAssignID 'WorkAssignID', CONCAT(CAST(L.lecturerID AS CHAR),'-' ,L.name) AS LecturerName, " + 
                                     "CONCAT(C.courseCode,'-', CAST(CS.sectionNo AS CHAR)) AS CourseCode, W.position 'Position', " + 
                                     "CASE WHEN W.status = '1' THEN 'Active' ELSE 'No Active' END AS Status " + 
                              "FROM WorkAssign W " + 
                              "INNER JOIN Lecturer L ON W.lecturerID=L.lecturerID " + 
                              "INNER JOIN CourseSection CS ON CS.courseSectionID=W.courseSectionID " + 
                              "INNER JOIN Course C ON C.courseID=CS.courseID " +
                              "ORDER BY W.workAssignID";
            DataSet ds = db.GetDataSet(cmd, "WorkAssign");
            return ds;
        }

        public bool Add(string courseSectionID, string lecturerID, string position, bool status)
        {
            try
            {
                DatabaseManager db = new DatabaseManager();
                cmd = new MySqlCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM WorkAssign WHERE courseSectionID=@CourseSectionID AND lecturerID=@LecturerID"; //authenticate
                cmd.Parameters.AddWithValue("@CourseSectionID", courseSectionID);
                cmd.Parameters.AddWithValue("@LecturerID", lecturerID);

                int reader = 0;
                reader = Convert.ToInt32(db.GetDataScalar(cmd));

                if (reader == 1)
                {
                    return false;
                }
                else
                {
                    if (this.SetCourseSectionID(Convert.ToInt32(courseSectionID)) && this.SetLecturerID(Convert.ToInt32(lecturerID)) && this.SetPosition(position) && this.SetStatus((status)))
                    {
                        cmd = new MySqlCommand();
                        cmd.CommandText = "INSERT INTO WorkAssign(workAssignID, courseSectionID, lecturerID, position, status) " +
                                          "VALUES(@P1, @P2, @P3, @P4, @P5)";
                        cmd.Parameters.AddWithValue("@P1", "");
                        cmd.Parameters.AddWithValue("@P2", this.GetCourseSectionID());
                        cmd.Parameters.AddWithValue("@P3", this.GetLecturerID());
                        cmd.Parameters.AddWithValue("@P4", this.GetPosition());
                        cmd.Parameters.AddWithValue("@P5", Convert.ToInt16(this.GetStatus()));
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

        public bool Update(string workAssignID, string courseSectionID, string lecturerID, string position, bool status)
        {
            try
            {
                DatabaseManager db = new DatabaseManager();
                cmd = new MySqlCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM WorkAssign WHERE courseSectionID=@CourseSectionID AND lecturerID=@LecturerID"; //authenticate
                cmd.Parameters.AddWithValue("@CourseSectionID", courseSectionID);
                cmd.Parameters.AddWithValue("@LecturerID", lecturerID);

                int reader = 0;
                reader = Convert.ToInt32(db.GetDataScalar(cmd));

                if (reader == 1)
                {
                    return false;
                }
                else
                {
                    if (this.SetWorkAssignID(Convert.ToInt32(workAssignID)) && this.SetCourseSectionID(Convert.ToInt32(courseSectionID)) && this.SetLecturerID(Convert.ToInt32(lecturerID)) && this.SetPosition(position) && this.SetStatus(status))
                    {
                        cmd = new MySqlCommand();
                        cmd.CommandText = "UPDATE WorkAssign " +
                                          "SET courseSectionID ='" + this.GetCourseSectionID() + "', lecturerID ='" + this.GetLecturerID() + "', position = '" + this.GetPosition() + "', status ='" + Convert.ToInt16(this.GetStatus()) + "' " +
                                          "WHERE workAssignID ='" + this.GetWorkAssignID() + "'";
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

        public bool UpdateLecturerMain(string workAssignID, string position, bool status)
        {

            try
            {
                if (this.SetWorkAssignID(Convert.ToInt32(workAssignID)) && this.SetPosition(position) && this.SetStatus(status))
                {
                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "UPDATE WorkAssign " +
                                      "SET position ='" + this.GetPosition() + "', status ='" + Convert.ToInt16(this.GetStatus()) + "' " +
                                      "WHERE workAssignID ='" + this.GetWorkAssignID() + "'";
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

        public bool DeleteWorkAssign(string workAssignID)
        {
            try
            {
                DatabaseManager db = new DatabaseManager();
                if (this.SetWorkAssignID(Convert.ToInt32(workAssignID)))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = "DELETE FROM WorkAssign WHERE workAssignID =" + this.GetWorkAssignID();   //delete with the workAssign ID
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

        public ArrayList SelectCourseCode()
        {
            ArrayList alTempResult = null;

            try
            {
                DatabaseManager db = new DatabaseManager();
                cmd = new MySqlCommand();
                cmd.CommandText = "SELECT CS.courseSectionID 'courseSectionID', CONCAT(CAST(C.courseCode AS CHAR),'-', CAST(CS.sectionNo AS CHAR)) AS CourseCode " + 
                                  "FROM CourseSection CS " + 
                                  "INNER JOIN Course C ON C.courseID=CS.CourseID"; //change this
                alTempResult = (ArrayList)db.GetArrayList(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return alTempResult;
        }

        public ArrayList SelectCourseCode(string strKey, string strValue)
        {
            ArrayList groupList = null;
            DatabaseManager db = new DatabaseManager();
            try
            {
                ArrayList arList = SelectCourseCode();
                groupList = db.GetConcatCode(arList, strKey, strValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return groupList;
        }

        public ArrayList SelectLecturerID()
        {
            ArrayList alTempResult = null;

            try
            {
                DatabaseManager db = new DatabaseManager();
                cmd = new MySqlCommand();
                cmd.CommandText = "SELECT lecturerID, CONCAT(CAST(lecturerID AS CHAR),'-',name) AS name FROM Lecturer"; //change this
                alTempResult = (ArrayList)db.GetArrayList(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return alTempResult;
        }

        public ArrayList SelectLecturerID(string strKey, string strValue)
        {
            ArrayList groupList = null;
            DatabaseManager db = new DatabaseManager();
            try
            {
                ArrayList arList = SelectLecturerID();
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