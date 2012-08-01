﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/AdminFull.master"
    AutoEventWireup="true" CodeFile="SchedulePromotionManagement.aspx.cs" Inherits="Administrator_SchedulePromotionManagement" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Promotion Management</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<style>
        #ctl00_ContentPlaceHolder1_gridSchedulePromotion_ctl00_ctl07_ctl02,#ctl00_ContentPlaceHolder1_gridSchedulePromotion_ctl00_ctl07_ctl03{width:680px}
    </style>--%>
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
                    var txtPhoneNumber = $("input[name*='txtPhoneNumber']");
                    if (txtPhoneNumber) {
                        txtPhoneNumber.val(arg);
                    }
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
            <telerik:RadWindow ID="RadWindow1" runat="server" Behaviors="Close" OnClientClose="OnClientClose"
                Width="850" Height="600" NavigateUrl="DialogPhoneNumber.aspx">
            </telerik:RadWindow>
            <telerik:RadWindow ID="RadWindow2" runat="server" Width="900" Height="600" Modal="true"
                NavigateUrl="PhoneSchedulePromotion.aspx">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <%--<AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="gridSchedulePromotion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridSchedulePromotion" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RadWindowManager1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>--%>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" CssClass="RadAjax RadAjax_Vista"
        Transparency="25">
        <div class="raDiv">
        </div>
        <div class="raColor raTransp">
        </div>
    </telerik:RadAjaxLoadingPanel>
    <div>
        <div style="float: left; width: 50%;">
            <table>
                <tr>
                    <td>
                        From:
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="txtStartDate" runat="server" DateInput-ReadOnly="true">
                            <Calendar ID="CalendarStartDate" runat="server" RangeMinDate="1900-01-01" RangeMaxDate="2050-12-29">
                            </Calendar>
                        </telerik:RadDatePicker>
                    </td>
                    <td>
                        To:
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="txtEndtDate" runat="server" DateInput-ReadOnly="true">
                            <Calendar ID="CalendarEndDate" runat="server" RangeMinDate="1900-01-01" RangeMaxDate="2099-12-29">
                            </Calendar>
                        </telerik:RadDatePicker>
                    </td>
                </tr>
            </table>
        </div>
        <div style="float: right; width: 50%; text-align: right;">
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
        <telerik:RadGrid ID="gridSchedulePromotion" runat="server" AutoGenerateColumns="false"
            AllowPaging="true" OnItemDataBound="gridSchedulePromotion_ItemDataBound" OnNeedDataSource="gridSchedulePromotion_NeedDataSource"
            OnUpdateCommand="gridSchedulePromotion_UpdateCommand" AllowMultiRowSelection="true"
            OnDeleteCommand="gridSchedulePromotion_DeleteCommand" OnInsertCommand="gridSchedulePromotion_InsertCommand"
            PageSize="20" Skin="Office2007" OnCreateColumnEditor="gridSchedulePromotion_CreateColumnEditor"
            OnItemCommand="gridSchedulePromotion_ItemCommand">
            <PagerStyle Mode="NextPrevAndNumeric" />
            <ClientSettings>
                <ClientEvents OnRowDataBound="gridSchedulePromotion_RowDataBound" />
            </ClientSettings>
            <MasterTableView DataKeyNames="Id" ClientDataKeyNames="Id" CommandItemDisplay="Top"
                InsertItemPageIndexAction="ShowItemOnCurrentPage" EditMode="EditForms">
                <Columns>
                    <telerik:GridClientSelectColumn UniqueName="CheckboxSelectColumn" FooterText="CheckBoxSelect footer" />
                    <telerik:GridEditCommandColumn ButtonType="ImageButton" />
                    <telerik:GridBoundColumn DataField="UpiCode" HeaderText="UPI Code" />
                    <telerik:GridTemplateColumn HeaderText="Title">
                        <ItemTemplate>
                            <asp:Literal ID="litTitle" runat="server" Text='<%# Eval("Title")%>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtTitle" runat="server" Width="680px" Text='<%# Eval("Title")%>'></asp:TextBox>
                        </EditItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Title">
                        <ItemTemplate>
                            <asp:Literal runat="server" ID="litTitle" Text='<%# Eval("Title")%>'></asp:Literal>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtTitle" runat="server" ReadOnly="false" Width="680px"></asp:TextBox>
                        </EditItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Phone Numbers">
                        <ItemTemplate>
                            <a href="javascript:void(0);" onclick="OpenPhoneList(<%# Eval("Id")%>);return false;">
                                Phone numbers (<%# Eval("TotalPhoneNumber")%>)</a>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPhoneNumber" runat="server" ReadOnly="false" Width="680px"></asp:TextBox>
                            <asp:HiddenField ID="hdfPhoneList" runat="server" Value='<%# Eval("PhoneNumbers")%>' />
                            <button onclick="openWin(<%# Eval("Id")%>); return false;">
                                Browse</button>
                            <div style="color: Blue; font-weight: bold; display:none" id="litSelectedPhoneTracking">Selected Phone Number Tracking</div>
                            <div id='PromotionHistoryPanel'>
                            </div>
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
                            <asp:TextBox ID="txtSMSContent" runat="server" TextMode="MultiLine" Width="680px"
                                Height="60px" onkeyup="countChar(this)"></asp:TextBox>
                            <div>
                                <div style="float: left">
                                    <h2>
                                        Remaining : <span id="charaterleft">150</span> characters</h2>
                                </div>
                                <div style="float: right">
                                    (You can send 150 characters per sms)</div>
                            </div>
                        </EditItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Web Content" Visible="false">
                        <ItemTemplate>
                            <asp:Literal ID="ltrWebContent" runat="server" Text='<%# Eval("WebContent")%>'></asp:Literal>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Literal ID="hdfWebContent" runat="server" Visible="false" Text='<%# Eval("WebContent")%>'></asp:Literal>
                            <telerik:RadEditor runat="server" ID="RadEditor1" SkinID="DefaultSetOfTools" Height="350px"
                                EditModes="Design" Visible="True">
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
            <telerik:RadButton ID="btnApproveAll" runat="server" Text="Approve Selected" SkinID="Office2007"
                OnClientClicked="ConfirmApprove" OnClick="btnApproveAll_Click">
            </telerik:RadButton>
            <telerik:RadButton ID="btnDeleteSchedule" runat="server" Text="Delete Selected" SkinID="Office2007"
                OnClientClicked="ConfirmDelete" OnClick="btnDeleteSchedule_Click">
            </telerik:RadButton>
        </div>
        <asp:HiddenField ID="hdfPhoneNumbers" runat="server" />
    </asp:Panel>
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
            function openWin(id) {
                if (!id) {
                    id = -1;
                }
                var oWnd = radopen("DialogPhoneNumber.aspx?promoId=" + id, "RadWindow1");
            }
            function OnClientClose(oWnd, args) {
                var arg = args.get_argument();
                if (arg) {
                    var txtPhoneNumber = $("input[name*='txtPhoneNumber']");
                    if (txtPhoneNumber) {
                        var selectedPhones = txtPhoneNumber.val() + ',' + arg[0];
                        txtPhoneNumber.val(selectedPhones);

                        BindPromotionHistory(parseInt(arg[1]));
                    }
                }
            }

            var globalPromoId = 0;
            function BindPromotionHistory(promoId) {
                $("body").css("cursor", "progress");
                globalPromoId = promoId;
                UnitedPharmaService.GetPromotionBrowseHistory(promoId, onSuccess, onFail, null);                
            }

            function onSuccess(result) {
                $("body").css("cursor", "auto");
                var stringHtml = "";

                if (result.Data.length > 0) {
                    $("#litSelectedPhoneTracking").show();
                    $("#PromotionHistoryPanel").show();

                    $.each(result.Data, function () {
                        stringHtml += "<tr><td style='border:1px solid black;'>" + this.SearchCriteriaLiteral + ". Selected phone: <span style='color: blue; font-weight: bold;'>" + this.SearchResutls + "</span></td>";
                        stringHtml += "<td style='border:1px solid black; vertical-align: middle'><a ";
                        stringHtml += " href=\"JavaScript:void(0);\" onclick=\"removeHistory('" + this.Id + "', " + globalPromoId + ");\">";
                        stringHtml += "<span style='font-size: 20px; font-weight: bold'>x</span></a></td></tr>";
                    });
                    $("#PromotionHistoryPanel").html("<table style='border:1px solid black;'>" + stringHtml + "</table>");
                }
                else {
                    $("#litSelectedPhoneTracking").hide();
                    $("#PromotionHistoryPanel").html();
                    $("#PromotionHistoryPanel").hide();
                }
                
            }

            function onFail(result) {
                $("body").css("cursor", "auto");
                globalPromoId = 0;
                alert("Can not get histories of this promotion");
            }

            function removeHistory(historyId, promoId) {
                $("body").css("cursor", "progress");              
                UnitedPharmaService.RemovePromotionHistory(historyId, promoId, onRemovingSuccess, onRemovingFail, null);
            }

            function onRemovingSuccess(result) {
                $("body").css("cursor", "auto");
                if (result.Status == 0) {

                    $("#PromotionHistoryPanel").html();
                    //RemoveDivHtml($("#PromotionHistoryPanel"));
                    //$("#PromotionHistoryPanel").empty();
                    
                    BindPromotionHistory(globalPromoId);

                    var phoneNumberObject = $("input[name*='txtPhoneNumber']");
                    if (phoneNumberObject) {
                        var phoneVal = phoneNumberObject.val();
                        phoneVal = phoneVal.replace(',' + result.Data, '');
                        phoneVal = phoneVal.replace(result.Data + ',', '');
                        phoneVal = phoneVal.replace(result.Data, '');
                        phoneNumberObject.val(phoneVal);
                    }
                } else {
                    globalPromoId = 0;
                    alert("Can not remove this history of this promotion");
                }
            }

            function onRemovingFail(result) {
                $("body").css("cursor", "auto");
                globalPromoId = 0;
                alert("Can not remove this history of this promotion");
            }

            function OpenPhoneList(Id) {
                var oWnd2 = radopen("PhoneSchedulePromotion.aspx?ID=" + Id, "RadWindow2");
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

            function gridSchedulePromotion_RowDataBound(sender, args) {
                alert('Dzo dzo');
            }

            function RemoveDivHtml(divObject) {
                divObject.html().replace(/<\/?[br|li|ol|ul]+\/?>/igm, '');
            }

            -->
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
