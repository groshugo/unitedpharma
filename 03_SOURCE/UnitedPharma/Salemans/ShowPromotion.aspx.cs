using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Salemans_ShowPromotion : System.Web.UI.Page
{
    PromotionRepository repo = new PromotionRepository();
    SchedulePromotionRepository SRepo = new SchedulePromotionRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.SetCurrentMenu("mPromotion");
            RadGrid1.DataSource = SRepo.GetAllApprovedPromotions();
            
        }
    } 
    
    protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        RadGrid1.DataSource = SRepo.GetAllApprovedPromotions();
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