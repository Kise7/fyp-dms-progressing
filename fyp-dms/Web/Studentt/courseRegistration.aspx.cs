using fyp_dms.Class;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.IO;

namespace fyp_dms.Web.Studentt
{
    public partial class courseRegistration : System.Web.UI.Page
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
                if (String.Equals(identity, "Student"))
                {
                    SetupStudentRegisterCourse();
                }
                else
                {
                    Response.Redirect("~/Web/login.aspx");
                }
            }
        }
        public void SetupStudentRegisterCourse()
        {
            dgStudentRegisterCourse.ShowFooter = true;
            string StudentID = Session["StudentID"].ToString();
            CourseSection lec = new CourseSection();
            DataSet ds = lec.GetDisplayBasedStudent();
            dgStudentRegisterCourse.DataSource = ds.Tables["CourseSection"];
            dgStudentRegisterCourse.DataBind();
        }


        protected void dgStudentRegisterCourse_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            dgStudentRegisterCourse.CurrentPageIndex = e.NewPageIndex;
            SetupStudentRegisterCourse();
        }

        protected void dgStudentRegisterCourse_ItemDataBound(object source, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {

            }
            else if (e.Item.ItemType == ListItemType.EditItem)
            {

            }

        }

        protected void dgEdit_StudentRegisterCourse(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgStudentRegisterCourse.EditItemIndex = e.Item.ItemIndex;
            SetupStudentRegisterCourse();
        }

        protected void dgCancel_StudentRegisterCourse(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgStudentRegisterCourse.EditItemIndex = -1;
            SetupStudentRegisterCourse();
        }


        protected void dgStudentRegisterCourse_ItemCommand(object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Register")
            {
                Label TempCourseSectionID = (Label)e.Item.Cells[0].FindControl("lblID");
                string studentID = Session["ID"].ToString();
                StudentCourseSection lec = new StudentCourseSection();

                if (lec.CheckDuplicate(studentID, TempCourseSectionID.Text) == false)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'You have already registered for this course.', 'error')</script>'");
                }
                else if (lec.Add(studentID, TempCourseSectionID.Text) == false)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Unsuccessful registration!', 'error')</script>'");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal({title: 'Register successfully!', type : 'success', confirmButtonText : 'OK'}, function (isConfirm) { if (isConfirm) { window.location.href = 'courseRegistration.aspx'; }});</script>'");
                }
            }
        }

        protected void dgUpdate_StudentRegisterCourse(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {

        }

        protected void dgStudentRegisterCourse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}
