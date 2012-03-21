<%@ page language="C#" autoeventwireup="true" inherits="Administrator_CreateDashboard, App_Web_l3oqsulm" %>

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
            height: 100%;
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
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadFormDecorator ID="RadFormDecorator1" DecoratedControls="All" runat="server"
        Skin="Office2007" />
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
    <telerik:RadAjaxLoadingPanel runat="server" Transparency="25" ID="RadAjaxLoadingPanel1"
        CssClass="RadAjax RadAjax_Vista">
        <div class="raDiv">
        </div>
        <div class="raColor raTransp">
        </div>
    </telerik:RadAjaxLoadingPanel>
    <table cellpadding="5" cellspacing="0" style="margin:10px;">
        <tr>
            <td valign="top">
                Title:
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <telerik:RadTextBox runat="server" ID="txtTitle" Width="300">
                            </telerik:RadTextBox>
                        </td>
                        <td>Append Aministrator</td>
                        <td><asp:CheckBox ID="chkAddAdmin" runat="server" AutoPostBack="true" 
                                oncheckedchanged="chkAddAdmin_CheckedChanged" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top">
                Content:
            </td>
            <td>
                <telerik:RadTextBox runat="server" ID="txtContent" TextMode="MultiLine" Width="300">
                </telerik:RadTextBox>
            </td>
        </tr>
        <tr>
            <td valign="top">
                To:
            </td>
            <td>
                <telerik:RadGrid ID="grdSalemen" runat="server" Skin="Office2007" AllowMultiRowSelection="true"
                    AutoGenerateColumns="false" OnNeedDataSource="grdSalemen_NeedDataSource" Width="714px"
                    Height="380px">
                    <MasterTableView DataKeyNames="Phone">
                        <Columns>
                            <telerik:GridClientSelectColumn UniqueName="CheckboxSelectColumn" FooterText="CheckBoxSelect footer" />
                            <telerik:GridBoundColumn DataField="FullName" HeaderText="Full Name" />
                            <telerik:GridBoundColumn DataField="Role" HeaderText="Role" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True">
                        </Scrolling>
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <telerik:RadButton runat="server" Text="Create" OnClick="btnCreateDashboard_Click"></telerik:RadButton>
                <button onclick="ClosePopup()">Cancel</button>
                <asp:Label runat="server" Text="" ID="lblMessage"></asp:Label>            
            </td>
        </tr>
    </table>
    
    <%--<div>
        <div style="margin-top: 4px; text-align: right; clear: both">
            <button title="Submit" id="close" onclick="returnToParent(); return false;">
                Submit</button>
        </div>
    </div>--%>
    </form>
</body>
</html>
