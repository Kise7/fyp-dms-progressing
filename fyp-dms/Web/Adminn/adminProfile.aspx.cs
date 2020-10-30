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

namespace fyp_dms.Web.Adminn
{
    public partial class adminProfile : System.Web.UI.Page
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
                if (String.Equals(identity, "Admin"))
                {
                    SetupAdminProfile();
                }
                else
                {
                    Response.Redirect("~/Web/login.aspx");
                }
            }
        }
        public void SetupAdminProfile()
        {
            dgAdminProfile.ShowFooter = true;
            Admin lec = new Admin();
            string AdminID = Session["AdminID"].ToString();
            DataSet ds = lec.GetProfile(AdminID);
            dgAdminProfile.DataSource = ds.Tables["Admin"];
            dgAdminProfile.DataBind();
        }

        protected void dgAdminProfile_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            dgAdminProfile.CurrentPageIndex = e.NewPageIndex;
            SetupAdminProfile();
        }

        protected void dgAdminProfile_ItemDataBound(object source, System.Web.UI.WebControls.DataGridItemEventArgs e)
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

        protected void dgEdit_AdminProfile(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgAdminProfile.EditItemIndex = e.Item.ItemIndex;
            SetupAdminProfile();
        }

        protected void dgCancel_AdminProfile(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgAdminProfile.EditItemIndex = -1;
            SetupAdminProfile();
        }

        protected void dgAdminProfile_ItemCommand(object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {

        }

        protected void dgUpdate_AdminProfile(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            Label _ID = (Label)e.Item.Cells[0].FindControl("txtID");
            TextBox _Name = (TextBox)e.Item.Cells[0].FindControl("txtName");
            TextBox _OldPassword = (TextBox)e.Item.Cells[0].FindControl("txtOldPassword");
            TextBox _NewPassword = (TextBox)e.Item.Cells[0].FindControl("txtNewPassword");
            TextBox _ConfirmPassword = (TextBox)e.Item.Cells[0].FindControl("txtConfirmPassword");

            //Validation for name (No digit allowed and only up to 30 characters)
            Regex nameRegex = new Regex(@"[a-zA-Z ]{1,30}$");
            Match checkName = nameRegex.Match(_Name.Text);

            //Validation for password (The password is not allowed. Password must be 8 characters including at least 1 uppercase letter, 1 lowercase letter, 1 special character, 1 digit and minimum 8 in length
            Regex passwordRegex = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            Match checkPassword = passwordRegex.Match(_NewPassword.Text);

            if (_ID.Text.Trim() == "" || _Name.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Please fill in the name first!', 'error')</script>'");
            }
            else if (!checkName.Success)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'The Admin Name is not allowed. Admin name is only support character, space and up to 30 characters only.', 'error')</script>'");
            }
            else if (_OldPassword.Text == "" && _NewPassword.Text == "" && _ConfirmPassword.Text == "")
            {
                Admin lec = new Admin();
                if (lec.UpdateName(_ID.Text, _Name.Text) == false)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Unsuccessful update name!', 'error')</script>'");
                }
                else
                {
                    Server.Transfer("adminProfile.aspx");
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
                Admin lec = new Admin();
                if (lec.UpdatePasswordAndName(_ID.Text, _Name.Text, _OldPassword.Text, _NewPassword.Text) == false)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Unsuccessful! Your old password is wrong.', 'error')</script>'");
                }
                else
                {
                    Server.Transfer("adminProfile.aspx");
                }
            }
        }

        protected void dgAdminProfile_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}