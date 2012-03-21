using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_ucCustomerHeader : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["objLogin"] != null)
        {
            lblUsername.Text = ((ObjLogin)Session["objLogin"]).Fullname;
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
    }

    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Session.Contents.Remove("objLogin");
        Response.Redirect("~/Default.aspx");
    }
}