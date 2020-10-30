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
    public partial class cpCourseSection : System.Web.UI.Page
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
                    SetupCourseSection();
                }
                else
                {
                    Response.Redirect("~/Web/login.aspx");
                }
            }
        }

        public void SetupCourseSection()
        {
            dgCourseSection.ShowFooter = true;
            CourseSection courseSection = new CourseSection();
            DataSet ds = courseSection.GetDisplay();
            dgCourseSection.DataSource = ds.Tables["CourseSection"];
            dgCourseSection.DataBind();
        }

        protected void dgCourseSection_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            dgCourseSection.CurrentPageIndex = e.NewPageIndex;
            SetupCourseSection();
        }

        protected void dgCourseSection_ItemDataBound(object source, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                ((Button)e.Item.FindControl("btnDeleteCourseSection")).Attributes.Add("onClick", "return confirmDeleteCourseSection(this);");
                ((WebControl)e.Item.Cells[2].Controls[0]).CssClass = "btn btn-warning";
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                ((WebControl)e.Item.Cells[2].Controls[0]).CssClass = "btn btn-warning";
                ((WebControl)e.Item.Cells[2].Controls[2]).CssClass = "btn btn-secondary";
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                CourseSection courseSection = new CourseSection();
                ArrayList courseList = courseSection.SelectCourseCode("", "");
                DropDownList dddl = (e.Item.FindControl("ddlCourseCodeNew") as DropDownList);

                dddl.DataSource = courseList;
                dddl.DataTextField = "Value";
                dddl.DataValueField = "Key";
                dddl.DataBind();
            }
            else if (e.Item.ItemType == ListItemType.EditItem)
            {
                CourseSection courseSection = new CourseSection();
                ArrayList courseList = courseSection.SelectCourseCode("", "");
                DropDownList dddl = (e.Item.FindControl("ddlCourseCode") as DropDownList);
                Label CourseCodeEdit = (Label)e.Item.Cells[0].FindControl("lblCourseCodeEdit");

                dddl.DataSource = courseList;
                dddl.DataTextField = "Value";
                dddl.DataValueField = "Key";
                dddl.DataBind();
                dddl.SelectedIndex = dddl.Items.IndexOf(dddl.Items.FindByText(CourseCodeEdit.Text));
            }
        }
        protected void dgEdit_CourseSection(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgCourseSection.EditItemIndex = e.Item.ItemIndex;
            SetupCourseSection();
        }

        protected void dgCancel_CourseSection(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgCourseSection.EditItemIndex = -1;
            SetupCourseSection();
        }


        protected void dgCourseSection_ItemCommand(object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            CourseSection lec = new CourseSection();

            if (e.CommandName == "AddCourseSection")
            {
                DropDownList txtTempCourseCode = (DropDownList)e.Item.Cells[0].FindControl("ddlCourseCodeNew");
                TextBox txtTempSectionNo = (TextBox)e.Item.Cells[1].FindControl("txtSectionNoNew");

                if (txtTempCourseCode.Text.Trim() == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Please fill in the course code!', 'error')</script>'");
                }
                else if (txtTempSectionNo.Text.Trim() == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Please fill in the section number!', 'error')</script>'");
                }
                else
                {

                    if (lec.Add(txtTempCourseCode.Text, txtTempSectionNo.Text) == false)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'This " + txtTempSectionNo.Text + " course section cannot have same section no, try another number!', 'error')</script>'");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal({title: 'Added!', text: 'This course section " + txtTempSectionNo.Text + " (" + txtTempCourseCode.Text + ") course section is added successfully', type : 'success', confirmButtonText : 'OK'}, function (isConfirm) { if (isConfirm) { window.location.href = 'cpCourseSection.aspx'; }});</script>'");
                    }
                }
            }

            else if (e.CommandName == "DeleteCourseSection")
            {
                Label rowCourseSectionID = (Label)e.Item.Cells[0].FindControl("lblID");

                if (lec.DeleteCourseSection(rowCourseSectionID.Text) == false)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Delete this course section record unsuccessfully', 'error')</script>'");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal({title: 'Deleted!', text: 'Deleted this course section record successfully', type : 'success', confirmButtonText : 'OK'}, function (isConfirm) { if (isConfirm) { window.location.href = 'cpCourseSection.aspx'; }});</script>'");
                }

            }

        }

        protected void dgUpdate_CourseSection(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            Label lblTempID = (Label)e.Item.Cells[0].FindControl("txtID");
            DropDownList txtTempCourseCode = (DropDownList)e.Item.Cells[0].FindControl("ddlCourseCode");
            TextBox txtTempSectionNo = (TextBox)e.Item.Cells[1].FindControl("txtSectionNo");


            if (lblTempID.Text.Trim() == "" || txtTempCourseCode.Text.Trim() == "" || txtTempSectionNo.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Please fill in the course section information first!', 'error')</script>'");
            }
            else
            {

                CourseSection lec = new CourseSection();
                if (lec.Update(lblTempID.Text, txtTempCourseCode.Text, txtTempSectionNo.Text) == false)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'This " + txtTempSectionNo.Text + " course section cannot have same section no, try another number!', 'error')</script>'");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal({title: 'Updated!', text: 'This course section " + txtTempSectionNo.Text + " (" + txtTempCourseCode.Text + ") course section is updated successfully', type : 'success', confirmButtonText : 'OK'}, function (isConfirm) { if (isConfirm) { window.location.href = 'cpCourseSection.aspx'; }});</script>'");
                }
            }
        }

        protected void dgCourseSection_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}