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

namespace fyp_dms.Web.Studentt
{
    public partial class studentDocumentMain : System.Web.UI.Page
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
                    SetupStudentCourseRegistered();
                }
                else
                {
                    Response.Redirect("~/Web/login.aspx");
                }
            }
        }
        public void SetupStudentCourseRegistered()
        {
            dgStudentCourseRegistered.ShowFooter = true;
            string StudentID = Session["StudentID"].ToString();
            StudentCourseSection lec = new StudentCourseSection();
            DataSet ds = lec.GetDisplayBasedStudent(StudentID);
            dgStudentCourseRegistered.DataSource = ds.Tables["StudentCourseSection"];
            dgStudentCourseRegistered.DataBind();
        }


        protected void dgStudentCourseRegistered_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            dgStudentCourseRegistered.CurrentPageIndex = e.NewPageIndex;
            SetupStudentCourseRegistered();
        }



        protected void dgStudentCourseRegistered_ItemDataBound(object source, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {

            }
            else if (e.Item.ItemType == ListItemType.EditItem)
            {

            }

        }

        protected void dgEdit_StudentCourseRegistered(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgStudentCourseRegistered.EditItemIndex = e.Item.ItemIndex;
            SetupStudentCourseRegistered();
        }

        protected void dgCancel_StudentCourseRegistered(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgStudentCourseRegistered.EditItemIndex = -1;
            SetupStudentCourseRegistered();
        }


        protected void dgStudentCourseRegistered_ItemCommand(object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                Label TempStudentCourseSectionID = (Label)e.Item.Cells[0].FindControl("lblID");
                string studentID = Session["ID"].ToString();
                Session["StudentCourseSectionID"] = TempStudentCourseSectionID.Text;
                Label TempCourseSectionID = (Label)e.Item.Cells[0].FindControl("lblCourseSectionID");
                Session["CourseSectionID"] = TempCourseSectionID.Text;
                Response.Redirect("studentFolder.aspx");
            }
        }

        protected void dgUpdate_StudentCourseRegistered(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {

        }

        protected void dgStudentCourseRegistered_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



    }
}