using fyp_dms.DB;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace fyp_dms.Class
{
    public class StudentBookmark
    {
        private int studentBookmarkID;
        private string studentID;
        private int documentID;
        private string tag;

        private MySqlCommand cmd = null;
        HttpContext context = HttpContext.Current;  //session

        public StudentBookmark()
        {

        }
        public int GetStudentBookmarkID()
        {
            return studentBookmarkID;
        }

        public string GetStudentID()
        {
            return studentID;
        }

        public int GetDocumentID()
        {
            return documentID;
        }

        public string GetTag()
        {
            return tag;
        }

        public bool SetStudentBookmarkID(int studentBookmarkID)
        {
            try
            {
                this.studentBookmarkID = studentBookmarkID;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetStudentID(string studentID)
        {
            try
            {
                this.studentID = studentID;
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

        public DataSet GetDisplay(string studentID)
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT *, ROW_NUMBER() OVER(ORDER BY B.studentBookmarkID) AS No, B.studentBookmarkID 'ID', Concat(fileName,Concat('.',type)) AS File " +
                              "FROM StudentBookmark B " + 
                              "INNER JOIN Document D ON D.documentID=B.documentID " + 
                              "INNER JOIN Folder F ON F.folderID=D.folderID " + 
                              "WHERE B.studentID='" + studentID + "' " +
                              "ORDER BY No";
            DataSet ds = db.GetDataSet(cmd, "StudentBookmark");
            return ds;
        }

        public DataSet GetDisplay(string studentID, string searchItem)
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT *, ROW_NUMBER() OVER(ORDER BY B.studentBookmarkIDID) AS No, B.studentBookmarkID 'ID', Concat(fileName,Concat('.', type)) AS File " +
                              "FROM StudentBookmark B " + 
                              "INNER JOIN Document D ON D.documentID=B.documentID " + 
                              "INNER JOIN Folder F ON F.folderID=D.folderID " + 
                              "WHERE B.studentID='" + studentID + "' AND folderName LIKE '%" + searchItem + "%' OR title LIKE '%" + searchItem + "%' AND B.studentID='" + studentID + "' " + 
                              "ORDER BY No";
            DataSet ds = db.GetDataSet(cmd, "StudentBookmark");
            return ds;
        }

        public DataSet GetDisplay(string studentID, string parameter, string order)
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT *, ROW_NUMBER() OVER(ORDER BY " + parameter + ") AS No, B.studentBookmarkID 'ID', Concat(fileName,Concat('.',type)) AS File " +
                              "FROM StudentBookmark B " + 
                              "INNER JOIN Document D ON D.documentID=B.documentID " + 
                              "INNER JOIN Folder F ON F.folderID=D.folderID " + 
                              "WHERE B.studentID='" + studentID + "' " + 
                              "ORDER BY " + parameter + " " + order;
            DataSet ds = db.GetDataSet(cmd, "StudentBookmark");
            return ds;
        }


        public bool Cancel(string documentID, string studentID)
        {
            try
            {
                if (this.SetStudentID(studentID) && this.SetDocumentID(Convert.ToInt32(documentID)))
                {
                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "SELECT COUNT(*) FROM StudentBookmark WHERE studentID='" + this.GetStudentID() + "' AND documentID='" + this.GetDocumentID() + "'";

                    int reader = db.GetIntScalar(cmd);

                    if (reader == 1)
                    {
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.CommandText = "DELETE FROM StudentBookmark WHERE studentID='" + this.GetStudentID() + "' AND documentID='" + this.GetDocumentID() + "'";
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

        public bool Add(string studentBookmarkID, string studentID)
        {
            try
            {
                if (this.SetDocumentID(Convert.ToInt32(studentBookmarkID)) && this.SetStudentID(studentID))
                {

                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "INSERT INTO StudentBookmark(studentID, documentID, tag) " +
                                      "VALUES(@P1, @P2, @P3)";
                    cmd.Parameters.AddWithValue("@P1", this.GetStudentID());
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



        public bool DeleteBookmark(string studentBookmarkID)
        {
            try
            {
                DatabaseManager db = new DatabaseManager();
                if (this.SetStudentBookmarkID(Convert.ToInt32(studentBookmarkID)))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = "DELETE FROM StudentBookmark WHERE studentBookmarkID =" + this.GetStudentBookmarkID();    //delete with the studentBookmark ID
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

        public bool Update(string studentBookmarkID, string tag)
        {

            try
            {
                if (this.SetStudentBookmarkID(Convert.ToInt32(studentBookmarkID)) && this.SetTag(tag))
                {
                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "UPDATE StudentBookmark " +
                                      "SET tag ='" + this.GetTag() + "' " +
                                      "WHERE studentBookmarkID ='" + this.GetStudentBookmarkID() + "'";
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