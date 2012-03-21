using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Administrator_SMSCalendar : System.Web.UI.Page
{
    SMSObjRepository FRepo = new SMSObjRepository();
    string dt = DateTime.Now.ToShortDateString().Replace("/", "");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["dt"] != null)
        {
            try
            {
                dt = Request.QueryString["dt"];
            }
            catch
            {

            }
        }
        if (!IsPostBack)
        {
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
        }
    }

    protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        ObjLogin adm = (ObjLogin)Session["objLogin"];
        RadGrid1.DataSource = FRepo.GetInboxSMSByDate(adm.Phone, dt);
    }

    protected void btnCompose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Administrator/ComposeSMS.aspx");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (RadGrid1.SelectedItems.Count > 0)
        {
            foreach (GridItem gi in RadGrid1.SelectedItems)
            {
                int id = Convert.ToInt32(gi.OwnerTableView.DataKeyValues[gi.ItemIndex]["Id"]);
                FRepo.Delete(id);
            }
            RadGrid1.Rebind();
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        ObjLogin adm = (ObjLogin)Session["objLogin"];
        int typeFilter = Convert.ToInt32(cbFilterType.SelectedValue);
        RadGrid1.DataSource = FRepo.FilterInboxSMSByDate(typeFilter, txtFilterValue.Text.Trim(), adm.Phone);
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

    protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
    {
        var id = (int)((GridDataItem)e.Item).GetDataKeyValue("Id");
        try
        {
            FRepo.Delete(id);
        }
        catch (System.Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }
}