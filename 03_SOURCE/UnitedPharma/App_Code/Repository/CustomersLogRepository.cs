using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CustomersRepository
/// </summary>
public class CustomersLogRepository
{
    private UPIDataContext db;
    public CustomersLogRepository()
	{
        db = new UPIDataContext();
	}
    public List<Customer> GetAllCustomers()
    {
        return (from e in db.Customers select e).ToList();
    }

    public List<vwCustomerLog> GetAllViewCustomerLog(int CustomerId)
    {
        return (from a in db.CustomerLogs where a.CustomerId==CustomerId
                select new vwCustomerLog
                { 
            Id = a.Id, 
            UpiCode = a.UpiCode, 
            FullName = a.FullName, 
            Address = a.Address, 
            Street = a.Street, 
            Ward = a.Ward,
            Phone=a.Phone,
            Password=a.Password,
            CustomerTypeId=a.CustomerTypeId,
            CustomerTypeName=a.CustomerType.TypeName,
            ChannelId=a.ChannelId,
            ChannelName=a.Channel.ChannelName,
            DistrictId=a.DistrictId,
            DistrictName=a.District.DistrictName,
            LocalId=a.LocalId,
            LocalName=a.Local.LocalName,
            CreateDate=a.CreateDate,
            UpdateDate=a.UpdateDate,
            Status=a.Status,
            IsApprove=a.IsApprove,
            ApproveBy=a.ApproveBy,
            ChangeBy=a.ChangeBy,
            CustomerId=a.CustomerId,
            NoteOfSalesmen = a.NoteOfSalesmen
                }).OrderByDescending(i => i.Id).ToList();
    }
    public CustomerLog GetLogByPhoneNumber(string phone)
    {
        return (from e in db.CustomerLogs where e.Phone == phone select e).SingleOrDefault();
    }
    public Customer GetCustomerById(int id)
    {
        return (from e in db.Customers where e.Id == id select e).SingleOrDefault();
    }
    public List<CustomerLog> GetLogByCustomerId(int CustomerId)
    {
        return (from a in db.CustomerLogs
                where a.CustomerId == CustomerId 
                select a).ToList();
    }
    public bool InsertCustomer(string UPICode, string FullName, string Address, string Street, string Ward, string Phone, string Password, int CustomerTypeId,
        int ChannelId, int DistrictId, int LocalId, DateTime CreateDate, DateTime UpdateDate, bool Status, int CustomerId, bool IsApprove, int APPROVED_BY, int changeBy,
        string noteOfSalesmen)
    {
        try
        {
            CustomerLog o = new CustomerLog();
            o.UpiCode = UPICode;
            o.FullName = FullName;
            o.Address = Address;
            o.Street = Street;
            o.Ward = Ward;
            o.Phone = Phone;
            o.Password = Password;
            o.CustomerTypeId = CustomerTypeId;
            o.ChannelId = ChannelId;
            o.DistrictId = DistrictId;
            o.LocalId = LocalId;
            o.CreateDate = CreateDate;
            o.UpdateDate = UpdateDate;
            o.Status = Status;
            o.CustomerId = CustomerId;
            o.IsApprove = IsApprove;
            o.ApproveBy = APPROVED_BY;
            o.ChangeBy = changeBy;
            o.NoteOfSalesmen = noteOfSalesmen;
            db.CustomerLogs.InsertOnSubmit(o);
            db.SubmitChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool DeleteCustomerById(int id)
    {
        var o = (from e in db.Customers where e.Id == id select e).SingleOrDefault();
        if (o != null)
        {
            try
            {
                db.Customers.DeleteOnSubmit(o);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        else
            return false;
    }
    public bool DeleteCustomerLogById(int customerLogId)
    {
        var e = from o in db.CustomerLogs where o.Id == customerLogId select o;
        if (e != null)
        {
            try
            {
                db.CustomerLogs.DeleteAllOnSubmit(e);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        else
            return false;
    }

    public List<vwCustomerLog> FilterCustomers(string upiCode, string fullname)
    {
        return (from a in db.CustomerLogs
                where (upiCode == string.Empty || a.UpiCode.Contains(upiCode))
                                        && (fullname == string.Empty || a.FullName.Contains(fullname)) 
                select new vwCustomerLog
                {
                    Id = a.Id,
                    UpiCode = a.UpiCode,
                    FullName = a.FullName,
                    Address = a.Address,
                    Street = a.Street,
                    Ward = a.Ward,
                    Phone = a.Phone,
                    Password = a.Password,
                    CustomerTypeId = a.CustomerTypeId,
                    CustomerTypeName = a.CustomerType.TypeName,
                    ChannelId = a.ChannelId,
                    ChannelName = a.Channel.ChannelName,
                    DistrictId = a.DistrictId,
                    DistrictName = a.District.DistrictName,
                    LocalId = a.LocalId,
                    LocalName = a.Local.LocalName,
                    CreateDate = a.CreateDate,
                    UpdateDate = a.UpdateDate,
                    Status = a.Status,
                    IsApprove = a.IsApprove,
                    ApproveBy = a.ApproveBy,
                    ChangeBy = a.ChangeBy,
                    CustomerId = a.CustomerId,
                    NoteOfSalesmen = a.NoteOfSalesmen
                }).OrderByDescending(i => i.Id).ToList();
    }
}