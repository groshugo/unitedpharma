<%@ page title="SMSQuota" language="C#" masterpagefile="~/Administrator/Admin.master" autoeventwireup="true" inherits="Administrator_SMSQuota, App_Web_wvzyt0rb" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>
        SMSQuota Management
    </h3>
    <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                    <telerik:AjaxUpdatedControl ControlID="RadWindowManager1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel runat="server" Transparency="25" ID="RadAjaxLoadingPanel1"
        CssClass="RadAjax RadAjax_Vista">
        <div class="raDiv">
        </div>
        <div class="raColor raTransp">
        </div>
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadGrid runat="server" ID="RadGrid1" AutoGenerateColumns="false" AllowPaging="true"
        OnNeedDataSource="RadGrid1_NeedDataSource" OnUpdateCommand="RadGrid1_UpdateCommand"
        Skin="Office2007">
        <MasterTableView DataKeyNames="Id" CommandItemDisplay="None" InsertItemPageIndexAction="ShowItemOnCurrentPage"
            EditMode="EditForms" EditFormSettings-EditColumn-ButtonType="ImageButton">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton" />
                <telerik:GridBoundColumn DataField="FullName" HeaderText="Salesmen"  ReadOnly="true"/>
                <telerik:GridBoundColumn DataField="SmsQuota" HeaderText="Sms Quota" />
                <telerik:GridTemplateColumn HeaderText="Expired Date">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblExpiredDate" Text='<%# Eval("ExpiredDate") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadDatePicker ID="txtExpiredDate" runat="server" DateInput-ReadOnly="true"
                            SelectedDate='<%# Eval("ExpiredDate") %>'>
                            <Calendar ID="CalendarStartDate" runat="server" RangeMinDate="1900-01-01" RangeMaxDate="2050-12-29">
                            </Calendar>
                        </telerik:RadDatePicker>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
            <EditFormSettings>
                <EditColumn ButtonType="ImageButton" />
            </EditFormSettings>
        </MasterTableView>
        <PagerStyle Mode="NextPrevAndNumeric" />
    </telerik:RadGrid>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Office2007" />
</asp:Content>
