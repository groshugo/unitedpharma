using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CustomersRepository
/// </summary>
public class CustomersRepository
{
    private UPIDataContext db;
    public CustomersRepository()
    {
        db = new UPIDataContext();
    }
    public List<Customer> GetAllCustomers()
    {
        return (from e in db.Customers select e).ToList();
    }

    public List<vwCustomer> GetAllViewCustomers()
    {
        var viewAllProerties = (from c in db.Customers where c.IsEnable==true
                                select new vwCustomer
                                {
                                    Id = c.Id,
                                    UpiCode = c.UpiCode,
                                    FullName = c.FullName,
                                    Address = c.Address,
                                    Street = c.Street,
                                    Ward = c.Ward,
                                    Phone = c.Phone,
                                    Password = c.Password,
                                    CustomerTypeId = c.CustomerTypeId,
                                    CustomerTypeName = c.CustomerType.TypeName,
                                    ChannelId = c.ChannelId,
                                    ChannelName = c.Channel.ChannelName,
                                    DistrictId = c.DistrictId,
                                    DistrictName = c.District.DistrictName,
                                    LocalId = c.LocalId,
                                    LocalName = c.Local.LocalName,
                                    GroupId = c.Local.Area.Region.GroupId,
                                    RegionId = c.Local.Area.RegionId,
                                    AreaId = c.Local.AreaId,
                                    CreateDate = c.CreateDate,
                                    UpdateDate = c.UpdateDate,
                                    Status = c.Status,
                                    ProvinceId = c.District.ProvinceId,
                                    SectionId = c.District.Province.SectionId,
                                    NoteOfSalesmen = c.NoteOfSalesmen
                                }).ToList();
        return viewAllProerties;
    }
    public vwCustomer GetCustomersByID(int ID)
    {
        var viewAllProerties = (from c in db.Customers
                                where c.Id == ID
                                select new vwCustomer
                                {
                                    Id = c.Id,
                                    UpiCode = c.UpiCode,
                                    FullName = c.FullName,
                                    Address = c.Address,
                                    Street = c.Street,
                                    Ward = c.Ward,
                                    Phone = c.Phone,
                                    Password = c.Password,
                                    CustomerTypeId = c.CustomerTypeId,
                                    CustomerTypeName = c.CustomerType.TypeName,
                                    ChannelId = c.ChannelId,
                                    ChannelName = c.Channel.ChannelName,
                                    DistrictId = c.DistrictId,
                                    DistrictName = c.District.DistrictName,
                                    LocalId = c.LocalId,
                                    LocalName = c.Local.LocalName,
                                    GroupId = c.Local.Area.Region.GroupId,
                                    RegionId = c.Local.Area.RegionId,
                                    AreaId = c.Local.AreaId,
                                    CreateDate = c.CreateDate,
                                    UpdateDate = c.UpdateDate,
                                    Status = c.Status,
                                    ProvinceId = c.District.ProvinceId,
                                    SectionId = c.District.Province.SectionId,
                                    NoteOfSalesmen = c.NoteOfSalesmen
                                }).SingleOrDefault();
        return viewAllProerties;
    }

    public Customer GetCustomerById(int id)
    {
        return (from e in db.Customers 
                where e.Id == id   select e).SingleOrDefault();
    }

    public Customer CheckLogin(string phone, string password)
    {
        return (from e in db.Customers where String.Equals(e.Phone, phone) && String.Equals(e.Password, password) select e).SingleOrDefault();
    }

    public ObjLogin CheckLoginSerialize(string phone, string password)
    {
        return (from e in db.Customers where String.Equals(e.Phone, phone) && String.Equals(e.Password, password) select new ObjLogin { Id = e.Id, Fullname = e.FullName, Phone = e.Phone }).SingleOrDefault();
    }

    public List<Customer> GetCustomerByType(int CustomerTypeId)
    {
        return (from e in db.Customers where e.CustomerTypeId == CustomerTypeId select e).ToList();
    }
    public List<Customer> GetCustomerByChannel(int ChannelId)
    {
        return (from e in db.Customers where e.ChannelId == ChannelId select e).ToList();
    }
    public List<Customer> GetCustomerByDistrict(int DistrictId)
    {
        return (from e in db.Customers where e.DistrictId == DistrictId select e).ToList();
    }
    public List<Customer> GetCustomerByLocal(int LocalId,int areaId)
    {
        return (from e in db.Customers
                join l in db.Locals on e.LocalId equals l.Id
                join a in db.Areas on l.AreaId equals a.Id
                where e.LocalId == LocalId                 
                select e).ToList();
    }
    public List<Customer> GetCustomerByGroup(int GroupId)
    {
        var CustomerOfGroup = from g in db.Groups
                              join r in db.Regions on g.Id equals r.GroupId
                              join a in db.Areas on r.Id equals a.RegionId
                              join l in db.Locals on a.Id equals l.AreaId
                              join c in db.Customers on l.Id equals c.LocalId
                              where g.Id == GroupId
                              select c;
        return CustomerOfGroup.ToList();
    }
    public List<Customer> GetCustomerByRegion(int RegionId,int groupId)
    {
        var CustomerOfRegion = from r in db.Regions
                              join a in db.Areas on r.Id equals a.RegionId
                              join l in db.Locals on a.Id equals l.AreaId
                              join c in db.Customers on l.Id equals c.LocalId
                              where r.Id == RegionId && r.GroupId==groupId
                              select c;
        return CustomerOfRegion.ToList();
    }
    public List<Customer> GetCustomerByArea(int AreaId, int RegionId)
    {
        var CustomerOfArea = from a in db.Areas
                             join l in db.Locals on a.Id equals l.AreaId
                             join c in db.Customers on l.Id equals c.LocalId
                             join r in db.Regions on a.RegionId equals r.Id
                             where a.Id == AreaId && r.Id==RegionId
                               select c;
        return CustomerOfArea.ToList();
    }

