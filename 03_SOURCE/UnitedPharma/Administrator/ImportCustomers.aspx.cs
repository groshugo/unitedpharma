using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using GemBox.Spreadsheet;
using System.Data;
using System.Configuration;

public partial class Administrator_ImportCustomers : System.Web.UI.Page
{
    List<vwMasterList> lstCustomer;
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        lstCustomer = new List<vwMasterList>();
        if (Session["objLogin"] != null)
        {
            ObjLogin adm = (ObjLogin)Session["objLogin"];
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
    }


    protected void btnImport_Click(object sender, EventArgs e)
    {
        btnImport.Enabled = false;
        fuImport.Enabled = false;
        btnImport.Text = "Processing...";
        if (!fuImport.HasFile)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Chọn file để nhập');", true);
        else
        {
            string ext = fuImport.FileName.Substring(fuImport.FileName.LastIndexOf('.') + 1).ToLower();
            if (!ext.Equals("xls") && !ext.Equals("xlsx"))
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Định dạng file nhập ko đúng');", true);
            else
            {
                string newFileName = Guid.NewGuid().ToString("N") + "." + ext;
                string pathToFile = Server.MapPath("~\\Import") + "\\" + newFileName;
                fuImport.SaveAs(pathToFile);
                try
                {
                    ObjLogin adm = (ObjLogin)Session["objLogin"];

                    int smsquota = int.Parse(ConfigurationManager.AppSettings["SMSQuota"]);
                    double expiredDateConfig = Convert.ToDouble(ConfigurationManager.AppSettings["ExpiredDate"]);
                    DateTime expiredDate = Convert.ToDateTime(DateTime.Now.AddDays(expiredDateConfig));

                    GroupsRepository GRepo = new GroupsRepository();
                    RegionsRepository RegionRepo = new RegionsRepository();
                    AreasRepository AreaRepo = new AreasRepository();
                    LocalsRepository LocalRepo = new LocalsRepository();
                    ChannelRepository ChRepo = new ChannelRepository();
                    SalesmanRepository SaleRepo = new SalesmanRepository();
                    CustomersLogRepository CLogRepo = new CustomersLogRepository();
                    CustomersRepository CRepo = new CustomersRepository();
                    CustomerTypeRepository CTRepo = new CustomerTypeRepository();
                    DistrictsRepository DisRepo = new DistrictsRepository();

                    SpreadsheetInfo.SetLicense("E24D-D739-F65A-4E00");
                    ExcelFile ef = new ExcelFile();
                    ef.LoadXls(pathToFile);
                    ExcelWorksheet ws = ef.Worksheets[0];

                    for (int i = 2; i < 100; i++)
                    {
                        try
                        {
                            vwMasterList vmMasterItem = new vwMasterList(ws.Rows[i].Cells[4].Value.ToString(),
                                                                ws.Rows[i].Cells[5].Value.ToString(),
                                                                ws.Rows[i].Cells[6].Value.ToString(),
                                                                ws.Rows[i].Cells[7].Value.ToString(),
                                                                ws.Rows[i].Cells[8].Value.ToString(),
                                                                ws.Rows[i].Cells[9].Value.ToString(),
                                                                ws.Rows[i].Cells[10].Value.ToString(),
                                                                ws.Rows[i].Cells[11].Value.ToString(),
                                                                ws.Rows[i].Cells[12].Value.ToString(),
                                                                ws.Rows[i].Cells[13].Value.ToString(),
                                                                ws.Rows[i].Cells[14].Value.ToString(),
                                                                ws.Rows[i].Cells[15].Value.ToString(),
                                                                ws.Rows[i].Cells[17].Value.ToString(),
                                                                ws.Rows[i].Cells[18].Value.ToString(),
                                                                ws.Rows[i].Cells[19].Value.ToString(),
                                                                ws.Rows[i].Cells[20].Value.ToString(),
                                                                ws.Rows[i].Cells[22].Value.ToString(),
                                                                ws.Rows[i].Cells[23].Value.ToString(),
                                                                ws.Rows[i].Cells[24].Value.ToString());

                            // Add Group - Region - Area - Local
                            var groupId = GRepo.Add("", vmMasterItem.Group, "");
                            var regionId = RegionRepo.Add("", vmMasterItem.Region, "", groupId);
                            var areaId = AreaRepo.Add("", vmMasterItem.Area, "", regionId);
                            var localId = LocalRepo.Add("", vmMasterItem.Local, "", areaId);

                            // Add Channel
                            ChRepo.Insert("", vmMasterItem.Channel1, 0);
                            ChRepo.Insert("", vmMasterItem.Channel2, ChRepo.GetChannelIdByName(vmMasterItem.Channel1));
                            ChRepo.Insert("", vmMasterItem.Channel3, ChRepo.GetChannelIdByName(vmMasterItem.Channel2));

                            // Add Customer Type
                            CTRepo.Add("", vmMasterItem.Channel3);

                            // Add Salesmen
                            var tromId = SaleRepo.ImportSalesmen("", vmMasterItem.TROM, "", (int)SalesmenRole.TROM, smsquota, expiredDate, -1);
                            var tpsId = SaleRepo.ImportSalesmen("", vmMasterItem.TPS, "", (int)SalesmenRole.TPS, smsquota, expiredDate, tromId);
                            var tprId = SaleRepo.ImportSalesmen("", vmMasterItem.TPR, "", (int)SalesmenRole.TPR, smsquota, expiredDate, tpsId);

                            var eromId = SaleRepo.ImportSalesmen("", vmMasterItem.EROM, "", (int)SalesmenRole.EROM, smsquota, expiredDate, -1);
                            var pss1Id = SaleRepo.ImportSalesmen("", vmMasterItem.PSS1, "", (int)SalesmenRole.PSS1, smsquota, expiredDate, eromId);
                            var psr1Id = SaleRepo.ImportSalesmen("", vmMasterItem.PSR1, "", (int)SalesmenRole.PSR1, smsquota, expiredDate, pss1Id);

                            var erom2Id = SaleRepo.ImportSalesmen("", vmMasterItem.EROM2, "", (int)SalesmenRole.EROM2, smsquota, expiredDate, -1);
                            var pss2Id = SaleRepo.ImportSalesmen("", vmMasterItem.PSS2, "", (int)SalesmenRole.PSS2, smsquota, expiredDate, erom2Id);
                            var psr2Id = SaleRepo.ImportSalesmen("", vmMasterItem.PSR2, "", (int)SalesmenRole.PSR2, smsquota, expiredDate, pss2Id);

                            // Add Salesgroup - salesregion - salesarea - saleslocal
                            ImportSalesGroup(tromId, tpsId, tprId, eromId, pss1Id, psr1Id, erom2Id, pss2Id, psr2Id, GRepo, groupId);
                            ImportSalesRegion(tromId, tpsId, tprId, eromId, pss1Id, psr1Id, erom2Id, pss2Id, psr2Id, RegionRepo, regionId);
                            ImportSalesArea(tromId, tpsId, tprId, eromId, pss1Id, psr1Id, erom2Id, pss2Id, psr2Id, AreaRepo, areaId);
                            ImportSalesLocal(tromId, tpsId, tprId, eromId, pss1Id, psr1Id, erom2Id, pss2Id, psr2Id, LocalRepo, localId);

                            // Add Customer - Customer Log
                            int CustomerId = CRepo.InsertCustomer(vmMasterItem.CustomerCode, vmMasterItem.Customername, vmMasterItem.Customeraddress, "", "", "", "",
                                CTRepo.GetCustomerTypeIdByName(vmMasterItem.Channel3),
                                ChRepo.GetChannelIdByName(vmMasterItem.Channel3), -1, localId, DateTime.Now, DateTime.Now,
                                true, false);
                            CLogRepo.InsertCustomer(vmMasterItem.CustomerCode, vmMasterItem.Customername, vmMasterItem.Customeraddress, "", "", "", "",
                                CTRepo.GetCustomerTypeIdByName(vmMasterItem.Channel3),
                                ChRepo.GetChannelIdByName(vmMasterItem.Channel3), -1, localId, DateTime.Now, DateTime.Now,
                                true, CustomerId, false, 0, adm.Id, string.Empty);

                            lstCustomer.Add(vmMasterItem);
                        }
                        catch (Exception ex)
                        {
                            // write log here => TBD
                        }
                    }

                    CustomerList.DataSource = lstCustomer;
                    CustomerList.DataBind();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('" + ex.Message + "');", true);
                }
                finally
                {

                }

            }
        }
        btnImport.Enabled = true;
        fuImport.Enabled = true;
        btnImport.Text = "Import";
    }

