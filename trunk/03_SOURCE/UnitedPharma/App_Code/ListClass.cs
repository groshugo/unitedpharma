using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

/// <summary>
/// Summary description for ListClass
/// </summary>
public class ListClass
{
	public ListClass()
	{
		
	}
}

public class vwRegion
{
    public int Id { get; set; }
    public string UpiCode { get; set; }
    public string RegionName { get; set; }
    public string Description { get; set; }
    public int GroupId { get; set; }
    public string GroupName { get; set; }
}

public class vwArea
{
    public int Id { get; set; }
    public string UpiCode { get; set; }
    public string AreaName { get; set; }
    public string Description { get; set; }
    public int RegionId { get; set; }
    public string RegionName { get; set; }
    public int GroupId { get; set; }
    public string GroupName { get; set; }
}

public class vwCustomer
{
    public int Id { get; set; }
    public string UpiCode { get; set; }
    public string FullName { get; set; }
    public string Address { get; set; }
    public string Street { get; set; }
    public string Ward { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public int? CustomerTypeId { get; set; }
    public string CustomerTypeName { get; set; }
    public int? ChannelId { get; set; }
    public string ChannelName { get; set; }
    public int SectionId { get; set; }
    public int ProvinceId { get; set; }
    public int? DistrictId { get; set; }
    public string DistrictName { get; set; }
    public int GroupId { get; set; }
    public int RegionId { get; set; }
    public int AreaId { get; set; }    
    public int? LocalId { get; set; }
    public string LocalName { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool? Status { get; set; }
    public bool? IsEnable { get; set; }
    public string NoteOfSalesmen { get; set; }
    public string SupervisorName { get; set; }
    
    public vwCustomer()
	{		
	}
    public vwCustomer(string strUPICode, string strFullName, string strAddress, string strStreet, string strWard, string strPhone)
    {
        UpiCode = strUPICode;
        FullName = strFullName;
        Address = strAddress;
        Street = strStreet;
        Ward = strWard;
        Phone = strPhone;
    }
}

public class vwCustomerLog
{
    public int Id { get; set; }
    public string UpiCode { get; set; }
    public string FullName { get; set; }
    public string Address { get; set; }
    public string Street { get; set; }
    public string Ward { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public int? CustomerTypeId { get; set; }
    public string CustomerTypeName { get; set; }
    public int? ChannelId { get; set; }
    public string ChannelName { get; set; }
    public int SectionId { get; set; }
    public int ProvinceId { get; set; }
    public int? DistrictId { get; set; }
    public string DistrictName { get; set; }
    public int GroupId { get; set; }
    public int RegionId { get; set; }
    public int AreaId { get; set; }
    public int? LocalId { get; set; }
    public string LocalName { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool? Status { get; set; }
    public bool? IsApprove { get; set; }
    public int? ApproveBy { get; set; }
    public int? ChangeBy { get; set; }
    public int? CustomerId { get; set; }
    public string NoteOfSalesmen { get; set; }

    public vwCustomerLog()
    {
    }
    public vwCustomerLog(string _UpiCode, string _FullName, string _Address, string _Street, string _Ward, string _Phone, int _CustomerTypeId, int _ChannelId, int _SectionId,
        int _ProvinceId, int _DistrictId, int _GroupId,int _RegionId, int _AreaId,int _LocalId, DateTime _CreateDate,DateTime _UpdateDate,bool _Status,bool _IsApprove,int _ApproveBy,
        int _ChangeBy, int _CustomerId)
    {
        UpiCode = _UpiCode;
        FullName = _FullName;
        Address = _Address;
        Street = _Street;
        Ward = _Ward;
        Phone = _Phone;
        CustomerTypeId = _CustomerTypeId;
        ChannelId = _ChannelId;
        SectionId = _SectionId;
        ProvinceId = _ProvinceId;
        DistrictId = _DistrictId;
        GroupId = _GroupId;
        RegionId = _RegionId;
        AreaId = _AreaId;
        LocalId = _LocalId;
        CreateDate = _CreateDate;
        UpdateDate = _UpdateDate;
        Status = _Status;
        IsApprove = _IsApprove;
        ApproveBy = _ApproveBy;
        ChangeBy = _ChangeBy;
        CustomerId = _CustomerId;
    }
}

public class vwLocal
{
    public int Id { get; set; }
    public string UpiCode { get; set; }
    public string LocalName { get; set; }
    public string Description { get; set; }
    public int GroupId { get; set; }
    public string GroupName { get; set; }
    public int RegionId { get; set; }
    public string RegionName { get; set; }
    public int AreaId { get; set; }
    public string AreaName { get; set; }
}

public class vwProvince
{
    public int Id { get; set; }    
    public string ProvinceName { get; set; }
    public int SectionId { get; set; }
    public string SectionName { get; set; }
}

public class vwDistrict
{
    public int Id { get; set; }
    public string DistrictName { get; set; }
    public int SectionId { get; set; }
    public string SectionName { get; set; }
    public int ProvinceId { get; set; }
    public string ProvinceName { get; set; }
}

public class vwChannel
{    
    public int Id { get; set; }
    public string UpiCode { get; set; }
    public string ChannelName { get; set; }
    public int ParentChannelId { get; set; }
    public string ParentChannelName { get; set; }
}

public class cbChannel
{
    public int Id { get; set; }    
    public string ChannelName { get; set; }    
}

public class vwFunction
{
    public int Id { get; set; }
    public string FunctionName { get; set; }
    public int ParentFunctionId { get; set; }
    public string ParentFunctionName { get; set; }
    public string Action { get; set; }
}

public class cbFunction
{
    public int Id { get; set; }
    public string FunctionName { get; set; }
}

public class vwRole
{
    public int Id { get; set; }
    public string RoleName { get; set; }
}

public class vwAssignFunction
{
    public int Id { get; set; }
    public int FunctionId { get; set; }
    public string FunctionName { get; set; }
    public int? AdministratorId { get; set; }
    public string AdministratorName { get; set; }    
}

public class vwSMSQuota
{
    public int Id { get; set; }
    public int Quota { get; set; }
    public int? Used { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? ExpiredDate { get; set; }
    public int? SalemenId { get; set; }
    public string SalemenName { get; set; }
}

public class vwCustomerSupervior
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Address { get; set; }
    public string Street { get; set; }
    public string Ward { get; set; }
    public string Phone { get; set; }
    public int? CustomerId { get; set; }
    public string CustomerName { get; set; }
    public int? DistrictId { get; set; }
    public string DistrictName { get; set; }
    public int SectionId { get; set; }
    public int ProvinceId { get; set; }
    public int? CustomerTypeId { get; set; }
    public int? PositionId { get; set; }
    public string PositionName { get; set; }
}

public class vwSalemen
{
    public int Id { get; set; }
    public string UpiCode { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public int? RoleId { get; set; }
    public string RoleName { get; set; }
    public int? SmsQuota { get; set; }
    public int? SmsUsed { get; set; }
    public DateTime? ExpiredDate { get; set; }
}

public class vwSupervisorManageCustomer
{
    public int Id { get; set; }    
    public string CustomerName { get; set; }
    public int? CustomerId { get; set; }
    public int? CustomerTypeId { get; set; }
    public int? SupervisorId { get; set; }
    public string SupervisorName { get; set; }    
}

public class vwSMS
{
    public int Id { get; set; }
    public string SMSCode { get; set; }
    public int? SmsParentId { get; set; }
    public int? CountParentSMS { get; set; }
    public int SenderId { get; set; }
    public int? SenderType { get; set; }
    public string SenderName { get; set; }
    public string SenderPhone { get; set; }
    public int ReceiverId { get; set; }
    public int? ReceiverType { get; set; }
    public string ReceiverName { get; set; }
    public string ReceiverPhone { get; set; }
    public DateTime? Date { get; set; }
    public string strDate { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public bool IsSendSuccess { get; set; }
    public bool IsRead { get; set; }
    public bool IsDelete { get; set; }
    public int? SmsTypeId { get; set; }
    public int? PromotionId { get; set; }    
}

public class vwContact
{
    public int Id { get; set; }
    public string FullName {get;set;}
    public string Phone { get; set; }
    public int Type { get; set; }
}

public class vwSchedulePromotion
{
    public int Id { get; set; }
    public string UPICode { get; set; }
    public string Title { get; set; }
    public string SMSContent { get; set; }
    public string WebContent { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? AdministratorId { get; set; }
    public string AdministratorName { get; set;}
    public bool? IsApprove { get; set; }
    public string PhoneNumbers { get; set; }
    public int TotalPhoneNumber { get; set; }
}

public class vwSchedulePhoneNumbers
{
    public int Id { get; set; }
    public string UPICode { get; set; }
    public string Phone { get; set; }
    public string SupervisorName { get; set; }
    public string PositionName { get; set; }
}

public class vwSalesGroup
{
    public int Id { get; set; }
    public int SalesmenId { get; set; }
    public int GroupId { get; set; }
    public string GroupName { get; set; }
}

public class vwSalesRegion
{
    public int Id { get; set; }
    public int SalesmenId { get; set; }
    public int RegionId { get; set; }
    public string RegionName { get; set; }
}

public class vwSalesArea
{
    public int Id { get; set; }
    public int SalesmenId { get; set; }
    public int AreaId { get; set; }
    public string AreaName { get; set; }
}

public class vwSalesLocal
{
    public int Id { get; set; }
    public int SalesmenId { get; set; }
    public int LocalId { get; set; }
    public string LocalName { get; set; }
}

public class vwDashboard
{
    public string Phone { get; set; }
    public string FullName { get; set; }
    public string Role { get; set; }
}
public class vwMasterList
{
    public string Salesmen1 { get; set; }
    public string Salesmen2 { get; set; }
    public string Salesmen3 { get; set; }
    public string Region1 { get; set; }
    public string Region2 { get; set; }    
    public string Area1 { get; set; }
    public string Area2 { get; set; }
    public string Local1 { get; set; }
    public string Local2 { get; set; }
    public string Upicode { get; set; }    
    public string Customername { get; set; }
    public string Customeraddress { get; set; }
    public string Group { get; set; }
    public string Region { get; set; }
    public string Area { get; set; }    
    public string Local { get; set; }
    public string Channel1 { get; set; }
    public string Channel2 { get; set; }
    public string Channel3 { get; set; }
    public vwMasterList()
    {
    }
    public vwMasterList(string salesmen1, string salesmen2, string salesmen3, string region1, string region2, string area1, string area2, string local1, string local2,
        string upicode,string customername,string customeraddress,string group,string region,string area,string local,string channel1,string channel2,string channel3)
    {
        Salesmen1 = salesmen1;
        Salesmen2 = salesmen2;
        Salesmen3 = salesmen3;
        Region1 = region1;
        Region2 = region2;
        Area1 = area1;        
        Area2 = area2;
        Local1 = local1;
        Local2 = local2;
        Upicode = upicode;
        Customername = customername;
        Customeraddress = customeraddress;
        Group = group;        
        Region = region;
        Area = area;
        Local = local;
        Channel1 = channel1;
        Channel2 = channel2;
        Channel3 = channel3;
    }
}
[Serializable]
public class ObjLogin
{
    public int Id { get; set; }    
    public string Phone { get; set; }
    public string Fullname { get; set; }
    public bool? AllowApprove { get; set; }
}
public static class Helper
{
    public static T Cast<T>(this Object myobj)
    {
        Type objectType = myobj.GetType();
        Type target = typeof(T);
        var x = Activator.CreateInstance(target, false);
        var z = from source in objectType.GetMembers().ToList() where source.MemberType == MemberTypes.Property select source;
        var d = from source in target.GetMembers().ToList() where source.MemberType == MemberTypes.Property select source;
        List<MemberInfo> members = d.Where(memberInfo => d.Select(c => c.Name).ToList().Contains(memberInfo.Name)).ToList();
        PropertyInfo propertyInfo;
        object value;
        foreach (var memberInfo in members)
        {
            propertyInfo = typeof(T).GetProperty(memberInfo.Name);
            value = myobj.GetType().GetProperty(memberInfo.Name).GetValue(myobj, null);

            propertyInfo.SetValue(x, value, null);
        }
        return (T)x;
    }
}