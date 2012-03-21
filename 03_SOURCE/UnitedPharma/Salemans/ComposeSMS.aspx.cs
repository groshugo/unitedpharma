using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;

public partial class Salemans_ComposeSMS : System.Web.UI.Page
{
    string PhoneList = string.Empty;
    #region Declare repository
    CustomersRepository CusRepo = new CustomersRepository();
    SalesmanRepository SRepo = new SalesmanRepository();
    CustomersRepository CRepo = new CustomersRepository();
    RegionsRepository regionRepo = new RegionsRepository();
    PromotionRepository ProRepo = new PromotionRepository();
    CustomerTypeRepository CTypeRepo = new CustomerTypeRepository();
    ChannelRepository ChannelRepo = new ChannelRepository();
    GroupsRepository groupRepo = new GroupsRepository();
    AreasRepository areaRepo = new AreasRepository();
    LocalsRepository localRepo = new LocalsRepository();
    SchedulePromotionRepository ScheduleRepo = new SchedulePromotionRepository();
    #endregion
    protected override void OnInit(EventArgs e)
    {
        InitializeComponent();
        base.OnInit(e);
    }
    private void InitializeComponent()
    {
        this.btnSendSMS.Click += new System.EventHandler(this.btnSendSMS_Click);
        this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);

        this.Load += new System.EventHandler(this.Page_Load);
    }
    private void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Utility.SetCurrentMenu("mSms");
            PromotionList();
        }
    }

    public void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        Utility U = new Utility();
        string sql = "SELECT a.FullName as CustomerName,a.UpiCode, a.Phone, b.FullName,s.PositionName FROM [Customer] as a LEFT JOIN [CustomerSupervisor] as b on a.Id=b.CustomerId left join SupervisorPosition as s on b.PositionId=s.Id where a.Phone in (" + e.Argument + ")";
        string sqlSale = "Select l.FullName as CustomerName,l.UpiCode,l.Phone,l.FullName as FullName,r.RoleName as PositionName FROM [Salesmen] as l LEFT JOIN [Role] as r on l.RoleId=r.Id where l.Phone in (" + e.Argument + ")";
        DataTable dtcustomer = U.GetList(sql);
        DataTable dtSale = U.GetList(sqlSale);
        DataTable dtAll = dtcustomer.Copy();
        dtAll.Merge(dtSale);
        SchedulePhoneNumbers.DataSource = dtAll;
        SchedulePhoneNumbers.Rebind();
        SchedulePhoneNumbers.Visible = true;

        SchedulePhoneNumbers.PageSize += 10;
        SchedulePhoneNumbers.Rebind();
    }
    private void PromotionList()
    {
        ddlPromotion.DataSource = ScheduleRepo.GetAll();
        ddlPromotion.DataTextField = "Title";
        ddlPromotion.DataValueField = "Id";
        ddlPromotion.DataBind();
    }

    private void btnSendSMS_Click(object sender, EventArgs e)
    {
        string subject = (!string.IsNullOrEmpty(txtSubject.Text)) ? txtSubject.Text : "No subject";
        SmsHandler smsobj = new SmsHandler();
        SMSObjRepository smsobjRepo = new SMSObjRepository();
        AdministratorRepository ARepo = new AdministratorRepository();
        ObjLogin adm = (ObjLogin)Session["objLogin"];
        string SMSCode = Guid.NewGuid().ToString().ToLower();
        PhoneList = hdfPhoneNumbers.Value;
        string listPhone = hdfPhoneNumbers.Value;
        char[] separator = new char[] { ',' };
        string[] phoneList = listPhone.Split(separator);
        bool flag = false;
        string PhoneNotExist = string.Empty;
        foreach (string phone in phoneList)
        {
            if (SRepo.GetSalemenByPhoneNumber(phone))
            {
                smsobjRepo.InsertSMS(SMSCode, 0, adm.Phone, Constant.AdminType, PhoneList, Constant.SalemenType, DateTime.Now, subject, txtContent.Text, true, false, false, 1, int.Parse(ddlPromotion.SelectedValue.ToString()));
                flag = true;
            }
            else
            {
                if (CRepo.GetCustomerByPhone(phone))
                {
                    smsobjRepo.InsertSMS(SMSCode, 0, adm.Phone, Constant.AdminType, PhoneList, Constant.CustomerType, DateTime.Now, subject, txtContent.Text, true, false, false, 1, int.Parse(ddlPromotion.SelectedValue.ToString()));
                    flag = true;
                }
                else
                {
                    if (ARepo.GetAdminByPhoneNumber(phone))
                    {
                        smsobjRepo.InsertSMS(SMSCode, 0, adm.Phone, Constant.AdminType, PhoneList, Constant.AdminType, DateTime.Now, subject, txtContent.Text, true, false, false, 1, int.Parse(ddlPromotion.SelectedValue.ToString()));
                        flag = true;
                    }
                }
            }
            if (flag == false)
            {
                PhoneNotExist += phone + ", ";
            }
        }
        if (!string.IsNullOrEmpty(PhoneNotExist))
        {
            ShowErrorMessage(PhoneNotExist.Substring(0, PhoneNotExist.Length - 2) + " not found in database.");
        }
        else
        {
            ShowErrorMessage("Send success");
        }
    }
    private void ShowErrorMessage(string message)
    {
        RadAjaxManager1.ResponseScripts.Add(string.Format("window.radalert(\"" + message + "\")"));
    }
    private void btnAbort_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}