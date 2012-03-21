using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Administrator_CustomerLog : System.Web.UI.Page
{
    CustomersRepository Crepo = new CustomersRepository();
    SalesmanRepository SRepo = new SalesmanRepository();
    CustomersLogRepository CLogRepo = new CustomersLogRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Utility.SetCurrentMenu("mAdministrator");
            CustomersList();
        }
    }
    protected override void OnInit(EventArgs e)
    {
        InitializeComponent();
        base.OnInit(e);
    }
    private void InitializeComponent()
    {
        btnApprove.Click+=new EventHandler(btnApprove_Click);
        this.Load += new System.EventHandler(this.Page_Load);
    }

    protected void gridCustomers_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        gridCustomers.DataSource = Crepo.GetAllViewCustomers();
    }

    protected void DetailLog_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        //GetCustomerLogByCustomerId(int.Parse(gridCustomers.SelectedValue.ToString()));
    }

    private void btnApprove_Click(object sender, EventArgs e)
    {
        int customerId = int.Parse(gridCustomers.SelectedValue.ToString());
        Crepo.UpdateCustomerFromLog(int.Parse(hdfID.Value), customerId);
        CLogRepo.DeleteCustomerLogById(customerId);
        CustomersList();
    }
    protected void gridCustomers_SelectedIndexChanged(object sender, EventArgs e)
    {
        int customerId= int.Parse(gridCustomers.SelectedValue.ToString());
        GetCustomerLogByCustomerId(customerId);
    }
    private void CustomersList()
    {
        var listCustomer = Crepo.GetAllViewCustomers().ToList();
        gridCustomers.DataSource = listCustomer;
        gridCustomers.DataBind();
        gridCustomers.Items[0].Selected = true;
        GetCustomerLogByCustomerId(listCustomer.FirstOrDefault().Id);
    }
    private void GetCustomerLogByCustomerId(int CustomerId)
    {
        var listLogs = CLogRepo.GetAllViewCustomerLog(CustomerId);
        DetailLog.DataSource = listLogs;
        DetailLog.DataBind();
    }
}