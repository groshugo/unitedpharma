﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateDashboard.aspx.cs" Inherits="Administrator_CreateDashboard" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Create new Dashboard</title>
    <style type="text/css">
        html, body, form
        {
            padding: 0;
            margin: 0;
            /*height: 100%;*/
            background: #f2f2de;
        }
        
        body
        {
            font: normal 11px Arial, Verdana, Sans-serif;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" method="post">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
    <telerik:RadFormDecorator ID="RadFormDecorator1" DecoratedControls="All" runat="server" Skin="Office2007" />
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            <!--
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }
            function returnToParent() {
                var oWnd = GetRadWindow();
                oWnd.close("1");
            }
            function ClosePopup() {
                var oWnd = GetRadWindow();
                oWnd.close();
            }


            function OnClientFileUploadFail(sender, args) {
                alert(args.get_message());
            };
            -->
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdSalemen">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdSalemen" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnCreateDashboard">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdSalemen" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel runat="server" Transparency="25" ID="RadAjaxLoadingPanel1" CssClass="RadAjax RadAjax_Vista">
        <div class="raDiv"></div>
        <div class="raColor raTransp"></div>
    </telerik:RadAjaxLoadingPanel>
    <table cellpadding="5" cellspacing="0" style="margin: 10px;">
        <tr>
            <td valign="middle">Title:</td>
            <td>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="40%"><telerik:RadTextBox runat="server" ID="txtTitle" Width="300"></telerik:RadTextBox></td>
                        <td width="12%" style="padding-left:10px">Attach File : </td>
                        <td width="48%">
                            <telerik:RadAsyncUpload ID="dashboardAttachedFile" runat="server" AllowedFileExtensions="jpg,png,bmp,doc,docx,xls,xlsx,ppt,pptx,zip,rar"
                                        MultipleFileSelection="Disabled" MaxFileInputsCount="1" MaxFileSize="1024000" Width="300"
                                        TargetFolder="~/Upload/Attachments" OnFileUploaded="RadAsyncUploadFileUploaded">
                            </telerik:RadAsyncUpload>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>Content:</td>
            <td>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="40%">
                            <telerik:RadTextBox runat="server" ID="txtContent" TextMode="MultiLine" Width="300">
                            </telerik:RadTextBox>
                        </td>
                        <td width="60%" style="padding-left:10px">Append Aministrator <asp:CheckBox ID="chkAddAdmin" runat="server" AutoPostBack="true" OnCheckedChanged="chkAddAdmin_CheckedChanged" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top">To:</td>
            <td>
                <telerik:RadGrid ID="grdSalemen" runat="server" Skin="Office2007" AllowMultiRowSelection="True"
                    AutoGenerateColumns="False" OnNeedDataSource="grdSalemen_NeedDataSource" 
                    Width="714px" Height="380px" CellSpacing="0" 
                    GridLines="None">
                    <MasterTableView DataKeyNames="Phone">
<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>

<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridClientSelectColumn UniqueName="CheckboxSelectColumn" 
                                FooterText="CheckBoxSelect footer" >
                                <HeaderStyle Width="35px" />
                            </telerik:GridClientSelectColumn>
                            <telerik:GridBoundColumn DataField="FullName" HeaderText="Full Name" >
                                <HeaderStyle Width="400px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Role" HeaderText="Role" >
                                <HeaderStyle Width="259px" />
                            </telerik:GridBoundColumn>
                        </Columns>

<EditFormSettings>
<EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
</EditFormSettings>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True">
                        </Scrolling>
                    </ClientSettings>

<FilterMenu EnableImageSprites="False"></FilterMenu>

<HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Office2007"></HeaderContextMenu>
                </telerik:RadGrid>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <telerik:RadButton runat="server" Text="Create" OnClick="btnCreateDashboard_Click"></telerik:RadButton>
                <button onclick="ClosePopup()">Cancel</button>
                <asp:Label runat="server" Text="" ID="lblMessage"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>