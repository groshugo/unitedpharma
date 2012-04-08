using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administrator_ViewDashboardDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            int dashboardId;
            if(int.TryParse(Request.QueryString["ID"], out dashboardId))
            {
                // get dashboard
                var dashboardRepo = new DashboardRepository();
                var dashboard = dashboardRepo.GetById(dashboardId);
                BindDashboardToUI(dashboard);
            }
            else
            {
                lblMessage.Text = "Cannot get Dashboard, please try again later";
            }
        }
    }

    private void BindDashboardToUI(Dashboard dashboard)
    {
        if (dashboard == null) throw new ArgumentNullException("dashboard");

        litTitle.Text = dashboard.Title;
        litContent.Text = dashboard.Content;
        AttachedFile.Text = dashboard.AttachedFileName;
        AttachedFile.NavigateUrl = string.Format("/Upload/Attachments/{0}", dashboard.AttachedFileName);
        litSenderPhoneNumber.Text = dashboard.SenderPhoneNumber;
        litReceiverPhoneNumber.Text = dashboard.ReceiverPhoneNumber;
    }
}