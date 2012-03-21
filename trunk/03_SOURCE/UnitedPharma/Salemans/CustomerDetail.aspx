<%@ Page Title="" Language="C#" MasterPageFile="~/Salemans/MasterPage.master" AutoEventWireup="true" CodeFile="CustomerDetail.aspx.cs" Inherits="Salemans_CustomerDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentCommand" Runat="Server">
    <style>
        #table1 tr.BgColor{background:#AEC7FF}
        #table1 td.title{font-weight:bold}
        
    </style>
    <div>
        <div style="clear:both">
            <div style="float:left; padding-left:5px;"><h3>Customer Detail</h3></div>
            <div style="float:right; line-height:30px; padding-right:5px;">
                <telerik:RadButton ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" Skin="Office2007"></telerik:RadButton>
            </div>
        </div>
        <table width="100%" cellpadding="0" cellspacing="0" border="0" id="table1">
            <tr class="BgColor">
                <td class="title" style="width:15%">UPI</td>
                <td style="width:25%"><asp:Label ID="lblUpi" runat="server"></asp:Label></td>
                <td class="title" style="width:15%">Customer Name</td>
                <td style="width:45%"><asp:Label ID="lblCustomerName" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="title">Phone number</td>
                <td><asp:Label ID="lblPhoneNumber" runat="server"></asp:Label></td>
                <td class="title">Address</td>
                <td><asp:Label ID="lblAddress" runat="server"></asp:Label></td>
            </tr>
            <tr class="BgColor">
                <td class="title">Customer Type</td>
                <td><asp:Label ID="lblCustomerType" runat="server"></asp:Label></td>
                <td class="title">Channel</td>
                <td><asp:Label ID="lblChannel" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="title">District</td>
                <td><asp:Label ID="lblDistrict" runat="server"></asp:Label></td>
                <td class="title">Local</td>
                <td><asp:Label ID="lblLocal" runat="server"></asp:Label> </td>
            </tr>
            <tr class="BgColor">
                <td class="title">Create Date</td>
                <td><asp:Label ID="lblCreateDate" runat="server"></asp:Label></td>
                <td class="title">Update Date</td>
                <td><asp:Label ID="lblUpdateDate" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="title">Status</td>
                <td><asp:Label ID="lblStatus" runat="server" /></td>
                <td class="title">Enable</td>
                <td><asp:CheckBox ID="chkEnable" runat="server" Enabled="false" /></td>
            </tr>
        </table>
        <div style="padding-top:15px; width:100%">
            <div style="float:left; width:44%">
                <h3>List of Salesmen</h3>
                <telerik:RadGrid ID="SalesManager" runat="server" Skin="Office2007" AllowMultiRowSelection="true"
                    AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="10">
                    <PagerStyle Visible="false" />
                    <MasterTableView>
                        <Columns>
                            <telerik:GridBoundColumn DataField="UpiCode" HeaderText="UpiCode" />  
                            <telerik:GridBoundColumn DataField="FullName" HeaderText="Customer Name" /> 
                            <telerik:GridBoundColumn DataField="Phone" HeaderText="Phone" />
                            <telerik:GridBoundColumn DataField="RoleName" HeaderText="Role" />        
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div style="float:right; width:54%;">
                <h3 style=" text-align:right">List of Supervisors</h3>
                <telerik:RadGrid ID="SupervisorManager" runat="server" Skin="Office2007" AllowMultiRowSelection="true"
                    AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="10">
                    <PagerStyle Visible="false" />
                    <MasterTableView>
                        <Columns>
                            <telerik:GridBoundColumn DataField="FullName" HeaderText="Supervisor" />
                            <telerik:GridBoundColumn DataField="Address" HeaderText="Address" />  
                            <telerik:GridBoundColumn DataField="Phone" HeaderText="Phone" />      
                            <telerik:GridBoundColumn DataField="DistrictName" HeaderText="District" />      
                            <telerik:GridBoundColumn DataField="PositionName" HeaderText="Position" />        
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div class="clear"></div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>

