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
    public partial class cpProgramme : System.Web.UI.Page
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
                    SetupProgramme();
                }
                else
                {
                    Response.Redirect("~/Web/login.aspx");
                }
            }
        }

        public void SetupProgramme()
        {
            dgProgramme.ShowFooter = true;
            Programme programme = new Programme();
            DataSet ds = programme.GetDisplay();
            dgProgramme.DataSource = ds.Tables["Programme"];
            dgProgramme.DataBind();
        }


        protected void dgProgramme_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            dgProgramme.CurrentPageIndex = e.NewPageIndex;
            SetupProgramme();
        }

        protected void dgProgramme_ItemDataBound(object source, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                ((Button)e.Item.FindControl("btnDeleteProgramme")).Attributes.Add("onClick", "return confirmDeleteProgramme(this);");
                ((WebControl)e.Item.Cells[3].Controls[0]).CssClass = "btn btn-warning";
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                ((WebControl)e.Item.Cells[3].Controls[0]).CssClass = "btn btn-warning";
                ((WebControl)e.Item.Cells[3].Controls[2]).CssClass = "btn btn-secondary";
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Programme programme = new Programme();
                ArrayList facultyList = programme.SelectFacultyID("", "");
                DropDownList dddl = (e.Item.FindControl("ddlFacultyNameNew") as DropDownList);

                dddl.DataSource = facultyList;
                dddl.DataTextField = "Value";
                dddl.DataValueField = "Key";
                dddl.DataBind();
            }
            else if (e.Item.ItemType == ListItemType.EditItem)
            {
                Programme programme = new Programme();
                ArrayList facultyList = programme.SelectFacultyID("", "");
                DropDownList dddl = (e.Item.FindControl("ddlFacultyName") as DropDownList);
                Label FacultyName = (Label)e.Item.Cells[1].FindControl("lblFacultyNameEdit");

                dddl.DataSource = facultyList;
                dddl.DataTextField = "Value";
                dddl.DataValueField = "Key";
                dddl.DataBind();
                dddl.SelectedIndex = dddl.Items.IndexOf(dddl.Items.FindByText(FacultyName.Text));
            }
        }
        protected void dgEdit_Programme(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgProgramme.EditItemIndex = e.Item.ItemIndex;
            SetupProgramme();
        }

        protected void dgCancel_Programme(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgProgramme.EditItemIndex = -1;
            SetupProgramme();
        }


        protected void dgProgramme_ItemCommand(object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            Programme programme = new Programme();

            if (e.CommandName == "AddProgramme")
            {
                TextBox txtTempProgrammeCode = (TextBox)e.Item.Cells[0].FindControl("txtProgrammeNew");
                DropDownList txtTempFacultyID = (DropDownList)e.Item.Cells[1].FindControl("ddlFacultyNameNew");
                TextBox txtTempProgrammeName = (TextBox)e.Item.Cells[2].FindControl("txtProgrammeNameNew");

                Regex codeRegex = new Regex(@"[A-Z]{3}$");
                Match checkCode = codeRegex.Match(txtTempProgrammeCode.Text);

                if (txtTempProgrammeCode.Text.Trim() == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Please fill in the programme code!', 'error')</script>'");
                }
                else if (!checkCode.Success)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'The programme code must have 3 uppercase characters', 'error')</script>'");
                }
                else if (txtTempFacultyID.Text.Trim() == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Please fill in the faculty ID!', 'error')</script>'");
                }
                else if (txtTempProgrammeName.Text.Trim() == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Please fill in the programme name!', 'error')</script>'");
                }
                else
                {
                    if (programme.Add(txtTempProgrammeCode.Text, txtTempFacultyID.Text, txtTempProgrammeName.Text) == false)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'This code is existed already, try another programme code!', 'error')</script>'");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal({title: 'Added!', text: 'This " + txtTempProgrammeName.Text + " (" + txtTempProgrammeCode.Text + ") programme is added successfully', type : 'success', confirmButtonText : 'OK'}, function (isConfirm) { if (isConfirm) { window.location.href = 'cpProgramme.aspx'; }});</script>'");
                    }
                }
            }

            else if (e.CommandName == "DeleteProgramme")
            {
                Label rowProgrammeID = (Label)e.Item.Cells[0].FindControl("lblID");

                if (programme.DeleteProgramme(rowProgrammeID.Text) == false)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Delete this programme unsuccessfully', 'error')</script>'");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal({title: 'Deleted!', text: 'Deleted this programme successfully', type : 'success', confirmButtonText : 'OK'}, function (isConfirm) { if (isConfirm) { window.location.href = 'cpProgramme.aspx'; }});</script>'");
                }
            }

        }

        protected void dgUpdate_Programme(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            Label lblTempID = (Label)e.Item.Cells[0].FindControl("txtID");
            TextBox txtTempProgrammeCode = (TextBox)e.Item.Cells[0].FindControl("txtProgrammeID");
            DropDownList txtTempFacultyID = (DropDownList)e.Item.Cells[1].FindControl("ddlFacultyName");
            TextBox txtTempProgrammeName = (TextBox)e.Item.Cells[2].FindControl("txtProgrammeName");

            Regex codeRegex = new Regex(@"[A-Z]{3}$");                          
            Match checkCode = codeRegex.Match(txtTempProgrammeCode.Text);

            if (lblTempID.Text.Trim() == "" || txtTempProgrammeCode.Text.Trim() == "" || txtTempFacultyID.Text.Trim() == "" || txtTempProgrammeName.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Please fill in the programme information first!', 'error')</script>'");
            }
            else if (!checkCode.Success)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'The programme code must have 3 upppercase characters', 'error')</script>'");
            }
            else
            {

                Programme programme = new Programme();
                if (programme.Update(lblTempID.Text, txtTempProgrammeCode.Text, txtTempFacultyID.Text, txtTempProgrammeName.Text) == false)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'This code is existed already, try another programme code!', 'error')</script>'");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal({title: 'Updated!', text: 'This " + txtTempProgrammeName.Text + " (" + txtTempProgrammeCode.Text + ") programme is updated successfully', type : 'success', confirmButtonText : 'OK'}, function (isConfirm) { if (isConfirm) { window.location.href = 'cpProgramme.aspx'; }});</script>'");
                }
            }
        }

        protected void dgProgramme_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}