using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Administrator_DetailSchedulePromotionManagement : System.Web.UI.Page
{
    SchedulePromotionRepository SRepo = new SchedulePromotionRepository();
    CustomersRepository CustomerRepo = new CustomersRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int ID =int.Parse(Request.QueryString["SID"].ToString());
            var Sdetail = SRepo.GetSchedulePromotionById(ID);
            if (Sdetail != null)
            {
                lblUPI.Text = Sdetail.UpiCode;
                lbltitle.Text = Sdetail.Title;
                lblSMSContent.Text = Sdetail.SMSContent;
                ltrWebContent.Text = Sdetail.WebContent;
                lblStartDate.Text = string.Format("{0:d}", Sdetail.StartDate);
                lblEndDate.Text = string.Format("{0:d}",Sdetail.EndDate);
                lblAdmin.Text = SRepo.GetAdministratorName(Sdetail.AdministratorId);

                string phoneList = SRepo.GetSchedulePromotionById(ID).PhoneNumbers;
                string sql = "SELECT a.FullName as CustomerName,a.UpiCode, a.Phone, b.FullName,s.PositionName FROM [Customer] as a LEFT JOIN [CustomerSupervisor] as b on a.Id=b.CustomerId left join [SupervisorPosition] as s on b.PositionId=s.Id where a.Phone in (" + phoneList + ")";
                Utility utility = new Utility();
                SchedulePhoneNumbers.DataSource = utility.GetList(sql);
            }
       }
    }
    protected void SchedulePhoneNumbers_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("SchedulePromotionManagement.aspx");
    }
}