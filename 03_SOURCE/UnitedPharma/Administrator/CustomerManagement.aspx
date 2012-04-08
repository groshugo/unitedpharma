<%@ Page Title="Customer Management" Language="C#" MasterPageFile="~/Administrator/Admin.master"
    AutoEventWireup="true" CodeFile="CustomerManagement.aspx.cs" Inherits="Administrator_CustomerManagement" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            <!--
            function openWin() {
                var oWnd = radopen("ImportCustomers.aspx", "RadWindow1");
            }
            function OnClientClose(oWnd, args) {
                
            }
            -->
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="RadWindowManager2" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Office2007" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Behaviors="Close" OnClientClose="OnClientClose" Width="650px" Height="480px"
                NavigateUrl="ImportCustomers.aspx">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <h3>
        Customer Management</h3>
    <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="CustomerList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="CustomerList" />
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
        <button onclick="openWin(); return false;">Import customers</button>
    </div>
    <telerik:RadGrid ID="CustomerList" runat="server" Skin="Office2007" AllowPaging="true"
        AutoGenerateColumns="false" OnNeedDataSource="CustomerList_NeedDataSource" OnUpdateCommand="CustomerList_UpdateCommand"
        OnDeleteCommand="CustomerList_DeleteCommand" OnInsertCommand="CustomerList_InsertCommand"
        OnItemDataBound="CustomerList_ItemDataBound" AllowMultiRowEdit="true">
        <MasterTableView DataKeyNames="Id" CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnCurrentPage"
            EditMode="EditForms">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn" />
                <telerik:GridBoundColumn DataField="UpiCode" HeaderText="UPI Code" />
                <telerik:GridBoundColumn DataField="FullName" HeaderText="Full Name" />
                <telerik:GridBoundColumn DataField="Password" HeaderText="Password" />
                <telerik:GridTemplateColumn HeaderText="Custome Typer">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblCustomerType" Text='<%# Eval("CustomerTypeName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadComboBox runat="server" ID="ddlCustomerType">
                        </telerik:RadComboBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Channel">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblChannel" Text='<%# Eval("ChannelName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadComboBox runat="server" ID="ddlChannels">
                        </telerik:RadComboBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Address" HeaderText="Address" />
                <telerik:GridBoundColumn DataField="Street" HeaderText="Street" />
                <telerik:GridBoundColumn DataField="Ward" HeaderText="Ward" Visible="false" />
                <telerik:GridBoundColumn DataField="Phone" HeaderText="Phone" />
                <telerik:GridTemplateColumn HeaderText="District" UniqueName="DistrictColumn">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblDistrict" Text='<%# Eval("DistrictName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:HiddenField ID="hdfSection" runat="server" Value='<%# Eval("SectionId") %>' />
                        <asp:HiddenField ID="hdfProvince" runat="server" Value='<%# Eval("ProvinceId") %>' />
                        <asp:HiddenField ID="hdfDistrict" runat="server" Value='<%# Eval("DistrictId") %>' />
                        Section:
                        <telerik:RadComboBox ID="ddlSection" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                        </telerik:RadComboBox>
                        &nbsp;Province:
                        <telerik:RadComboBox ID="ddlprovince" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlprovince_SelectedIndexChanged">
                        </telerik:RadComboBox>                        
                        &nbsp;District:
                        <telerik:RadComboBox runat="server" ID="ddlDistricts">
                        </telerik:RadComboBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="CreateDate">
                    <ItemTemplate>
                        <asp:Label ID="lblCreateDate" runat="server" Text='<%# String.Format("{0:d}", Eval("CreateDate")) %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:HiddenField ID="hdfCreateDate" runat="server" Value='<%# String.Format("{0:d}", Eval("CreateDate")) %>' />
                        <telerik:RadDatePicker ID="txtCreateDate" runat="server" DateInput-ReadOnly="true">
                            <Calendar ID="CalendarCreateDate" runat="server" RangeMinDate="1900-01-01" RangeMaxDate="2050-12-29">
                            </Calendar>
                        </telerik:RadDatePicker>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="UpdateDate">
                    <ItemTemplate>
                        <asp:Label ID="lblUpdateDate" runat="server" Text='<%# String.Format("{0:d}", Eval("UpdateDate")) %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:HiddenField ID="hdfUpdateDate" runat="server" Value='<%# String.Format("{0:d}", Eval("UpdateDate")) %>' />
                        <telerik:RadDatePicker ID="txtUpdateDate" runat="server" DateInput-ReadOnly="true">
                            <Calendar ID="CalendarUpdateDate" runat="server" RangeMinDate="1900-01-01" RangeMaxDate="2050-12-29">
                            </Calendar>
                        </telerik:RadDatePicker>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Location" UniqueName="LocationColumn">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblLocation" Text='<%# Eval("LocalName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:HiddenField ID="hdfGroup" runat="server" Value='<%# Eval("GroupId") %>' />
                        <asp:HiddenField ID="hdfRegion" runat="server" Value='<%# Eval("RegionId") %>' />
                        <asp:HiddenField ID="hdfArea" runat="server" Value='<%# Eval("AreaId") %>' />
                        <asp:HiddenField ID="hdfLocal" runat="server" Value='<%# Eval("LocalId") %>' />
                         Group:
                        <telerik:RadComboBox ID="ddlGroup" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                        </telerik:RadComboBox>
                        &nbsp;Region:
                        <telerik:RadComboBox ID="ddlRegion" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                        </telerik:RadComboBox>                        
                        <br />Area:&nbsp;&nbsp;&nbsp;
                        <telerik:RadComboBox ID="ddlArea" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged">
                        </telerik:RadComboBox>                        
                        &nbsp;Local: &nbsp;&nbsp;
                        <telerik:RadComboBox runat="server" ID="ddlLocation">
                        </telerik:RadComboBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridCheckBoxColumn DataField="Status" HeaderText="Status" UniqueName="Status">
                </telerik:GridCheckBoxColumn>
                <telerik:GridTemplateColumn HeaderText="Note Of Salesmen" DataField="NoteOfSalesmen">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtNoteOfSalesmen" TextMode="MultiLine" Rows="4"
                            Columns="50" ></asp:TextBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridButtonColumn ConfirmText="Delete this customer?" ConfirmDialogType="RadWindow"
                    ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" />
            </Columns>
            <EditFormSettings>
                <EditColumn ButtonType="ImageButton" />
            </EditFormSettings>
        </MasterTableView>
        <PagerStyle Mode="NextPrevAndNumeric" />
    </telerik:RadGrid>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Office2007" Width="650px" Height="480px">
    </telerik:RadWindowManager>
</asp:Content>
