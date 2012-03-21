<%@ page title="SMS Contact" language="C#" masterpagefile="~/Administrator/SMS.master" autoeventwireup="true" inherits="Administrator_SMSContact, App_Web_wvzyt0rb" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentCommand" runat="Server">
    <div style="float: left; height: 35px;">
        <telerik:RadButton runat="server" ID="btnBack" Text="Back to Inbox" Skin="Office2007"
            ToolTip="Back to Inbox" OnClick="btnBack_Click">
        </telerik:RadButton>        
    </div>
    <div style="margin-right: 5px; float: right; height: 35px;">
        <telerik:RadButton runat="server" ID="btnFilter" Skin="Office2007" Text="Filter"
            ToolTip="Filter"  OnClick="btnFilter_Click">
        </telerik:RadButton>
        <telerik:RadButton runat="server" ID="btnClearFilter" Text="Clear Filter"
            ToolTip="Filter" Skin="Office2007" OnClick="btnClearFilter_Click">
        </telerik:RadButton>
    </div>
    <div style="float: right; height: 35px; width: 358px;">
        Filter:
        <telerik:RadComboBox runat="server" ID="cbFilterType" Skin="Office2007">
            <Items>
                <telerik:RadComboBoxItem Value="0" Text="By Name" />
                <telerik:RadComboBoxItem Value="1" Text="By Phone" />                
            </Items>
        </telerik:RadComboBox>
        <telerik:RadTextBox runat="server" ID="txtFilterValue"  Height="18px" Width="150" Skin="Office2007" >
        </telerik:RadTextBox>
    </div>
    
    <div class="clear"></div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilter">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnClearFilter">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" CssClass="RadAjax RadAjax_Vista"
        Transparency="25">
        <div class="raDiv">
        </div>
        <div class="raColor raTransp">
        </div>
    </telerik:RadAjaxLoadingPanel>
    <div style="margin: 5px 1px;">
        <telerik:RadGrid runat="server" ID="RadGrid1" AutoGenerateColumns="false" AllowPaging="true"
            OnNeedDataSource="RadGrid1_NeedDataSource" AllowSorting="true" Skin="Office2007"
            PageSize="20" AllowMultiRowSelection="true">
            <MasterTableView DataKeyNames="Id" CommandItemDisplay="None">
                <Columns>                    
                    <telerik:GridBoundColumn DataField="FullName" SortExpression="FullName" HeaderText="Name" />
                    <telerik:GridBoundColumn DataField="Phone" SortExpression="Phone" HeaderText="Phone" />                   
                </Columns>
                <EditFormSettings>
                    <EditColumn ButtonType="ImageButton" />
                </EditFormSettings>
            </MasterTableView>           
            <PagerStyle Mode="NextPrevAndNumeric" />
        </telerik:RadGrid>
    </div>
</asp:Content>


