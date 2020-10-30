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
    public partial class cpCourse : System.Web.UI.Page
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
                    SetupCourse();
                }
                else
                {
                    Response.Redirect("~/Web/login.aspx");
                }
            }
        }

        public void SetupCourse()
        {
            dgCourse.ShowFooter = true;
            Course course = new Course();
            DataSet ds = course.GetDisplay();
            dgCourse.DataSource = ds.Tables["Course"];
            dgCourse.DataBind();
        }

        protected void dgCourse_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            dgCourse.CurrentPageIndex = e.NewPageIndex;
            SetupCourse();
        }

        protected void dgCourse_ItemDataBound(object source, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                ((Button)e.Item.FindControl("btnDeleteCourse")).Attributes.Add("onClick", "return confirmDeleteCourse(this);");
                ((WebControl)e.Item.Cells[3].Controls[0]).CssClass = "btn btn-warning";
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                ((WebControl)e.Item.Cells[3].Controls[0]).CssClass = "btn btn-warning";
                ((WebControl)e.Item.Cells[3].Controls[2]).CssClass = "btn btn-secondary";
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Course course = new Course();
                ArrayList programmeList = course.SelectProgrammeID("", "");
                DropDownList dddl = (e.Item.FindControl("ddlProgrammeIDNew") as DropDownList);

                dddl.DataSource = programmeList;
                dddl.DataTextField = "Value";
                dddl.DataValueField = "Key";
                dddl.DataBind();
            }
            else if (e.Item.ItemType == ListItemType.EditItem)
            {
                Course course = new Course();
                ArrayList programmeList = course.SelectProgrammeID("", "");
                DropDownList dddl = (e.Item.FindControl("ddlProgrammeID") as DropDownList);
                Label ProgrammeIDEdit = (Label)e.Item.Cells[1].FindControl("lblProgrammeIDEdit");

                dddl.DataSource = programmeList;
                dddl.DataTextField = "Value";
                dddl.DataValueField = "Key";
                dddl.DataBind();
                dddl.SelectedIndex = dddl.Items.IndexOf(dddl.Items.FindByText(ProgrammeIDEdit.Text));
            }
        }
        protected void dgEdit_Course(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgCourse.EditItemIndex = e.Item.ItemIndex;
            SetupCourse();
        }

        protected void dgCancel_Course(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgCourse.EditItemIndex = -1;
            SetupCourse();
        }

        protected void dgCourse_ItemCommand(object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            Course lec = new Course();

            if (e.CommandName == "AddCourse")
            {
                TextBox txtTempCourseCode = (TextBox)e.Item.Cells[0].FindControl("txtCourseCodeNew");
                DropDownList txtTempProgrammeID = (DropDownList)e.Item.Cells[1].FindControl("ddlProgrammeIDNew");
                TextBox txtTempCourseName = (TextBox)e.Item.Cells[2].FindControl("txtCourseNameNew");

                if (txtTempCourseCode.Text.Trim() == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Please fill in the course code!', 'error')</script>'");
                }
                else if (txtTempProgrammeID.Text.Trim() == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Please select program!', 'error')</script>'");
                }
                else if (txtTempCourseName.Text.Trim() == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Please fill in the course name!', 'error')</script>'");
                }
                else
                {

                    if (lec.Add(txtTempCourseCode.Text, txtTempProgrammeID.Text, txtTempCourseName.Text) == false)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'This course code is existed already, try another course code!', 'error')</script>'");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal({title: 'Added!', text: 'This " + txtTempCourseName.Text + " (" + txtTempCourseCode.Text + ") course is added successfully', type : 'success', confirmButtonText : 'OK'}, function (isConfirm) { if (isConfirm) { window.location.href = 'cpCourse.aspx'; }});</script>'");
                    }
                }
            }

            else if (e.CommandName == "DeleteCourse")
            {
                Label rowCourseID = (Label)e.Item.Cells[0].FindControl("lblID");

                if (lec.DeleteCourse(rowCourseID.Text) == false)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Delete this course record unsuccessfully', 'error')</script>'");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal({title: 'Deleted!', text: 'Deleted this course record successfully', type : 'success', confirmButtonText : 'OK'}, function (isConfirm) { if (isConfirm) { window.location.href = 'cpCourse.aspx'; }});</script>'");
                }

            }

        }

        protected void dgUpdate_Course(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            Label lblTempID = (Label)e.Item.Cells[0].FindControl("txtID");
            TextBox txtTempCourseCode = (TextBox)e.Item.Cells[0].FindControl("txtCourseCode");
            DropDownList txtTempprogrammeID = (DropDownList)e.Item.Cells[1].FindControl("ddlProgrammeID");
            TextBox txtTempCourseName = (TextBox)e.Item.Cells[2].FindControl("txtCourseName");


            if (lblTempID.Text.Trim() == "" || txtTempCourseCode.Text.Trim() == "" || txtTempprogrammeID.Text.Trim() == "" || txtTempCourseName.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Please fill in the course information first!', 'error')</script>'");
            }
            else
            {

                Course lec = new Course();
                if (lec.Update(lblTempID.Text, txtTempCourseCode.Text, txtTempprogrammeID.Text, txtTempCourseName.Text) == false)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'This course code is existed already, try another course code!', 'error')</script>'");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal({title: 'Updated!', text: 'This " + txtTempCourseName.Text + " (" + txtTempCourseCode.Text + ") course is updated successfully', type : 'success', confirmButtonText : 'OK'}, function (isConfirm) { if (isConfirm) { window.location.href = 'cpCourse.aspx'; }});</script>'");
                }
            }
        }

        protected void dgCourse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}