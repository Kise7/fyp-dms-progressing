using fyp_dms.DB;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace fyp_dms.Class
{
    public class Folder
    {
        private int folderID;
        private int lecturerID;
        private int courseSectionID;
        private string folderName;
        private int privelegeID;

        private MySqlCommand cmd = null;
        HttpContext context = HttpContext.Current;  //sesion

        public Folder()
        {

        }

        public int GetFolderID()
        {
            return folderID;
        }

        public int GetLecturerID()
        {
            return lecturerID;
        }

        public int GetCourseSectionID()
        {
            return courseSectionID;
        }

        public string GetFolderName()
        {
            return folderName;
        }

        public int GetPrivelegeID()
        {
            return privelegeID;
        }

        public bool SetFolderID(int folderID)
        {
            try
            {
                this.folderID = folderID;
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

        public bool SetFolderName(string folderName)
        {
            try
            {
                this.folderName = folderName;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetPrivelegeID(int privelegeID)
        {
            try
            {
                this.privelegeID = privelegeID;
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
            cmd.CommandText = "SELECT ROW_NUMBER() OVER(ORDER BY folderID) AS No, folderID, lecturerID, courseSectionID, folderName, privilegeID " + 
                              "FROM Folder " + 
                              "ORDER BY No";
            DataSet ds = db.GetDataSet(cmd, "Folder");
            return ds;
        }

        //based on lecturer id and course section id
        public DataSet GetFolderDisplay(string LecturerID, string CourseSectionID)
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT ROW_NUMBER() OVER(ORDER BY ID) AS No, folderID, lecturerID, courseSectionID, folderName, privilegeID " + 
                              "FROM Folder " + 
                              "WHERE lecturerID='" + LecturerID + "' AND courseSectionID='" + CourseSectionID + "' " + 
                              "ORDER BY No";
            DataSet ds = db.GetDataSet(cmd, "Folder");
            return ds;
        }

        //based course section ID only
        public DataSet GetFolderDisplay(string CourseSectionID)
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT ROW_NUMBER() OVER(ORDER BY ID) AS No, ID, Folder.LecturerID 'LecturerID', name, CourseSectionID, FolderName, " + 
                                     "CASE WHEN PrivilegeID=1 THEN 'Open' " +
                                          "WHEN PrivilegeID=0 THEN 'Lock' " +
                                     "END 'Privilege' " + 
                              "FROM Folder " + 
                              "INNER JOIN Lecturer ON Lecturer.lecturerID=Folder.lecturerID " + 
                              "WHERE courseSectionID='" + CourseSectionID + "' " + 
                              "ORDER BY No";
            DataSet ds = db.GetDataSet(cmd, "Folder");
            return ds;
        }

        public DataSet GetFolderBasedCourseSection(string StudentID, string CourseSectionID)
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT *, ROW_NUMBER() OVER(ORDER BY F.folderID) AS No, F.folderID 'ID', F.lecturerID 'LecturerID', F.courseSectionID 'CourseSectionID', F.folderName 'FolderName', F.privilegeID 'PrivilegeID', " + 
                                      "CASE WHEN PrivilegeID=1 THEN 'Open' " +
                                           "WHEN PrivilegeID=0 THEN 'Lock' " +
                                      "END 'Privilege', L.name 'name' " + 
                              "FROM Folder F " + 
                              "INNER JOIN Lecturer L ON L.lecturerID=F.lecturerID " + 
                              "WHERE F.courseSectionID='" + CourseSectionID + "'";
            DataSet ds = db.GetDataSet(cmd, "Folder");
            return ds;
        }

        public string GetFolderNameBasedID(string folderID)
        {
            try
            {
                if (this.SetFolderID(Convert.ToInt32(folderID)))
                {
                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "SELECT FolderName FROM Folder " +
                                      "WHERE folderID='" + this.GetFolderID() + "'";
                    string reader = db.GetStringScalar(cmd);
                    return reader;
                }
                else
                {
                    return "unknown folder";
                }
            }
            catch
            {
                return "unknown folder";
            }
        }

        public bool Add(string folderName)
        {
            try
            {
                if (this.SetLecturerID(Convert.ToInt32(context.Session["ID"].ToString())) && this.SetCourseSectionID(Convert.ToInt32(context.Session["CourseSectionID"].ToString())) && this.SetFolderName(folderName))
                {
                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "INSERT INTO Folder(LecturerID, CourseSectionID, FolderName, PrivilegeID) " +
                                      "VALUES(@P1, @P2, @P3, @P4)";
                    cmd.Parameters.AddWithValue("@P1", this.GetLecturerID());
                    cmd.Parameters.AddWithValue("@P2", this.GetCourseSectionID());
                    cmd.Parameters.AddWithValue("@P3", this.GetFolderName());
                    cmd.Parameters.AddWithValue("@P4", 0);
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

        public bool UpdateFolder(string folderID, string folderName, string privelegeID)
        {

            try
            {
                if (this.SetFolderID(Convert.ToInt32(folderID)) && this.SetFolderName(folderName) && this.SetPrivelegeID(Convert.ToInt32(privelegeID)))
                {
                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "UPDATE Folder " +
                                      "SET folderName ='" + this.GetFolderName() + "', privilegeID ='" + this.GetPrivelegeID() + "' " +
                                      "WHERE folderID = '" + this.GetFolderID() + "'";
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

        public bool DeleteFolder(string folderID)
        {
            try
            {
                DatabaseManager db = new DatabaseManager();
                if (this.SetFolderID(Convert.ToInt32(folderID)))
                {

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = "DELETE FROM Folder WHERE folderID ='" + this.GetFolderID() + "'";
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