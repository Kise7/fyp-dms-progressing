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
    public class Course
    {
        private int courseID;
        private string courseCode;
        private int programmeID;
        private string courseName;

        private MySqlCommand cmd = null;
        HttpContext context = HttpContext.Current;

        public Course()
        {

        }

        public int GetCourseID()
        {
            return courseID;
        }

        public string GetCourseCode()
        {
            return courseCode;
        }

        public int GetProgrammeID()
        {
            return programmeID;
        }

        public string GetCourseName()
        {
            return courseName;
        }

        public bool SetCourseID(int courseID)
        {
            try
            {
                this.courseID = courseID;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SetCourseCode(string courseCode)
        {
            try
            {
                this.courseCode = courseCode;
                return true;
            }
            catch
            {
                return false;
            }
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
        public bool SetCourseName(string courseName)
        {
            try
            {
                this.courseName = courseName;
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
            cmd.CommandText = "SELECT C.courseID 'CourseID', P.programmeName 'ProgrammeName', C.CourseCode 'CourseCode', C.CourseName 'CourseName' " +
                              "FROM Course C " + 
                              "INNER JOIN Programme P ON C.programmeID=P.programmeID " +
                              "ORDER BY C.courseID";
            DataSet ds = db.GetDataSet(cmd, "Course");
            return ds;
        }

        public bool Add(string courseCode, string programmeID, string courseName)
        {
            try
            {
                DatabaseManager db = new DatabaseManager();
                cmd = new MySqlCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM Course WHERE courseCode=@CourseCode"; //authenticate
                cmd.Parameters.AddWithValue("@CourseCode", courseCode);

                int reader = 0;
                reader = Convert.ToInt32(db.GetDataScalar(cmd));

                if (reader == 1)
                {
                    return false;
                }
                else
                {
                    if (this.SetCourseCode(courseCode) && this.SetProgrammeID(Convert.ToInt32(programmeID)) && this.SetCourseName(courseName))
                    {
                        cmd = new MySqlCommand();
                        cmd.CommandText = "INSERT INTO Course(courseID, courseCode, programmeID, courseName) " +
                                          "VALUES(@P1, @P2, @P3, @P4)";

                        cmd.Parameters.AddWithValue("@P1", "");
                        cmd.Parameters.AddWithValue("@P2", this.GetCourseCode());
                        cmd.Parameters.AddWithValue("@P3", this.GetProgrammeID());
                        cmd.Parameters.AddWithValue("@P4", this.GetCourseName());
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

        public bool Update(string courseID, string courseCode, string programmeID, string courseName)
        {
            try
            {
                DatabaseManager db = new DatabaseManager();
                cmd = new MySqlCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM Course WHERE courseCode=@CourseCode"; //authenticate
                cmd.Parameters.AddWithValue("@CourseCode", courseCode);

                int reader = 0;
                reader = Convert.ToInt32(db.GetDataScalar(cmd));

                if (reader == 1)
                {
                    return false;
                }
                else
                {
                    if (this.SetCourseID(Convert.ToInt32(courseID)) && this.SetCourseCode(courseCode) && this.SetProgrammeID(Convert.ToInt32(programmeID)) && this.SetCourseName(courseName))
                    {
                        cmd = new MySqlCommand();
                        cmd.CommandText = "UPDATE Course " +
                                          "SET courseCode ='" + this.GetCourseCode() + "', programmeID ='" + this.GetProgrammeID() + "', courseName ='" + this.GetCourseName() + "' " +
                                          "WHERE courseID ='" + this.GetCourseID() + "'";
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

        public bool DeleteCourse(string id)
        {
            try
            {
                DatabaseManager db = new DatabaseManager();
                if (this.SetCourseID(Convert.ToInt32(id)))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = "DELETE FROM Course WHERE courseID =" + this.GetCourseID(); //delete with the course ID
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

        public ArrayList SelectProgrammeID()
        {
            ArrayList alTempResult = null;

            try
            {
                DatabaseManager db = new DatabaseManager();
                cmd = new MySqlCommand();
                cmd.CommandText = "SELECT programmeID, programmeName FROM Programme"; //change this
                alTempResult = (ArrayList)db.GetArrayList(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return alTempResult;
        }

        public ArrayList SelectProgrammeID(string strKey, string strValue)
        {
            ArrayList groupList = null;
            DatabaseManager db = new DatabaseManager();
            try
            {
                ArrayList arList = SelectProgrammeID();
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