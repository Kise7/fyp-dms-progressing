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

namespace fyp_dms.Web.Adminn
{
    public partial class manageStudent : System.Web.UI.Page
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
                    SetupStudent();
                }
                else
                {
                    Response.Redirect("~/Web/login.aspx");
                }
            }
        }

        //TODO got time separate add new student into another page

        public void SetupStudent()
        {
            dgStudent.ShowFooter = true;

            Student student = new Student();
            DataSet ds = student.GetDisplay();
            dgStudent.DataSource = ds.Tables["Student"];
            dgStudent.DataBind();
        }

        protected void dgStudent_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            dgStudent.CurrentPageIndex = e.NewPageIndex;
            SetupStudent();
        }

        protected void dgStudent_ItemDataBound(object source, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                ((Button)e.Item.FindControl("btnDeleteStudent")).Attributes.Add("onClick", "return confirmDeleteStudent(this);");
                ((WebControl)e.Item.Cells[8].Controls[0]).CssClass = "btn btn-warning";
            }
            if (e.Item.ItemType == ListItemType.EditItem)
            {
                ((WebControl)e.Item.Cells[8].Controls[0]).CssClass = "btn btn-warning";
                ((WebControl)e.Item.Cells[8].Controls[2]).CssClass = "btn btn-secondary";

            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Student student = new Student();
                ArrayList arLecList = student.SelectYearName("", "");
                DropDownList dddl = (e.Item.FindControl("ddlYearNew") as DropDownList);

                dddl.DataSource = arLecList;
                dddl.DataTextField = "Value";
                dddl.DataValueField = "Key";
                dddl.DataBind();

                ArrayList arLecLists = student.SelectProgrammeID("", "");
                DropDownList dddls = (e.Item.FindControl("ddlProgrammeIDNew") as DropDownList);

                dddls.DataSource = arLecLists;
                dddls.DataTextField = "Value";
                dddls.DataValueField = "Key";
                dddls.DataBind();

                ArrayList arLecListss = student.SelectIntake("", "");
                DropDownList dddlss = (e.Item.FindControl("ddlIntakeNew") as DropDownList);

                dddlss.DataSource = arLecListss;
                dddlss.DataTextField = "Value";
                dddlss.DataValueField = "Key";
                dddlss.DataBind();
            }
            else if (e.Item.ItemType == ListItemType.EditItem)
            {
                Student lec = new Student();
                ArrayList arLecList = lec.SelectYearName("", "");
                DropDownList dddl = (e.Item.FindControl("ddlYear") as DropDownList);
                Label YearEdit = (Label)e.Item.Cells[1].FindControl("lblYearEdit");

                dddl.DataSource = arLecList;
                dddl.DataTextField = "Value";
                dddl.DataValueField = "Key";
                dddl.DataBind();
                dddl.SelectedIndex = dddl.Items.IndexOf(dddl.Items.FindByText(YearEdit.Text));

                ArrayList arLecLists = lec.SelectProgrammeID("", "");
                DropDownList dddls = (e.Item.FindControl("ddlProgrammeID") as DropDownList);
                Label ProgrammeIDEdit = (Label)e.Item.Cells[2].FindControl("lblProgrammeIDEdit");

                dddls.DataSource = arLecLists;
                dddls.DataTextField = "Value";
                dddls.DataValueField = "Key";
                dddls.DataBind();
                dddls.SelectedIndex = dddls.Items.IndexOf(dddls.Items.FindByText(ProgrammeIDEdit.Text));

                ArrayList arLecListss = lec.SelectIntake("", "");
                DropDownList dddlss = (e.Item.FindControl("ddlIntake") as DropDownList);
                Label IntakeEdit = (Label)e.Item.Cells[2].FindControl("lblIntakeEdit");

                dddlss.DataSource = arLecListss;
                dddlss.DataTextField = "Value";
                dddlss.DataValueField = "Key";
                dddlss.DataBind();
                dddlss.SelectedIndex = dddlss.Items.IndexOf(dddlss.Items.FindByText(IntakeEdit.Text));
            }
        }

        protected void dgEdit_Student(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgStudent.EditItemIndex = e.Item.ItemIndex;
            SetupStudent();
        }

        protected void dgCancel_Student(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgStudent.EditItemIndex = -1;
            SetupStudent();
        }


        protected void dgStudent_ItemCommand(object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            Student student = new Student();

            if (e.CommandName == "AddStudent")
            {
                TextBox txtTempStudentID = (TextBox)e.Item.Cells[0].FindControl("txtStudentIDNew");
                DropDownList txtTempYear = (DropDownList)e.Item.Cells[1].FindControl("ddlYearNew");
                DropDownList txtTempIntake = (DropDownList)e.Item.Cells[2].FindControl("ddlIntakeNew");
                DropDownList txtTempProgramme = (DropDownList)e.Item.Cells[3].FindControl("ddlProgrammeIDNew");
                TextBox txtTempName = (TextBox)e.Item.Cells[4].FindControl("txtNameNew");
                TextBox txtTempPhone = (TextBox)e.Item.Cells[5].FindControl("txtPhoneNew");


                if (txtTempName.Text.Trim() == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Please fill in the student name!', 'error')</script>'");
                }
                else if (txtTempStudentID.Text.Trim() == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Please fill in the student ID!', 'error')</script>'");
                }
                else
                {
                    if (student.Add(txtTempStudentID.Text, txtTempYear.Text, txtTempIntake.Text, txtTempProgramme.Text, txtTempName.Text, txtTempPhone.Text) == false)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Cannot have same duplicate student ID!', 'error')</script>'");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal({title: 'Added!', text: 'This " + txtTempName.Text + " student is added successfully, your new password is (holdDoc)', type : 'success', confirmButtonText : 'OK'}, function (isConfirm) { if (isConfirm) { window.location.href = 'manageStudent.aspx'; }});</script>'");
                    }
                }
            }

            else if (e.CommandName == "DeleteStudent")
            {

                Label rowStudentID = (Label)e.Item.Cells[0].FindControl("lblStudentID");

                if (student.DeleteStudent(rowStudentID.Text) == false)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Delete this student record unsuccessfully', 'error')</script>'");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal({title: 'Deleted!', text: 'Deleted this student record successfully', type : 'success', confirmButtonText : 'OK'}, function (isConfirm) { if (isConfirm) { window.location.href = 'manageStudent.aspx'; }});</script>'");
                }

            }
            else if (e.CommandName == "ResetPassword")
            {
                Label rowStudentID = (Label)e.Item.Cells[0].FindControl("lblStudentID");

                if (student.ResetPassword(rowStudentID.Text) == false)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Reset Password unsuccessfully', 'error')</script>'");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal({title: 'Reset!', text: 'Reset Password successfully, your new password is (holdDoc)', type : 'success', confirmButtonText : 'OK'}, function (isConfirm) { if (isConfirm) { window.location.href = 'manageStudent.aspx'; }});</script>'");
                }
            }
        }

        protected void dgUpdate_Student(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            Label rowStudentID = (Label)e.Item.Cells[0].FindControl("lblStudentID");
            DropDownList txtTempYear = (DropDownList)e.Item.Cells[1].FindControl("ddlYear");
            DropDownList txtTempIntake = (DropDownList)e.Item.Cells[2].FindControl("ddlIntake");
            DropDownList txtTempProgrammeID = (DropDownList)e.Item.Cells[3].FindControl("ddlProgrammeID");
            TextBox txtTempName = (TextBox)e.Item.Cells[4].FindControl("txtName");
            TextBox txtTempPhone = (TextBox)e.Item.Cells[5].FindControl("txtPhone");

            //Validation for name (No digit allowed and only up to 30 characters)
            Regex nameRegex = new Regex(@"[a-zA-Z ]{1,30}$");
            Match checkName = nameRegex.Match(txtTempName.Text);

            //Validation for phone (Only 10-11 digit is allowed)
            Regex phoneRegex = new Regex(@"^(01)[0-9]*[0-9]{8,9}$");
            Match checkPhone = phoneRegex.Match(txtTempPhone.Text);

            if (rowStudentID.Text.Trim() == "" || txtTempName.Text.Trim() == "" || txtTempPhone.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Please fill in the student information first!', 'error')</script>'");
            }
            else if (!checkName.Success)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'The Admin Name is not allowed. Admin name is only support character, space and up to 30 characters only.', 'error')</script>'");
            }
            else if (!checkPhone.Success)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'The phone number is not allowed. Phone number is only support 7 or 8 digits', 'error')</script>'");
            }
            else
            {

                Student lec = new Student();

                if (lec.Update(rowStudentID.Text, txtTempYear.Text, txtTempIntake.Text, txtTempProgrammeID.Text, txtTempName.Text, txtTempPhone.Text) == false)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'This " + txtTempName.Text + " (" + rowStudentID.Text + ") student cannot be updated successfully!', 'error')</script>'");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal({title: 'Updated!', text: 'This " + txtTempName.Text + " (" + rowStudentID.Text + ") student is updated successfully', type : 'success', confirmButtonText : 'OK'}, function (isConfirm) { if (isConfirm) { window.location.href = 'manageStudent.aspx'; }});</script>'");
                }
            }
        }

        protected void dgStudent_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}