using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Administrator_CreateDashboard : System.Web.UI.Page
{
    private const string FilePathSessionKey = "FilePathSessionKey";
    private const string FileNameSessionKey = "FileNameSessionKey";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }
    }
    protected void grdSalemen_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        //var repo = new SalesmanRepository();
        //grdSalemen.DataSource = repo.GetAllViewSales();
    }
    
    protected void btnCreateDashboard_Click(object sender, EventArgs e)
    {
        if (grdSalemen.SelectedItems.Count > 0)
        {
            try
            {
                var fileName = Session[FileNameSessionKey] as string;

                foreach (GridItem gi in grdSalemen.SelectedItems)
                {
                    string ReceiverPhoneNumber = gi.OwnerTableView.DataKeyValues[gi.ItemIndex]["Phone"].ToString();
                    var repo = new DashboardRepository();
                    ObjLogin adm = (ObjLogin)Session["objLogin"];
                    repo.Add(txtTitle.Text, txtContent.Text, ReceiverPhoneNumber, adm.Phone, fileName);
                    ClearFileSessionInfo();
                }

                lblMessage.Text = "<script type='text/javascript'>returnToParent()</" + "script>";
            }
            catch
            {
                lblMessage.Text = "Create Dashboard Fail";
                RemoveUploadedFile();
            }
        }
        else
        {
            RemoveUploadedFile();
        }
    }
    protected void chkAddAdmin_CheckedChanged(object sender, EventArgs e)
    {
        LoadData();
    }
    private void LoadData()
    {
        AdministratorRepository ARepo = new AdministratorRepository();
        SalesmanRepository SRepo = new SalesmanRepository();
        var SaleList = SRepo.GetSaleToDashboard();
        if (chkAddAdmin.Checked)
        {
            var listAdmin = ARepo.GetAdminToDashboard();
            foreach (var item in listAdmin)
            {
                SaleList.Add(item);
            }
        }
        grdSalemen.DataSource = SaleList;
        grdSalemen.Rebind();
    }

    public void RadAsyncUploadFileUploaded(object sender, FileUploadedEventArgs e)
    {
        var adm = (ObjLogin)Session["objLogin"];

        var fileName = string.Format("{0}_{1}", adm.Id, e.File.GetName());
        var fullPath = Server.MapPath(Path.Combine(dashboardAttachedFile.TargetFolder, fileName));

        e.IsValid = false;
        e.File.SaveAs(fullPath, true);

        Session[FilePathSessionKey] = fullPath;
        Session[FileNameSessionKey] = fileName;
    }

    private void RemoveUploadedFile()
    {
        var filePath = Session[FilePathSessionKey] as string;

        if(!string.IsNullOrEmpty(filePath))
        {
            try
            {
                File.Delete(filePath);
                ClearFileSessionInfo();
            }
            catch (Exception)
            {
            }
        }
    }

    private void ClearFileSessionInfo()
    {
        Session[FilePathSessionKey] = string.Empty;
        Session[FileNameSessionKey] = string.Empty;
    }
}