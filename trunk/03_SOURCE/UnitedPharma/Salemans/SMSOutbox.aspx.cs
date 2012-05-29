using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Salemans_SMSOutbox : System.Web.UI.Page
{
    SMSObjRepository FRepo = new SMSObjRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.SetCurrentMenu("mSms");
            ObjLogin sale = (ObjLogin)Session["objLogin"];
            if (sale != null)
            {
                RadGrid1.EnableAjaxSkinRendering = true;
                RadGrid1.DataSource = FRepo.GetOutboxSMS(sale.Phone);
                RadGrid1.DataBind();
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
        RadGrid1.DataSource = FRepo.GetOutboxSMS(sale.Phone);
    }

    protected void btnCompose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Salemans/ComposeSMS.aspx");
    }
    
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        ObjLogin sale = (ObjLogin)Session["objLogin"];
        int typeFilter = Convert.ToInt32(cbFilterType.SelectedValue);
        RadGrid1.DataSource = FRepo.FilterOutboxSMS(typeFilter, txtFilterValue.Text.Trim(), sale.Phone);
        RadGrid1.DataBind();
    }

    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Salemans/SMSOutbox.aspx");
    }

    private void ShowMessage(string message)
    {
        RadAjaxManager1.ResponseScripts.Add(string.Format("window.radalert(\"" + message + "!\")"));
    }    
}