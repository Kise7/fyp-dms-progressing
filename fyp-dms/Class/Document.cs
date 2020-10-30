using fyp_dms.DB;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace fyp_dms.Class
{
    public class Document
    {
        private int documentID;
        private int folderID;
        private string title;
        private string fileName;
        private string type;
        private string createdDate;

        private MySqlCommand cmd = null;
        HttpContext context = HttpContext.Current;  //session

        public Document()
        {

        }

        public int GetDocumentID()
        {
            return documentID;
        }

        public int GetFolderID()
        {
            return folderID;
        }

        public string GetTitle()
        {
            return title;
        }

        public string GetFilename()
        {
            return fileName;
        }

        public string GetFileType()
        {
            return type;
        }

        public string GetCreatedDate()
        {
            return createdDate;
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

        public bool SetFilename(string fileName)
        {
            try
            {
                this.fileName = fileName;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetType(string type)
        {
            try
            {
                this.type = type;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetCreatedDate(string createdDate)
        {
            try
            {
                this.createdDate = createdDate;
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Lecturer Document
        public DataSet GetDisplayBasedFolderIDForLecturer(string folderID)
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT ROW_NUMBER() OVER(ORDER BY documentID) AS No, Document.documentID 'ID', folderID, title, fileName, " +
                                     "Concat(fileName,Concat('.', fileType)) AS File, Concat(title,Concat('.', fileType)) AS DisplayName, createdDate, " +
                                     "CASE WHEN LecturerBookmark.lecturerBookmarkID IS NULL THEN 'Add Bookmark' " +
                                                                                  "ELSE 'Cancel Bookmark' " +
                                     "END AS Bookmark " +
                              "FROM Document " +
                              "LEFT JOIN LecturerBookmark ON LecturerBookmark.documentID = Document.documentID " +
                              "WHERE folderID='" + folderID + "'";
            DataSet ds = db.GetDataSet(cmd, "Document");
            return ds;
        }

        //Lecturer Document
        public DataSet GetDisplayBasedFolderIDForLecturer(string folderID, string parameter)
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT ROW_NUMBER() OVER(ORDER BY documentID) AS No, Document.documentID 'ID', folderID, title, fileName, " +
                                     "Concat(filename,Concat('.',fileType)) AS File, fileName, Concat(Title,Concat('.',FileType)) AS DisplayName, " +
                                     "created_date, CASE WHEN LecturerBookmark.lecturerBookmarkID IS NULL THEN 'Add Bookmark' " +
                                                                                                       "ELSE 'Cancel Bookmark' " +
                                                    "END AS Bookmark " +
                              "FROM Document " +
                              "LEFT JOIN LecturerBookmark ON LecturerBookmark.documentID = Document.documentID " +
                              "WHERE folderID='" + folderID + "' AND title LIKE '%" + parameter + "%'";
            DataSet ds = db.GetDataSet(cmd, "Document");
            return ds;
        }

        //Student Document
        public DataSet GetDisplayBasedFolderIDForStudent(string folderID)
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT ROW_NUMBER() OVER(ORDER BY Document.documentID) AS No, Document.documentID 'ID', folderID, title, fileName, " +
                                     "Concat(fileName,Concat('.', type)) AS File, Concat(title,Concat('.', type)) AS DisplayName, createdDate, " +
                                     "CASE WHEN StudentBookmark.studentBookmarkID IS NULL THEN 'Add Bookmark' " +
                                                                                  "ELSE 'Cancel Bookmark' " +
                                     "END AS Bookmark " + 
                              "FROM Document " +
                              "LEFT JOIN StudentBookmark ON StudentBookmark.documentID = Document.documentID " + 
                              "WHERE Document.folderID='" + folderID + "'";
            DataSet ds = db.GetDataSet(cmd, "Document");
            return ds;
        }

        //Student Document
        public DataSet GetDisplayBasedFolderIDForStudent(string folderID, string parameter)
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT ROW_NUMBER() OVER(ORDER BY documentID) AS No, Document.documentID 'ID', folderID, title, fileName, " +
                                     "Concat(filename,Concat('.',type)) AS File, fileName, Concat(Title,Concat('.',FileType)) AS DisplayName, " +
                                     "created_date, CASE WHEN StudentBookmark.studentBookmarkID IS NULL THEN 'Add Bookmark' " +
                                                                                                       "ELSE 'Cancel Bookmark' " +
                                                    "END AS Bookmark " +
                              "FROM Document " +
                              "LEFT JOIN StudentBookmark ON StudentBookmark.documentID = Document.documentID " +
                              "WHERE folderID='" + folderID + "' AND title LIKE '%" + parameter + "%'";
            DataSet ds = db.GetDataSet(cmd, "Document");
            return ds;
        }

        public DataSet GetDisplayBasedStudentCourseID(string FolderID)
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT ROW_NUMBER() OVER(ORDER BY documentID) AS No, documentID, folderID, title, fileName, " +
                                      "Concat(fileName, Concat('.',fileType)) AS File, created_date " +
                              "FROM Document " +
                              "WHERE folderID='" + FolderID + "'";
            DataSet ds = db.GetDataSet(cmd, "Document");
            return ds;
        }//no yet handle

        //search document
        public string GetTitleBasedID(string documentID)
        {
            try
            {
                if (this.SetDocumentID(Convert.ToInt32(documentID)))
                {
                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "SELECT title " +
                                      "FROM Document " +
                                      "WHERE documentID='" + this.GetDocumentID() + "'";
                    string reader = db.GetStringScalar(cmd);
                    return reader;
                }
                else
                {
                    return "error";
                }
            }
            catch
            {
                return "error";
            }
        }

        public bool Add(string fileName, string extension)
        {
            try
            {
                if (this.SetFolderID(Convert.ToInt32(context.Session["FolderID"].ToString())) && this.SetTitle(fileName) && this.SetType(extension.Substring(extension.LastIndexOf(".") + 1)))
                {
                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "INSERT INTO Document(folderID, title, filename, fileType, created_Date) " +
                                      "VALUES(@P1, @P2, @P3, @P4, now())";
                    cmd.Parameters.AddWithValue("@P1", this.GetFolderID());
                    cmd.Parameters.AddWithValue("@P2", this.GetTitle());

                    //file
                    DateTime now = DateTime.Now;
                    SHA256 sha256Hash = SHA256.Create();
                    byte[] sourceBytes = Encoding.UTF8.GetBytes(now.ToString() + context.Session["ID"].ToString());
                    byte[] hashBytes = sha256Hash.ComputeHash(sourceBytes);
                    string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                    this.SetFilename(hash);
                    cmd.Parameters.AddWithValue("@P3", hash);
                    cmd.Parameters.AddWithValue("@P4", this.GetFileType());

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

        public bool UpdateDocument(string id, string title, string extension)//with file
        {
            try
            {
                if (this.SetDocumentID(Convert.ToInt32(id)) && this.SetTitle(title) && this.SetType(extension.Substring(extension.LastIndexOf(".") + 1)))
                {
                    //file
                    DateTime now = DateTime.Now;
                    SHA256 sha256Hash = SHA256.Create();
                    byte[] sourceBytes = Encoding.UTF8.GetBytes(now.ToString() + context.Session["ID"].ToString());
                    byte[] hashBytes = sha256Hash.ComputeHash(sourceBytes);
                    string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                    this.SetFilename(hash);

                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "UPDATE Document " +
                                      "SET title ='" + this.GetTitle() + "', fileName ='" + this.GetFilename() + "', fileType ='" + this.GetFileType() + "' " +
                                      "WHERE documentID = '" + this.GetDocumentID() + "'";
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

        public bool UpdateWithoutDocument(string id, string title)//with file
        {

            try
            {
                if (this.SetDocumentID(Convert.ToInt32(id)) && this.SetTitle(title))
                {
                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "UPDATE Document " +
                                      "SET title ='" + this.GetTitle() + "' " +
                                      "WHERE documentID = '" + this.GetDocumentID() + "'";
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

        public bool DeleteDocument(string id)
        {
            try
            {
                DatabaseManager db = new DatabaseManager();
                if (this.SetDocumentID(Convert.ToInt32(id)))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = "DELETE FROM Document WHERE documentID ='" + this.GetDocumentID() + "'";
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