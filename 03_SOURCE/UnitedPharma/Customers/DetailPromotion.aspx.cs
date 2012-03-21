using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Customers_DetailPromotion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                var repo = new SchedulePromotionRepository();
                SchedulePromotion pr = repo.GetSchedulePromotionById(id);
                if (pr != null)
                {
                    ltrStartDate.Text = string.Format("{0: dddd, MMMM dd, yyyy}", pr.StartDate);
                    ltrEndDate.Text = string.Format("{0: dddd, MMMM dd, yyyy}", pr.EndDate);
                    ltrContent.Text = pr.WebContent;
                    Page.Title = pr.Title;
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "var oWnd = GetRadWindow();oWnd.close();alert('This promotion not found');", true);
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }
    }
}