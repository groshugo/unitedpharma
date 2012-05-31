using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Salemans_SMSCalendar : System.Web.UI.Page
{
    SMSObjRepository FRepo = new SMSObjRepository();

    string dt = DateTime.Now.ToShortDateString().Replace("/", "");

    protected override void OnPreRender(EventArgs e)
    {
        var selectedDate = this.Form.Parent.FindControl("SelectedDate") as HiddenField;
        if (selectedDate != null)
        {
            dt = selectedDate.Value;
            if (string.IsNullOrEmpty(dt))
                dt = DateTime.Now.ToShortDateString().Replace("/", "");
        }

        Utility.SetCurrentMenu("mSms");
        ObjLogin adm = (ObjLogin)Session["objLogin"];
        if (adm != null)
        {
            RadGrid1.EnableAjaxSkinRendering = true;
            RadGrid1.DataSource = FRepo.GetInboxSMSByDate(adm.Phone, dt);
            RadGrid1.DataBind();
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }

        base.OnPreRender(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Request.QueryString["dt"] != null)
        //{
        //    try
        //    {
        //        dt = Request.QueryString["dt"];
        //    }
        //    catch
        //    {

        //    }
        //}
        //if (!IsPostBack)
        //{
        //    Utility.SetCurrentMenu("mSms");
        //    ObjLogin adm = (ObjLogin)Session["objLogin"];
        //    if (adm != null)
        //    {
        //        RadGrid1.EnableAjaxSkinRendering = true;
        //        RadGrid1.DataSource = FRepo.GetInboxSMSByDate(adm.Phone, dt);
        //        RadGrid1.DataBind();
        //    }
        //    else
        //    {
        //        Response.Redirect("~/Default.aspx");
        //    }
        //}
    }

    protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        ObjLogin sale = (ObjLogin)Session["objLogin"];
        RadGrid1.DataSource = FRepo.GetInboxSMSByDate(sale.Phone, dt);
    }

    protected void btnCompose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Salemans/ComposeSMS.aspx");
    }    

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        ObjLogin sale = (ObjLogin)Session["objLogin"];
        int typeFilter = Convert.ToInt32(cbFilterType.SelectedValue);
        RadGrid1.DataSource = FRepo.FilterInboxSMSByDate(typeFilter, txtFilterValue.Text.Trim(), sale.Phone);
        RadGrid1.DataBind();
    }

    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        RadGrid1.Rebind();
    }

    private void ShowMessage(string message)
    {
        RadAjaxManager1.ResponseScripts.Add(string.Format("window.radalert(\"" + message + "!\")"));
    }
    
}