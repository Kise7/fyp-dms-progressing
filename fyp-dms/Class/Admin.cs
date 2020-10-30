using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections;
using fyp_dms.DB;

namespace fyp_dms.Class
{
    public class Admin
    {
        private int adminID;
        private string name;
        private string password;

        private MySqlCommand cmd = null;
        HttpContext context = HttpContext.Current;  //session

        public Admin()
        {

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

        public DataSet GetDisplay()
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();

            cmd.CommandText = "SELECT * FROM Admin ORDER BY 1";

            DataSet ds = db.GetDataSet(cmd, "Admin");

            return ds;
        }

        public DataSet GetProfile(string adminid)
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM Admin WHERE adminID='" + adminid + "'";
            DataSet ds = db.GetDataSet(cmd, "Admin");
            return ds;
        }

        public bool Add(string adminID, string name)
        {
            try
            {
                if (this.SetAdminID(Convert.ToInt32(adminID)) && this.SetName(name))
                {

                    DatabaseManager db = new DatabaseManager();

                    cmd = new MySqlCommand();
                    cmd.CommandText = "INSERT INTO Admin(adminID, password, adminName) " +
                                      "VALUES(@P1, @P2, @P3)";
                    cmd.Parameters.AddWithValue("@P1", this.GetAdminID());
                    cmd.Parameters.AddWithValue("@P2", "holdDoc123");
                    cmd.Parameters.AddWithValue("@P3", this.GetName());
                    
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

        public bool UpdateName(string adminID, string name)
        {
            try
            {
                if (this.SetAdminID(Convert.ToInt32(adminID)) && this.SetName(name))
                {
                    DatabaseManager db = new DatabaseManager();

                    cmd = new MySqlCommand();
                    cmd.CommandText = "UPDATE Admin " +
                                      "SET adminName ='" + this.GetName() + "' " +
                                      "WHERE adminID ='" + this.GetAdminID() + "'";

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

        public bool UpdatePasswordAndName(string adminID, string name, string oldpassword, string newpassword)
        {
            try
            {
                if (this.SetAdminID(Convert.ToInt32(adminID)) && this.SetName(name) && this.SetPassword(newpassword))
                {
                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "SELECT COUNT(*) FROM Admin WHERE adminID='" + this.GetAdminID() + "' AND password='" + oldpassword + "'";
                    int reader = db.GetIntScalar(cmd);
                    if (reader == 1)
                    {
                        cmd = new MySqlCommand();
                        cmd.CommandText = "UPDATE Admin " +
                                          "SET adminName ='" + this.GetName() + "', password ='" + this.GetPassword() + "'" +
                                          " WHERE adminID='" + this.GetAdminID() + "'";
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

        public bool Update(string adminID, string name)
        {

            try
            {
                if (this.SetAdminID(Convert.ToInt32(adminID)) && this.SetName(name))
                {
                    DatabaseManager db = new DatabaseManager();

                    cmd = new MySqlCommand();
                    cmd.CommandText = "UPDATE Admin " +
                                      "SET adminName ='" + this.GetName() + "' " +
                                      "WHERE adminID ='" + this.GetAdminID() + "'";

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

        public bool DeleteAdmin(string adminID)
        {
            try
            {
                DatabaseManager db = new DatabaseManager();

                if (this.SetAdminID(Convert.ToInt32(adminID)))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = "DELETE FROM Admin WHERE adminID =" + this.GetAdminID(); //delete with the adminID

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

        public bool SelectAdminLogin(string adminID, string password, string checkbox)
        {

            try
            {
                if (this.SetAdminID(Convert.ToInt32(adminID)) && this.SetPassword(password))
                {

                    DatabaseManager db = new DatabaseManager();

                    cmd = new MySqlCommand();
                    cmd.CommandText = "SELECT COUNT(*) FROM Admin WHERE adminID=@AdminID AND password=@Password"; //authenticate
                    cmd.Parameters.AddWithValue("@AdminID", this.GetAdminID());
                    cmd.Parameters.AddWithValue("@Password", this.GetPassword());

                    int reader = Convert.ToInt32(db.GetDataScalar(cmd));
                    reader = 1;

                    if (reader == 1)
                    {
                        cmd = new MySqlCommand();
                        cmd.CommandText = "SELECT adminName FROM Admin WHERE adminID=@AdminID AND password=@Password";
                        cmd.Parameters.AddWithValue("@AdminID", this.GetAdminID());
                        cmd.Parameters.AddWithValue("@Password", this.GetPassword());
                        string name = db.GetDataScalar(cmd).ToString();

                        HttpContext context = HttpContext.Current;
                        context.Session["Username"] = name;
                        context.Session["AdminID"] = this.GetAdminID();
                        context.Session["Identity"] = "Admin";
                        context.Session["ID"] = this.GetAdminID();

                        if (checkbox == "Remember Me")
                        {
                            context.Session.Timeout = 60;   // 1 hour
                        }
                        else
                        {
                            context.Session.Timeout = 10;   // 10 mins
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