using fyp_dms.DB;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace fyp_dms.Class
{
    public class Notifications
    {
        private int notificationID;
        private int lecturerID;
        private int courseSectionID;
        private string title;
        private string message;
        private string createDate;

        private MySqlCommand cmd = null;
        HttpContext context = HttpContext.Current;  //session

        public Notifications()
        {

        }


        public int GetNotificationID()
        {
            return notificationID;
        }

        public int GetLecturerID()
        {
            return lecturerID;
        }

        public int GetCourseSectionID()
        {
            return courseSectionID;
        }

        public string GetTitle()
        {
            return title;
        }

        public string GetMessage()
        {
            return message;
        }

        public string GetCreateDate()
        {
            return createDate;
        }

        public bool SetNotificationID(int notificationID)
        {
            try
            {
                this.notificationID = notificationID;
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

        public bool SetTitle(string title)
        {
            try
            {
                this.title = title;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetMessage(string message)
        {
            try
            {
                this.message = message;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetCreateDate(string createDate)
        {
            try
            {
                this.createDate = createDate;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public DataSet GetDisplayBasedStudent()
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM Notifications";
            DataSet ds = db.GetDataSet(cmd, "Notifications");
            return ds;
        }   //no use

        public DataSet GetDisplayBasedLecturer(string lecturerID)
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT *, CONCAT(title,' Updates') AS titles, CONCAT('File has ',message) AS messages, " +
                "                     ROW_NUMBER() OVER(ORDER BY Notifications.notificationID DESC) AS No, Notifications.notificationID 'ID' " + 
                              "FROM Notifications " + 
                              "INNER JOIN Lecturer ON Lecturer.lecturerID=Notifications.LecturerID " + 
                              "WHERE lecturer.lecturerID='" + lecturerID + "'";
            DataSet ds = db.GetDataSet(cmd, "Notifications");
            return ds;
        }

        public string GetUnreadNotice(string lecturerID)
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();
            cmd.CommandText = "SELLECT (SELECT COUNT(*) " + 
                                       "FROM Notifications N " + 
                                       "LEFT JOIN WorkAssign W ON W.courseSectionID=N.courseSectionID " + 
                                       "WHERE W.lecturerID='" + lecturerID + "') - (SELECT noticeNo " + 
                                                                                   "FROM Lecturer " + 
                                                                                   "WHERE lecturerID='" + lecturerID + "') " + 
                              "'countunread'";
            string ds = db.GetStringScalar(cmd);
            return ds;
        }

        public string GetAllNotice(string LecturerID)
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();
            cmd.CommandText = "Select (SELECT COUNT(*) FROM Notifications N left join WorkAssign W ON W.courseSectionID=N.CourseSectionID WHERE W.LecturerID='" + LecturerID + "') 'countunread'";
            string ds = db.GetStringScalar(cmd);
            return ds;
        }

        public DataSet GetDisplay(string LecturerID, string parameter, string order)
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT *,CONCAT(title,' Updates') AS titles, CONCAT('File has ',message) AS messages, ROW_NUMBER() OVER(ORDER BY Notifications.ID DESC) AS No, Notifications.ID 'ID' FROM Notifications inner join  Lecturer ON Lecturer.lecturerID=Notifications.LecturerID WHERE lecturer.lecturerID='" + LecturerID + "' ORDER BY " + parameter + " " + order;
            DataSet ds = db.GetDataSet(cmd, "Notifications");
            return ds;
        }

        public bool Add(string LecturerID, string CourseSectionID, string title, string message)
        {
            try
            {
                if (this.SetLecturerID(Convert.ToInt32(LecturerID)) && this.SetCourseSectionID(Convert.ToInt32(CourseSectionID)) && this.SetTitle(title) && this.SetMessage(message))
                {

                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "INSERT INTO Notifications(LecturerID, CourseSectionID, title, message, create_date) " +
                                      "VALUES(@P1, @P2, @P3, @P4, now())";
                    cmd.Parameters.AddWithValue("@P1", this.GetLecturerID());
                    cmd.Parameters.AddWithValue("@P2", this.GetCourseSectionID());
                    cmd.Parameters.AddWithValue("@P3", this.GetTitle());
                    cmd.Parameters.AddWithValue("@P4", this.GetMessage());
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

        public bool AddDelete(string LecturerID, string CourseSectionID, string title, string message)
        {
            try
            {
                if (this.SetLecturerID(Convert.ToInt32(LecturerID)) && this.SetCourseSectionID(Convert.ToInt32(CourseSectionID)) && this.SetTitle(title) && this.SetMessage(message))
                {

                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "INSERT INTO Notifications" +
                        "(LecturerID, CourseSectionID, title, message, create_date)" +
                        "VALUES" +
                        "(@P1, @P2, @P3, @P4, now())";
                    cmd.Parameters.AddWithValue("@P1", this.GetLecturerID());
                    cmd.Parameters.AddWithValue("@P2", this.GetCourseSectionID());
                    cmd.Parameters.AddWithValue("@P3", this.GetTitle());
                    cmd.Parameters.AddWithValue("@P4", this.GetMessage());
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


        public bool DeleteNotifications(string id)
        {
            try
            {
                DatabaseManager db = new DatabaseManager();
                if (this.SetNotificationID(Convert.ToInt32(id)))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = "DELETE FROM Notifications WHERE ID =" + this.GetNotificationID();//delete with the ID
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