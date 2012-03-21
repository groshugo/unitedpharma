using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Salemans_ViewDetailDashboard : System.Web.UI.Page
{
    DashboardRepository DRepo = new DashboardRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int Id = int.Parse(Request.QueryString["ID"]);
            var a = DRepo.GetById(Id);
            if (a != null)
            {
                lblTitle.Text = a.Title;
                ltrContent.Text = a.Content;
                Page.Title = "Message from phone number " + a.SenderPhoneNumber;
            }
            DRepo.UpdateStatus(Id);
        }
        catch
        {
        }
    }
}