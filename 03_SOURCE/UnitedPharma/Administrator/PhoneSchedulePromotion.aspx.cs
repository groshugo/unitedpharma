using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Administrator_PhoneSchedulePromotion : System.Web.UI.Page
{
    SchedulePromotionRepository ScheduleRepo = new SchedulePromotionRepository();
    UPIDataContext db = new UPIDataContext();
    SqlConnection connection;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Utility.SetCurrentMenu("mAdministrator");
            int Id =Convert.ToInt32(Request.QueryString["ID"]);
            string phoneList = ScheduleRepo.GetSchedulePromotionById(Id).PhoneNumbers;
            string sql = "SELECT a.FullName as CustomerName,a.UpiCode, a.Phone, b.FullName,s.PositionName FROM [Customer] as a LEFT JOIN [CustomerSupervisor] as b on a.Id=b.CustomerId left join SupervisorPosition as s on b.PositionId=s.Id where a.Phone in (" + phoneList+")";
            Utility utility = new Utility();
            SchedulePhoneNumbers.DataSource = utility.GetList(sql);
        }
    }

    protected void SchedulePhoneNumbers_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {

    }
}