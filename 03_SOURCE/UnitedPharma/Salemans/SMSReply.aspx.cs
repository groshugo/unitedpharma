﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Salemans_SMSReply : System.Web.UI.Page
{
    private SMSObjRepository repo = new SMSObjRepository();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] != null)
        {
            if (!IsPostBack)
            {
                Utility.SetCurrentMenu("mSms");
                try
                {
                    ObjLogin adm = (ObjLogin) Session["objLogin"];
                    if (repo.CheckOwner(adm.Phone, Convert.ToInt32(Request.QueryString["ID"])))
                    {
                        List<vwSMS> rs = new List<vwSMS>();
                        rs = repo.GetSMSById(Convert.ToInt32(Request.QueryString["ID"]));
                        var result = rs.FirstOrDefault();
                        txtPhoneNumber.Text = result.SenderPhone;

                        txtTitle.Text = string.Format("Re: {0}", result.Subject);

                        txtReplyContent.Text = string.Format("\n-----------\n{0}", result.Content);
                    }
                    else
                    {
                        Response.Redirect("~/Salemans/Default.aspx");
                    }
                }
                catch
                {
                    Response.Redirect("~/Salemans/Default.aspx");
                }
            }
        }
        else
        {
            Response.Redirect("~/Salemans/Default.aspx");
        }
    }

    protected void btnAbort_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    protected void btnSendSMS_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
        {
            txtTitle.Focus();
        }
        else
        {
            List<vwSMS> rs = new List<vwSMS>();
            ObjLogin sale = (ObjLogin) Session["objLogin"];
            rs = repo.GetSMSById(Convert.ToInt32(Request.QueryString["ID"]));
            var result = rs.FirstOrDefault();
            repo.InsertSMS(result.SMSCode, (int) result.Id, sale.Phone, Constant.SalemenType, txtPhoneNumber.Text,
                           (int) result.SenderType, DateTime.Now, txtTitle.Text, txtReplyContent.Text, true, false,
                           false, (int) result.SmsTypeId, (int) result.PromotionId);
            Response.Redirect("Default.aspx");
        }
    }

}