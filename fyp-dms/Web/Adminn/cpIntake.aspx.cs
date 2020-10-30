using fyp_dms.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fyp_dms.Web.Adminn
{
    public partial class cpIntake : System.Web.UI.Page
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
                    SetupIntake();
                }
                else
                {
                    Response.Redirect("~/Web/login.aspx");
                }
            }
        }

        public void SetupIntake()
        {
            dgIntake.ShowFooter = true;
            Intake intake = new Intake();
            DataSet ds = intake.GetDisplay();
            dgIntake.DataSource = ds.Tables["Intake"];
            dgIntake.DataBind();
        }

        protected void dgIntake_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            dgIntake.CurrentPageIndex = e.NewPageIndex;
            SetupIntake();
        }



        protected void dgIntake_ItemDataBound(object source, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                ((Button)e.Item.FindControl("btnDeleteIntake")).Attributes.Add("onClick", "return confirmDeleteIntake(this);");
                ((WebControl)e.Item.Cells[1].Controls[0]).CssClass = "btn btn-warning";
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                ((WebControl)e.Item.Cells[1].Controls[0]).CssClass = "btn btn-warning";
                ((WebControl)e.Item.Cells[1].Controls[2]).CssClass = "btn btn-secondary";
            }
        }

        protected void dgEdit_Intake(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgIntake.EditItemIndex = e.Item.ItemIndex;
            SetupIntake();
        }

        protected void dgCancel_Intake(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgIntake.EditItemIndex = -1;
            SetupIntake();
        }


        protected void dgIntake_ItemCommand(object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            Intake intake = new Intake();

            if (e.CommandName == "AddIntake")
            {
                TextBox txtTempIntake = (TextBox)e.Item.Cells[1].FindControl("txtMonthNew");

               if (txtTempIntake.Text.Trim() == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Please fill in the intake Month!', 'error')</script>'");
                }
                else
                {
                    if (intake.Add(txtTempIntake.Text) == false)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'This intake " + txtTempIntake.Text + " is existed already, try another month', 'error')</script>'");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal({title: 'Added!', text: '" + txtTempIntake.Text + " intake is added successfully', type : 'success', confirmButtonText : 'OK'}, function (isConfirm) { if (isConfirm) { window.location.href = 'cpIntake.aspx'; }});</script>'");
                    }
                }
            }

            else if (e.CommandName == "DeleteIntake")
            {
                Label rowIntakeID = (Label)e.Item.Cells[0].FindControl("lblID");

                if (intake.DeleteIntake(rowIntakeID.Text) == false)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Delete intake unsuccessfully', 'error')</script>'");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal({title: 'Deleted!', text: 'Deleted intake successfully', type : 'success', confirmButtonText : 'OK'}, function (isConfirm) { if (isConfirm) { window.location.href = 'cpIntake.aspx'; }});</script>'");
                }

            }

        }

        protected void dgUpdate_Intake(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            Label rowIntakeID = (Label)e.Item.Cells[0].FindControl("txtID");
            TextBox txtTempMonth = (TextBox)e.Item.Cells[1].FindControl("txtMonth");

            if (txtTempMonth.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Please fill in the intake information first!', 'error')</script>'");
            }
            else
            {
                Intake intake = new Intake();
                if (intake.Update(rowIntakeID.Text, txtTempMonth.Text) == false)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'This intake " + txtTempMonth.Text + " is existed already, try another month', 'error')</script>'");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal({title: 'Updated!', text: 'This " + txtTempMonth.Text + " intake is updated successfully', type : 'success', confirmButtonText : 'OK'}, function (isConfirm) { if (isConfirm) { window.location.href = 'cpIntake.aspx'; }});</script>'");
                }
            }
        }

        protected void dgIntake_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}