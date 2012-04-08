<%@ Page Title="Customers Management" Language="C#" MasterPageFile="~/Salemans/MasterPage.master" AutoEventWireup="true" CodeFile="CustomersManagement.aspx.cs" Inherits="Salemans_CustomersManagement" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentCommand" Runat="Server">
    <style>
        .divControl{width:150px;}
        .divLabel{width:100px; font-weight:bold; text-align:right}
        
    </style>
    
    <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="CustomerList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                    <telerik:AjaxUpdatedControl ControlID="RadWindowManager1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlSection">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid2" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="ddlprovince" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RadWindowManager1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlprovince">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid2" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="ddlDistricts" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RadWindowManager1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlGroup">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlRegion" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RadWindowManager1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlRegion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlArea" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RadWindowManager1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlArea">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlLocal" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RadWindowManager1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" CssClass="RadAjax RadAjax_Vista" Transparency="25">
        <div class="raDiv">
        </div>
        <div class="raColor raTransp">
        </div>
    </telerik:RadAjaxLoadingPanel>
    <div>
        <div style="float: left;">
            <h3>List of customers</h3>
        </div>
        <div style="float: right;">
            <table border="0" cellpadding="5" cellspacing="5">
            <tr>
                    <td class="divLabel">Group</td>
                    <td class="divControl"><telerik:RadComboBox ID="ddlGroup" runat="server" 
                            AutoPostBack="true" onselectedindexchanged="ddlGroup_SelectedIndexChanged"></telerik:RadComboBox></td>
                    <td class="divLabel">Region</td>
                    <td class="divControl"><telerik:RadComboBox ID="ddlRegion" runat="server" 
                            AutoPostBack="true" Enabled="false" 
                            onselectedindexchanged="ddlRegion_SelectedIndexChanged"></telerik:RadComboBox></td>
                    <td class="divLabel">Area</td>
                    <td class="divControl"><telerik:RadComboBox ID="ddlArea" runat="server" 
                            AutoPostBack="true" Enabled="false" 
                            onselectedindexchanged="ddlArea_SelectedIndexChanged"></telerik:RadComboBox></td>
                    <td class="divLabel">Local</td>
                    <td class="divControl"><telerik:RadComboBox ID="ddlLocal" runat="server" AutoPostBack="true" Enabled="false"></telerik:RadComboBox></td>
            </tr>
            <tr>
                    <td class="divLabel">Fullname</td>
                    <td class="divControl"><telerik:RadTextBox ID="txtFullname" runat="server" Width="156px"></telerik:RadTextBox></td>
                    <td class="divLabel">Phone</td>
                    <td class="divControl"><telerik:RadTextBox ID="txtPhoneNumber" runat="server" Width="156px"></telerik:RadTextBox></td>

                    <td class="divLabel">UPI Code</td>
                    <td class="divControl"><telerik:RadTextBox ID="txtUpiCode" runat="server" Width="156px"></telerik:RadTextBox></td>
                    <td colspan="2">
                        <telerik:RadButton ID="btnFilter" runat="server" SkinID="Office2007" Text="Filter" OnClick="btnFilter_Click"></telerik:RadButton>
                        <telerik:RadButton ID="btnClear" runat="server" SkinID="Office2007" Text="Clear" OnClick="btnClear_Click"></telerik:RadButton>
                    </td>
            </tr>
            </table>
        </div>
        <div class="clear"></div>
        <telerik:RadGrid runat="server" ID="RadGrid2" AutoGenerateColumns="false" AllowPaging="true"
            OnNeedDataSource="RadGrid2_NeedDataSource" AllowSorting="true" Skin="Office2007"
            PageSize="50" AllowMultiRowSelection="true" OnUpdateCommand="RadGrid2_UpdateCommand" OnItemDataBound="RadGrid2_ItemDataBound">
            <MasterTableView DataKeyNames="Id" CommandItemDisplay="None" InsertItemPageIndexAction="ShowItemOnCurrentPage" EditMode="EditForms">
                <Columns>
                    <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn" />
                <telerik:GridBoundColumn DataField="UpiCode" HeaderText="UPI Code" ReadOnly="true" />
                <telerik:GridTemplateColumn HeaderText="Full Name" UniqueName="FullNameColumn">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblFullName" Text='<%# Eval("FullName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtFullName" runat="server" Text='<%# Eval("FullName") %>'></asp:TextBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="CustomerTypeName" HeaderText="Custome Typer" ReadOnly="true" />
                <telerik:GridTemplateColumn HeaderText="Phone" UniqueName="PhoneNumber">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblPhone" Text='<%# Eval("Phone") %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Address" HeaderText="Address" Visible="false" />
                <telerik:GridBoundColumn DataField="Street" HeaderText="Street" Visible="false" />
                <telerik:GridBoundColumn DataField="Ward" HeaderText="Ward" Visible="false" />
                <telerik:GridTemplateColumn HeaderText="District" UniqueName="DistrictColumn" Visible="false">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblDistrict" Text='<%# Eval("DistrictName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:HiddenField ID="hdfSection" runat="server" Value='<%# Eval("SectionId") %>' />
                        <asp:HiddenField ID="hdfProvince" runat="server" Value='<%# Eval("ProvinceId") %>' />
                        <asp:HiddenField ID="hdfDistrict" runat="server" Value='<%# Eval("DistrictId") %>' />
                        <table>
                            <tr>
                                <td>Section:</td>
                            <td>
                                <telerik:RadComboBox ID="ddlSection" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                                </telerik:RadComboBox>
                            </td>
                            <td>Province:</td>
                            <td>
                                <telerik:RadComboBox ID="ddlprovince" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlprovince_SelectedIndexChanged">
                                </telerik:RadComboBox>
                            </td>
                            <td>District:</td>
                            <td>
                                <telerik:RadComboBox runat="server" ID="ddlDistricts"></telerik:RadComboBox>
                            </td>
                            </tr>
                        </table>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <%--<telerik:GridBoundColumn DataField="CreateDate" HeaderText="CreateDate" DataFormatString="{0:d}" ReadOnly="true" />
                <telerik:GridBoundColumn DataField="UpdateDate" HeaderText="UpdateDate" DataFormatString="{0:d}" ReadOnly="true" />--%>
                <telerik:GridBoundColumn DataField="LocalName" HeaderText="Local" ReadOnly="true" />
                <telerik:GridCheckBoxColumn DataField="Status" HeaderText="Status" UniqueName="Status" ReadOnly="true">
                </telerik:GridCheckBoxColumn>
                <telerik:GridTemplateColumn HeaderText="View detail">
                    <ItemTemplate>
                        <asp:HyperLink ID="CustomerDetail" runat="server" Text="View customer detail" NavigateUrl='<%# String.Format("CustomerDetail.aspx?ID={0}",Eval("Id")) %>'></asp:HyperLink>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                </Columns>
                <EditFormSettings>
                    <EditColumn ButtonType="ImageButton" />
                </EditFormSettings>
            </MasterTableView>
            <PagerStyle Mode="NextPrevAndNumeric" />
        </telerik:RadGrid>
    </div>
    <div style="padding-top:20px">
        <h3>Customers are waiting for approval</h3>
        <telerik:RadGrid runat="server" ID="RadGrid1" AutoGenerateColumns="false" AllowPaging="true"
            OnNeedDataSource="RadGrid1_NeedDataSource" AllowSorting="true" Skin="Office2007"
            PageSize="50" AllowMultiRowSelection="true">
            <MasterTableView DataKeyNames="Id" CommandItemDisplay="None" InsertItemPageIndexAction="ShowItemOnCurrentPage">
                <Columns>
                <telerik:GridBoundColumn DataField="UpiCode" HeaderText="UPI Code" />
                <telerik:GridTemplateColumn HeaderText="Full Name" UniqueName="FullNameColumn">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblFullName" Text='<%# Eval("FullName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtFullName" runat="server" Text='<%# Eval("FullName") %>'></asp:TextBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="CustomerTypeName" HeaderText="Custome Typer" ReadOnly="true" />
                <telerik:GridBoundColumn DataField="ChannelName" HeaderText="Channel" ReadOnly="true" />
                <telerik:GridTemplateColumn HeaderText="Address" UniqueName="AddressColumn">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblAddress" Text='<%# Eval("Address") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtAddress" runat="server" Text='<%# Eval("Address") %>'></asp:TextBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Street" HeaderText="Street" ReadOnly="true" />
                <telerik:GridBoundColumn DataField="Ward" HeaderText="Ward" Visible="false" ReadOnly="true" />
                <telerik:GridTemplateColumn HeaderText="Phone" UniqueName="PhoneNumber">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblPhone" Text='<%# Eval("Phone") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtPhone" runat="server" Text='<%# Eval("Phone") %>'></asp:TextBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="DistrictName" HeaderText="District" ReadOnly="true" />
                <telerik:GridBoundColumn DataField="CreateDate" HeaderText="CreateDate" DataFormatString="{0:d}" ReadOnly="true" />
                <telerik:GridBoundColumn DataField="UpdateDate" HeaderText="UpdateDate" DataFormatString="{0:d}" ReadOnly="true" />
                <telerik:GridBoundColumn DataField="LocalName" HeaderText="Local" ReadOnly="true" />
                <telerik:GridCheckBoxColumn DataField="Status" HeaderText="Status" UniqueName="Status" ReadOnly="true">
                </telerik:GridCheckBoxColumn>
                </Columns>
                <EditFormSettings>
                    <EditColumn ButtonType="ImageButton" />
                </EditFormSettings>
            </MasterTableView>
            <PagerStyle Mode="NextPrevAndNumeric" />
        </telerik:RadGrid>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>

