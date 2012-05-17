<%@ Page Title="Salesmen - Area" Language="C#" MasterPageFile="~/Administrator/Admin.master"
    AutoEventWireup="true" CodeFile="SalesmenAreaManagement.aspx.cs" Inherits="Administrator_SalesmenAreaManagement" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>
        Salesmen - Area Management</h3>
    <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlGroup">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlRegion" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlRegion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlArea" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlGroup">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ListSalemens" />  
                    <telerik:AjaxUpdatedControl ControlID="ListManagers" />                  
                </UpdatedControls>
            </telerik:AjaxSetting>  
            <telerik:AjaxSetting AjaxControlID="ddlRegion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ListSalemens" />  
                    <telerik:AjaxUpdatedControl ControlID="ListManagers" />                  
                </UpdatedControls>
            </telerik:AjaxSetting>  
            <telerik:AjaxSetting AjaxControlID="ddlArea">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ListSalemens" />  
                    <telerik:AjaxUpdatedControl ControlID="ListManagers" />                  
                </UpdatedControls>
            </telerik:AjaxSetting>            
            <telerik:AjaxSetting AjaxControlID="btnUpdate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ListSalemens" />  
                    <telerik:AjaxUpdatedControl ControlID="ListManagers" />                  
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
        <div style="margin-bottom: 10px;">
            <table>
                <tr>
                    <td>Choose a Group:</td>
                    <td>Choose a Region:</td>
                    <td>Choose a Area:</td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadComboBox Skin="Office2007" ID="ddlGroup" runat="server" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged"
                            AutoPostBack="true"></telerik:RadComboBox>
                    </td>
                    <td>
                        <telerik:RadComboBox Skin="Office2007" ID="ddlRegion" runat="server" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged"
                            AutoPostBack="true"></telerik:RadComboBox>
                    </td>
                    <td>
                        <telerik:RadComboBox Skin="Office2007" ID="ddlArea" runat="server" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged"
                            AutoPostBack="true"></telerik:RadComboBox>
                    </td>
                </tr>
            </table>
        </div>        
        <div style="width: 300px; float:left;">
            List Salesmen:<br />
            <telerik:RadListBox ID="ListSalemens" runat="server" AllowTransferDuplicates="false"
                TransferMode="Move" AllowTransfer="true" TransferToID="ListManagers" Height="400px"
                Width="300px" Skin="Office2007">
            </telerik:RadListBox>
        </div>
        <div style="width: 300px;float:left;">
            Managers of this Area:<br />
            <telerik:RadListBox Skin="Office2007" ID="ListManagers" runat="server" Height="400px"
                Width="300px" AllowDelete="true">
            </telerik:RadListBox>
        </div>
        <div class="clear"></div>        
        <div style="margin-top: 10px;">
            <telerik:RadButton ID="btnUpdate" runat="server" Text="Save" OnClick="btnUpdate_Click">
            </telerik:RadButton>
        </div>
    </div>   
</asp:Content>