    private static void ImportSalesGroup(int tromId, int tpsId, int tprId, int eromId, int pss1Id, int psr1Id, int erom2Id, int pss2Id, int psr2Id,
                                         GroupsRepository GRepo, int groupId)
    {
        GRepo.AddSalesmenGroup(tprId, groupId);
        GRepo.AddSalesmenGroup(tpsId, groupId);
        GRepo.AddSalesmenGroup(tromId, groupId);

        GRepo.AddSalesmenGroup(psr1Id, groupId);
        GRepo.AddSalesmenGroup(pss1Id, groupId);
        GRepo.AddSalesmenGroup(eromId, groupId);

        GRepo.AddSalesmenGroup(psr2Id, groupId);
        GRepo.AddSalesmenGroup(pss2Id, groupId);
        GRepo.AddSalesmenGroup(erom2Id, groupId);
    }

    private static void ImportSalesRegion(int tromId, int tpsId, int tprId, int eromId, int pss1Id, int psr1Id, int erom2Id, int pss2Id, int psr2Id,
                                         RegionsRepository RRepo, int regionId)
    {
        RRepo.AddSalesmenRegion(tprId, regionId);
        RRepo.AddSalesmenRegion(tpsId, regionId);
        RRepo.AddSalesmenRegion(tromId, regionId);

        RRepo.AddSalesmenRegion(psr1Id, regionId);
        RRepo.AddSalesmenRegion(pss1Id, regionId);
        RRepo.AddSalesmenRegion(eromId, regionId);

        RRepo.AddSalesmenRegion(psr2Id, regionId);
        RRepo.AddSalesmenRegion(pss2Id, regionId);
        RRepo.AddSalesmenRegion(erom2Id, regionId);
    }

