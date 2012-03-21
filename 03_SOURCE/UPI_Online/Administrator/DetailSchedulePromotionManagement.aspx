<%@ page title="" language="C#" masterpagefile="~/Administrator/AdminFull.master" autoeventwireup="true" inherits="Administrator_DetailSchedulePromotionManagement, App_Web_wvzyt0rb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<telerik:RadFormDecorator ID="FormDecorator1" runat="server" DecoratedControls="all" Skin="Office2007"></telerik:RadFormDecorator>
    <style>
        table#detailSchedule td.rgColumn{padding-left:10px}
        #detailSchedule tr.rgRow{background:#C3D9F1;}
        #detailSchedule td.boldText{font-weight:bold; width:150px; }
    </style>
    <div style="border:1px solid #688CAF">
        <div style="clear:both">
            <div style="float:left; padding:10px;"><h3>Detail of scheduled promotion</h3></div>
            <div style="float:right;padding:10px;">
                <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
            </div>
        </div>
        <table id="detailSchedule" width="100%">
            <tr class="rgRow">
                <td class="boldText rgColumn">UPI Code:</td>
                <td class="rgColumn"<asp:Label ID="lblUPI" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="boldText rgColumn">Title</td>
                <td class="rgColumn"><asp:Label ID="lbltitle" runat="server"></asp:Label></td>
            </tr>
            <tr class="rgRow">
                <td class="boldText rgColumn">SMS Content</td>
                <td class="rgColumn"><asp:Label ID="lblSMSContent" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="boldText rgColumn">Web Content</td>
                <td class="rgColumn" align="justify"><asp:Literal ID="ltrWebContent" runat="server"></asp:Literal></td>
            </tr>
            <tr class="rgRow">
                <td class="boldText rgColumn">Start Date</td>
                <td class="rgColumn"><asp:Label ID="lblStartDate" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="boldText rgColumn">End Date</td>
                <td class="rgColumn"><asp:Label ID="lblEndDate" runat="server"></asp:Label></td>
            </tr>
            <tr class="rgRow">
                <td class="boldText rgColumn">Administrator</td>
                <td class="rgColumn"><asp:Label ID="lblAdmin" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2" class="boldText rgColumn">Phone Numbers</td>
            </tr>
            <tr>
                <td colspan="2">
                    <telerik:RadGrid ID="SchedulePhoneNumbers" runat="server" Skin="Office2007" AllowMultiRowSelection="true"
                        AutoGenerateColumns="false" OnNeedDataSource="SchedulePhoneNumbers_NeedDataSource" Width="1080px">
                        <MasterTableView>
                            <Columns>
                                <telerik:GridBoundColumn DataField="Phone" HeaderText="Phone" />      
                                <telerik:GridBoundColumn DataField="CustomerName" HeaderText="Customer Name" />
                                <telerik:GridBoundColumn DataField="UpiCode" HeaderText="UpiCode" />       
                                <telerik:GridBoundColumn DataField="FullName" HeaderText="Supervisor" />    
                                <telerik:GridBoundColumn DataField="PositionName" HeaderText="Position" />        
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True">
                            </Scrolling>
                        </ClientSettings>
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

