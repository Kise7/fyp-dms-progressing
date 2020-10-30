using fyp_dms.Class;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fyp_dms.Web.Adminn
{
    public partial class manageWorkAssign : System.Web.UI.Page
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
                    SetupWorkAssign();
                }
                else
                {
                    Response.Redirect("~/Web/login.aspx");
                }
            }
        }

        public void SetupWorkAssign()
        {
            dgWorkAssign.ShowFooter = true;
            WorkAssign workAssign = new WorkAssign();
            DataSet ds = workAssign.GetDisplay();
            dgWorkAssign.DataSource = ds.Tables["WorkAssign"];
            dgWorkAssign.DataBind();
        }

        protected void dgWorkAssign_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            dgWorkAssign.CurrentPageIndex = e.NewPageIndex;
            SetupWorkAssign();
        }

        protected void dgWorkAssign_ItemDataBound(object source, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                ((Button)e.Item.FindControl("btnDeleteWorkAssign")).Attributes.Add("onClick", "return confirmDeleteWorkAssign(this);");
                ((WebControl)e.Item.Cells[4].Controls[0]).CssClass = "btn btn-warning";
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                ((WebControl)e.Item.Cells[4].Controls[0]).CssClass = "btn btn-warning";
                ((WebControl)e.Item.Cells[4].Controls[2]).CssClass = "btn btn-secondary";
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                WorkAssign workAssign = new WorkAssign();
                ArrayList courseList = workAssign.SelectCourseCode("", "");
                ArrayList lecturerList = workAssign.SelectLecturerID("", "");
                DropDownList ddlCourse = (e.Item.FindControl("ddlCourseCodeNew") as DropDownList);
                DropDownList ddlLecturer = (e.Item.FindControl("ddlLecturerNameNew") as DropDownList);

                ddlCourse.DataSource = courseList;
                ddlCourse.DataTextField = "Value";
                ddlCourse.DataValueField = "Key";
                ddlCourse.DataBind();

                ddlLecturer.DataSource = lecturerList;
                ddlLecturer.DataTextField = "Value";
                ddlLecturer.DataValueField = "Key";
                ddlLecturer.DataBind();
            }
            else if (e.Item.ItemType == ListItemType.EditItem)
            {
                WorkAssign workAssign = new WorkAssign();
                ArrayList courseList = workAssign.SelectCourseCode("", "");
                ArrayList lecturerList = workAssign.SelectLecturerID("", "");
                DropDownList ddlCourse = (e.Item.FindControl("ddlCourseCode") as DropDownList);
                DropDownList ddlLecturer = (e.Item.FindControl("ddlLecturerName") as DropDownList);
                DropDownList ddlStatus = (e.Item.FindControl("ddlStatusEdit") as DropDownList);
                Label courseCodeEdit = (Label)e.Item.Cells[0].FindControl("lblCourseCodeEdit");
                Label lecturerNameEdit = (Label)e.Item.Cells[1].FindControl("lblLecturerNameEdit");
                Label statusEdit = (Label)e.Item.Cells[1].FindControl("txtStatus");

                ddlCourse.DataSource = courseList;
                ddlCourse.DataTextField = "Value";
                ddlCourse.DataValueField = "Key";
                ddlCourse.DataBind();
                ddlCourse.SelectedIndex = ddlCourse.Items.IndexOf(ddlCourse.Items.FindByText(courseCodeEdit.Text));

                ddlLecturer.DataSource = lecturerList;
                ddlLecturer.DataTextField = "Value";
                ddlLecturer.DataValueField = "Key";
                ddlLecturer.DataBind();
                ddlLecturer.SelectedIndex = ddlLecturer.Items.IndexOf(ddlLecturer.Items.FindByText(lecturerNameEdit.Text));

                ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByText(statusEdit.Text));
            }
        }

        protected void dgEdit_WorkAssign(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgWorkAssign.EditItemIndex = e.Item.ItemIndex;
            SetupWorkAssign();
        }

        protected void dgCancel_WorkAssign(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgWorkAssign.EditItemIndex = -1;
            SetupWorkAssign();
        }

        protected void dgWorkAssign_ItemCommand(object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            WorkAssign workAssign = new WorkAssign();

            if (e.CommandName == "AddWorkAssign")
            {
                DropDownList txtTempCourseCode = (DropDownList)e.Item.Cells[0].FindControl("ddlCourseCodeNew");
                DropDownList txtTempLecturerName = (DropDownList)e.Item.Cells[1].FindControl("ddlLecturerNameNew");
                TextBox txtTempName = (TextBox)e.Item.Cells[2].FindControl("txtPositionNew");
                DropDownList ddlStatus = (DropDownList)e.Item.Cells[3].FindControl("ddlStatus");

                bool status = false;

                if (ddlStatus.Text.Equals("Active"))
                {
                    status = true;
                }
                else if ((ddlStatus.Text.Equals("Not Active")))
                {
                    status = false;
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Please select one status first!', 'error')</script>'");
                }

                if (txtTempCourseCode.Text.Trim() == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Please fill in the course code!', 'error')</script>'");
                }
                else if (txtTempLecturerName.Text.Trim() == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Please fill in the section number!', 'error')</script>'");
                }
                else if (txtTempName.Text.Trim() == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Please fill in the name for work assign!', 'error')</script>'");
                }
                else
                {
                    if (workAssign.Add(txtTempCourseCode.Text, txtTempLecturerName.Text, txtTempName.Text, status) == false)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Assigned unsuccessfully! Please select another one', 'error')</script>'");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal({title: 'Added!', text: 'Assigned successfully!', type : 'success', confirmButtonText : 'OK'}, function (isConfirm) { if (isConfirm) { window.location.href = 'manageWorkAssign.aspx'; }});</script>'");
                    }
                }
            }

            else if (e.CommandName == "DeleteWorkAssign")
            {
                Label rowWorkAssignID = (Label)e.Item.Cells[0].FindControl("lblID");

                if (workAssign.DeleteWorkAssign(rowWorkAssignID.Text) == false)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Delete this work assign record unsuccessfully', 'error')</script>'");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal({title: 'Deleted!', text: 'Deleted this work assign record successfully', type : 'success', confirmButtonText : 'OK'}, function (isConfirm) { if (isConfirm) { window.location.href = 'manageWorkAssign.aspx'; }});</script>'");
                }
            }
        }

        protected void dgUpdate_WorkAssign(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            Label lblTempID = (Label)e.Item.Cells[0].FindControl("txtID");
            DropDownList txtTempCourseCode = (DropDownList)e.Item.Cells[0].FindControl("ddlCourseCode");
            DropDownList txtTempLecturerName = (DropDownList)e.Item.Cells[1].FindControl("ddlLecturerName");
            TextBox txtTempName = (TextBox)e.Item.Cells[2].FindControl("txtPosition");
            DropDownList ddlStatus = (DropDownList)e.Item.Cells[3].FindControl("ddlStatusEdit");

            bool status = false;

            if (ddlStatus.Text.Equals("Active"))
            {
                status = true;
            }
            else if ((ddlStatus.Text.Equals("Not Active")))
            {
                status = false;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Please select one status first!', 'error')</script>'");
            }

            if (lblTempID.Text.Trim() == "" || txtTempCourseCode.Text.Trim() == "" || txtTempLecturerName.Text.Trim() == "" || txtTempName.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Cannot have the same name or same work assign to same lecturer!', 'error')</script>'");
            }
            else
            {
                WorkAssign workAssign = new WorkAssign();
                if (workAssign.Update(lblTempID.Text, txtTempCourseCode.Text, txtTempLecturerName.Text, txtTempName.Text, status) == false)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Assigned unsuccessfully! Please select another one', 'error')</script>'");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal({title: 'Updated!', text: 'Assigned successfully!', type : 'success', confirmButtonText : 'OK'}, function (isConfirm) { if (isConfirm) { window.location.href = 'manageWorkAssign.aspx'; }});</script>'");
                }
            }
        }

        protected void dgWorkAssign_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}