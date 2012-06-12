<%@ Page Title="Show Promotion" Language="C#" MasterPageFile="~/Salemans/MasterPage.master"
    AutoEventWireup="true" CodeFile="ShowPromotion.aspx.cs" Inherits="Salemans_ShowPromotion" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentCommand" runat="Server">    
    <script type="text/javascript">
        function centerUpdatePanel() {
            centerElementOnScreen(document.getElementById("LoadingPanel1"));
        }
        function centerElementOnScreen(element) {
            var scrollTop = document.body.scrollTop;
            var scrollLeft = document.body.scrollLeft;
            var viewPortHeight = document.body.clientHeight;
            var viewPortWidth = document.body.clientWidth;
            if (document.compatMode == "CSS1Compat") {
                viewPortHeight = document.documentElement.clientHeight;
                viewPortWidth = document.documentElement.clientWidth;
                scrollTop = document.documentElement.scrollTop;
                scrollLeft = document.documentElement.scrollLeft;
            }
            var topOffset = Math.ceil(viewPortHeight / 2 - element.offsetHeight / 2);
            var leftOffset = Math.ceil(viewPortWidth / 2 - element.offsetWidth / 2);
            var top = scrollTop + topOffset - 40;
            var left = scrollLeft + leftOffset - 70;
            element.style.position = "absolute";
            element.style.top = top + "px";
            element.style.left = left + "px";
        }   
    </script>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            <!--
            function openWin(id) {
                var oWnd = radopen("DetailPromotion.aspx?id=" + id, "RadWindow2");
            }
            -->
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="RadWindow1" runat="server">
    </telerik:RadWindow>
    <h3 style="color: #000;">
        Show Promotion
    </h3>
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Office2007" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="RadWindow2" runat="server" Behaviors="Close" Width="800" Height="600"
                NavigateUrl="DetailPromotion.aspx">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="50"
        OnNeedDataSource="RadGrid1_NeedDataSource" OnSelectedIndexChanged="RadGrid1_SelectedIndexChanged" Skin="Office2007">
        <MasterTableView DataKeyNames="Id" >
            <Columns>
                <telerik:GridBoundColumn DataField="Title" HeaderText="Title" />
                <telerik:GridTemplateColumn HeaderText="Start date">
                    <ItemTemplate>
                        <asp:Label ID="lblStartDate" runat="server" Text='<%# string.Format("{0:d}",Eval("StartDate")) %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="End date">
                    <ItemTemplate>
                        <asp:Label ID="lblEndDate" runat="server" Text='<%# string.Format("{0:d}",Eval("EndDate")) %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Approved">
                    <ItemTemplate>
                        <asp:Label ID="lblApproved" runat="server" Text='<%# Eval("IsApprove") != null && Eval("IsApprove").ToString() == "True" ? "Yes" : "No"  %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>           
                <%--<telerik:GridButtonColumn UniqueName="SelectColumn" Text="Detail" CommandName="Select">
                </telerik:GridButtonColumn>--%>
                <telerik:GridTemplateColumn>
                    <ItemTemplate>
                        <a href="javascript:void(0);" onclick="openWin(<%# Eval("Id")%>);return false;">View Details</a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                    <telerik:AjaxUpdatedControl ControlID="RadWindow1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>
