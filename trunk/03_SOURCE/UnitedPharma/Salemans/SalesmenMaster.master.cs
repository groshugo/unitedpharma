using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Calendar;
using Telerik.Web.UI;

public partial class Salemans_SalesmenMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["objLogin"] != null)
        {            
            switch (System.IO.Path.GetFileName(Page.Request.FilePath).ToLower())
            {
                case "smscalendar.aspx": RadPanelBar1.Items[1].Expanded = true; break;
                case "smscontact.aspx": RadPanelBar1.Items[2].Expanded = true; break;
                default: RadPanelBar1.Items[0].Expanded = true; break;
            }
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
    }

    protected void SelectedDateChange(object sender, SelectedDatesEventArgs e)
    {
        if (e.SelectedDates != null && e.SelectedDates.Count > 0)
        {
            var strDate = e.SelectedDates[e.SelectedDates.Count - 1].Date.ToShortDateString();
            SelectedDate.Value = strDate.Replace("/", "");
        }
    }

    protected void RadPanelBar1_ItemClick(object sender, Telerik.Web.UI.RadPanelBarEventArgs e)
    {
        string controlClicked = e.Item.Text;
        switch (e.Item.Text)
        {
            case "SMS List":
            case "Inbox": Response.Redirect("~/Salemans/Default.aspx"); break;
            case "Outbox": Response.Redirect("~/Salemans/SMSOutbox.aspx"); break;
            case "Calendar": Response.Redirect("~/Salemans/SMSCalendar.aspx"); Session.Contents.Remove("SMSDate"); break;
            case "Contacts":
            case "My Contacts": Response.Redirect("~/Salemans/SMSContact.aspx?t=1"); break;
            case "My Customer": Response.Redirect("~/Salemans/CustomersManagement.aspx"); break;
            case "Failure": Response.Redirect("~/Salemans/SMSFailure.aspx"); break;
        }
    }
}
