using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Customers_ViewDetailSMS : System.Web.UI.Page
{
    SMSObjRepository repo = new SMSObjRepository();
    private const int NumberSmsPerday = 5;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] != null)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["T"] != null)
                {
                    if (Request.QueryString["T"] == "1")
                    {
                        btnReply.Visible = true;
                    }
                    else
                    {
                        btnReply.Visible = false;
                    }
                }
                Utility.SetCurrentMenu("mSms");
                try
                {
                    ObjLogin cust = (ObjLogin)Session["objLogin"];
                    if (repo.CheckOwner(cust.Phone, Convert.ToInt32(Request.QueryString["ID"])))
                    {
                        List<vwSMS> rs = new List<vwSMS>();
                        repo.GetListParentSMS(Convert.ToInt32(Request.QueryString["ID"]), rs);
                        lvSMS.DataSource = rs;
                        lvSMS.DataBind();
                        repo.SetIsRead(Convert.ToInt32(Request.QueryString["ID"]));

                        if(!CanSendSmsToday(cust.Phone))
                        {
                            btnReply.Enabled = false;
                            btnReply.ToolTip = string.Format("You can send 5 SMS message per day");
                        }
                    }
                    else
                    {
                        Response.Redirect("~/Customers/Default.aspx");
                    }
                }
                catch
                {
                    Response.Redirect("~/Customers/Default.aspx");
                }
            }
        }
        else
        {
            Response.Redirect("~/Customers/Default.aspx");
        }
    }

    protected void btnReply_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] != null)
        {
            Response.Redirect("~/Customers/SMSReply.aspx?ID=" + Request.QueryString["ID"]);
        }
        else
        {
            Response.Redirect("~/Customers/Default.aspx");
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Customers/Default.aspx");
    }

    private bool CanSendSmsToday(string phone)
    {
        // Count SMS per day, get OutSms to count
        int usedSms = repo.GetOutboxSMS(phone).Count(vwSms => vwSms.Date.HasValue && vwSms.Date.Value.DayOfYear == DateTime.Now.DayOfYear);

        return usedSms < NumberSmsPerday;
    }
}