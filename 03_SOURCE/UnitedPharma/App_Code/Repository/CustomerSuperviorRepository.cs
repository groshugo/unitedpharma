using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CustomerSuperviorRepository
/// </summary>
public class CustomerSuperviorRepository
{
    private UPIDataContext db;
	public CustomerSuperviorRepository()
	{
        db = new UPIDataContext();
	}
    public List<CustomerSupervisor> GetAll()
    {
        return (from e in db.CustomerSupervisors select e).ToList();
    }

    public List<vwCustomerSupervior> GetAllViewSupervior()
    {
        try
        {
            var e = (from a in db.CustomerSupervisors
                     select new vwCustomerSupervior
                     {
                         Id = a.Id,
                         FullName = a.FullName,
                         Address = a.Address,
                         Street = a.Street,
                         Ward = a.Ward,
                         Phone = a.Phone,
                         CustomerId = a.CustomerId,
                         CustomerName = a.Customer.FullName,
                         DistrictId = a.DistrictId,
                         CustomerTypeId = a.Customer.CustomerTypeId,
                         DistrictName = a.District.DistrictName,
                         SectionId = a.District.Province.SectionId,
                         ProvinceId = a.District.ProvinceId,
                         PositionId = a.PositionId,
                         PositionName = a.SupervisorPosition.PositionName
                     }).ToList();
            if (e != null)
                return e;
            else
                return null;
        }
        catch
        {
            return null;
        }
    }

    public CustomerSupervisor GetCustomerSuperviorById(int id)
    {
        return (from e in db.CustomerSupervisors where e.Id == id select e).SingleOrDefault();
    }

    public List<vwCustomerSupervior> GetCustomerSupervisorById(int customerId)
    {
        return (from e in db.CustomerSupervisors where e.CustomerId == customerId select new vwCustomerSupervior { 
            Id=e.Id,
            FullName=e.FullName,
            Address =e.Address+", "+e.Street+", "+e.Ward,
            Phone=e.Phone,
            DistrictName=e.District.DistrictName,
            PositionName = e.SupervisorPosition.PositionName
        }).ToList();
    }

    public bool InsertCustomerSupervisor(string FullName, string Address, string Street, string Ward, string Phone, int CustomerId, int DistrictId, int PositionId)
    {
        try
        {
            CustomerSupervisor o = new CustomerSupervisor();
            o.FullName = FullName;
            o.Address = Address;
            o.Street = Street;
            o.Ward = Ward;
            o.Phone = Phone;
            o.CustomerId = CustomerId;
            o.DistrictId = DistrictId;
            o.PositionId = PositionId;
            db.CustomerSupervisors.InsertOnSubmit(o);
            db.SubmitChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool UpdateCustomerSupervisor(int id, string FullName, string Address, string Street, string Ward, string Phone, int CustomerId, int DistrictId, int PositionId)
    {
        try
        {
            var o = (from e in db.CustomerSupervisors where e.Id == id select e).SingleOrDefault();
            if (o != null)
            {
                o.FullName = FullName;
                o.Address = Address;
                o.Street = Street;
                o.Ward = Ward;
                o.Phone = Phone;
                o.CustomerId = CustomerId;
                o.DistrictId = DistrictId;
                o.PositionId = PositionId;
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

    public bool DeleteCustomerSupervisorById(int id)
    {
        var o = (from e in db.CustomerSupervisors where e.Id == id select e).SingleOrDefault();
        if (o != null)
        {
            try
            {
                db.CustomerSupervisors.DeleteOnSubmit(o);
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
}