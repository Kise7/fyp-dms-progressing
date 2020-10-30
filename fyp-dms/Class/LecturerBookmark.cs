using fyp_dms.DB;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace fyp_dms.Class
{
    public class LecturerBookmark
    {
        private int lecturerBookmarkID;
        private string lecturerID;
        private int documentID;
        private string tag;

        private MySqlCommand cmd = null;
        HttpContext context = HttpContext.Current;  //session

        public LecturerBookmark()
        {

        }
        public int GetLecturerBookmarkID()
        {
            return lecturerBookmarkID;
        }

        public string GetLecturerID()
        {
            return lecturerID;
        }

        public int GetDocumentID()
        {
            return documentID;
        }

        public string GetTag()
        {
            return tag;
        }

        public bool SetLecturerBookmarkID(int lecturerBookmarkID)
        {
            try
            {
                this.lecturerBookmarkID = lecturerBookmarkID;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetLecturerID(string lecturerID)
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

        public bool SetDocumentID(int documentID)
        {
            try
            {
                this.documentID = documentID;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetTag(string tag)
        {
            try
            {
                this.tag = tag;
                return true;
            }
            catch
            {
                return false;
            }

        }

        public DataSet GetDisplay(string lecturerID)
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT *, ROW_NUMBER() OVER(ORDER BY B.lecturerBookmarkID) AS No, B.lecturerBookmarkID 'ID', Concat(fileName,Concat('.',fileType)) AS File " +
                              "FROM LecturerBookmark B " +
                              "INNER JOIN Document D ON D.documentID=B.documentID " +
                              "INNER JOIN Folder F ON F.folderID=D.folderID " +
                              "WHERE B.lecturerID='" + lecturerID + "' " +
                              "ORDER BY No";
            DataSet ds = db.GetDataSet(cmd, "LecturerBookmark");
            return ds;
        }

        public DataSet GetDisplay(string lecturerID, string searchItem)
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT *, ROW_NUMBER() OVER(ORDER BY B.lecturerBookmarkID) AS No, B.lecturerBookmarkID 'ID', Concat(fileName,Concat('.', fileType)) AS File " +
                              "FROM LecturerBookmark B " +
                              "INNER JOIN Document D ON D.documentID=B.documentID " +
                              "INNER JOIN Folder F ON F.folderID=D.folderID " +
                              "WHERE B.lecturerID='" + lecturerID + "' AND folderName LIKE '%" + searchItem + "%' OR title LIKE '%" + searchItem + "%' AND B.lecturerID='" + lecturerID + "' " +
                              "ORDER BY No";
            DataSet ds = db.GetDataSet(cmd, "LecturerBookmark");
            return ds;
        }

        public DataSet GetDisplay(string lecturerID, string parameter, string order)
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT *, ROW_NUMBER() OVER(ORDER BY " + parameter + ") AS No, B.lecturerBookmarkID 'ID', Concat(fileName,Concat('.',fileType)) AS File " +
                              "FROM LecturerBookmark B " +
                              "INNER JOIN Document D ON D.documentID=B.documentID " +
                              "INNER JOIN Folder F ON F.folderID=D.folderID " +
                              "WHERE B.lecturerID='" + lecturerID + "' " +
                              "ORDER BY " + parameter + " " + order;
            DataSet ds = db.GetDataSet(cmd, "LecturerBookmark");
            return ds;
        }


        public bool Cancel(string documentID, string lecturerID)
        {
            try
            {
                if (this.SetLecturerID(lecturerID) && this.SetDocumentID(Convert.ToInt32(documentID)))
                {
                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "SELECT COUNT(*) FROM LecturerBookmark WHERE lecturerID='" + this.GetLecturerID() + "' AND documentID='" + this.GetDocumentID() + "'";

                    int reader = db.GetIntScalar(cmd);

                    if (reader == 1)
                    {
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.CommandText = "DELETE FROM LecturerBookmark WHERE lecturerID='" + this.GetLecturerID() + "' AND documentID='" + this.GetDocumentID() + "'";
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

        public bool Add(string lecturerBookmarkID, string lecturerID)
        {
            try
            {
                if (this.SetDocumentID(Convert.ToInt32(lecturerBookmarkID)) && this.SetLecturerID(lecturerID))
                {

                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "INSERT INTO LecturerBookmark(lecturerID, documentID, tag) " +
                                      "VALUES(@P1, @P2, @P3)";
                    cmd.Parameters.AddWithValue("@P1", this.GetLecturerID());
                    cmd.Parameters.AddWithValue("@P2", this.GetDocumentID());
                    cmd.Parameters.AddWithValue("@P3", "");
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



        public bool DeleteBookmark(string lecturerBookmarkID)
        {
            try
            {
                DatabaseManager db = new DatabaseManager();
                if (this.SetLecturerBookmarkID(Convert.ToInt32(lecturerBookmarkID)))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = "DELETE FROM LecturerBookmark WHERE lecturerBookmarkID =" + this.GetLecturerBookmarkID();    //delete with the LecturerBookmark ID
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

        public bool Update(string lecturerBookmarkID, string tag)
        {

            try
            {
                if (this.SetLecturerBookmarkID(Convert.ToInt32(lecturerBookmarkID)) && this.SetTag(tag))
                {
                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "UPDATE LecturerBookmark " +
                                      "SET tag ='" + this.GetTag() + "' " +
                                      "WHERE lecturerBookmarkID ='" + this.GetLecturerBookmarkID() + "'";
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