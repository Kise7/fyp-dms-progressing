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
    public class CourseSection
    {
        private int courseSectionID;
        private int courseID;
        private int sectionNo;

        private MySqlCommand cmd = null;
        HttpContext context = HttpContext.Current;

        public CourseSection()
        {

        }

        public int GetCourseSectionID()
        {
            return courseSectionID;
        }
        public int GetCourseID()
        {
            return courseID;
        }
        public int GetSectionNo()
        {
            return sectionNo;
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
        public bool SetSectionNo(int sectionNo)
        {
            try
            {
                this.sectionNo = sectionNo;
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
            cmd.CommandText = "SELECT CS.courseSectionID 'CourseSectionID', C.courseCode 'CourseCode', CS.sectionNo 'SectionNo' " + 
                              "FROM CourseSection CS INNER JOIN Course C ON CS.courseID=C.courseID";
            DataSet ds = db.GetDataSet(cmd, "CourseSection");
            return ds;
        }

        public DataSet GetDisplayBasedStudent()
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT ROW_NUMBER() OVER(ORDER BY CS.courseSectionID) AS No, CS.courseSectionID 'CourseSectionID', CS.sectionNo 'SectionNo', C.courseCode 'CourseCode', C.courseName 'CourseName' " +
                              "FROM CourseSection CS " + 
                              "INNER JOIN Course C ON CS.courseID=C.courseID " + 
                              "ORDER BY CS.courseSectionID";//display section
            DataSet ds = db.GetDataSet(cmd, "CourseSection");
            return ds;
        }

        public bool Add(string courseID, string sectionNo)
        {
            try
            {
                DatabaseManager db = new DatabaseManager();
                cmd = new MySqlCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM CourseSection WHERE courseID=@CourseID AND sectionNo=@SectionNo"; //authenticate
                cmd.Parameters.AddWithValue("@CourseID", courseID);
                cmd.Parameters.AddWithValue("@SectionNo", sectionNo);

                int reader = 0;
                reader = Convert.ToInt32(db.GetDataScalar(cmd));

                if (reader == 1)
                {
                    return false;
                }
                else
                {
                    if (this.SetCourseID(Convert.ToInt32(courseID)) && this.SetSectionNo(Convert.ToInt32(sectionNo)))
                    {
                        cmd = new MySqlCommand();
                        cmd.CommandText = "INSERT INTO CourseSection(courseSectionID, courseID , sectionNo) " +
                                          "VALUES(@P1, @P2, @P3)";
                        cmd.Parameters.AddWithValue("@P1", "");
                        cmd.Parameters.AddWithValue("@P2", this.GetCourseID());
                        cmd.Parameters.AddWithValue("@P3", this.GetSectionNo());
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

        public bool Update(string courseSectionID, string courseID, string sectionNo)
        {
            try
            {
                DatabaseManager db = new DatabaseManager();
                cmd = new MySqlCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM CourseSection WHERE courseID=@CourseID AND sectionNo=@SectionNo"; //authenticate
                cmd.Parameters.AddWithValue("@CourseID", courseID);
                cmd.Parameters.AddWithValue("@SectionNo", sectionNo);

                int reader = 0;
                reader = Convert.ToInt32(db.GetDataScalar(cmd));

                if (reader == 1)
                {
                    return false;
                }
                else
                {
                    if (this.SetCourseSectionID(Convert.ToInt32(courseSectionID)) && this.SetCourseID(Convert.ToInt32(courseID)) && this.SetSectionNo(Convert.ToInt32(sectionNo)))
                    {
                        cmd = new MySqlCommand();
                        cmd.CommandText = "UPDATE CourseSection " +
                                          "SET courseID ='" + this.GetCourseID() + "', sectionNo ='" + this.GetSectionNo() + "'" +
                                          "WHERE courseSectionID ='" + this.GetCourseSectionID() + "'";
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

        public bool DeleteCourseSection(string courseSectionID)
        {
            try
            {
                DatabaseManager db = new DatabaseManager();
                if (this.SetCourseSectionID(Convert.ToInt32(courseSectionID)))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = "DELETE FROM CourseSection WHERE courseSectionID =" + this.GetCourseSectionID(); //delete with the course section ID
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
                cmd.CommandText = "SELECT courseID, courseCode FROM Course"; //change this
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
    }
}