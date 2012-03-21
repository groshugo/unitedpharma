using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Customers_SMSReply : System.Web.UI.Page
{
    SMSObjRepository repo = new SMSObjRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] != null)
        {
            if (!IsPostBack)
            {
                Utility.SetCurrentMenu("mSms");
                try
                {
                    ObjLogin adm = (ObjLogin)Session["objLogin"];
                    if (repo.CheckOwner(adm.Phone, Convert.ToInt32(Request.QueryString["ID"])))
                    {
                        List<vwSMS> rs = new List<vwSMS>();
                        rs = repo.GetSMSById(Convert.ToInt32(Request.QueryString["ID"]));
                        var result = rs.FirstOrDefault();
                        txtPhoneNumber.Text = result.SenderPhone;
                        if (!result.Subject.Contains("RE:"))
                            txtTitle.Text = "RE: " + result.Subject;
                        else
                            txtTitle.Text = result.Subject;
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

    protected void btnAbort_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    protected void btnSendSMS_Click(object sender, EventArgs e)
    {
        List<vwSMS> rs = new List<vwSMS>();
        if (!string.IsNullOrEmpty(txtReplyContent.Text.Trim()) || !string.IsNullOrEmpty(txtTitle.Text.Trim()))
        {
            ObjLogin adm = (ObjLogin)Session["objLogin"];
            rs = repo.GetSMSById(Convert.ToInt32(Request.QueryString["ID"]));
            var result = rs.FirstOrDefault();
            repo.InsertSMS(result.SMSCode, (int)result.Id, adm.Phone, Constant.CustomerType, txtPhoneNumber.Text, (int)result.SenderType, DateTime.Now, txtTitle.Text.Trim(), txtReplyContent.Text.Trim(), true, false, false, (int)result.SmsTypeId, (int)result.PromotionId);
            Response.Redirect("Default.aspx");
        }
    }
}