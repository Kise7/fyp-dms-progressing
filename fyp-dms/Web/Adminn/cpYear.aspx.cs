using fyp_dms.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace fyp_dms.Web.Adminn
{
    public partial class cpYear : System.Web.UI.Page
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
                    SetupYear();
                }
                else
                {
                    Response.Redirect("~/Web/login.aspx");
                }
            }
        }

        public void SetupYear()
        {
            dgYear.ShowFooter = true;
            Year year = new Year();
            DataSet ds = year.GetDisplay();
            dgYear.DataSource = ds.Tables["Year"];
            dgYear.DataBind();
        }

        protected void dgYear_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            dgYear.CurrentPageIndex = e.NewPageIndex;
            SetupYear();
        }

        protected void dgYear_ItemDataBound(object source, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                ((Button)e.Item.FindControl("btnDeleteYear")).Attributes.Add("onClick", "return confirmDeleteYear(this);");
                ((WebControl)e.Item.Cells[1].Controls[0]).CssClass = "btn btn-warning";
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                ((WebControl)e.Item.Cells[1].Controls[0]).CssClass = "btn btn-warning";
                ((WebControl)e.Item.Cells[1].Controls[2]).CssClass = "btn btn-secondary";
            }
        }

        protected void dgEdit_Year(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgYear.EditItemIndex = e.Item.ItemIndex;
            SetupYear();
        }

        protected void dgCancel_Year(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgYear.EditItemIndex = -1;
            SetupYear();
        }


        protected void dgYear_ItemCommand(object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            Year year = new Year();

            if (e.CommandName == "AddYear")
            {
                TextBox txtTempYear = (TextBox)e.Item.Cells[0].FindControl("txtYearNew");

                //Validation for year 
                Regex yearRegex = new Regex(@"^\d{4}$");
                Match checkYear = yearRegex.Match(txtTempYear.Text);

                if (txtTempYear.Text.Trim() == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Please fill in the Year!', 'error')</script>'");
                }
                else if (!checkYear.Success)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Year must be 4 digit number!', 'error')</script>'");
                }
                else
                {
                    if (year.Add(txtTempYear.Text) == false)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'This year is existed already, try another year!', 'error')</script>'");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal({title: 'Added!', text: 'Year " + txtTempYear.Text + " is added successfully', type : 'success', confirmButtonText : 'OK'}, function (isConfirm) { if (isConfirm) { window.location.href = 'cpYear.aspx'; }});</script>'");
                    }
                }
            }

            else if (e.CommandName == "DeleteYear")
            {
                Label rowYearID = (Label)e.Item.Cells[0].FindControl("lblID");

                if (year.DeleteYear(rowYearID.Text) == false)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Deleted unsuccessfully', 'error')</script>'");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal({title: 'Deleted!', text: 'Deleted successfully', type : 'success', confirmButtonText : 'OK'}, function (isConfirm) { if (isConfirm) { window.location.href = 'cpYear.aspx'; }});</script>'");
                }

            }

        }

        protected void dgUpdate_Year(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            Label lblTempID = (Label)e.Item.Cells[0].FindControl("txtID");
            TextBox txtTempYear = (TextBox)e.Item.Cells[1].FindControl("txtYearID");

            //Validation for year 
            Regex yearRegex = new Regex(@"^\d{1,4}$");
            Match checkYear = yearRegex.Match(txtTempYear.Text);

            if (lblTempID.Text.Trim() == "" || txtTempYear.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Please fill in the year first!', 'error')</script>'");
            }
            else if (!checkYear.Success)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'Year must be 4 digit number!', 'error')</script>'");
            }
            else
            {
                Year year = new Year();
                if (year.Update(lblTempID.Text, txtTempYear.Text) == false)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script type='text/javascript'>swal('Error!', 'This " + txtTempYear.Text + " year is existed already, try another year', 'error')</script>'");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal({title: 'Updated!', text: 'Updated successfully', type : 'success', confirmButtonText : 'OK'}, function (isConfirm) { if (isConfirm) { window.location.href = 'cpYear.aspx'; }});</script>'");
                }
            }
        }

        protected void dgYear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}