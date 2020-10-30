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
    public partial class studentDocument : System.Web.UI.Page
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

                    SetupStudentDocument();
                }
                else
                {
                    Response.Redirect("~/Web/login.aspx");
                }
            }
        }
        public void SetupStudentDocument()
        {
            dgStudentDocument.ShowFooter = true;
            string FolderID = Session["FolderID"].ToString();
            Document lec = new Document();
            DataSet ds = lec.GetDisplayBasedFolderIDForStudent(FolderID);
            dgStudentDocument.DataSource = ds.Tables["Document"];
            dgStudentDocument.DataBind();
        }


        protected void dgStudentDocument_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            dgStudentDocument.CurrentPageIndex = e.NewPageIndex;
            SetupStudentDocument();
        }



        protected void dgStudentDocument_ItemDataBound(object source, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                //btnAddBookmark
                Label lblbookmark = (Label)e.Item.Cells[4].FindControl("lblBookmark");
                LinkButton AddBookmark = (LinkButton)e.Item.Cells[4].FindControl("btnAddBookmark");
                if (lblbookmark.Text == "Cancel Bookmark")
                {
                    AddBookmark.CssClass = "btn btn-secondary";
                    AddBookmark.Text = "<i class='fa fa-heart'></i>";
                }
                else
                {
                    AddBookmark.CssClass = "btn btn-danger";
                    AddBookmark.Text = "<i class='fa fa-heart'></i>";
                }
            }
            else if (e.Item.ItemType == ListItemType.EditItem)
            {

            }

        }

        protected void dgEdit_StudentDocument(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgStudentDocument.EditItemIndex = e.Item.ItemIndex;
            SetupStudentDocument();
        }

        protected void dgCancel_StudentDocument(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgStudentDocument.EditItemIndex = -1;
            SetupStudentDocument();
        }


        protected void dgStudentDocument_ItemCommand(object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {

            if (e.CommandName == "View")
            {
                Label TempID = (Label)e.Item.Cells[1].FindControl("lblFile");
                Response.Redirect("upload/" + TempID.Text);
            }
            else if (e.CommandName == "Download")
            {
                Label TempID = (Label)e.Item.Cells[1].FindControl("lblFile");
                string filePath = Server.MapPath("~/upload/" + TempID.Text);
                FileInfo file = new FileInfo(filePath);
                if (file.Exists)
                {
                    Response.Clear();
                    Response.ClearHeaders();
                    Response.ClearContent();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    if (file.Extension == "txt")
                    {
                        Response.ContentType = "text/plain";
                    }
                    else if (file.Extension == "jfif" || file.Extension == "jpeg" || file.Extension == "jpg")
                    {
                        Response.ContentType = "image/JPEG";
                    }
                    else if (file.Extension == "pdf")
                    {
                        Response.ContentType = "application/pdf";
                    }
                    else if (file.Extension == "html")
                    {
                        Response.ContentType = "text/HTML";
                    }
                    else if (file.Extension == "gif")
                    {
                        Response.ContentType = "image/GIF";
                    }
                    else if (file.Extension == "docx" || file.Extension == "doc")
                    {
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    }
                    Response.Flush();
                    Response.TransmitFile(file.FullName);
                    Response.End();
                }
                else
                {
                    Response.Redirect("upload/" + TempID.Text);
                }
            }
            else if (e.CommandName == "AddBookmark")
            {
                Label TempID = (Label)e.Item.Cells[0].FindControl("lblID");
                string studentID = Session["StudentID"].ToString();
                StudentBookmark lec = new StudentBookmark();
                if (lec.Cancel(TempID.Text, studentID) == true)
                {
                    Server.Transfer("studentDocument.aspx");
                }
                else if (lec.Add(TempID.Text, studentID) == false)
                {
                    lblAttention.Text = "Bookmark have not been add successfully.";
                }
                else
                {
                    Server.Transfer("studentDocument.aspx");
                }
            }
        }

        protected void dgUpdate_StudentDocument(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {

        }

        protected void dgStudentDocument_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            TextBox searchparameter = (TextBox)FindControl("txtSearch");
            string tempsearch = searchparameter.Text;
            dgStudentDocument.ShowFooter = true;
            string FolderID = Session["FolderID"].ToString();
            Document lec = new Document();
            DataSet ds = lec.GetDisplayBasedFolderIDForStudent(FolderID, tempsearch);
            dgStudentDocument.DataSource = ds.Tables["Document"];
            dgStudentDocument.DataBind();
            Label searchresult = (Label)FindControl("lblSearchResult");
            searchresult.Text = "<H3>Search Result:" + tempsearch + "</H3>";
        }
    }
}
