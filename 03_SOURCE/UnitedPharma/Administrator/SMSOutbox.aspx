<%@ Page Title="SMS Outbox" Language="C#" MasterPageFile="~/Administrator/SMS.master"
    AutoEventWireup="true" CodeFile="SMSOutbox.aspx.cs" Inherits="Administrator_SMSOutbox" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentCommand" runat="Server">
    <div style="float: left; height: 35px;">
        <telerik:RadButton runat="server" ID="btnCompose" Text="Compose" Skin="Office2007"
            ToolTip="Compose" OnClick="btnCompose_Click">
        </telerik:RadButton>
        <telerik:RadButton runat="server" ID="btnDelete" Text="Delete" ToolTip="Delete" Skin="Office2007"
            OnClick="btnDelete_Click" Style="margin-left: 5px;" OnClientClicked="ConfirmDelete">
        </telerik:RadButton>
    </div>
    <div style="margin-right: 5px; float: right; height: 35px;">
        <telerik:RadButton runat="server" ID="btnFilter" Text="Filter" Skin="Office2007"
            ToolTip="Filter" OnClick="btnFilter_Click">
        </telerik:RadButton>
        <telerik:RadButton runat="server" ID="btnClearFilter" Text="Clear Filter" ToolTip="Filter"
            OnClick="btnClearFilter_Click" Skin="Office2007">
        </telerik:RadButton>
    </div>
    <div style="float: right; height: 35px; width: 358px;">
        Filter:
        <telerik:RadComboBox runat="server" ID="cbFilterType" Skin="Office2007">
            <Items>
                <telerik:RadComboBoxItem Value="0" Text="By To" />
                <telerik:RadComboBoxItem Value="1" Text="By Phone" />
                <telerik:RadComboBoxItem Value="2" Text="By Subject" />
            </Items>
        </telerik:RadComboBox>
        <telerik:RadTextBox runat="server" Height="18px" Width="150" ID="txtFilterValue" Skin="Office2007">
        </telerik:RadTextBox>
    </div>
    <div class="clear">
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>
    <telerik:RadCodeBlock runat="server" ID="radCodeBlock">
        <script type="text/javascript">
            var fireConfirm = true;
            function ConfirmDelete(button, args) {
                if ($find("<%= RadGrid1.MasterTableView.ClientID %>").get_selectedItems().length > 0) {
                    var callBackFunction = Function.createDelegate(button, function (argument) {
                        if (fireConfirm && argument) {
                            this.set_autoPostBack(true);
                            fireConfirm = false;
                            this.click();
                        }
                    });
                    if (fireConfirm) {
                        button.set_autoPostBack(false);
                        var text = "Are you sure to delete?";
                        var result = radconfirm(text, callBackFunction, 300, 100, null, "Confirm");
                    }
                }
                else {
                    alert("Please choose SMS to delete");
                    button.set_autoPostBack(false);
                }
            }                  
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnDelete">
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
    <div style="margin: 5px 1px; clear: both">
        <telerik:RadGrid runat="server" ID="RadGrid1" AutoGenerateColumns="false" AllowPaging="true"
            OnNeedDataSource="RadGrid1_NeedDataSource" AllowSorting="true" GridLines="Horizontal"
            BorderWidth="0" PageSize="20" AllowMultiRowSelection="true" OnDeleteCommand="RadGrid1_DeleteCommand"
            Skin="Office2007">
            <MasterTableView DataKeyNames="Id" CommandItemDisplay="None" ItemStyle-Height="30"
                HeaderStyle-Height="35">
                <Columns>
                    <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn" />
                    <telerik:GridBoundColumn DataField="ReceiverName" SortExpression="ReceiverName" HeaderText="To" />
                    <telerik:GridBoundColumn DataField="ReceiverPhone" SortExpression="ReceiverPhone"
                        HeaderText="Phone" />
                    <telerik:GridHyperLinkColumn DataTextField="Subject" SortExpression="Subject" HeaderText="Subject"
                        DataNavigateUrlFormatString="~/Administrator/ViewDetailSMS.aspx?ID={0}" DataNavigateUrlFields="Id" />
                    <telerik:GridBoundColumn DataFormatString="{0:d}" DataField="Date" DataType="System.DateTime"
                        HeaderText="Sent" SortExpression="Date" UniqueName="Date" />
                    <telerik:GridButtonColumn ConfirmText="Delete this SMS?" ConfirmDialogType="RadWindow"
                        ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" />
                </Columns>
                <EditFormSettings>
                    <EditColumn ButtonType="ImageButton" />
                </EditFormSettings>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
            <PagerStyle Mode="NextPrevAndNumeric" />
        </telerik:RadGrid>
    </div>
</asp:Content>
