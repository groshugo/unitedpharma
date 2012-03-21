<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerLog.aspx.cs" Inherits="Administrator_CustomerLog" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            <!--
             function GetId() {
                 if ($find("<%=DetailLog.ClientID %>").get_masterTableView().get_selectedItems().length < 1) {
                     alert("Select version in log to update");
                 }
                 else {
                     var RadGrid1 = $find("<%=DetailLog.ClientID %>");
                     var MasterTable = RadGrid1.get_masterTableView();
                     var row = MasterTable.get_selectedItems()[0];
                     var keyValues = row.getDataKeyValue("Id");
                     document.getElementById("<%= hdfID.ClientID %>").value=keyValues;
                 }
             }
             function GetRadWindow()   //Get reference to window    
             {
                 var oWindow = null;
                 if (window.radWindow)
                     oWindow = window.radWindow;
                 else if (window.frameElement.radWindow)
                     oWindow = window.frameElement.radWindow;
                 return oWindow;
             }
             function ClosePopup() {
                 GetRadWindow().close();
                 top.location.href = top.location.href;
             }
            -->
        </script>
    </telerik:RadCodeBlock>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadFormDecorator ID="RadFormDecorator1" DecoratedControls="All" runat="server" Skin="Office2007" />
    <div style="width:915px;">
        <telerik:RadGrid runat="server" ID="DetailLog" AutoGenerateColumns="false" Skin="Office2007" AllowMultiRowSelection="false" 
            AllowPaging="false">
            <MasterTableView DataKeyNames="Id" ClientDataKeyNames="Id">
                <Columns>
                    <telerik:GridClientSelectColumn UniqueName="CheckboxSelectColumn" FooterText="CheckBoxSelect footer" />
                    <telerik:GridBoundColumn DataField="UpiCode" HeaderText="UPI Code" />
                    <telerik:GridBoundColumn DataField="FullName" HeaderText="Full Name" />
                    <telerik:GridBoundColumn DataField="Password" HeaderText="Password" />
                    <telerik:GridTemplateColumn HeaderText="Custome Typer">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblCustomerType" Text='<%# Eval("CustomerTypeName") %>'></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Channel">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblChannel" Text='<%# Eval("ChannelName") %>'></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="Address" HeaderText="Address" />
                    <telerik:GridBoundColumn DataField="Street" HeaderText="Street" />
                    <telerik:GridBoundColumn DataField="Ward" HeaderText="Ward" Visible="false" />
                    <telerik:GridBoundColumn DataField="Phone" HeaderText="Phone" />
                    <telerik:GridTemplateColumn HeaderText="District" UniqueName="DistrictColumn">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblDistrict" Text='<%# Eval("DistrictName") %>'></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="CreateDate">
                        <ItemTemplate>
                            <asp:Label ID="lblCreateDate" runat="server" Text='<%# String.Format("{0:d}", Eval("CreateDate")) %>'></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="UpdateDate">
                        <ItemTemplate>
                            <asp:Label ID="lblUpdateDate" runat="server" Text='<%# String.Format("{0:d}", Eval("UpdateDate")) %>'></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Location" UniqueName="LocationColumn">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblLocation" Text='<%# Eval("LocalName") %>'></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                        <telerik:GridCheckBoxColumn DataField="Status" HeaderText="Status" UniqueName="Status">
                    </telerik:GridCheckBoxColumn>
                <%--<telerik:GridBoundColumn DataField="SupervisorName" HeaderText="Supervisor Name" />
                <telerik:GridBoundColumn DataField="PositionName" HeaderText="Position" />
                <telerik:GridBoundColumn DataField="supervisorPhone" HeaderText="Supervisor Phone" />--%>
                </Columns>
            </MasterTableView>
            <ClientSettings>
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
        </telerik:RadGrid>
        <div>
            <asp:Button ID="btn1" runat="server" Text="Approve" onclick="btn1_Click" OnClientClick="GetId();" SkinID="Office2007" Enabled="false" />
            <asp:Button ID="btnnotApprove" runat="server" Text="Not Approve" OnClick="btnnotApprove_Click" OnClientClick="GetId();" SkinID="Office2007" Enabled="false" />
            <button onclick="ClosePopup()">Close window</button>
        </div>
        <asp:HiddenField ID="hdfID" runat="server" Value="" />
    </div>
    </div>
    </form>
</body>
</html>
