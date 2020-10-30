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
    public partial class studentBookmark : System.Web.UI.Page
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

                    SetupStudentMain();
                }
                else
                {
                    Response.Redirect("~/Web/login.aspx");
                }
            }
        }
        public void SetupStudentMain()
        {
            dgStudentMain.ShowFooter = true;
            StudentBookmark lec = new StudentBookmark();
            string StudentID = Session["StudentID"].ToString();
            DataSet ds = lec.GetDisplay(StudentID);
            dgStudentMain.DataSource = ds.Tables["StudentBookmark"];
            dgStudentMain.DataBind();
        }

        protected void dgStudentMain_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            dgStudentMain.CurrentPageIndex = e.NewPageIndex;
            SetupStudentMain();
        }

        protected void dgStudentMain_ItemDataBound(object source, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                ((WebControl)e.Item.Cells[6].Controls[0]).CssClass = "btn btn-warning";
                ((LinkButton)e.Item.FindControl("btnDeleteLec")).Attributes.Add("onClick", "javascript:return confirm('Are you sure wish to delete the data?')");
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                ((WebControl)e.Item.Cells[6].Controls[0]).CssClass = "btn btn-warning";
                ((WebControl)e.Item.Cells[6].Controls[2]).CssClass = "btn btn-secondary";
            }
        }

        protected void dgEdit_StudentMain(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgStudentMain.EditItemIndex = e.Item.ItemIndex;
            SetupStudentMain();
        }

        protected void dgCancel_StudentMain(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgStudentMain.EditItemIndex = -1;
            SetupStudentMain();
        }


        protected void dgStudentMain_ItemCommand(object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                Label TempID = (Label)e.Item.Cells[2].FindControl("lblFile");
                Response.Redirect("upload/" + TempID.Text);
            }
            else if (e.CommandName == "Download")
            {
                Label TempID = (Label)e.Item.Cells[2].FindControl("lblFile");
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
            else if (e.CommandName == "DeleteLec")
            {
                Label _ID = (Label)e.Item.Cells[0].FindControl("lblID");
                StudentBookmark lec = new StudentBookmark();
                if (lec.DeleteBookmark(_ID.Text) == false)
                {
                    lblAttention.Text = "*Unsuccessful! ";
                }
                else
                {
                    Server.Transfer("studentHome.aspx");
                }
            }
            else if (e.CommandName == "Sort")
            {
                string parameter = e.CommandArgument.ToString();
                dgStudentMain.ShowFooter = true;
                StudentBookmark lec = new StudentBookmark();
                string StudentID = Session["StudentID"].ToString();
                DataSet ds;
                if (parameter == "TitleASC")
                {
                    ds = lec.GetDisplay(StudentID, "Title", "ASC");
                }
                else if (parameter == "TitleDESC")
                {
                    ds = lec.GetDisplay(StudentID, "Title", "DESC");
                }
                else if (parameter == "FolderASC")
                {
                    ds = lec.GetDisplay(StudentID, "FolderName", "ASC");
                }
                else if (parameter == "FolderDESC")
                {
                    ds = lec.GetDisplay(StudentID, "FolderName", "DESC");
                }
                else
                {
                    ds = lec.GetDisplay(StudentID);
                }
                dgStudentMain.DataSource = ds.Tables["StudentBookmark"];
                dgStudentMain.DataBind();
                
            }
        }

        protected void dgUpdate_StudentMain(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            Label _ID = (Label)e.Item.Cells[0].FindControl("txtID");
            TextBox _tag = (TextBox)e.Item.Cells[1].FindControl("txtTag");

            if (_ID.Text.Trim() == "" || _tag.Text.Trim() == "")
            {
                lblAttention.Text = "* Please fill in the info!";
            }
            else
            {
                StudentBookmark lec = new StudentBookmark();
                if (lec.Update(_ID.Text, _tag.Text) == false)
                {
                    lblAttention.Text = "* Unsuccessful!";
                }
                else
                {
                    Server.Transfer("StudentMain.aspx");
                }
                
            }
        }

        protected void dgStudentMain_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            TextBox searchparameter = (TextBox)FindControl("txtSearch");
            string tempsearch = searchparameter.Text;
            dgStudentMain.ShowFooter = true;
            StudentBookmark lec = new StudentBookmark();
            string StudentID = Session["StudentID"].ToString();
            DataSet ds = lec.GetDisplay(StudentID, tempsearch);
            dgStudentMain.DataSource = ds.Tables["StudentBookmark"];
            dgStudentMain.DataBind();
            Label searchresult = (Label)FindControl("lblSearchResult");
            searchresult.Text = "<H3>Search Result:" + tempsearch + "</H3>";
            
        }
    }
}