using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;

public partial class Salemans_SMSContact : System.Web.UI.Page
{
    SalesmanRepository repo = new SalesmanRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.SetCurrentMenu("mSms");
            ObjLogin sale = (ObjLogin)Session["objLogin"];
            if (sale != null && Request.QueryString["t"] != null)
            {
                RadGrid1.EnableAjaxSkinRendering = true;
                if (Request.QueryString["t"] == "1")
                {
                    RadGrid1.DataSource = repo.GetSaleContact(sale.Id);
                    RadGrid1.DataBind();
                }
                else
                {
                    RadGrid1.DataSource = repo.GetCustomerContact(sale.Id);
                    RadGrid1.DataBind();
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }
    }

    protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        ObjLogin sale = (ObjLogin)Session["objLogin"];
        string t = Request.QueryString["t"]; 
        Utility U = new Utility();        
        if (t == "1")
        {            
            RadGrid1.DataSource = repo.GetSaleContact(sale.Id);
        }
        else
        {
            RadGrid1.DataSource = repo.GetCustomerContact(sale.Id);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Salemans/Default.aspx");
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        ObjLogin sale = (ObjLogin)Session["objLogin"];
        string t = Request.QueryString["t"];
        int typeFilter = Convert.ToInt32(cbFilterType.SelectedValue);
        if (t == "1")
        {
            RadGrid1.DataSource = repo.FilterSaleContact(sale.Id, typeFilter, txtFilterValue.Text.Trim());
        }
        else
        {
            RadGrid1.DataSource = repo.FilterCustomerContact(sale.Id, typeFilter, txtFilterValue.Text.Trim());
        }
        RadGrid1.DataBind();
    }

    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        RadGrid1.Rebind();
    }
}