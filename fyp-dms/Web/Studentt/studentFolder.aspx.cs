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
    public partial class studentFolder : System.Web.UI.Page
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
                    SetupStudentFolder();
                }
                else
                {
                    Response.Redirect("~/Web/login.aspx");
                }
            }
        }
        public void SetupStudentFolder()
        {
            dgStudentFolder.ShowFooter = true;
            string StudentID = Session["StudentID"].ToString();
            string CourseSectionID = Session["CourseSectionID"].ToString();
            Folder lec = new Folder();
            DataSet ds = lec.GetFolderBasedCourseSection(StudentID, CourseSectionID);
            dgStudentFolder.DataSource = ds.Tables["Folder"];
            dgStudentFolder.DataBind();
        }


        protected void dgStudentFolder_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            dgStudentFolder.CurrentPageIndex = e.NewPageIndex;
            SetupStudentFolder();
        }



        protected void dgStudentFolder_ItemDataBound(object source, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Label check = (Label)e.Item.Cells[3].FindControl("lblPrivilege");
                LinkButton Manage = (LinkButton)e.Item.Cells[4].FindControl("btnManage");
                Label lblshow = (Label)e.Item.Cells[3].FindControl("lblshow");
                if (check.Text == "Open")
                {
                    Manage.Enabled = true;
                    Manage.CssClass = "btn btn-success";
                    lblshow.Text = "<i class='fa fa-unlock'></i>";

                }
                else
                {
                    Manage.Enabled = false;
                    Manage.CssClass = "btn btn-secondary";
                    Manage.Text = "Disabled";
                    lblshow.Text = "<i class='fa fa-lock'></i>";
                }

            }

        }

        protected void dgEdit_StudentFolder(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgStudentFolder.EditItemIndex = e.Item.ItemIndex;
            SetupStudentFolder();
        }

        protected void dgCancel_StudentFolder(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgStudentFolder.EditItemIndex = -1;
            SetupStudentFolder();
        }


        protected void dgStudentFolder_ItemCommand(object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            Folder lec = new Folder();

            if (e.CommandName == "Manage")
            {
                Label TempID = (Label)e.Item.Cells[0].FindControl("lblID");
                Session["FolderID"] = TempID.Text;
                Response.Redirect("StudentDocument.aspx");
            }
        }

        protected void dgUpdate_StudentFolder(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {

        }

        protected void dgStudentFolder_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
