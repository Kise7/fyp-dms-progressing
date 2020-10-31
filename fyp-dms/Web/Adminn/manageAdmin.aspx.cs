using fyp_dms.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fyp_dms.Web.Adminn
{
    public partial class manageAdmin : System.Web.UI.Page
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
                    SetupAdmin();
                }
                else
                {
                    Response.Redirect("~/Web/login.aspx");
                }

            }
        }

        public void SetupAdmin()
        {
            dgAdmin.ShowFooter = true;
            Admin admin = new Admin();
            DataSet ds = admin.GetDisplay();
            dgAdmin.DataSource = ds.Tables["Admin"];
            dgAdmin.DataBind();
        }


        protected void dgAdmin_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            dgAdmin.CurrentPageIndex = e.NewPageIndex;
            SetupAdmin();
        }



        protected void dgAdmin_ItemDataBound(object source, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                ((Button)e.Item.FindControl("btnDeleteAdmin")).Attributes.Add("onClick", "return confirmDeleteAdmin(this);");
                ((WebControl)e.Item.Cells[2].Controls[0]).CssClass = "btn btn-warning";
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                ((WebControl)e.Item.Cells[2].Controls[0]).CssClass = "btn btn-warning";
                ((WebControl)e.Item.Cells[2].Controls[2]).CssClass = "btn btn-secondary";
            }
        }

        protected void dgEdit_Admin(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgAdmin.EditItemIndex = e.Item.ItemIndex;
            SetupAdmin();
        }

        protected void dgCancel_Admin(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgAdmin.EditItemIndex = -1;
            SetupAdmin();
        }


        protected void dgAdmin_ItemCommand(object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            Admin admin = new Admin();

            if (e.CommandName == "AddAdmin")
            {
                TextBox txtTempAdminID = (TextBox)e.Item.Cells[0].FindControl("txtAdminIDNew");
                TextBox txtTempName = (TextBox)e.Item.Cells[1].FindControl("txtNameNew");
                Regex adminregex = new Regex(@"^\d{1,5}$");                                 //only support up to 5 integer
                Match checkadmin = adminregex.Match(txtTempAdminID.Text);
                Regex nameRegex = new Regex(@"[a-zA-Z ]{1,30}$");                          //Only allow a-z,A-Z and space
                Match checkname = nameRegex.Match(txtTempName.Text);

                if (!checkadmin.Success)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'The Admin ID is not allowed. Please Admin ID is only up to 5 digit number.', 'error')</script>'");
                }
                else if (!checkname.Success)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'The Admin Name is not allowed. Admin name is only support character, space and up to 30 character only.', 'error')</script>'");
                }
                else if (txtTempName.Text.Trim() == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Please fill in the Name!', 'error')</script>'");
                }
                else if (txtTempAdminID.Text.Trim() == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Please fill in the Admin ID!', 'error')</script>'");
                }
                else
                {

                    if (admin.Add(txtTempAdminID.Text, txtTempName.Text) == false)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Cannot have the same duplicate Admin ID!', 'error')</script>'");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal({title: 'Added!', text: 'Admin " + txtTempName.Text + " (" + txtTempAdminID.Text + ") is added successfully Your password is (holdDoc123)!', type : 'success', confirmButtonText : 'OK'}, function (isConfirm) { if (isConfirm) { window.location.href = 'manageAdmin.aspx'; }});</script>'");
                    }
                }
            }

            else if (e.CommandName == "DeleteAdmin")
            {
                string id = Session["ID"].ToString();
                Label rowAdminID = (Label)e.Item.Cells[0].FindControl("lblAdminID");
                if (id == rowAdminID.Text)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Warning!', 'Hey, You cannot delete yourself!', 'error')</script>'");
                }
                else if (admin.DeleteAdmin(rowAdminID.Text) == false)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'This admin record is deleted unsuccessfully', 'error')</script>'");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal({title: 'Deleted!', text: 'This admin record is deleted successfully', type : 'success', confirmButtonText : 'OK'}, function (isConfirm) { if (isConfirm) { window.location.href = 'manageAdmin.aspx'; }});</script>'");
                }

            }

        }

        protected void dgUpdate_Admin(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            Label rowAdminID = (Label)e.Item.Cells[0].FindControl("txtAdminID");
            TextBox rowAdminName = (TextBox)e.Item.Cells[1].FindControl("txtName");
            Regex adminregex = new Regex(@"^\d{1,5}$");                                 //only support up to 5 integer
            Match checkadmin = adminregex.Match(rowAdminID.Text);
            Regex nameRegex = new Regex(@"[a-zA-Z ]{1,30}$");                       //Only allow a-z,A-Z and . symbol
            Match checkname = nameRegex.Match(rowAdminName.Text);


            if (rowAdminID.Text.Trim() == "" || rowAdminName.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Warning!', 'Please fill in the information first!', 'error')</script>'");
            }
            else if (!checkadmin.Success)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'The Admin ID is not allowed. Please Admin ID is only up to 5 digit number.', 'error')</script>'");
            }
            else if (!checkname.Success)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'The Admin Name is not allowed. Admin name is only support character, space and up to 30 character only.', 'error')</script>'");
            }
            else
            {
                Admin admin = new Admin();
                if (admin.Update(rowAdminID.Text, rowAdminName.Text) == false)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Admin " + rowAdminName.Text + " (" + rowAdminID.Text + ") is updated unsuccessfully', 'error')</script>'");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal({title: 'Updated!', text: 'Admin " + rowAdminName.Text + " (" + rowAdminID.Text + ") is updated successfully', type : 'success', confirmButtonText : 'OK'}, function (isConfirm) { if (isConfirm) { window.location.href = 'manageAdmin.aspx'; }});</script>'");
                }
            }
        }

        protected void dgAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}