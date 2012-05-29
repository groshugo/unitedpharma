using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Salemans_SMSForward : System.Web.UI.Page
{
    SMSObjRepository repo = new SMSObjRepository();
    SalesmanRepository SRepo = new SalesmanRepository();
    CustomersRepository CRepo = new CustomersRepository();
    AdministratorRepository ARepo = new AdministratorRepository();
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
                        string listPhone = txtTitle.Text;
                        char[] separator = new char[] { ',' };
                        string[] phoneList = listPhone.Split(separator);
                        List<vwSMS> rs = new List<vwSMS>();
                        rs = repo.GetSMSById(Convert.ToInt32(Request.QueryString["ID"]));
                        if (rs != null)
                        {
                            var result = rs.FirstOrDefault();
                            if(result != null)
                            {
                                txtTitle.Text = string.Format("Fwd: {0}", result.Subject);
                                txtForwardContent.Text = string.Format("\n---------- Forwarded message ----------\n{0}",
                                                                       result.Content);
                            }
                        }
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
    private void ShowErrorMessage(string message)
    {
        RadAjaxManager1.ResponseScripts.Add(string.Format("window.radalert(\"" + message + "\")"));
    }
    protected void btnSendSMS_Click(object sender, EventArgs e)
    {
        if(string.IsNullOrEmpty(txtTitle.Text.Trim()))
        {
            ShowErrorMessage("Please provide title to forward");
            txtTitle.Focus();
            return;
        }

        ObjLogin adm = (ObjLogin)Session["objLogin"];
        if (repo.CheckOwner(adm.Phone, Convert.ToInt32(Request.QueryString["ID"])))
        {
            if (!String.IsNullOrEmpty(txtPhoneNumbers.Text))
            {
                string listPhone = txtPhoneNumbers.Text;
                char[] separator = new char[] { ',' };
                string[] phoneList = listPhone.Split(separator);
                List<vwSMS> rs = new List<vwSMS>();
                rs = repo.GetSMSById(Convert.ToInt32(Request.QueryString["ID"]));
                var result = rs.FirstOrDefault();
                foreach (string phone in phoneList)
                {
                    if (SRepo.CheckSalemenByPhoneNumber(phone))
                        repo.InsertSMS(result.SMSCode, 0, adm.Phone, Constant.SalemenType, phone, Constant.SalemenType, DateTime.Now, txtTitle.Text, txtForwardContent.Text, true, false, false, (int)result.SmsTypeId, (int)result.PromotionId);
                    else if (CRepo.IsExistedCustomerByPhone(phone))
                        repo.InsertSMS(result.SMSCode, 0, adm.Phone, Constant.SalemenType, phone, Constant.CustomerType, DateTime.Now, txtTitle.Text, txtForwardContent.Text, true, false, false, (int)result.SmsTypeId, (int)result.PromotionId);
                    else if (ARepo.GetAdminByPhoneNumber(phone))
                    {
                        repo.InsertSMS(result.SMSCode, 0, adm.Phone, Constant.SalemenType, phone, Constant.AdminType, DateTime.Now, txtTitle.Text, txtForwardContent.Text, true, false, false, (int)result.SmsTypeId, (int)result.PromotionId);
                    }
                }
                Response.Redirect("~/Salemans/Default.aspx");
            }
            else
            {
                ShowErrorMessage("Phone numer is not allow null.");
                txtPhoneNumbers.Focus();
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
}