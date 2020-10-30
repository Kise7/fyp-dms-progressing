using System;
using fyp_dms.Class;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.IO;

namespace fyp_dms.Web.Lecturerr
{
    public partial class lecturerProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("~/Web/login.aspx");
            }
            if (!IsPostBack)
            {
                string identity = Session["Identity"].ToString();
                if (String.Equals(identity, "Lecturer"))
                {
                    SetupLecturerProfile();
                }
                else
                {
                    Response.Redirect("~/Web/login.aspx");
                }
            }
        }
        public void SetupLecturerProfile()
        {
            dgLecturerProfile.ShowFooter = true;
            Lecturer lec = new Lecturer();
            string LecturerID = Session["LecturerID"].ToString();
            DataSet ds = lec.GetProfile(LecturerID);
            dgLecturerProfile.DataSource = ds.Tables["Lecturer"];
            dgLecturerProfile.DataBind();
            //Notifications newnotice = new Notifications();
            //string UnreadNotice = newnotice.GetUnreadNotice(LecturerID);
            //BtnNotifications.Text = "Notifications(" + UnreadNotice + ")";
            //BtnNotifications.DataBind();
        }

        protected void dgLecturerProfile_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            dgLecturerProfile.CurrentPageIndex = e.NewPageIndex;
            SetupLecturerProfile();
        }

        protected void dgLecturerProfile_ItemDataBound(object source, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                ((WebControl)e.Item.Cells[1].Controls[0]).CssClass = "btn btn-success";
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                ((WebControl)e.Item.Cells[1].Controls[0]).CssClass = "btn btn-warning";
                ((WebControl)e.Item.Cells[1].Controls[2]).CssClass = "btn btn-secondary";
            }
        }

        protected void dgEdit_LecturerProfile(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgLecturerProfile.EditItemIndex = e.Item.ItemIndex;
            SetupLecturerProfile();
        }
        //yc
        protected void dgCancel_LecturerProfile(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgLecturerProfile.EditItemIndex = -1;
            SetupLecturerProfile();
        }


        protected void dgLecturerProfile_ItemCommand(object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {

        }

        protected void dgUpdate_LecturerProfile(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            Label _ID = (Label)e.Item.Cells[0].FindControl("txtID");
            TextBox _Phone = (TextBox)e.Item.Cells[0].FindControl("txtPhone");
            TextBox _OldPassword = (TextBox)e.Item.Cells[0].FindControl("txtOldPassword");
            TextBox _NewPassword = (TextBox)e.Item.Cells[0].FindControl("txtNewPassword");
            TextBox _ConfirmPassword = (TextBox)e.Item.Cells[0].FindControl("txtConfirmPassword");

            //Validation for name (No digit allowed and only up to 30 characters)
            Regex phoneRegex = new Regex(@"^(01)[0-9]*[0-9]{8,9}$");
            Match checkPhone = phoneRegex.Match(_Phone.Text);

            //Validation for password (The password is not allowed. Password must be 8 characters including at least 1 uppercase letter, 1 lowercase letter, 1 special character, 1 digit and minimum 8 in length
            Regex passwordRegex = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            Match checkPassword = passwordRegex.Match(_NewPassword.Text);

            if (_ID.Text.Trim() == "" || _Phone.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Please fill in the Phone Number!', 'error')</script>'");
            }
            else if (!checkPhone.Success)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'The phone number is not allowed. Phone number is only support 7 or 8 digits', 'error')</script>'");
            }
            else if (_OldPassword.Text == "" && _NewPassword.Text == "" && _ConfirmPassword.Text == "")
            {
                Lecturer lec = new Lecturer();
                if (lec.UpdatePhoneNo(_ID.Text, _Phone.Text) == false)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Unsuccessful update phone!', 'error')</script>'");
                }
                else
                {
                    Server.Transfer("lecturerProfile.aspx");
                }
            }
            else if (!checkPassword.Success)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'The password is not allowed. Password must be 8 characters including at least 1 uppercase letter, 1 lowercase letter, 1 special character, 1 digit and minimum 8 in length', 'error')</script>'");
            }
            else if (_NewPassword.Text != _ConfirmPassword.Text)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Confirm Password and New Password not same!', 'error')</script>'");
            }
            else
            {
                Lecturer lec = new Lecturer();
                if (lec.UpdatePhoneAndPassword(_ID.Text, _Phone.Text, _OldPassword.Text, _NewPassword.Text) == false)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Unsuccessful! Your old password is wrong.', 'error')</script>'");
                }
                else
                {
                    Server.Transfer("lecturerProfile.aspx");
                }
            }
        }

        protected void dgLecturerProfile_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}