using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Calendar;
using Telerik.Web.UI;

public partial class Administrator_SMSMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["objLogin"] != null)
        {
            ObjLogin adm = (ObjLogin)Session["objLogin"];                       
            switch(System.IO.Path.GetFileName(Page.Request.FilePath).ToLower())
            {
                case "smscalendar.aspx": RadPanelBar1.Items[1].Expanded = true; break;
                case "smscontact.aspx": RadPanelBar1.Items[2].Expanded = true; break;
                default: RadPanelBar1.Items[0].Expanded = true; break;
            }
            //if (Application["SelectedDate"] != null)
            //{
            //    string script = "var datePicker = $find('ctl00_RadPanelBar1_i1_i0_Calendar1');datePicker.set_selectedDate('" + Application["SelectedDate"] + "');";
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "SetDate", script, true);
            //    Application["SelectedDate"] = null;
            //}
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }        
    }    

    protected void SelectedDateChange(object sender, SelectedDatesEventArgs e)
    {
        var strDate = e.SelectedDates[e.SelectedDates.Count - 1].Date.ToShortDateString();
        SelectedDate.Value = strDate.Replace("/", "");
    }

    protected void RadPanelBar1_ItemClick(object sender, Telerik.Web.UI.RadPanelBarEventArgs e)
    {
        string controlClicked = e.Item.Text;
        switch (e.Item.Text)
        {
            case "SMS List":
            case "Inbox": Response.Redirect("~/Administrator/Default.aspx"); break;
            case "Outbox": Response.Redirect("~/Administrator/SMSOutbox.aspx"); break;
            case "Deleted Items": Response.Redirect("~/Administrator/SMSDeletedItem.aspx"); break;
            case "Calendar": Response.Redirect("~/Administrator/SMSCalendar.aspx"); Session.Contents.Remove("SMSDate"); break;
            case "Contacts":
            case "My Contacts": Response.Redirect("~/Administrator/SMSContact.aspx?t=1"); break;
            case "My Customer": Response.Redirect("~/Administrator/SMSContact.aspx?t=2"); break;
            case "Failure": Response.Redirect("~/Administrator/SMSFailure.aspx"); break;
        }
    }

}
