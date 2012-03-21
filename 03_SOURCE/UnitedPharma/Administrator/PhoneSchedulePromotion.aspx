<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PhoneSchedulePromotion.aspx.cs" Inherits="Administrator_PhoneSchedulePromotion" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadFormDecorator ID="RadFormDecorator1" DecoratedControls="All" runat="server" Skin="Office2007" />
    
    <telerik:RadAjaxLoadingPanel runat="server" Transparency="25" ID="RadAjaxLoadingPanel1" CssClass="RadAjax RadAjax_Vista">
        <div class="raDiv">
        </div>
        <div class="raColor raTransp">
        </div>
    </telerik:RadAjaxLoadingPanel>
    <div>
        <telerik:RadGrid ID="SchedulePhoneNumbers" runat="server" Skin="Office2007" AllowMultiRowSelection="true"
            AutoGenerateColumns="false" OnNeedDataSource="SchedulePhoneNumbers_NeedDataSource" Width="850px" Height="530px">
            <MasterTableView >
                <Columns>
                    <telerik:GridBoundColumn DataField="Phone" HeaderText="Phone" />      
                    <telerik:GridBoundColumn DataField="CustomerName" HeaderText="Customer Name" />
                    <telerik:GridBoundColumn DataField="UpiCode" HeaderText="UpiCode" />       
                    <telerik:GridBoundColumn DataField="FullName" HeaderText="Supervisor" />    
                    <telerik:GridBoundColumn DataField="PositionName" HeaderText="Position" />        
                </Columns>
            </MasterTableView>
            <ClientSettings>
                <Selecting AllowRowSelect="true" />
                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True">
                </Scrolling>
            </ClientSettings>
        </telerik:RadGrid>
    </div>
    </form>
</body>
</html>
