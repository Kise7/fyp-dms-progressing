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
    public class Student
    {
        private string studentID;
        private int adminID;
        private int yearID;
        private int programmeID;
        private int intakeID;
        private string name;
        private string email;
        private string phoneNo;
        private string password;

        private MySqlCommand cmd = null;
        HttpContext context = HttpContext.Current;

        public Student()
        {

        }

        public string GetStudentID()
        {
            return studentID.ToUpper();
        }

        public int GetAdminID()
        {
            return adminID;
        }

        public int GetYearID()
        {
            return yearID;
        }

        public int GetProgrammeID()
        {
            return programmeID;
        }

        public int GetIntakeID()
        {
            return intakeID;
        }

        public string GetName()
        {
            return name;
        }

        public string GetEmail()
        {
            return email;
        }

        public string GetPassword()
        {
            return password;
        }

        public string GetPhoneNo()
        {
            return phoneNo;
        }

        public bool SetStudentID(string studentID)
        {
            try
            {
                this.studentID = studentID.ToUpper();
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

        public bool SetYearID(int yearID)
        {
            try
            {
                this.yearID = yearID;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetProgrammeID(int programmeID)
        {
            try
            {
                this.programmeID = programmeID;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetIntakeID(int intakeID)
        {
            try
            {
                this.intakeID = intakeID;
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

        public DataSet GetDisplay()
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT S.studentID 'StudentID', S.name 'Name', S.email 'Email', A.adminName 'AdminName', S.phoneNo 'PhoneNo', Y.yearValue 'YearValue', P.programmeName 'ProgrammeName', I.intakeMonth 'IntakeMonth' " + 
                              "FROM STUDENT S " + 
                              "INNER JOIN Admin A ON S.adminID=A.adminID " + 
                              "INNER JOIN Year Y ON S.yearID=Y.yearID " + 
                              "INNER JOIN Programme P ON S.programmeID=P.programmeID " +
                              "INNER JOIN Intake I ON S.intakeID=I.intakeID " +
                              "ORDER BY S.studentID";
            DataSet ds = db.GetDataSet(cmd, "Student");
            return ds;
        }

        public DataSet GetProfile(string studentID)
        {
            DatabaseManager db = new DatabaseManager();
            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM Student " + 
                              "INNER JOIN Year ON Year.yearID=Student.YearID " +
                              "INNER JOIN Programme ON Programme.programmeID=Student.programmeID " + 
                              "INNER JOIN Intake on Intake.intakeID=Student.intakeID " + 
                              "WHERE StudentID='" + studentID + "'";
            DataSet ds = db.GetDataSet(cmd, "Student");
            return ds;
        }

        public bool Add(string studentID, string yearID, string intakeID, string programmeID, string name, string phoneno)
        {
            string studentEmail = GenerateEmail(studentID, name, yearID);

            Year year = new Year();
            string newStudentID = (Convert.ToInt32(year.GetYear(yearID)) / 100) + studentID;

            try
            {
                if (this.SetStudentID(newStudentID.ToUpper()) && this.SetYearID(Convert.ToInt32(yearID)) && this.SetIntakeID(Convert.ToInt32(intakeID)) && this.SetProgrammeID(Convert.ToInt32(programmeID)) && this.SetName(name) && this.SetAdminID(Convert.ToInt32(context.Session["ID"].ToString())) && this.SetEmail(studentEmail + "@student.tarc.edu.my") && this.SetPhoneNo(phoneno))
                {

                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "INSERT INTO Student(studentID, adminID, yearID, programmeID, intakeID, name, email, phoneNo, password) " +
                                      "VALUES(@P1, @P2, @P3, @P4, @P5, @P6, @P7, @P8, @P9)";
                    cmd.Parameters.AddWithValue("@P1", this.GetStudentID());
                    cmd.Parameters.AddWithValue("@P2", this.GetAdminID());
                    cmd.Parameters.AddWithValue("@P3", this.GetYearID());
                    cmd.Parameters.AddWithValue("@P4", this.GetProgrammeID());
                    cmd.Parameters.AddWithValue("@P5", this.GetIntakeID());
                    cmd.Parameters.AddWithValue("@P6", this.GetName());
                    cmd.Parameters.AddWithValue("@P7", this.GetEmail());
                    cmd.Parameters.AddWithValue("@P8", this.GetPhoneNo());
                    cmd.Parameters.AddWithValue("@P9", "holdDoc");
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

        public bool UpdatePhoneNo(string studentID, string phoneNo)
        {
            try
            {
                if (this.SetStudentID(studentID) && this.SetPhoneNo(phoneNo))
                {
                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "UPDATE Student " +
                                      "SET phoneNo ='" + this.GetPhoneNo() + "' " +
                                      "WHERE studentID ='" + this.GetStudentID() + "'";
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

        public bool ResetPassword(string studentID)
        {
            try
            {
                if (this.SetStudentID(studentID))
                {
                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "UPDATE Student " +
                                      "SET Password ='holdDoc' " +
                                      "WHERE studentID ='" + this.GetStudentID() + "'";
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

        public bool UpdatePhoneAndPassword(string studentID, string phoneNo, string oldPassword, string newPassword)
        {
            try
            {
                if (this.SetStudentID(studentID) && this.SetPhoneNo(phoneNo) && this.SetPassword(newPassword))
                {
                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "SELECT COUNT(*) " + 
                                      "FROM Student " + 
                                      "WHERE studentID='" + this.GetStudentID() + "' AND password='" + oldPassword + "'";
                    int reader = db.GetIntScalar(cmd);
                    if (reader == 1)
                    {
                        cmd = new MySqlCommand();
                        cmd.CommandText = "UPDATE STUDENT " +
                                          "SET phoneNo='" + this.GetPhoneNo() + "', password='" + this.GetPassword() + "' " +
                                          "WHERE studentID='" + this.GetStudentID() + "'";
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

        public string GenerateEmail(string studentID, string name, string yearID)
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

            Year yearEntered = new Year();
            int year = Convert.ToInt32(yearEntered.GetYear(yearID));

            newName = surname + removeFirstCharacter.ToString() + "-" + studentID[0] + studentID[1] + (year%100);
            return newName.ToLower();
        }

        public bool Update(string studentID, string yearID, string programmeID, string intakeID, string name, string phoneNo)
        {
            try
            {
                if (this.SetStudentID(studentID) && this.SetYearID(Convert.ToInt32(yearID)) && this.SetIntakeID(Convert.ToInt32(intakeID)) && this.SetProgrammeID(Convert.ToInt32(programmeID)) && this.SetName(name) && this.SetAdminID(Convert.ToInt32(context.Session["ID"].ToString())) && this.SetPhoneNo(phoneNo))
                {
                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "UPDATE Student " +
                                      "SET name ='" + this.GetName() + "', yearID='" + this.GetYearID() + "', programmeID='" + this.GetProgrammeID() + "', intakeID='" + this.GetIntakeID() + "', " + 
                                      "phoneNo ='" + this.GetPhoneNo() + "', adminID ='" + this.GetAdminID() + "' " +
                                      "WHERE studentID ='" + this.GetStudentID() + "'";
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

        public bool DeleteStudent(string studentID)
        {
            try
            {
                DatabaseManager db = new DatabaseManager();
                if (this.SetStudentID(studentID))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = "DELETE FROM Student WHERE studentID ='" + this.GetStudentID() + "'";
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

        public ArrayList SelectYearName()
        {
            ArrayList alTempResult = null;

            try
            {
                DatabaseManager db = new DatabaseManager();
                cmd = new MySqlCommand();
                cmd.CommandText = "SELECT yearID, yearValue FROM Year"; 
                alTempResult = (ArrayList)db.GetArrayList(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return alTempResult;
        }

        public ArrayList SelectYearName(string strKey, string strValue)
        {
            ArrayList groupList = null;
            DatabaseManager db = new DatabaseManager();
            try
            {
                ArrayList arList = SelectYearName();
                groupList = db.GetConcatCode(arList, strKey, strValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return groupList;
        }
        public ArrayList SelectProgrammeID()
        {
            ArrayList alTempResult = null;

            try
            {
                DatabaseManager db = new DatabaseManager();
                cmd = new MySqlCommand();
                cmd.CommandText = "SELECT programmeID, programmeName FROM Programme"; 
                alTempResult = (ArrayList)db.GetArrayList(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return alTempResult;
        }

        public ArrayList SelectProgrammeID(string strKey, string strValue)
        {
            ArrayList groupList = null;
            DatabaseManager db = new DatabaseManager();
            try
            {
                ArrayList arList = SelectProgrammeID();
                groupList = db.GetConcatCode(arList, strKey, strValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return groupList;
        }

        public ArrayList SelectIntake()
        {
            ArrayList alTempResult = null;

            try
            {
                DatabaseManager db = new DatabaseManager();
                cmd = new MySqlCommand();
                cmd.CommandText = "SELECT intakeID, intakeMonth FROM Intake"; 
                alTempResult = (ArrayList)db.GetArrayList(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return alTempResult;
        }

        public ArrayList SelectIntake(string strKey, string strValue)
        {
            ArrayList groupList = null;
            DatabaseManager db = new DatabaseManager();
            try
            {
                ArrayList arList = SelectIntake();
                groupList = db.GetConcatCode(arList, strKey, strValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return groupList;
        }

        public bool SelectStudentLogin(string no, string password, string checkbox)
        {
            try
            {
                if (this.SetStudentID(no) && this.SetPassword(password))
                {

                    DatabaseManager db = new DatabaseManager();
                    cmd = new MySqlCommand();
                    cmd.CommandText = "SELECT COUNT(*) FROM Student WHERE studentID=@StudentID AND password=@Password"; //authenticate
                    cmd.Parameters.AddWithValue("@StudentID", this.GetStudentID());
                    cmd.Parameters.AddWithValue("@Password", this.GetPassword());
                    int reader = Convert.ToInt32(db.GetDataScalar(cmd));
                    reader = 1;
                    if (reader == 1)
                    {
                        cmd = new MySqlCommand();
                        cmd.CommandText = "SELECT name FROM Student WHERE studentID=@StudentID AND password=@Password";
                        cmd.Parameters.AddWithValue("@StudentID", this.GetStudentID());
                        cmd.Parameters.AddWithValue("@Password", this.GetPassword());
                        string name = db.GetDataScalar(cmd).ToString();


                        HttpContext context = HttpContext.Current;
                        context.Session["Username"] = name;
                        context.Session["StudentID"] = this.GetStudentID();
                        context.Session["Identity"] = "Student";
                        context.Session["ID"] = this.GetStudentID();

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