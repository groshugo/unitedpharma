<%@ page title="Assign Function" language="C#" masterpagefile="~/Administrator/Admin.master" autoeventwireup="true" inherits="Administrator_AssignFunctionManagement, App_Web_wvzyt0rb" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>
        Assign Function</h3>
    <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlUsers">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ListBoxFunctions" />
                    <telerik:AjaxUpdatedControl ControlID="ListBoxFunctionUpdate" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnUpdateFunction">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ListBoxFunctions" />
                    <telerik:AjaxUpdatedControl ControlID="ListBoxFunctionUpdate" />                    
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
    <div style="margin-left: 20px;">
        <div style="margin-bottom:10px;">
            Choose an Administrator:
            <telerik:RadComboBox Skin="Office2007" ID="ddlUsers" runat="server" OnSelectedIndexChanged="ddlUsers_SelectedIndexChanged"
                AutoPostBack="true">
            </telerik:RadComboBox>
        </div>
        <div>
        <telerik:RadListBox ID="ListBoxFunctions" runat="server" AllowTransferDuplicates="false"
            TransferMode="Move" AllowTransfer="true" TransferToID="ListBoxFunctionUpdate"
            Height="400px" Width="300px" Skin="Office2007" Style="float:left;">
        </telerik:RadListBox>
        <telerik:RadListBox Skin="Office2007" ID="ListBoxFunctionUpdate" runat="server" Height="400px"
            Width="300px" AllowDelete="false">
        </telerik:RadListBox>
        <div class="clear"></div>
        </div>        
        <div style="margin-top: 10px;">
            <telerik:RadButton ID="btnUpdateFunction" runat="server" Text="Save" OnClick="btnUpdateFunction_Click">
            </telerik:RadButton>
        </div>
    </div>    
</asp:Content>
