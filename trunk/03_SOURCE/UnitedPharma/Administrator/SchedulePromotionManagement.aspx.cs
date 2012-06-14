using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;
using Telerik.Web.UI;
using System.Collections;
using System.Data;

public partial class Administrator_SchedulePromotionManagement : System.Web.UI.Page
{
    SchedulePromotionRepository ScheduleRepo = new SchedulePromotionRepository();
    AdministratorRepository ARepo = new AdministratorRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["objLogin"] != null)
        {
            ObjLogin adm = (ObjLogin)Session["objLogin"];
            if (!IsPostBack)
            {
                gridSchedulePromotion.DataSource = ScheduleRepo.GetAllViewSchedulePromotion();
                Utility.SetCurrentMenu("mPromotion");
                if (adm.AllowApprove == true)
                {
                    btnApproveAll.Enabled = true;
                    btnApproveAll.Text = Pharma.Approve;
                    btnDeleteSchedule.Enabled = true;
                    btnDeleteSchedule.Text = Pharma.Delete;
                }
                else
                {
                    btnApproveAll.Enabled = false;
                    btnApproveAll.Text = Pharma.You_don_t_have_permission_to_approve;
                    btnDeleteSchedule.Enabled = false;
                    btnDeleteSchedule.Text = Pharma.You_don_t_have_permission_to_delete;
                }
            }
        }
    }
    protected void gridSchedulePromotion_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        gridSchedulePromotion.DataSource = ScheduleRepo.GetAllViewSchedulePromotion();
    }
    protected void gridSchedulePromotion_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridPagerItem)
        {
            GridPagerItem gridPager = e.Item as GridPagerItem;
            Control numericPagerControl = gridPager.GetNumericPager();
            Control placeHolder = gridPager.FindControl("NumericPagerPlaceHolder");
            placeHolder.Controls.Add(numericPagerControl);
        }
    }
    protected void gridSchedulePromotion_UpdateCommand(object source, GridCommandEventArgs e)
    {
        GridEditFormItem gdItem = (e.Item as GridEditFormItem);
        var editableItem = ((GridEditableItem)e.Item);
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);
        var PromotionId = (int)editableItem.GetDataKeyValue("Id");
        try
        {
            DateTime StartDate = DateTime.Now;
            var StartDateControl = gdItem.FindControl("txtStartDate") as RadDatePicker;
            if(StartDateControl != null && StartDateControl.SelectedDate.HasValue)
            {
                StartDate = StartDateControl.SelectedDate.Value;
            }

            DateTime EndDate = DateTime.Now;
            var EndDateControl = gdItem.FindControl("txtEndDate") as RadDatePicker;
            if (EndDateControl != null && EndDateControl.SelectedDate.HasValue)
            {
                EndDate = EndDateControl.SelectedDate.Value;
            }
            //DateTime StartDate = Convert.ToDateTime(((RadDatePicker)gdItem.FindControl("txtStartDate")).SelectedDate.Value.Date);
            //DateTime EndDate = Convert.ToDateTime(((RadDatePicker)gdItem.FindControl("txtEndDate")).SelectedDate.Value.Date);
            double startdate = Utility.ConvertToUnixTimestamp(StartDate);
            double endDate = Utility.ConvertToUnixTimestamp(EndDate);

            if (endDate >= startdate)
            {
                ObjLogin adm = (ObjLogin)Session["objLogin"];
                int administratorId = adm.Id;
                string webcontent = ((RadEditor)editableItem.FindControl("RadEditor1")).Text;
                string SMSContent = ((TextBox)gdItem.FindControl("txtSMSContent")).Text;
                var result =  ScheduleRepo.UpdateSchedulePromotion(PromotionId, (string)values["UpiCode"], (string)values["Title"], SMSContent,
                     webcontent, StartDate, EndDate, administratorId, false);
                if(!result)
                {
                    ShowErrorMessage("Can not add, please try again later or contact administrator.");
                    e.Canceled = true;
                }
            }
            else
            {
                ShowErrorMessage("End date must be >= start date");
                e.Canceled = true;
            }
                
        }
        catch (Exception ex)
        {
            ShowErrorMessage(ex.Message);
            e.Canceled = true;
        }
    }

    protected void gridSchedulePromotion_ItemCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            RadWindow openWindow = new RadWindow();
            openWindow.NavigateUrl = "DialogPhoneNumber.aspx";
            openWindow.VisibleOnPageLoad = true;
            this.RadWindowManager1.Windows.Add(openWindow);
        }
    }

    protected void gridSchedulePromotion_InsertCommand(object source, GridCommandEventArgs e)
    {
        //GridEditFormItem gdItem = (e.Item as GridEditFormItem);
        //var editableItem = ((GridEditableItem)e.Item);
        //Hashtable values = new Hashtable();
        //editableItem.ExtractValues(values);
        //try
        //{
        //    //DateTime StartDate = Convert.ToDateTime(((RadDatePicker)gdItem.FindControl("txtStartDate")).SelectedDate.Value.Date);
        //    //DateTime EndDate = Convert.ToDateTime(((RadDatePicker)gdItem.FindControl("txtEndDate")).SelectedDate.Value.Date);

        //    DateTime StartDate = DateTime.Now;
        //    var StartDateControl = gdItem.FindControl("txtStartDate") as RadDatePicker;
        //    if (StartDateControl != null && StartDateControl.SelectedDate.HasValue)
        //    {
        //        StartDate = StartDateControl.SelectedDate.Value;
        //    }

        //    DateTime EndDate = DateTime.Now;
        //    var EndDateControl = gdItem.FindControl("txtEndDate") as RadDatePicker;
        //    if (EndDateControl != null && EndDateControl.SelectedDate.HasValue)
        //    {
        //        EndDate = EndDateControl.SelectedDate.Value;
        //    }

        //    double startdate = Utility.ConvertToUnixTimestamp(StartDate);
        //    double endDate = Utility.ConvertToUnixTimestamp(EndDate);
        //    if (endDate >= startdate)
        //    {
        //        ObjLogin adm = (ObjLogin)Session["objLogin"];
        //        int administratorId = adm.Id;
        //        string SMSContent = ((TextBox)gdItem.FindControl("txtSMSContent")).Text;
        //        string webcontent = ((RadEditor)editableItem.FindControl("RadEditor1")).Text;
        //        string PhoneNumbers = hdfPhoneNumbers.Value;
        //        var result =  ScheduleRepo.InsertSchedulePromotion((string)values["UpiCode"], (string)values["Title"], SMSContent,
        //             webcontent, StartDate, EndDate, administratorId, false, PhoneNumbers);
        //        if(!result)
        //        {
        //            ShowErrorMessage("Can not add, please try again or contact administrator");
        //            e.Canceled = true;
        //        }
        //    }
        //    else
        //    {
        //        ShowErrorMessage("End date must be >= start date");
        //        e.Canceled = true;
        //    }
                
        //}
        //catch (Exception ex)
        //{
        //    ShowErrorMessage(ex.Message);
        //    e.Canceled = true;
        //}
    }

    protected void gridSchedulePromotion_CreateColumnEditor(object sender, GridCreateColumnEditorEventArgs e)
    {
        if (e.Column is GridBoundColumn)
        {
            if (e.Column.UniqueName == "SMSContent")
            {
                e.ColumnEditor = new MultiLineTextBoxColumnEditor();
            }
        }
    }

    protected void gridSchedulePromotion_DeleteCommand(object source, GridCommandEventArgs e)
    {
        var PromotionId = (int)((GridDataItem)e.Item).GetDataKeyValue("Id");
        try
        {
            ScheduleRepo.DeleteSchedulePromotion(PromotionId);
        }
        catch (System.Exception)
        {
            ShowErrorMessage("Error");
        }
    }

    protected void gridSchedulePromotion_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
        {
            GridEditableItem edititem = (GridEditableItem)e.Item;
            Hashtable values = new Hashtable();
            edititem.ExtractValues(values);
            using (UPIDataContext db = new UPIDataContext())
            {
                RadDatePicker txtStartDate=((RadDatePicker)edititem.FindControl("txtStartDate"));
                string StartDate = String.IsNullOrEmpty(((HiddenField)edititem.FindControl("hdfStartDate")).Value) ? string.Empty : ((HiddenField)edititem.FindControl("hdfStartDate")).Value;
                txtStartDate.DbSelectedDate = StartDate;
                string EndDate = String.IsNullOrEmpty(((HiddenField)edititem.FindControl("hdfEndDate")).Value) ? string.Empty : ((HiddenField)edititem.FindControl("hdfEndDate")).Value;
                RadDatePicker txtEndDate = ((RadDatePicker)edititem.FindControl("txtEndDate"));
                txtEndDate.DbSelectedDate = EndDate;
                string webcontent = String.IsNullOrEmpty(((Literal)edititem.FindControl("hdfWebContent")).Text) ? string.Empty : ((Literal)edititem.FindControl("hdfWebContent")).Text;
                RadEditor RadEditor1 = (RadEditor)edititem.FindControl("RadEditor1");
                RadEditor1.Content = webcontent;

                string smscontent = String.IsNullOrEmpty(((Literal)edititem.FindControl("hdfSMSContent")).Text) ? string.Empty : ((Literal)edititem.FindControl("hdfSMSContent")).Text;
                TextBox txtSMSContent = ((TextBox)edititem.FindControl("txtSMSContent"));
                txtSMSContent.Text = smscontent;

                string PhoneNumber = string.IsNullOrEmpty(((HiddenField)edititem.FindControl("hdfPhoneList")).Value) ? string.Empty : ((HiddenField)edititem.FindControl("hdfPhoneList")).Value;
                RadTextBox txtPhoneNumber = edititem.FindControl("txtPhoneNumber") as RadTextBox;
                txtPhoneNumber.Text = PhoneNumber;
            }
        }
    }

    private void ShowErrorMessage(string message)
    {
        RadAjaxManager1.ResponseScripts.Add(string.Format("window.radalert(\"" + message + "\")"));
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        var startDate = txtStartDate.SelectedDate == null ? Convert.ToDateTime("1/1/1900") : txtStartDate.SelectedDate.Value;

        var endDate = txtEndtDate.SelectedDate == null ? Convert.ToDateTime("12/31/9999") : txtEndtDate.SelectedDate.Value;

        if(startDate > endDate)
        {
            ShowErrorMessage("From date must less than to date");
            return;
        }

        gridSchedulePromotion.DataSource = ScheduleRepo.Filter(startDate, endDate, int.Parse(ddlFilters.SelectedValue));
        gridSchedulePromotion.Rebind();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Administrator/SchedulePromotionManagement.aspx", true);
    }

    protected void btnApproveAll_Click(object sender, EventArgs e)
    {
        if (gridSchedulePromotion.SelectedItems.Count > 0)
        {
            foreach (GridItem gi in gridSchedulePromotion.SelectedItems)
            {
                int id = Convert.ToInt32(gi.OwnerTableView.DataKeyValues[gi.ItemIndex]["Id"]);
                ScheduleRepo.ApproveSchedulePromotion(id);
            }
            gridSchedulePromotion.Rebind();
        }
    }

    protected void btnDeleteSchedule_Click(object sender, EventArgs e)
    {
        if (gridSchedulePromotion.SelectedItems.Count > 0)
        {
            foreach (GridItem gi in gridSchedulePromotion.SelectedItems)
            {
                int id = Convert.ToInt32(gi.OwnerTableView.DataKeyValues[gi.ItemIndex]["Id"]);
                ScheduleRepo.DeleteSchedulePromotion(id);
            }
            gridSchedulePromotion.Rebind();
        }
    }
    Utility U = new Utility();
    public string TotalFailPhone(string ScheduleId,bool isSuccess)
    {
        string sql = "select s.SMSIdList from SchedulePromotion s where Id=" + int.Parse(ScheduleId);
        DataTable dt = U.GetList(sql);
        if (dt.Rows[0][0].ToString() != "")
        {
            string sqlSMS = "select count(Id) as CountPhone from SMSObj where Id in (" + dt.Rows[0][0] + ") and IsSendSuccess=" + isSuccess;
            DataTable dt2 = U.GetList(sqlSMS);
            return dt2.Rows[0][0].ToString();
        }
        else
            return "0";
    }
}