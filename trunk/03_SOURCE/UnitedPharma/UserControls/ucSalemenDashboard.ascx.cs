using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class UserControls_ucSalemenDashboard : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var sale = ((ObjLogin)Session["objLogin"]);
            if (sale != null)
            {
                var repo = new DashboardRepository();
                RadPanelItem rpi = new RadPanelItem();
                rpi.Text = "View all dashboards (" + repo.CountDashboardForsalemens(sale.Phone) + ")";                
                rpi.ImageUrl = "~/Images/bullet_blue.png";
                RadPanelBar1.GetAllItems()[0].Items.Add(rpi);
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }
    }
    protected void RadPanelBar1_ItemClick(object sender, Telerik.Web.UI.RadPanelBarEventArgs e)
    {
        Response.Redirect("~/Salemans/ShowDashboard.aspx");               
    }

    
 
}