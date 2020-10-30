using fyp_dms.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fyp_dms.Web.Adminn
{
    public partial class cpFaculty : System.Web.UI.Page
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
                    SetupFaculty();
                }
                else
                {
                    Response.Redirect("~/Web/login.aspx");
                }
            }
        }

        public void SetupFaculty()
        {
            dgFaculty.ShowFooter = true;
            Faculty faculty = new Faculty();
            DataSet ds = faculty.GetDisplay();
            dgFaculty.DataSource = ds.Tables["Faculty"];
            dgFaculty.DataBind();
        }


        protected void dgFaculty_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            dgFaculty.CurrentPageIndex = e.NewPageIndex;
            SetupFaculty();
        }



        protected void dgFaculty_ItemDataBound(object source, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                ((Button)e.Item.FindControl("btnDeleteFaculty")).Attributes.Add("onClick", "return confirmDeleteFaculty(this);");
                ((WebControl)e.Item.Cells[1].Controls[0]).CssClass = "btn btn-warning";
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                ((WebControl)e.Item.Cells[1].Controls[0]).CssClass = "btn btn-warning";
                ((WebControl)e.Item.Cells[1].Controls[2]).CssClass = "btn btn-secondary";
            }
        }

        protected void dgEdit_Faculty(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgFaculty.EditItemIndex = e.Item.ItemIndex;
            SetupFaculty();
        }

        protected void dgCancel_Faculty(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgFaculty.EditItemIndex = -1;
            SetupFaculty();
        }


        protected void dgFaculty_ItemCommand(object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            Faculty faculty = new Faculty();

            if (e.CommandName == "AddFaculty")
            {
                TextBox txtTempFaculty = (TextBox)e.Item.Cells[0].FindControl("txtFacultyNew");


                if (txtTempFaculty.Text.Trim() == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Please fill in the faculty name!', 'error')</script>'");
                }
                else
                {
                    if (faculty.Add(txtTempFaculty.Text) == false)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'This " + txtTempFaculty.Text + " faculty is existed!', 'error')</script>'");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal({title: 'Added!', text: 'This " + txtTempFaculty.Text + " faculty is added successfully', type : 'success', confirmButtonText : 'OK'}, function (isConfirm) { if (isConfirm) { window.location.href = 'cpFaculty.aspx'; }});</script>'");
                    }
                }
            }

            else if (e.CommandName == "DeleteFaculty")
            {
                Label rowFacultyID = (Label)e.Item.Cells[0].FindControl("lblID");

                if (faculty.DeleteFaculty(rowFacultyID.Text) == false)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Delete this faculty unsuccessfully', 'error')</script>'");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal({title: 'Deleted!', text: 'Deleted this faculty successfully', type : 'success', confirmButtonText : 'OK'}, function (isConfirm) { if (isConfirm) { window.location.href = 'cpFaculty.aspx'; }});</script>'");
                }

            }

        }

        protected void dgUpdate_Faculty(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            Label lblTempID = (Label)e.Item.Cells[0].FindControl("txtID");
            TextBox txtTempFaculty = (TextBox)e.Item.Cells[1].FindControl("txtFacultyName");

            if (lblTempID.Text.Trim() == "" || txtTempFaculty.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Please fill in the faculty name!', 'error')</script>'");
            }
            else
            {

                Faculty Faculty = new Faculty();
                if (Faculty.Update(lblTempID.Text, txtTempFaculty.Text) == false)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'This " + txtTempFaculty.Text + " faculty name is existed, try another name!', 'error')</script>'");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal({title: 'Updated!', text: 'This " + txtTempFaculty.Text + " faculty is updated successfully', type : 'success', confirmButtonText : 'OK'}, function (isConfirm) { if (isConfirm) { window.location.href = 'cpFaculty.aspx'; }});</script>'");
                }
            }
        }

        protected void dgFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}