using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Administrator_SMSContact : System.Web.UI.Page
{
    SMSObjRepository FRepo = new SMSObjRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.SetCurrentMenu("mSms");
            ObjLogin adm = (ObjLogin)Session["objLogin"];
            if (adm != null && Request.QueryString["t"] != null)
            {                
                RadGrid1.EnableAjaxSkinRendering = true;
                if (Request.QueryString["t"] == "1")
                {
                    RadGrid1.DataSource = FRepo.GetMyContact(0, adm.Id);
                    RadGrid1.DataBind();
                }
                else 
                {
                    RadGrid1.DataSource = FRepo.GetCustomerContact();
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
        ObjLogin adm = (ObjLogin)Session["objLogin"];
        string t = Request.QueryString["t"];        
        if (t == "1")
        {
            RadGrid1.DataSource = FRepo.GetMyContact(0, adm.Id);            
        }
        else
        {
            RadGrid1.DataSource = FRepo.GetCustomerContact();            
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Administrator/Default.aspx");
    }   

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        ObjLogin adm = (ObjLogin)Session["objLogin"];
        string t = Request.QueryString["t"];
        int typeFilter = Convert.ToInt32(cbFilterType.SelectedValue);
        RadGrid1.DataSource = FRepo.FileterMyContact(t, typeFilter, txtFilterValue.Text.Trim(), 0,  adm.Id);
        RadGrid1.DataBind();
    }

    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        RadGrid1.Rebind();
    }
    
}