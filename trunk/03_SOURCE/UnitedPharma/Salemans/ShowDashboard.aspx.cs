using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Salemans_ShowDashboard : System.Web.UI.Page
{
    DashboardRepository dRepo = new DashboardRepository();
    SalesmanRepository sRepo = new SalesmanRepository();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.SetCurrentMenu("mDashboard");
            if (Session["objLogin"] != null)
            {
                RadGrid1.DataSource = dRepo.GetAllForSalemens(((ObjLogin)Session["objLogin"]).Phone);
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
       RadGrid1.DataSource = dRepo.GetAllForSalemens(((ObjLogin)Session["objLogin"]).Phone);
    }
    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            if (item["IsReadCol"].Text == "False")
            {
                item.Font.Bold = true;
            }
        }
    } 
    private void ShowErrorMessage()
    {
        RadAjaxManager1.ResponseScripts.Add(string.Format("window.radalert(\"Please enter valid data!\")"));
    }    

    protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
    {
        var id = (int)((GridDataItem)e.Item).GetDataKeyValue("Id");
        try
        {
            dRepo.Delete(id);
        }
        catch (System.Exception)
        {
            ShowErrorMessage();
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Salemans/Default.aspx");
    }
}