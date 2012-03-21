using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Calendar;

public partial class Customers_CustomerMaster : System.Web.UI.MasterPage
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
        string strDate = e.SelectedDates[e.SelectedDates.Count - 1].Date.ToShortDateString().Replace("/", "");
        Response.Redirect("~/Customers/SMSCalendar.aspx?dt=" + strDate);
    }

    protected void RadPanelBar1_ItemClick(object sender, Telerik.Web.UI.RadPanelBarEventArgs e)
    {
        string controlClicked = e.Item.Text;
        switch (e.Item.Text)
        {
            case "SMS List":
            case "Inbox": Response.Redirect("~/Customers/Default.aspx"); break;
            case "Outbox": Response.Redirect("~/Customers/SMSOutbox.aspx"); break;            
            case "Calendar": Response.Redirect("~/Customers/SMSCalendar.aspx"); Session.Contents.Remove("SMSDate"); break;
            case "Contacts":
            case "My Contacts": Response.Redirect("~/Customers/SMSContact.aspx"); break;            
        }
    }
}
