using System;
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
                    SpreadsheetInfo.SetLicense("E24D-D739-F65A-4E00");
                    ExcelFile ef = new ExcelFile();
                    ef.LoadXls(pathToFile);
                    ExcelWorksheet ws = ef.Worksheets[0];
                    string col1 = "", col2 = "", col3 = "", col4 = "", col5 = "", col6 = "", col7 = "", col8 = "", col9 = "";
                    for (int i = 0; i < ws.Rows.Count; i++)
                    {
                        try
                        {
                            lstCustomer.Add(new vwMasterList(ws.Rows[i].Cells[4].Value.ToString(), ws.Rows[i].Cells[5].Value.ToString(), ws.Rows[i].Cells[6].Value.ToString(),
                                ws.Rows[i].Cells[7].Value.ToString(), ws.Rows[i].Cells[8].Value.ToString(), ws.Rows[i].Cells[9].Value.ToString(), ws.Rows[i].Cells[10].Value.ToString(), ws.Rows[i].Cells[11].Value.ToString(),
                                ws.Rows[i].Cells[12].Value.ToString(), ws.Rows[i].Cells[13].Value.ToString(), ws.Rows[i].Cells[14].Value.ToString(), ws.Rows[i].Cells[15].Value.ToString(),
                                ws.Rows[i].Cells[17].Value.ToString(), ws.Rows[i].Cells[18].Value.ToString(), ws.Rows[i].Cells[19].Value.ToString(), ws.Rows[i].Cells[20].Value.ToString(),
                                ws.Rows[i].Cells[22].Value.ToString(), ws.Rows[i].Cells[23].Value.ToString(), ws.Rows[i].Cells[24].Value.ToString()));
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    CustomerList.DataSource = lstCustomer;
                    CustomerList.DataBind();

                    ObjLogin adm = (ObjLogin)Session["objLogin"];
                    // import to DB
                    if (lstCustomer.Count > 0)
                    {
                        RolesRepository RoleRepo = new RolesRepository();
                        RoleRepo.Add(lstCustomer.First().Salesmen1);
                        RoleRepo.Add(lstCustomer.First().Salesmen2);
                        RoleRepo.Add(lstCustomer.First().Salesmen3);
                        RoleRepo.Add(lstCustomer.First().Region1);
                        RoleRepo.Add(lstCustomer.First().Region2);
                        RoleRepo.Add(lstCustomer.First().Area1);
                        RoleRepo.Add(lstCustomer.First().Area2);
                        RoleRepo.Add(lstCustomer.First().Local1);
                        RoleRepo.Add(lstCustomer.First().Local2);

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
                        int flag = 0;
                        int smsquota = int.Parse(ConfigurationManager.AppSettings["SMSQuota"]);
                        double expiredDateConfig = Convert.ToDouble(ConfigurationManager.AppSettings["ExpiredDate"]);
                        DateTime expiredDate = Convert.ToDateTime(DateTime.Now.AddDays(expiredDateConfig));
                        foreach (var item in lstCustomer)
                        {
                            if (flag == 0)
                            {
                                // Add role
                                col1 = item.Salesmen1;
                                col2 = item.Salesmen2;
                                col3 = item.Salesmen3;
                                col4 = item.Region1;
                                col5 = item.Region2;
                                col6 = item.Area1;
                                col7 = item.Area2;
                                col8 = item.Local1;
                                col9 = item.Local2;
                            }
                            else
                            {
                                // Add Group - Region - Area - Local
                                GRepo.Add("", item.Group, "");
                                RegionRepo.Add("", item.Region, "", GRepo.GetGroupIdByName(item.Group));
                                AreaRepo.Add("", item.Area, "", RegionRepo.GetRegionIdByName(item.Region));
                                LocalRepo.Add("", item.Local, "", AreaRepo.GetAreaIdByName(item.Area));
                                // Add Channel
                                ChRepo.Insert("", item.Channel1, 0);
                                ChRepo.Insert("", item.Channel2, ChRepo.GetChannelIdByName(item.Channel1));
                                ChRepo.Insert("", item.Channel3, ChRepo.GetChannelIdByName(item.Channel2));
                                // Add Customer Type
                                CTRepo.Add("", item.Channel3);
                                // Add Salesmen
                                SaleRepo.Add("", item.Salesmen1, "", RoleRepo.GetRoleIdByName(col1), smsquota, expiredDate);
                                SaleRepo.Add("", item.Salesmen2, "", RoleRepo.GetRoleIdByName(col2), smsquota, expiredDate);
                                SaleRepo.Add("", item.Salesmen3, "", RoleRepo.GetRoleIdByName(col3), smsquota, expiredDate);
                                SaleRepo.Add("", item.Region1, "", RoleRepo.GetRoleIdByName(col4), smsquota, expiredDate);
                                SaleRepo.Add("", item.Region2, "", RoleRepo.GetRoleIdByName(col5), smsquota, expiredDate);
                                SaleRepo.Add("", item.Area1, "", RoleRepo.GetRoleIdByName(col6), smsquota, expiredDate);
                                SaleRepo.Add("", item.Area2, "", RoleRepo.GetRoleIdByName(col7), smsquota, expiredDate);
                                SaleRepo.Add("", item.Local1, "", RoleRepo.GetRoleIdByName(col8), smsquota, expiredDate);
                                SaleRepo.Add("", item.Local2, "", RoleRepo.GetRoleIdByName(col9), smsquota, expiredDate);
                                // Add Salesgroup - salesregion - salesarea - saleslocal
                                GRepo.AddSalesmenGroup(SaleRepo.GetSalesmenIdByName(item.Salesmen1), GRepo.GetGroupIdByName(item.Group));
                                GRepo.AddSalesmenGroup(SaleRepo.GetSalesmenIdByName(item.Salesmen2), GRepo.GetGroupIdByName(item.Group));
                                GRepo.AddSalesmenGroup(SaleRepo.GetSalesmenIdByName(item.Salesmen3), GRepo.GetGroupIdByName(item.Group));
                                RegionRepo.AddSalesmenRegion(SaleRepo.GetSalesmenIdByName(item.Region1), RegionRepo.GetRegionIdByName(item.Region));
                                RegionRepo.AddSalesmenRegion(SaleRepo.GetSalesmenIdByName(item.Area2), RegionRepo.GetRegionIdByName(item.Region));
                                AreaRepo.AddSalesmenArea(SaleRepo.GetSalesmenIdByName(item.Region2), AreaRepo.GetAreaIdByName(item.Area));
                                AreaRepo.AddSalesmenArea(SaleRepo.GetSalesmenIdByName(item.Local1), AreaRepo.GetAreaIdByName(item.Area));
                                LocalRepo.AddSalesmenLocal(SaleRepo.GetSalesmenIdByName(item.Area1), LocalRepo.GetLocalIdByName(item.Local));
                                LocalRepo.AddSalesmenLocal(SaleRepo.GetSalesmenIdByName(item.Local2), LocalRepo.GetLocalIdByName(item.Local));
                                // Add Customer - Customer Log
                                int CustomerId = CRepo.InsertCustomer(item.Upicode, item.Customername, item.Customeraddress, "", "", "", "", CTRepo.GetCustomerTypeIdByName(item.Channel3),
                                    ChRepo.GetChannelIdByName(item.Channel3), DisRepo.GetDistrictIdByName(""), LocalRepo.GetLocalIdByName(item.Local), DateTime.Now, DateTime.Now,
                                    true, false);
                                CLogRepo.InsertCustomer(item.Upicode, item.Customername, item.Customeraddress, "", "", "", "", CTRepo.GetCustomerTypeIdByName(item.Channel3),
                                    ChRepo.GetChannelIdByName(item.Channel3), DisRepo.GetDistrictIdByName(""), LocalRepo.GetLocalIdByName(item.Local), DateTime.Now, DateTime.Now,
                                    true, CustomerId, false, 0,adm.Id);
                            }
                            flag++;
                        }
                    }
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
}