using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Customers_SMSOutbox : System.Web.UI.Page
{
    SMSObjRepository FRepo = new SMSObjRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.SetCurrentMenu("mSms");
            ObjLogin cust = (ObjLogin)Session["objLogin"];
            if (cust != null)
            {
                RadGrid1.EnableAjaxSkinRendering = true;
                RadGrid1.DataSource = FRepo.GetOutboxSMS(cust.Phone);
                RadGrid1.DataBind();

                PromotionList();
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }
    }

    protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        ObjLogin adm = (ObjLogin)Session["objLogin"];
        RadGrid1.DataSource = FRepo.GetOutboxSMS(adm.Phone);
    }        

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        ObjLogin cust = (ObjLogin)Session["objLogin"];
        int typeFilter = Convert.ToInt32(cbFilterType.SelectedValue);
        RadGrid1.DataSource = FRepo.FilterOutboxSMS(typeFilter, txtFilterValue.Text.Trim(), cust.Phone);
        RadGrid1.DataBind();
    }

    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        RadGrid1.Rebind();
    }

    private void ShowMessage(string message)
    {
        RadAjaxManager1.ResponseScripts.Add(string.Format("window.radalert(\"" + message + "!\")"));
    }

    SchedulePromotionRepository ScheduleRepo = new SchedulePromotionRepository();
    private void PromotionList()
    {
        ObjLogin cust = (ObjLogin)Session["objLogin"];
        ddlPromotion.DataSource = ScheduleRepo.GetListPhonePromotion(cust.Phone,Constant.outbox);
        ddlPromotion.DataTextField = "Title";
        ddlPromotion.DataValueField = "Id";
        ddlPromotion.DataBind();
        RadComboBoxItem item = new RadComboBoxItem("Select all", "0");
        ddlPromotion.Items.Insert(0, item);
    }
    protected void ddlPromotion_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        ObjLogin cust = (ObjLogin)Session["objLogin"];
        int promotionId = int.Parse(ddlPromotion.SelectedValue);
        if (promotionId > 0)
        {
            RadGrid1.DataSource = FRepo.GetOutboxSMSByPromotionId(cust.Phone, promotionId);
        }
        else
        {
            RadGrid1.DataSource = FRepo.GetOutboxSMS(cust.Phone);
        }
        RadGrid1.DataBind();
    }
}