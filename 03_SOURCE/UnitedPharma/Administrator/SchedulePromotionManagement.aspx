<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/AdminFull.master" AutoEventWireup="true" CodeFile="SchedulePromotionManagement.aspx.cs" 
Inherits="Administrator_SchedulePromotionManagement" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Promotion Management</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_gridSchedulePromotion_ctl00_ctl07_ctl02,#ctl00_ContentPlaceHolder1_gridSchedulePromotion_ctl00_ctl07_ctl03{width:680px}
    </style>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            <!--
            $(document).ready(function () {
                var txtSMSContent = $("input[name*='txtSMSContent']"); //$("#ctl00_ContentPlaceHolder1_gridSchedulePromotion_ctl00_ctl07_txtSMSContent");
                if (txtSMSContent != null && txtSMSContent.val() != null) {
                    var len = txtSMSContent.val().length;
                    if (len > 150) {
                        val.value = val.value.substring(0, 150);
                    } else {
                        $('#charaterleft').text(150 - len);
                    }    
                }
            });
            function countChar(val) {
                var len = val.value.length;
                if (len > 150) {
                    val.value = val.value.substring(0, 150);
                } else {
                    $('#charaterleft').text(150 - len);
                }
            };
            function openWin() {
                var oWnd = radopen("DialogPhoneNumber.aspx", "RadWindow1");
            }
            function OnClientClose(oWnd, args) {
                var arg = args.get_argument();
                if (arg) {
                    var Phone = "";
                    for (var i = 0; i < arg.length; i++) {
                        Phone += arg[i] + ",";
                    }
                    Phone = Phone.substring(0, Phone.length - 1);

                    var txtPhoneNumber = $("input[name*='txtPhoneNumber']");
                    if (txtPhoneNumber != null) {
                        txtPhoneNumber.val(Phone);
                    }
                    var hdfPhoneNumbers = $("input[name*='hdfPhoneNumbers']");
                    if (hdfPhoneNumbers)
                        hdfPhoneNumbers.val(Phone);
                    
                }
            }
            function OpenPhoneList(Id) {
                var oWnd2 = radopen("PhoneSchedulePromotion.aspx?ID="+Id, "RadWindow2");
            }
            var fireConfirm = true;
            function ConfirmApprove(button, args) {
                if ($find("<%= gridSchedulePromotion.MasterTableView.ClientID %>").get_selectedItems().length > 0) {
                    var callBackFunction = Function.createDelegate(button, function (argument) {
                        if (fireConfirm && argument) {
                            this.set_autoPostBack(true);
                            fireConfirm = false;
                            this.click();
                        }
                    });
                    if (fireConfirm) {
                        button.set_autoPostBack(false);
                        var text = "Are you sure to approve?";
                        var result = radconfirm(text, callBackFunction, 300, 100, null, "Confirm");
                    }
                }
                else {
                    alert("Please choose promotion to approve");
                    button.set_autoPostBack(false);
                }
            }
            var Confirm = true;
            function ConfirmDelete(button, args) {
                if ($find("<%= gridSchedulePromotion.MasterTableView.ClientID %>").get_selectedItems().length > 0) {
                    var callBackFunction = Function.createDelegate(button, function (argument) {
                        if (fireConfirm && argument) {
                            this.set_autoPostBack(true);
                            Confirm = false;
                            this.click();
                        }
                    });
                    if (Confirm) {
                        button.set_autoPostBack(false);
                        var text = "Are you sure to delete?";
                        var result = radconfirm(text, callBackFunction, 300, 100, null, "Confirm");
                    }
                }
                else {
                    alert("Please choose promotion to delete");
                    button.set_autoPostBack(false);
                }
            }
            var tableView = null;
            function pageLoad(sender, args) {
                tableView = $find("<%= gridSchedulePromotion.ClientID %>").get_masterTableView();
            }

            function RadComboBox1_SelectedIndexChanged(sender, args) {
                tableView.set_pageSize(sender.get_value());
            }

            function changePage(argument) {
                tableView.page(argument);
            }
            -->
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadFormDecorator ID="FormDecorator1" runat="server" DecoratedControls="all" Skin="Office2007"></telerik:RadFormDecorator>
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Office2007" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Behaviors="Close" OnClientClose="OnClientClose" Width="850" Height="600"
                NavigateUrl="DialogPhoneNumber.aspx">
            </telerik:RadWindow>
            <telerik:RadWindow ID="RadWindow2" runat="server" Width="900" Height="600" Modal="true"
                NavigateUrl="PhoneSchedulePromotion.aspx">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1" >
        <%--<AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="gridSchedulePromotion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridSchedulePromotion" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RadWindowManager1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>--%>        
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" CssClass="RadAjax RadAjax_Vista" Transparency="25">
        <div class="raDiv">
        </div>
        <div class="raColor raTransp">
        </div>
    </telerik:RadAjaxLoadingPanel>
    <div>
        <div style="float:left; width:50%;">
            <table>
                <tr>
                    <td>From: </td>
                    <td>
                        <telerik:RadDatePicker ID="txtStartDate" runat="server" DateInput-ReadOnly="true">
                            <Calendar ID="CalendarStartDate" runat="server" RangeMinDate="1900-01-01" RangeMaxDate="2050-12-29">
                            </Calendar>
                        </telerik:RadDatePicker>
                    </td>
                    <td>To: </td>
                    <td>
                        <telerik:RadDatePicker ID="txtEndtDate" runat="server" DateInput-ReadOnly="true">
                            <Calendar ID="CalendarEndDate" runat="server" RangeMinDate="1900-01-01" RangeMaxDate="2099-12-29">
                            </Calendar>
                        </telerik:RadDatePicker>
                    </td>
                </tr>
            </table>
        </div>
        <div style="float:right; width:50%; text-align:right;">
            Approved 
            <telerik:RadComboBox ID="ddlFilters" runat="server">
                <Items>
                    <telerik:RadComboBoxItem Text="Show All" Value="2" Selected="true" />
                    <telerik:RadComboBoxItem Text="Approved" Value="1" />
                    <telerik:RadComboBoxItem Text="Not Approved" Value="0" />
                </Items>
            </telerik:RadComboBox>
            <asp:Button ID="btnFilter" runat="server" Text="Filter" OnClick="btnFilter_Click" />
            <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
        </div>
    </div>
    <asp:Panel ID="RadPanel1" runat="server">
        <telerik:RadGrid ID="gridSchedulePromotion" runat="server" AutoGenerateColumns="false" AllowPaging="true" OnItemDataBound="gridSchedulePromotion_ItemDataBound"
        OnNeedDataSource="gridSchedulePromotion_NeedDataSource" OnUpdateCommand="gridSchedulePromotion_UpdateCommand" AllowMultiRowSelection="true" 
        OnDeleteCommand="gridSchedulePromotion_DeleteCommand" OnInsertCommand="gridSchedulePromotion_InsertCommand" PageSize="20"
        Skin="Office2007"  OnCreateColumnEditor="gridSchedulePromotion_CreateColumnEditor" OnItemCommand="gridSchedulePromotion_ItemCommand">
            <PagerStyle Mode="NextPrevAndNumeric" />
            <MasterTableView DataKeyNames="Id" ClientDataKeyNames="Id" CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnCurrentPage" EditMode="EditForms">
                <Columns>
                    <telerik:GridClientSelectColumn UniqueName="CheckboxSelectColumn" FooterText="CheckBoxSelect footer" />
                    <telerik:GridEditCommandColumn ButtonType="ImageButton" />
                    <telerik:GridBoundColumn DataField="UpiCode" HeaderText="UPI Code" />
                    <telerik:GridBoundColumn DataField="Title" HeaderText="Title" />
                    <telerik:GridTemplateColumn HeaderText="Phone Numbers">
                        <ItemTemplate>
                            <a href="javascript:void(0);" onclick="OpenPhoneList(<%# Eval("Id")%>);return false;">Phone numbers (<%# Eval("TotalPhoneNumber")%>)</a>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <telerik:RadTextBox ID="txtPhoneNumber" runat="server" ReadOnly="true" Width="608px"></telerik:RadTextBox>
                            <asp:HiddenField ID="hdfPhoneList" runat="server" Value='<%# Eval("PhoneNumbers")%>' />
                            <button onclick="openWin(); return false;">Browse</button>
                        </EditItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Total of fail">
                        <ItemTemplate>
                            <%# TotalFailPhone(Eval("Id").ToString(),false)%>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Total of Success">
                        <ItemTemplate>
                            <%# TotalFailPhone(Eval("Id").ToString(),true)%>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="SMS Content" Visible="false">
                        <ItemTemplate>
                            <asp:Literal ID="ltrSMSContent" runat="server" Text='<%# Eval("SMSContent")%>'></asp:Literal>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Literal ID="hdfSMSContent" runat="server" Text='<%# Eval("SMSContent")%>' Visible="false"></asp:Literal>
                            <asp:TextBox ID="txtSMSContent" runat="server" TextMode="MultiLine" Width="680px" Height="60px" onkeyup="countChar(this)"></asp:TextBox>
                            <div>
                                <div style="float:left">
                                    <h2>Remaining : <span id="charaterleft">150</span> characters</h2>
                                </div>
                                <div style="float:right">(You can send 150 characters per sms)</div>
                            </div>
                        </EditItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Web Content" Visible="false">
                        <ItemTemplate>
                            <asp:Literal ID="ltrWebContent" runat="server" Text='<%# Eval("WebContent")%>'></asp:Literal>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Literal ID="hdfWebContent" runat="server" Visible="false" Text='<%# Eval("WebContent")%>'></asp:Literal>
                            <telerik:RadEditor runat="server" ID="RadEditor1" SkinID="DefaultSetOfTools" Height="350px" EditModes="Design" Visible="True">
                            </telerik:RadEditor>
                        </EditItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Start date">
                        <ItemTemplate>
                            <asp:Label ID="lblStartDate" runat="server" Text='<%# string.Format("{0:d}",Eval("StartDate")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:HiddenField ID="hdfStartDate" runat="server" Value='<%# String.Format("{0:d}", Eval("StartDate")) %>' />
                            <telerik:RadDatePicker ID="txtStartDate" runat="server" DateInput-ReadOnly="true">
                                <Calendar ID="CalendarStartDate" runat="server" RangeMinDate="1900-01-01" RangeMaxDate="2050-12-29">
                                </Calendar>
                            </telerik:RadDatePicker>
                        </EditItemTemplate>
                    </telerik:GridTemplateColumn>                    
                    <telerik:GridTemplateColumn HeaderText="End date">
                        <ItemTemplate>
                            <asp:Label ID="lblEndDate" runat="server" Text='<%# string.Format("{0:d}",Eval("EndDate")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:HiddenField ID="hdfEndDate" runat="server" Value='<%# String.Format("{0:d}", Eval("EndDate")) %>' />
                            <telerik:RadDatePicker ID="txtEndDate" runat="server" DateInput-ReadOnly="true">
                                <Calendar ID="CalendarEndDate" runat="server" RangeMinDate="1900-01-01" RangeMaxDate="2050-12-29">
                                </Calendar>
                            </telerik:RadDatePicker>
                        </EditItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Administrator" UniqueName="AdministratorName">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblAdministratorName" Text='<%# Eval("AdministratorName") %>'></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Approved" UniqueName="IsApprove" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# ((bool)Eval("IsApprove")==true) ? "<img src='Images/Checked.png' alt='Approved' />" : "" %>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn>
                        <ItemTemplate>
                            <a href='DetailSchedulePromotionManagement.aspx?SID=<%# Eval("Id")%>'>Detail</a>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
                <EditFormSettings>
                    <EditColumn ButtonType="ImageButton" />
                </EditFormSettings>
                <PagerStyle Mode="NumericPages" PageButtonCount="10" />
            </MasterTableView>
            <ClientSettings>
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <PagerStyle Mode="NextPrevAndNumeric" />
        </telerik:RadGrid>
        <div>
            <telerik:RadButton ID="btnApproveAll" runat="server" Text="Approve Selected" SkinID="Office2007" OnClientClicked="ConfirmApprove" OnClick="btnApproveAll_Click"></telerik:RadButton>
            <telerik:RadButton ID="btnDeleteSchedule" runat="server" Text="Delete Selected" SkinID="Office2007" OnClientClicked="ConfirmDelete" OnClick="btnDeleteSchedule_Click"></telerik:RadButton>
        </div>
        <asp:HiddenField ID="hdfPhoneNumbers" runat="server" />
    </asp:Panel>
</asp:Content>

