using fyp_dms.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace fyp_dms.Web.Adminn
{
    public partial class manageLecturer : System.Web.UI.Page
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
                    SetupLecturer();
                }
                else
                {
                    Response.Redirect("~/Web/login.aspx");
                }
            }
        }

        public void SetupLecturer()
        {
            dgLecturer.ShowFooter = true;
            Lecturer lecturer = new Lecturer();
            DataSet ds = lecturer.GetDisplay();
            dgLecturer.DataSource = ds.Tables["Lecturer"];
            dgLecturer.DataBind();
        }

        protected void dgLecturer_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            dgLecturer.CurrentPageIndex = e.NewPageIndex;
            SetupLecturer();
        }

        protected void dgLecturer_ItemDataBound(object source, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                ((Button)e.Item.FindControl("btnDeleteLecturer")).Attributes.Add("onClick", "return confirmDeleteLecturer(this);");
                ((WebControl)e.Item.Cells[5].Controls[0]).CssClass = "btn btn-warning";
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {

                ((WebControl)e.Item.Cells[5].Controls[0]).CssClass = "btn btn-warning";
                ((WebControl)e.Item.Cells[5].Controls[2]).CssClass = "btn btn-secondary";
            }


        }

        protected void dgEdit_Lecturer(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgLecturer.EditItemIndex = e.Item.ItemIndex;
            SetupLecturer();
        }

        protected void dgCancel_Lecturer(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgLecturer.EditItemIndex = -1;
            SetupLecturer();
        }


        protected void dgLecturer_ItemCommand(object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            Lecturer lecturer = new Lecturer();

            if (e.CommandName == "AddLecturer")
            {
                TextBox txtTempLecturerID = (TextBox)e.Item.Cells[0].FindControl("txtLecturerIDNew");
                TextBox txtTempName = (TextBox)e.Item.Cells[1].FindControl("txtNameNew");
                TextBox txtTempPhoneNo = (TextBox)e.Item.Cells[2].FindControl("txtPhoneNew");
                Regex lecregex = new Regex(@"^\d{1,5}$");                                 //only support up to 5 integer
                Match checklec = lecregex.Match(txtTempLecturerID.Text);
                Regex nameRegex = new Regex(@"[a-zA-Z ]{1,30}$");                          //Only allow a-z,A-Z and space
                Match checkname = nameRegex.Match(txtTempName.Text);

                //Validation for name (No digit allowed and only up to 30 characters)
                Regex phoneRegex = new Regex(@"^(01)[0-9]*[0-9]{8,9}$");
                Match checkPhone = phoneRegex.Match(txtTempPhoneNo.Text);

                if (txtTempName.Text.Trim() == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Please fill in the lecturer name!', 'error')</script>'");
                }
                else if (!checklec.Success)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'The Lecturer ID is not allowed. Please Lecturer ID is only up to 5 digit number.', 'error')</script>'");
                }
                else if (!checkname.Success)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'The Lecturer Name is not allowed. Lecturer name is only support character, space and up to 30 character only.', 'error')</script>'");
                }
                else if (txtTempLecturerID.Text.Trim() == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Please fill in the lecturer ID!', 'error')</script>'");
                }
                else if (!checkPhone.Success)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'The phone number is not allowed. Phone number must start with 01 and is only support 7 or 8 digits', 'error')</script>'");
                }
                else
                {
                    if (lecturer.Add(txtTempLecturerID.Text, txtTempName.Text, txtTempPhoneNo.Text) == false)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Cannot have same duplicate lecturer ID!', 'error')</script>'");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal({title: 'Added!', text: 'This " + txtTempName.Text + " (" + txtTempLecturerID.Text + ") lecturer is added successfully, Your password is (holdDoc1)', type : 'success', confirmButtonText : 'OK'}, function (isConfirm) { if (isConfirm) { window.location.href = 'manageLecturer.aspx'; }});</script>'");
                    }
                }
            }

            else if (e.CommandName == "DeleteLecturer")
            {
                Label rowLecturerID = (Label)e.Item.Cells[0].FindControl("lblLecturerID");

                if (lecturer.DeleteLecturer(rowLecturerID.Text) == false)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Delete this lecturer record unsuccessfully', 'error')</script>'");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal({title: 'Deleted!', text: 'Deleted this lecturer record successfully', type : 'success', confirmButtonText : 'OK'}, function (isConfirm) { if (isConfirm) { window.location.href = 'manageLecturer.aspx'; }});</script>'");
                }

            }
            else if (e.CommandName == "ResetPassword")
            {

                Label rowLecturerID = (Label)e.Item.Cells[0].FindControl("lblLecturerID");

                if (lecturer.ResetPassword(rowLecturerID.Text) == false)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Reset Password unsuccessfully', 'error')</script>'");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal({title: 'Reset!', text: 'Reset Password successfully! Your password is (holdDoc1)', type : 'success', confirmButtonText : 'OK'}, function (isConfirm) { if (isConfirm) { window.location.href = 'manageLecturer.aspx'; }});</script>'");
                }

            }
        }

        protected void dgUpdate_Lecturer(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            Label rowLecturerID = (Label)e.Item.Cells[0].FindControl("lblLecturerID");
            TextBox txtName = (TextBox)e.Item.Cells[1].FindControl("txtName");
            TextBox txtTempPhoneNo = (TextBox)e.Item.Cells[2].FindControl("txtPhone");
            Regex nameRegex = new Regex(@"[a-zA-Z ]{1,30}$");                          //Only allow a-z,A-Z and space
            Match checkname = nameRegex.Match(txtName.Text);

            //Validation for phone (Only 10-11 digit is allowed)
            Regex phoneRegex = new Regex(@"^(01)[0-9]*[0-9]{8,9}$");
            Match checkPhone = phoneRegex.Match(txtTempPhoneNo.Text);

            if (txtName.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Please fill in the lecturer name first!', 'error')</script>'");
            }
            else if (!checkname.Success)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'The Admin Name is not allowed. Admin name is only support character, space and up to 30 character only.', 'error')</script>'");
            }
            else if (!checkPhone.Success)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'The phone number is not allowed. Phone number is only support 7 or 8 digits', 'error')</script>'");
            }
            else
            {
                Lecturer lecturer = new Lecturer();
                if (lecturer.Update(rowLecturerID.Text, txtName.Text, txtTempPhoneNo.Text) == false)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'This " + txtName.Text + " (" + rowLecturerID.Text + ") lecturer cannot be updated successfully!', 'error')</script>'");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal({title: 'Updated!', text: 'This " + txtName.Text + " (" + rowLecturerID.Text + ") lecturer is updated successfully', type : 'success', confirmButtonText : 'OK'}, function (isConfirm) { if (isConfirm) { window.location.href = 'manageLecturer.aspx'; }});</script>'");
                }
            }
        }

        protected void dgLecturer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}