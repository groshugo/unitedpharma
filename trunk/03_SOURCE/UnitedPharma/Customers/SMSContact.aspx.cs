using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Customers_SMSContact : System.Web.UI.Page
{
    CustomersRepository repo = new CustomersRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.SetCurrentMenu("mSms");
            
            var cust = (ObjLogin)Session["objLogin"];
            if (cust == null)
            {
                Response.Redirect("~/Default.aspx");
            }
        }
    }

    
    protected void RadGrid2_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        ObjLogin cust = (ObjLogin)Session["objLogin"];
        RadGrid2.DataSource = repo.GetManagerOfCustomer(cust.Id);
    }
}