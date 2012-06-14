using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;

public partial class Customers_CustomerMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["objLogin"] != null)
        {            
            switch (System.IO.Path.GetFileName(Page.Request.FilePath).ToLower())
            {
                case "smscalendar.aspx": 
                    RadPanelBar1.Items[1].Expanded = true;
                    break;
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
        if (e != null && e.SelectedDates != null && e.SelectedDates.Count > 0)
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
            case "Inbox": Response.Redirect("~/Customers/Default.aspx"); break;
            case "Outbox": Response.Redirect("~/Customers/SMSOutbox.aspx"); break;            
            case "Calendar": Response.Redirect("~/Customers/SMSCalendar.aspx"); Session.Contents.Remove("SMSDate"); break;
            case "Contacts":
            case "My Contacts": Response.Redirect("~/Customers/SMSContact.aspx"); break;            
        }
    }

   protected void RadPanelBar1_ItemCreated(object sender, RadPanelBarEventArgs e)
    {
        var radPanel = ((RadPanelBar)sender).FindItemByText("Calendar");
        if (radPanel != null)
        {
            var cal1 = radPanel.FindControl("Calendar1") as RadCalendar;
            if (cal1 != null)
            {
                var currentDateString = Request.QueryString["dt1"];
                if (!string.IsNullOrEmpty(currentDateString))
                {
                    try
                    {
                        var dateArr = currentDateString.Split('/');
                        var year = int.Parse(dateArr[2]);
                        var month = int.Parse(dateArr[0]);
                        var day = int.Parse(dateArr[1]);

                        var currentDate = new DateTime(year, month, day);

                        cal1.SelectedDate = currentDate;
                    }
                    catch (Exception)
                    {
                        // write log
                    }
                }
            }
        }
    }

    
}
