using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fyp_dms.Web.Studentt
{
    public partial class studentLayout : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // * TODO check admin login session
            if (Session["Username"] == null)
            {
                Response.Redirect("~/Web/login.aspx");
            }


            if (!IsPostBack)
            {
                string identity = Session["Identity"].ToString();
                if (String.Equals(identity, "Student"))
                {
                    SessionUsername.DataBind();
                    Label1.Text = SessionUsername.Text;
                }
                else
                {
                    Response.Redirect("~/Web/login.aspx");
                }
            }
        }

        protected void lbLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();

            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successLogoutAlert();", true);
        }
    }
}