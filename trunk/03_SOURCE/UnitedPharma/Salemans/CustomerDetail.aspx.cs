using System;
using System.Data;

public partial class Salemans_CustomerDetail : System.Web.UI.Page
{
    CustomersRepository CRepo = new CustomersRepository();
    Utility U = new Utility();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        Utility.SetCurrentMenu("mCustomer");
        if (Request.QueryString["ID"] != null)
        {
            int CustomerID = int.Parse(Request.QueryString["ID"]);
            var Select = CRepo.GetCustomersByID(CustomerID);
            lblUpi.Text = Select.UpiCode;
            lblCustomerName.Text = Select.FullName;
            lblPhoneNumber.Text = Select.Phone;
            lblAddress.Text = Select.Address + ", " + Select.Street + ", " + Select.Ward;
            lblCustomerType.Text = Select.CustomerTypeName;
            lblChannel.Text = Select.ChannelName;
            lblDistrict.Text = Select.DistrictName;
            lblLocal.Text = Select.LocalName;
            lblCreateDate.Text = string.Format("{0:d}", Select.CreateDate);
            lblUpdateDate.Text = string.Format("{0:d}", Select.UpdateDate);
            lblStatus.Text = (Select.Status == true) ? "Active" : "Closed";
            chkEnable.Checked = Convert.ToBoolean(Select.IsEnable);

            LoadSaleManager(Convert.ToInt32(Select.LocalId));
            LoadSupervisorManager(CustomerID);
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("CustomersManagement.aspx");
    }
    private void LoadSaleManager(int localId)
    {
        this.SalesManager.DataSource = this.CRepo.GetManagerOfCustomerByLocal(localId);
        this.SalesManager.Rebind();
    }

    private void LoadSupervisorManager(int CustomerId)
    {
        CustomerSuperviorRepository CRepo = new CustomerSuperviorRepository();
        SupervisorManager.DataSource = CRepo.GetCustomerSupervisorById(CustomerId);
    }
}