    private static void ImportSalesArea(int tromId, int tpsId, int tprId, int eromId, int pss1Id, int psr1Id, int erom2Id, int pss2Id, int psr2Id,
                                         AreasRepository aRepo, int areaId)
    {
        aRepo.AddSalesmenArea(tprId, areaId);
        aRepo.AddSalesmenArea(tpsId, areaId);
        aRepo.AddSalesmenArea(tromId, areaId);

        aRepo.AddSalesmenArea(psr1Id, areaId);
        aRepo.AddSalesmenArea(pss1Id, areaId);
        aRepo.AddSalesmenArea(eromId, areaId);

        aRepo.AddSalesmenArea(psr2Id, areaId);
        aRepo.AddSalesmenArea(pss2Id, areaId);
        aRepo.AddSalesmenArea(erom2Id, areaId);
    }

    private static void ImportSalesLocal(int tromId, int tpsId, int tprId, int eromId, int pss1Id, int psr1Id, int erom2Id, int pss2Id, int psr2Id,
                                         LocalsRepository lRepo, int localId)
    {
        lRepo.AddSalesmenLocal(tprId, localId);
        lRepo.AddSalesmenLocal(tpsId, localId);
        lRepo.AddSalesmenLocal(tromId, localId);

        lRepo.AddSalesmenLocal(psr1Id, localId);
        lRepo.AddSalesmenLocal(pss1Id, localId);
        lRepo.AddSalesmenLocal(eromId, localId);

        lRepo.AddSalesmenLocal(psr2Id, localId);
        lRepo.AddSalesmenLocal(pss2Id, localId);
        lRepo.AddSalesmenLocal(erom2Id, localId);
    }
}