using fyp_dms.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fyp_dms.Web
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            TextBox txtTempUsername = this.TxtID;
            TextBox txtTempPassword = this.TxtPassword;


            if (txtTempUsername.Text.Trim() == "" || txtTempPassword.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "errorNoIDPasswordAlert();", true);
            }
            else if (RadioButton1.Checked)
            {
                Admin admin = new Admin();
                if (admin.SelectAdminLogin(txtTempUsername.Text, txtTempPassword.Text, checkbox.Text) == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "errorAdminAlert();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successAdminAlert();", true);
                }
            }
            else if (RadioButton2.Checked)
            {
                Lecturer lecturer = new Lecturer();
                if (lecturer.SelectLecturerLogin(txtTempUsername.Text, txtTempPassword.Text, checkbox.Text) == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "errorLecturerAlert();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successLecturerAlert();", true);
                }
            }
            else if (RadioButton3.Checked)
            {
                Student student = new Student();

                if (student.SelectStudentLogin(txtTempUsername.Text, txtTempPassword.Text, checkbox.Text) == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "errorStudentAlert();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successStudentAlert();", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "errorNoRadioBox();", true);
            }
        }
    }
}