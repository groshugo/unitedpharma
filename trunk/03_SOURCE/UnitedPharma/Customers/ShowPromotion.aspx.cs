using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Customers_ShowPromotion : System.Web.UI.Page
{
    SchedulePromotionRepository ScheduleRepo = new SchedulePromotionRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.SetCurrentMenu("mPromotion");
            ObjLogin cust = (ObjLogin)Session["objLogin"];
            if (cust != null)
            {
                RadGrid1.DataSource = ScheduleRepo.GetAllViewSchedulePromotion(cust.Phone);
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
        if (cust != null)
        {
            RadGrid1.DataSource = ScheduleRepo.GetAllViewSchedulePromotion(cust.Phone);
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }        
    }

    protected void RadGrid1_SelectedIndexChanged(object source, EventArgs e)
    {
        string id = RadGrid1.SelectedValue.ToString();
        RadWindow1.VisibleOnPageLoad = true;
        RadWindow1.VisibleStatusbar = false;
        RadWindow1.Behaviors = WindowBehaviors.Close;
        RadWindow1.NavigateUrl = "DetailPromotion.aspx?id=" + id;
    }
}