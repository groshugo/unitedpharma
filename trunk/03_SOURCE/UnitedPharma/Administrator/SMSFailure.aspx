<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/SMS.master" AutoEventWireup="true" CodeFile="SMSFailure.aspx.cs" Inherits="Administrator_SMSFailure" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>SMS Failure</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentCommand" Runat="Server">
    <h3>Failure SMS</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style>
        .sms_header
        {
            background: url(Images/nav_bg.png) repeat-x;
        }
    </style>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>
    <telerik:RadCodeBlock runat="server" ID="radCodeBlock">
        <script type="text/javascript">
            <!--
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
                        var text = "Are you sure to resend?";
                        var result = radconfirm(text, callBackFunction, 300, 100, null, "Confirm");
                    }
                }
                else {
                    alert("Please choose SMS to resend");
                    button.set_autoPostBack(false);
                }
            }         
            -->
        </script>
    </telerik:RadCodeBlock>
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
            <telerik:AjaxSetting AjaxControlID="btnResendSelected">
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
    <div style="margin: 5px 1px; position:relative">
        <div>
            <telerik:RadGrid runat="server" ID="RadGrid1" AutoGenerateColumns="false" AllowPaging="true" OnDeleteCommand="RadGrid1_DeleteCommand"
                AllowSorting="true" Skin="Office2007" PageSize="20" OnNeedDataSource="RadGrid1_NeedDataSource" OnItemCommand="RadGrid1_ItemCommand"
                AllowMultiRowSelection="True">
                <MasterTableView DataKeyNames="Id" CommandItemDisplay="None" ClientDataKeyNames="Id">
                    <Columns>                    
                        <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn" />
                        <telerik:GridBoundColumn DataField="SenderName" SortExpression="SenderName" HeaderText="From" />
                        <telerik:GridBoundColumn DataField="SenderPhone" SortExpression="SenderPhone" HeaderText="Phone" />
                        <telerik:GridBoundColumn DataField="Subject" SortExpression="Subject" HeaderText="Subject" />
                        <telerik:GridBoundColumn DataField="ReceiverPhone" SortExpression="ReceiverPhone"
                            HeaderText="ReceiverPhone" />
                        <telerik:GridButtonColumn ConfirmDialogType="RadWindow" ButtonType="LinkButton" Text="Resend" CommandName="Resend" HeaderText="Resend" />
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
    </div>
        <div style="float:right; position:absolute; top:5px; right:5px">
            <telerik:RadButton ID="btnResendSelected" runat="server" Text="Resend Selected" Skin="Office2007" 
            OnClick="btnResendSelected_Click" OnClientClicked="ConfirmDelete">
            </telerik:RadButton>
        </div>
</asp:Content>

