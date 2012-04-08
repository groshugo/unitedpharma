using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Customers_Default : System.Web.UI.Page
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
                var listVwSMS = FRepo.GetInboxSMS(cust.Phone);
                RadGrid1.EnableAjaxSkinRendering = true;
                RadGrid1.DataSource = listVwSMS;
                RadGrid1.DataBind();

                // Count SMS per day, get OutSms to count
                int usedSms = 0;
                foreach (var vwSms in FRepo.GetOutboxSMS(cust.Phone))
                {
                    if (vwSms.Date.HasValue && vwSms.Date.Value.DayOfYear == DateTime.Now.DayOfYear)
                    {
                        usedSms += 1;
                    }
                }

                litSmsStatus.Text = string.Format("Used : {0} SMS - Remaining : {1} SMS.<br />(You can send 5 SMS message per day)", usedSms, 5 - usedSms);

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
        ObjLogin cust = (ObjLogin)Session["objLogin"];
        RadGrid1.DataSource = FRepo.GetInboxSMS(cust.Phone);
    }

    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            if (item["IsReadCol"].Text == "False")
            {
                item.Font.Bold = true;
            }
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        ObjLogin cust = (ObjLogin)Session["objLogin"];
        int typeFilter = Convert.ToInt32(cbFilterType.SelectedValue);
        RadGrid1.DataSource = FRepo.FilterInboxSMS(typeFilter, txtFilterValue.Text.Trim(), cust.Phone);
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
        ddlPromotion.DataSource = ScheduleRepo.GetListPhonePromotion(cust.Phone, Constant.inbox);
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
            RadGrid1.DataSource = FRepo.GetSMSByPromotionId(cust.Phone, promotionId);
        }
        else
        {
            RadGrid1.DataSource = FRepo.GetInboxSMS(cust.Phone);
        }
        RadGrid1.DataBind();
    }
}