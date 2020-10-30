using fyp_dms.DB;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace fyp_dms.Class
{
    public class StudentCourseSection
    {
        private int studentCourseSectionID;
        private string studentID;
        private int courseSectionID;

        private MySqlCommand cmd = null;
        HttpContext context = HttpContext.Current;

        public StudentCourseSection()
        {

        }

        public int GetID()
        {
            return studentCourseSectionID;
        }

        public string GetStudentID()
        {
            return studentID.ToUpper();
        }

        public int GetCourseSectionID()
        {
            return courseSectionID;
        }

        public bool SetID(int value)
        {
            try
            {
                studentCourseSectionID = value;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetStudentID(string value)
        {
            try
            {
                studentID = value.ToUpper();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetCourseSectionID(int value)
        {
            try
            {
                courseSectionID = value;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public DataSet GetDisplayBasedStudent(string studentid)
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT ROW_NUMBER() OVER(ORDER BY CS.CourseID) AS No, SCS.studentCourseSectionID 'StudentCourseSectionID',CS.courseSectionID 'CourseSectionID',CS.SectionNo 'SectionNo',C.CourseCode 'CourseCode',C.CourseName 'CourseName' FROM studentcoursesection SCS INNER JOIN coursesection CS ON SCS.CourseSectionID=CS.CourseSectionID INNER JOIN course C ON C.courseID=CS.CourseID WHERE SCS.StudentID='" + studentid + "'";
            DataSet ds = db.GetDataSet(cmd, "StudentCourseSection");
            return ds;
        }

        public bool CheckDuplicate(string studentID, string coursesectionid)
        {
            try
            {
                if (this.SetStudentID(studentID) && this.SetCourseSectionID(Convert.ToInt32(coursesectionid)))
                {
                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "SELECT COUNT(*) FROM StudentCourseSection " +
                        "WHERE StudentID='" + this.GetStudentID() + "' AND CourseSectionID='" + this.GetCourseSectionID() + "'";
                    int reader = db.GetIntScalar(cmd);
                    if (reader == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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

        public bool Add(string studentID, string coursesectionid)
        {
            try
            {

                if (this.SetStudentID(studentID) && this.SetCourseSectionID(Convert.ToInt32(coursesectionid)))
                {
                    //should be verify first
                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "INSERT INTO StudentCourseSection" +
                        "(ID,StudentID,CourseSectionID)" +
                        "VALUES" +
                        "(@P1, @P2, @P3)";
                    cmd.Parameters.AddWithValue("@P1", "");
                    cmd.Parameters.AddWithValue("@P2", this.GetStudentID());
                    cmd.Parameters.AddWithValue("@P3", this.GetCourseSectionID());
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

        public bool DeleteStudentCourseSection(string id)
        {
            try
            {
                DatabaseManager db = new DatabaseManager();
                if (this.SetID(Convert.ToInt32(id)))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = "DELETE FROM StudentCourseSection WHERE ID ='" + this.GetID() + "'";
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