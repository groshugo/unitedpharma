using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Salemans_SMSFailure : System.Web.UI.Page
{
    SMSObjRepository FRepo = new SMSObjRepository();
    SmsHandler objSMS = new SmsHandler();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ObjLogin adm = (ObjLogin)Session["objLogin"];
            if (adm != null)
            {
                RadGrid1.EnableAjaxSkinRendering = true;
                RadGrid1.DataSource = FRepo.SMSFailure(adm.Phone);
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
        RadGrid1.DataSource = FRepo.SMSFailure(adm.Phone);
    }
    protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == "Resend")
        {
            var id = (int)((GridDataItem)e.Item).GetDataKeyValue("Id");
            var SMS = FRepo.GetSMSById(id).SingleOrDefault();
            // tranh truong hop test het tien :D
            //objSMS.SendSMS(SMS.ReceiverPhone, SMS.Content);
        }
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

    private void ShowMessage(string message)
    {
        RadAjaxManager1.ResponseScripts.Add(string.Format("window.radalert(\"" + message + "!\")"));
    }

    protected void btnResendSelected_Click(object sender, EventArgs e)
    {
        if (RadGrid1.SelectedItems.Count > 0)
        {
            foreach (GridItem gi in RadGrid1.SelectedItems)
            {
                int id = Convert.ToInt32(gi.OwnerTableView.DataKeyValues[gi.ItemIndex]["Id"]);
                var SMS = FRepo.GetSMSById(id).SingleOrDefault();
                //objSMS.SendSMS(SMS.ReceiverPhone, SMS.Content);
            }
            RadGrid1.Rebind();
        }
    }
}