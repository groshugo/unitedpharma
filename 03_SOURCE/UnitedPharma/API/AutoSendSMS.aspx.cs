using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class API_AutoSendSMS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UPIDataContext db = new UPIDataContext();
            List<SchedulePromotion> lst = (from sp in db.SchedulePromotions where sp.IsApprove == true select sp).ToList();
            if (lst.Count > 0)
            {
                foreach (SchedulePromotion sp in lst)
                {
                    //if sp.StartDate DateTime.Now;
                }
            }
        }
    }
}