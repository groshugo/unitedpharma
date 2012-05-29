<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Salemans/MasterPage.master"
    CodeFile="ShowDashboard.aspx.cs" Inherits="Salemans_ShowDashboard" Title="View all Dashboard" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentCommand" runat="Server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            <!--
            function openWin(id) {
                var oWnd = radopen("ViewDetailDashboard.aspx?Id=" + id, "RadWindow1");
            }
            function closeRadWindow() {
                window.location.reload();
                //$find("<%= RadAjaxManager1.ClientID %>").ajaxRequest();
            }  
            -->
        </script>
    </telerik:RadCodeBlock>
    <div style="float: left; width: 70%; padding: 10px">
        <h3 style="color: #000;">
            View all Dashboards
        </h3>
    </div>
    <div style="float: right; width: 20%; text-align: right; padding: 10px">
        <telerik:RadButton runat="server" ID="btnBack" Text="Back" Skin="Office2007" ToolTip="Back"
            OnClick="btnBack_Click" Style="margin-left: 5px;">
        </telerik:RadButton>
    </div>
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Office2007" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Behaviors="Close" Width="600" Height="400"
                NavigateUrl="ViewDetailDashboard.aspx" OnClientClose="closeRadWindow">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                    <telerik:AjaxUpdatedControl ControlID="RadWindowManager1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cbSalesmen">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" CssClass="RadAjax RadAjax_Vista"
        Transparency="25">
        <div class="raDiv">
        </div>
        <div class="raColor raTransp">
        </div>
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadGrid runat="server" ID="RadGrid1" AutoGenerateColumns="false" AllowPaging="true"
        OnItemDataBound="RadGrid1_ItemDataBound" OnNeedDataSource="RadGrid1_NeedDataSource"
        OnDeleteCommand="RadGrid1_DeleteCommand" Skin="Office2007">
        <MasterTableView DataKeyNames="Id" InsertItemPageIndexAction="ShowItemOnCurrentPage"
            CommandItemDisplay="Top">
            <CommandItemSettings ShowAddNewRecordButton="false" />
            <Columns>
                <telerik:GridBoundColumn DataField="Title" HeaderText="Title" />
                <telerik:GridTemplateColumn HeaderText="Subject">
                    <ItemTemplate>
                        <a href="javascript:void(0);" onclick="openWin(<%# Eval("Id")%>);return false;">
                            <%# Eval("Content")%></a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="IsRead" SortExpression="IsRead" HeaderText="IsRead"
                    Visible="false" UniqueName="IsReadCol" />
                <telerik:GridTemplateColumn HeaderText="View Attachment">
                    <ItemTemplate>
                        <a href="/Upload/Attachments/<%# Eval("AttachedFileName")%>" target="blank">
                            <%# Eval("AttachedFileName") != null && !string.IsNullOrEmpty(Eval("AttachedFileName").ToString()) ? Eval("AttachedFileName") : string.Empty %></a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="CreateDate" HeaderText="Create Date" ReadOnly="true" />
                <telerik:GridButtonColumn ConfirmText="Delete this dashboard?" ConfirmDialogType="RadWindow"
                    ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" />
            </Columns>
            <EditFormSettings>
                <EditColumn ButtonType="ImageButton" />
            </EditFormSettings>
        </MasterTableView>
        <PagerStyle Mode="NextPrevAndNumeric" />
    </telerik:RadGrid>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>