    public int InsertCustomer(string UPICode, string FullName, string Address, string Street, string Ward, string Phone, string Password, int CustomerTypeId, int ChannelId,
        int DistrictId, int LocalId, DateTime CreateDate, DateTime UpdateDate, bool Status, bool IsEnable)
    {
        try
        {
            Customer o = new Customer();
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
            o.IsEnable = IsEnable;
            db.Customers.InsertOnSubmit(o);
            db.SubmitChanges();
            return o.Id;
        }
        catch
        {
            return 0;
        }
    }
    public bool SetEnableOfCustomer(int id, bool Isenable)
    {
        try
        {
            var o = (from e in db.Customers where e.Id == id select e).SingleOrDefault();
            if (o != null)
            {
                o.IsEnable = Isenable;
                db.SubmitChanges();
                return true;
            }
            else
                return false;
        }
        catch
        { return false; }
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

    public bool IsExistedCustomerByPhone(string phone)
    {
        try
        {
            var customer = GetCustomerByPhone(phone);
            return customer != null;
        }
        catch
        {
            return false;
        }
    }

    public Customer GetCustomerByPhone(string phone)
    {
        var o = from e in db.Customers where e.Phone == phone select e;
        return o.Count() > 0 ? o.SingleOrDefault() : null;
    }

    public bool UpdateCustomerFromLog(int Id, int customerId)
    {
        var l = (from o in db.CustomerLogs where o.Id == Id select o).FirstOrDefault();
        if (l != null)
        {
            try
            {
                var c = (from a in db.Customers where a.Id == customerId select a).FirstOrDefault();
                c.UpiCode = l.UpiCode;
                c.FullName = l.FullName;
                c.Address = l.Address;
                c.Street = l.Street;
                c.Ward = l.Ward;
                c.Phone = l.Phone;
                c.Password = l.Password;
                c.CustomerTypeId = l.CustomerTypeId;
                c.ChannelId = l.ChannelId;
                c.DistrictId = l.DistrictId;
                c.LocalId = l.LocalId;
                c.CreateDate = l.CreateDate;
                c.UpdateDate = l.UpdateDate;
                c.Status = l.Status;
                c.IsEnable = true;
                c.NoteOfSalesmen = l.NoteOfSalesmen;
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

    public List<Customer> FilterCustomerByName(string name)
    {
        var CustomerList = from o in db.Customers where o.FullName.Contains(name) select o;
        return CustomerList.ToList();
    }

    public bool SetPassword(string phone, string password)
    {
        try
        {
            Customer o = (from e in db.Customers where e.Phone == phone select e).SingleOrDefault();
            if (o != null)
            {
                o.Password = password;
                db.SubmitChanges();
                return true;
            }
            else
                return false;
        }
        catch
        {
            return false;
        }
    }
    public string SupervisorName(int SupervisorID)
    {
        var a=(from e in db.CustomerSupervisors where e.Id == SupervisorID select e).FirstOrDefault();
        if (a != null)
            return a.FullName;
        else
            return null;
    }

    public List<vwSalemen> GetManagerOfCustomer(int customerId)
    {
        var o = (from e in db.Customers where e.Id == customerId select e).SingleOrDefault();
        if (o != null)
        {
            List<vwSalemen> lst = new List<vwSalemen>();
            List<vwSalemen> lstSaleLocal = (from e in db.SalesLocals
                                            join r in db.Roles on e.Salesmen.RoleId equals r.Id
                                            where e.LocalId == (int)o.LocalId
                                            select new vwSalemen
                                            {
                                                FullName = e.Salesmen.FullName,
                                                Phone = e.Salesmen.Phone,
                                                RoleName = r.RoleName
                                            }).ToList();
            if (lstSaleLocal != null)
            {
                lst.AddRange(lstSaleLocal);
            }
            return lst;
        }
        else
            return null;
    }
    public List<vwSalemen> GetCustomerContact(int customerId)
    {
        var o = (from e in db.Customers where e.Id == customerId select e).SingleOrDefault();
        if (o != null)
        {
            List<vwSalemen> lst = new List<vwSalemen>();
            List<vwSalemen> lstSaleLocal = (from e in db.SalesLocals
                                           join r in db.Roles on e.Salesmen.RoleId equals r.Id
                                            where e.LocalId == (int)o.LocalId
                                            select new vwSalemen
                                            {
                                                FullName = e.Salesmen.FullName,
                                                Phone = e.Salesmen.Phone,
                                                RoleName = r.RoleName
                                            }).ToList();
            if (lstSaleLocal != null)
            {
                lst.AddRange(lstSaleLocal);
            }
            List<vwSalemen> lstSaleArea = (from e in db.SalesAreas
                                          join r in db.Roles on e.Salesmen.RoleId equals r.Id
                                          where e.AreaId == o.Local.AreaId
                                           select new vwSalemen 
                                           {
                                               FullName=e.Salesmen.FullName,
                                               Phone=e.Salesmen.Phone,
                                               RoleName=r.RoleName
                                           }).ToList();
            if (lstSaleArea != null)
            {
                lst.AddRange(lstSaleArea);
            }
            List<vwSalemen> lstSaleRegion = (from e in db.SalesRegions
                                            join r in db.Roles on e.Salesmen.RoleId equals r.Id
                                            where e.RegionId == o.Local.Area.RegionId
                                            select new vwSalemen
                                            {
                                                FullName = e.Salesmen.FullName,
                                                Phone = e.Salesmen.Phone,
                                                RoleName = r.RoleName
                                            }).ToList();
            if (lstSaleRegion != null)
            {
                lst.AddRange(lstSaleRegion);
            }
            List<vwSalemen> lstSaleGroup = (from e in db.SalesGroups
                                           join r in db.Roles on e.Salesmen.RoleId equals r.Id
                                            where e.GroupId == o.Local.Area.Region.GroupId
                                            select new vwSalemen
                                            {
                                                FullName = e.Salesmen.FullName,
                                                Phone = e.Salesmen.Phone,
                                                RoleName = r.RoleName
                                            }).ToList();
            if (lstSaleGroup != null)
            {
                lst.AddRange(lstSaleGroup);
            }
            return lst;
                        
        }
        else
        {
            return null;
        }
    }

    public List<vwCustomer> FilterCustomers(string upiCode, string fullname)
    {
        var viewAllProerties = (from c in db.Customers
                                where c.IsEnable == true  
                                        && (upiCode == string.Empty || c.UpiCode.Contains(upiCode))
                                        && (fullname == string.Empty || c.FullName.Contains(fullname)) 
                                select new vwCustomer
                                {
                                    Id = c.Id,
                                    UpiCode = c.UpiCode,
                                    FullName = c.FullName,
                                    Address = c.Address,
                                    Street = c.Street,
                                    Ward = c.Ward,
                                    Phone = c.Phone,
                                    Password = c.Password,
                                    CustomerTypeId = c.CustomerTypeId,
                                    CustomerTypeName = c.CustomerType.TypeName,
                                    ChannelId = c.ChannelId,
                                    ChannelName = c.Channel.ChannelName,
                                    DistrictId = c.DistrictId,
                                    DistrictName = c.District.DistrictName,
                                    LocalId = c.LocalId,
                                    LocalName = c.Local.LocalName,
                                    GroupId = c.Local.Area.Region.GroupId,
                                    RegionId = c.Local.Area.RegionId,
                                    AreaId = c.Local.AreaId,
                                    CreateDate = c.CreateDate,
                                    UpdateDate = c.UpdateDate,
                                    Status = c.Status,
                                    ProvinceId = c.District.ProvinceId,
                                    SectionId = c.District.Province.SectionId,
                                    NoteOfSalesmen = c.NoteOfSalesmen
                                }).ToList();
        return viewAllProerties;
    }

    private bool UpdateCustomerUsedSms(int id, int noUsed)
    {
        try
        {
            var o = (from e in db.Customers where e.Id == id select e).SingleOrDefault();
            if (o != null)
            {
                o.UsedSMS = noUsed;
                o.LastLoggedDate = DateTime.Now;
                db.SubmitChanges();
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    public bool ResetCustomerUsedSms(int id)
    {
        return UpdateCustomerUsedSms(id, 0);
    }

    public bool UpdateCustomerLastLoggedDate(int id)
    {
        try
        {
            var o = (from e in db.Customers where e.Id == id select e).SingleOrDefault();
            if (o != null)
            {
                o.LastLoggedDate = DateTime.Now;
                db.SubmitChanges();
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    public bool IncreaseCustomerUsedSms(int id, int value)
    {
        try
        {
            var o = (from e in db.Customers where e.Id == id select e).SingleOrDefault();
            if (o != null)
            {
                if(o.UsedSMS.HasValue)
                    o.UsedSMS += value;
                else
                    o.UsedSMS = value;

                db.SubmitChanges();
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }
}