using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;
using Telerik.Web.UI;
using System.Collections;

public partial class Administrator_AdministratorManagement : System.Web.UI.Page
{
    AdministratorRepository repo = new AdministratorRepository();
    ValidationFields checkValid = new ValidationFields();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.SetCurrentMenu("mAdministrator");
            RadGrid1.DataSource = repo.GetAll();
            RadGrid1.DataBind();
        }
    }

    protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        RadGrid1.DataSource = repo.GetAll(); ;
    }

    protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
    {
        var editableItem = ((GridEditableItem)e.Item);
        var id = (int)editableItem.GetDataKeyValue("Id");
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);
        try
        {
            var phone = values["Phone"] as string;
            var fullname = values["Fullname"] as string;
            var password = values["Password"] as string;
            var upiCode = values["UpiCode"] as string;

            if (string.IsNullOrEmpty(phone) && string.IsNullOrEmpty(fullname) && string.IsNullOrEmpty(password) && string.IsNullOrEmpty(upiCode))
            {
                ShowErrorMessage(Pharma.Provide_info_to_insert__please);
                e.Canceled = true;
            }
            else
            {
                if (fullname == null || string.IsNullOrEmpty(fullname.Trim()) || upiCode == null || string.IsNullOrEmpty(upiCode.Trim()))
                {
                    ShowErrorMessage(Pharma.Provide_full_name_to_save__please);
                    e.Canceled = true;
                }
                else
                {
                    if (checkValid.phoneFormat(phone))
                    {
                        var result = repo.Edit(id, (string)values["UpiCode"], (string)values["Fullname"], (string)values["Password"], (string)values["Phone"]);
                        if (!result)
                        {
                            ShowErrorMessage(Pharma.Can_not_save__change_to_another_phone_numer_or_try_again_later_or_contact_administrator_);
                            e.Canceled = true;
                        }
                    }
                    else
                    {
                        ShowErrorMessage(Pharma.Phone_number_not_valid);
                        e.Canceled = true;
                    } 
                }
            }
        }
        catch (Exception ex)
        {
            ShowErrorMessage(ex.Message);
            e.Canceled = true;
        }
    }

    private void ShowErrorMessage(string message)
    {
        RadAjaxManager1.ResponseScripts.Add(string.Format("window.radalert(\"" + message + "\")"));
    }

    protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
    {
        var editableItem = ((GridEditableItem)e.Item);
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);
        try
        {
            var phone = values["Phone"] as string;
            var fullname = values["Fullname"] as string;
            var password = values["Password"] as string;
            var upiCode = values["UpiCode"] as string;

            if (string.IsNullOrEmpty(phone) && string.IsNullOrEmpty(fullname) && string.IsNullOrEmpty(password) && string.IsNullOrEmpty(upiCode))
            {
                ShowErrorMessage(Pharma.Provide_info_to_insert__please);
                e.Canceled = true;
            }
            else
            {
                if (fullname == null || string.IsNullOrEmpty(fullname.Trim()) || upiCode == null || string.IsNullOrEmpty(upiCode.Trim())
                    || password == null || string.IsNullOrEmpty(password.Trim())
                    || phone == null || string.IsNullOrEmpty(phone.Trim()))
                {
                    ShowErrorMessage(Pharma.Provide_full_name_to_save__please);
                    e.Canceled = true;
                }
                else
                {
                    if (checkValid.phoneFormat(phone))
                    {
                        var result = repo.Add(upiCode, fullname, password, phone);
                        if (!result)
                        {
                            ShowErrorMessage(Pharma.Can_not_save__change_to_another_phone_numer_or_try_again_later_or_contact_administrator_);
                            e.Canceled = true;
                        }
                    }
                    else
                    {
                        ShowErrorMessage(Pharma.Phone_number_not_valid);
                        e.Canceled = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ShowErrorMessage(ex.Message);
            e.Canceled = true;
        }
    }

    protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
    {
        var id = (int)((GridDataItem)e.Item).GetDataKeyValue("Id");
        try
        {
            repo.Delete(id);
        }
        catch (Exception ex)
        {
            ShowErrorMessage(ex.Message);
        }

    }
}