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
    public class Workflow
    {
        private int workflowID;
        private int lecturerID;
        private int receiptNo;
        private string amount;
        private string claimType;
        private string createDate;
        private string receiptDate;
        private string hour;
        private string reason;
        private int adminID;
        private string status;     //boolean
        private string fileName;
        private string fileType;

        private MySqlCommand cmd = null;
        HttpContext context = HttpContext.Current;

        public Workflow()
        {

        }

        public int GetWorkflowID()
        {
            return workflowID;
        }
        
        public int GetLecturerID()
        {
            return lecturerID;
        }
        
        public int GetReceiptNo()
        {
            return receiptNo;
        }
        
        public string GetAmount()
        {
            return amount;
        }
        
        public string GetClaimType()
        {
            return claimType;
        }
        
        public string GetCreateDate()
        {
            return createDate;
        }
        
        public string GetReceiptDate()
        {
            return receiptDate;
        }
        
        public string GetHour()
        {
            return hour;
        }
        
        public string GetReason()
        {
            return reason;
        }
        
        public int GetAdminID()
        {
            return adminID;
        }
        
        public string GetStatus()
        {
            return status;
        }
        
        public string GetFilename()
        {
            return fileName;
        }
        
        public string GetFiletype()
        {
            return fileType;
        }
        
        public bool SetWorkflowID(int workflowID)
        {
            try
            {
                this.workflowID = workflowID;
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
        
        public bool SetReceiptNo(int receiptNo)
        {
            try
            {
                this.receiptNo = receiptNo;
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public bool SetAmount(string amount)
        {
            try
            {
                this.amount = amount;
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public bool SetClaimType(string claimType)
        {
            try
            {
                this.claimType = claimType;
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

        public bool SetReceiptDate(string receiptDate)
        {
            try
            {
                this.receiptDate = receiptDate;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetHour(string hour)
        {
            try
            {
                this.hour = hour;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetReason(string reason)
        {
            try
            {
                this.reason = reason;
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

        public bool SetStatus(string status)
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
        public bool SetFilename(string value)
        {
            try
            {
                fileName = value;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SetFiletype(string value)
        {
            try
            {
                fileType = value;
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
            //cmd.CommandText = "SELECT * FROM Workflow";
            cmd.CommandText = "SELECT *, Concat(fileName,Concat('.',fileType)) AS File, " + 
                                     "CASE WHEN status IS NULL THEN 'Pending' " +
                                          "WHEN status=TRUE THEN 'Approved' " +
                                          "WHEN status=FALSE THEN 'Rejected' " +
                                          "Else 'Nothing' " +
                                     "END 'Condition' " +
                              "FROM Workflow " + 
                              "ORDER BY workflowID DESC";
            DataSet ds = db.GetDataSet(cmd, "Workflow");
            return ds;
        }

        public DataSet GetDisplay(string lecturerID)
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT *, Concat(fileName,Concat('.',fileType)) AS File, " + 
                                     "CASE WHEN status IS NULL THEN 'Pending' " +
                                          "WHEN status=TRUE THEN 'Approved' " +
                                          "WHEN status=FALSE THEN 'Rejected' " +
                                          "Else 'Nothing' " +
                                     "END 'Condition' " +
                              "FROM Workflow " + 
                              "WHERE lecturerID='" + lecturerID + "' AND status IS NULL " + 
                              "ORDER BY workflowID DESC";
            DataSet ds = db.GetDataSet(cmd, "Workflow");
            return ds;
        }

        public DataSet GetDisplay(string lecturerID, string year, string month)
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT *, Concat(Filename,Concat('.',FileType)) AS File, " + 
                                    "CASE WHEN status IS NULL THEN 'Pending' " +
                                         "WHEN status=TRUE THEN 'Approved' " +
                                         "WHEN status=FALSE THEN 'Rejected' " +
                                         "Else 'Nothing' " +
                                    "END 'Condition' " +
                              "FROM Workflow " + 
                              "WHERE lecturerID='" + lecturerID + "' AND status='1' AND Year(receiptDate)='" + year + "' AND Month(receiptDate)='" + month + "' " +
                              "ORDER BY receiptDate ASC";
            DataSet ds = db.GetDataSet(cmd, "Workflow");
            return ds;
        }

        public string GetMonthTotal(string lecturerID, string year, string month)
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT SUM(amount) 'Total' " +
                              "FROM Workflow " + 
                              "WHERE lecturerID='" + lecturerID + "' AND status='1' AND Year(receiptDate)='" + year + "' AND Month(receiptDate)='" + month + "' " +
                              "ORDER BY receiptDate ASC";
            string ds = db.GetStringScalar(cmd);
            return ds;
        }

        public DataSet GetDisplay(string lecturerID, string parameter)
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT *, Concat(fileName,Concat('.',fileType)) AS File, " + 
                                     "CASE WHEN status IS NULL THEN 'Pending' " +
                                          "WHEN status=TRUE THEN 'Approved' " +
                                          "WHEN status=FALSE THEN 'Rejected' " +
                                          "Else 'Nothing' " +
                                     "END 'Condition' " +
                              "FROM Workflow " + 
                              "WHERE lecturerID='" + lecturerID + "' AND status IS NULL AND receiptNo LIKE '%" + parameter + "%' " + 
                              "ORDER BY workflowID DESC";
            DataSet ds = db.GetDataSet(cmd, "Workflow");
            return ds;
        }

        public DataSet GetHistory(string lecturerID)
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT *, Concat(filename,Concat('.',fileType)) AS File, " + 
                                     "CASE WHEN status IS NULL THEN 'Pending' " +
                                          "WHEN status=TRUE THEN 'Approved' " +
                                          "WHEN Status=FALSE THEN 'Rejected' " +
                                          "Else 'Nothing' " +
                                     "END 'Condition' " +
                              "FROM Workflow " + 
                              "WHERE lecturerID='" + lecturerID + "' AND Status IS NOT NULL " + 
                              "ORDER BY workflowID DESC";
            DataSet ds = db.GetDataSet(cmd, "Workflow");
            return ds;
        }

        public DataSet GetHistory(string lecturerid, string parameter)
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT *, Concat(fileName,Concat('.',fileType)) AS File, " + 
                                      "CASE WHEN status IS NULL THEN 'Pending' " + 
                                           "WHEN status=TRUE THEN 'Approved' " +
                                           "WHEN status=FALSE THEN 'Rejected' " +
                                           "Else 'Nothing' " +
                                      "END 'Condition' " +
                              "FROM Workflow " + 
                              "WHERE lecturerID='" + lecturerid + "' AND status IS NOT NULL AND receiptNo LIKE '%" + parameter + "%' " + 
                              "ORDER BY workflowID DESC";
            DataSet ds = db.GetDataSet(cmd, "Workflow");
            return ds;
        }

        public bool Add(string LecturerID, string ClaimType, string Receiptno, string ReceiptDate, string Amount, string Hour, string filename)
        {
            try
            {
                if (this.SetLecturerID(Convert.ToInt32(LecturerID)) && this.SetClaimType(ClaimType) && this.SetReceiptNo(Convert.ToInt32(Receiptno)) && this.SetReceiptDate(ReceiptDate) && this.SetAmount(Amount) && this.SetHour(Hour) && this.SetFiletype(filename.Substring(filename.LastIndexOf(".") + 1)))
                {

                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "INSERT INTO Workflow(lecturerID, receiptNo, amount, claimType, created_date, receiptDate, hour, fileName, fileType) " +
                                      "VALUES(@P1, @P2, @P3, @P4, now(), @P6, @P7, @P8, @P9)";
                    cmd.Parameters.AddWithValue("@P1", this.GetLecturerID());
                    cmd.Parameters.AddWithValue("@P2", this.GetReceiptNo());
                    cmd.Parameters.AddWithValue("@P3", this.GetAmount());
                    cmd.Parameters.AddWithValue("@P4", this.GetClaimType());
                    cmd.Parameters.AddWithValue("@P6", this.GetReceiptDate());
                    cmd.Parameters.AddWithValue("@P7", this.GetHour());

                    //receipt file upload
                    DateTime now = DateTime.Now;
                    SHA256 sha256Hash = SHA256.Create();
                    byte[] sourceBytes = Encoding.UTF8.GetBytes(now.ToString() + context.Session["ID"].ToString());
                    byte[] hashBytes = sha256Hash.ComputeHash(sourceBytes);
                    string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                    this.SetFilename(hash);
                    cmd.Parameters.AddWithValue("@P8", this.GetFilename());
                    cmd.Parameters.AddWithValue("@P9", this.GetFiletype());
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

        public bool Update(string ID, string adminID, string Status, string Reason)
        {
            try
            {
                if (this.SetWorkflowID(Convert.ToInt32(ID)) && this.SetAdminID(Convert.ToInt32(adminID)) && this.SetStatus(Status) && this.SetReason(Reason))
                {
                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "UPDATE Workflow " +
                                      "SET adminID ='" + this.GetAdminID() + "', status ='" + this.GetStatus() + "', reason ='" + this.GetReason() + "' " +
                                      "WHERE workflowID ='" + this.GetWorkflowID() + "'";
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