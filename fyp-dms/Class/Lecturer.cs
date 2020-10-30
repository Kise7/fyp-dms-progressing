using fyp_dms.DB;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace fyp_dms.Class
{
    public class Lecturer
    {
        private int lecturerID;
        private int adminID;
        private string name;
        private string password;
        private string phoneNo;
        private string email;
        private int noticeNo;

        private MySqlCommand cmd = null;
        HttpContext context = HttpContext.Current;

        public Lecturer()
        {

        }

        public int GetLecturerID()
        {
            return lecturerID;
        }

        public int GetAdminID()
        {
            return adminID;
        }

        public string GetName()
        {
            return name;
        }


        public string GetPassword()
        {
            return password;
        }

        public string GetPhoneNo()
        {
            return phoneNo;
        }

        public string GetEmail()
        {
            return email;
        }

        public int GetNoticeNo()
        {
            return noticeNo;
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

        public bool SetAdminID(int adminID)
        {
            try
            {
                this.adminID = adminID;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetName(string name)
        {
            try
            {
                this.name = name;
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool SetPassword(string password)
        {
            try
            {
                this.password = password;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetPhoneNo(string phoneNo)
        {
            try
            {
                this.phoneNo = phoneNo;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetEmail(string email)
        {
            try
            {
                this.email = email;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetNoticeNo(int noticeNo)
        {
            try
            {
                this.noticeNo = noticeNo;
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
            cmd.CommandText = "SELECT L.lecturerID 'LecturerID', L.name 'Name', A.adminName 'AdminName', L.phoneNo 'PhoneNo', L.email 'Email' " + 
                              "FROM Lecturer L " + 
                              "INNER JOIN Admin A ON L.adminID=A.adminID " +
                              "ORDER BY L.lecturerID";
            DataSet ds = db.GetDataSet(cmd, "Lecturer");
            return ds;
        }

        public DataSet GetLecturerMainDisplay(string lecturerID)
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT ROW_NUMBER() OVER(ORDER BY CS.courseID) AS No, CONCAT(C.courseCode,' ',C.courseName) AS CourseCode, " + 
                                     "CS.courseSectionID 'CourseSectionID', CS.sectionNo 'SectionNo', W.Name 'NickName', W.workAssignID 'ID', " + 
                                     "CASE WHEN W.Status = '1' THEN 'Active' ELSE 'No Active' END AS Status " + 
                              "FROM CourseSection CS " +
                              "INNER JOIN Course C ON CS.courseID=C.courseID " +
                              "INNER JOIN WorkAssign W ON CS.courseSectionID=W.courseSectionID " +
                              "WHERE W.lecturerID=" + lecturerID;
            DataSet ds = db.GetDataSet(cmd, "CourseSection");
            return ds;
        }

        public DataSet GetProfile(string lecturerID)
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM Lecturer WHERE lecturerID='" + lecturerID + "'";
            DataSet ds = db.GetDataSet(cmd, "Lecturer");
            return ds;
        }

        public bool UpdatePhoneNo(string lecturerID, string phoneNo)
        {
            try
            {
                if (this.SetLecturerID(Convert.ToInt32(lecturerID)) && this.SetPhoneNo(phoneNo))
                {
                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "UPDATE Lecturer " +
                                      "SET phoneNo ='" + this.GetPhoneNo() + "' " +
                                      "WHERE lecturerID ='" + this.GetLecturerID() + "'";
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

        public bool ResetPassword(string lecturerID)
        {
            try
            {
                if (this.SetLecturerID(Convert.ToInt32(lecturerID)))
                {
                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "UPDATE Lecturer " +
                                      "SET Password ='holdDoc1' " +
                                      "WHERE lecturerID ='" + this.GetLecturerID() + "'";
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

        public bool UpdatePhoneAndPassword(string lecturerID, string phoneNo, string oldPassword, string newPassword)
        {
            try
            {
                if (this.SetLecturerID(Convert.ToInt32(lecturerID)) && this.SetPhoneNo(phoneNo) && this.SetPassword(newPassword))
                {
                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "SELECT COUNT(*) FROM Lecturer WHERE lecturerID='" + this.GetLecturerID() + "' AND password='" + oldPassword + "'";
                    int reader = db.GetIntScalar(cmd);
                    if (reader == 1)
                    {
                        cmd = new MySqlCommand();
                        cmd.CommandText = "UPDATE Lecturer " +
                                          "SET phoneNo ='" + this.GetPhoneNo() + "', password='" + this.GetPassword() + "' " +
                                          "WHERE lecturerID ='" + this.GetLecturerID() + "'";
                        db.ExecuteNonQuery(cmd);
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

        public string GenerateEmail(string name)
        {
            StringBuilder charBuffer = new StringBuilder();

            string lowerName = name.ToLower(), surname = "", newName = "";

            surname = lowerName.Split()[0];

            string[] tempName = lowerName.Split(' ');

            foreach (string value in tempName)
            {
                charBuffer.Append(value[0]);
            }

            StringBuilder removeFirstCharacter = charBuffer.Remove(0, 1);

            newName = surname + removeFirstCharacter.ToString();
            return newName.ToLower();
        }

        public bool Add(string lecturerID, string name, string phoneNo)
        {
            string lecturerEmail = GenerateEmail(name);

            try
            {
                if (this.SetLecturerID(Convert.ToInt32(lecturerID)) && this.SetAdminID(Convert.ToInt32(context.Session["ID"].ToString())) && this.SetName(name) && this.SetPhoneNo(phoneNo) && this.SetEmail(lecturerEmail + "@tarc.edu.my"))
                {

                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "INSERT INTO Lecturer(lecturerID, adminID, name, password, phoneNo, email, noticeNo) " +
                                      "VALUES(@P1, @P2, @P3, @P4, @P5, @P6, @P7)";
                    cmd.Parameters.AddWithValue("@P1", this.GetLecturerID());
                    cmd.Parameters.AddWithValue("@P2", this.GetAdminID());
                    cmd.Parameters.AddWithValue("@P3", this.GetName());
                    cmd.Parameters.AddWithValue("@P4", "holdDoc1");
                    cmd.Parameters.AddWithValue("@P5", this.GetPhoneNo());
                    cmd.Parameters.AddWithValue("@P6", this.GetEmail());
                    cmd.Parameters.AddWithValue("@P7", 0);
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

        public bool Update(string lecturerID, string name, string phoneNo)
        {
            try
            {
                if (this.SetAdminID(Convert.ToInt32(context.Session["ID"].ToString())) && this.SetLecturerID(Convert.ToInt32(lecturerID)) && this.SetName(name) && this.SetPhoneNo(phoneNo))
                {
                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "UPDATE Lecturer " +
                                      "SET name ='" + this.GetName() + "', adminID ='" + this.GetAdminID() + "', phoneNo ='" + this.GetPhoneNo() + "' " + 
                                      "WHERE lecturerID = '" + this.GetLecturerID() + "'";
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

        public bool UpdateNoticeNo(string lecturerID, string noticeNo)
        {
            try
            {
                if (this.SetLecturerID(Convert.ToInt32(lecturerID)) && this.SetNoticeNo(Convert.ToInt32(noticeNo)))
                {
                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "UPDATE Lecturer " +
                                      "SET noticeNo='" + this.GetNoticeNo() + "' " +
                                      "WHERE lecturerID= '" + this.GetLecturerID() + "'";
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

        public bool DeleteLecturer(string id)
        {
            try
            {
                DatabaseManager db = new DatabaseManager();
                if (this.SetLecturerID(Convert.ToInt32(id)))
                {

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = "DELETE FROM Lecturer WHERE lecturerID='" + this.GetLecturerID() + "'";
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
        public bool SelectLecturerLogin(string no, string password, string checkbox)
        {
            try
            {
                if (this.SetLecturerID(Convert.ToInt32(no)) && this.SetPassword(password))
                {

                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "SELECT COUNT(*) FROM Lecturer WHERE lecturerID=@LecturerID AND password=@Password"; //authenticate
                    cmd.Parameters.AddWithValue("@LecturerID", this.GetLecturerID());
                    cmd.Parameters.AddWithValue("@Password", this.GetPassword());

                    int reader = Convert.ToInt32(db.GetDataScalar(cmd));
                    reader = 1;

                    if (reader == 1)
                    {
                        cmd = new MySqlCommand();
                        cmd.CommandText = "SELECT name FROM Lecturer WHERE lecturerID=@LecturerID AND password=@Password";
                        cmd.Parameters.AddWithValue("@LecturerID", this.GetLecturerID());
                        cmd.Parameters.AddWithValue("@Password", this.GetPassword());
                        string name = db.GetDataScalar(cmd).ToString();

                        HttpContext context = HttpContext.Current;
                        context.Session["Username"] = name;
                        context.Session["LecturerID"] = this.GetLecturerID();
                        context.Session["Identity"] = "Lecturer";
                        context.Session["ID"] = this.GetLecturerID();
                        if (checkbox == "Remember Me")
                        {
                            context.Session.Timeout = 60;
                        }
                        else
                        {
                            context.Session.Timeout = 10;
                        }
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
    }
}