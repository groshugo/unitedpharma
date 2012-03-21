using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Administrator_CustomerLog : System.Web.UI.Page
{
    CustomersLogRepository CLogRepo = new CustomersLogRepository();
    CustomersRepository Crepo = new CustomersRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["objLogin"] != null)
        {
            ObjLogin adm = (ObjLogin)Session["objLogin"];
            if (!IsPostBack)
            {
                GetCustomerLogByPhone(int.Parse(Request.QueryString["Id"]));
                if (adm.AllowApprove == true)
                {
                    btn1.Enabled = true;
                    btn1.Text = "Approve";
                    btnnotApprove.Enabled = true;
                    btnnotApprove.Text = "Not Approve";
                }
                else
                {
                    btn1.Enabled = false;
                    btn1.Text = "You don't have permission to approve";
                    btnnotApprove.Enabled = false;
                    btnnotApprove.Text = "You don't have permission to approve";
                }
            }
        }
    }

    private void GetCustomerLogByPhone(int customerId)
    {
        var listLogs = CLogRepo.GetAllViewCustomerLog(customerId);
        DetailLog.DataSource = listLogs;
        DetailLog.DataBind();
    }

    protected void btnnotApprove_Click(object sender, EventArgs e)
    {
        Crepo.SetEnableOfCustomer(int.Parse(Request.QueryString["Id"]), true);
        CLogRepo.DeleteCustomerLogById(int.Parse(Request.QueryString["Id"]));
        GetCustomerLogByPhone(int.Parse(Request.QueryString["Id"]));
    }
    protected void btn1_Click(object sender, EventArgs e)
    {
        Crepo.UpdateCustomerFromLog(int.Parse(hdfID.Value), int.Parse(Request.QueryString["Id"]));
        CLogRepo.DeleteCustomerLogById(int.Parse(Request.QueryString["Id"]));
        GetCustomerLogByPhone(int.Parse(Request.QueryString["Id"]));
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Approve success.');", true);
    }
}