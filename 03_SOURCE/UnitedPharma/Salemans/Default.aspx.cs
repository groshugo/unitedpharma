﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Salemans_Default : System.Web.UI.Page
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
                RadGrid1.DataSource = FRepo.GetInboxSMS(sale.Phone);
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
        RadGrid1.DataSource = FRepo.GetInboxSMS(sale.Phone);
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

    protected void btnCompose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Salemans/ComposeSMS.aspx");
    }    

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        ObjLogin sale = (ObjLogin)Session["objLogin"];
        int typeFilter = Convert.ToInt32(cbFilterType.SelectedValue);
        RadGrid1.DataSource = FRepo.FilterInboxSMS(typeFilter, txtFilterValue.Text.Trim(), sale.Phone);
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