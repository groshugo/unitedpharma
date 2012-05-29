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
        return (db.CustomerSupervisors.Select(a => new { a, district = a.District }).Where(@t => @t.district != null).Select(@t => @t.a.Customer != null ? 
            new vwCustomerSupervior
        {
            Id = @t.a.Id,
            FullName = @t.a.FullName,
            Address = @t.a.Address,
            Street = @t.a.Street,
            Ward = @t.a.Ward,
            Phone = @t.a.Phone,
            CustomerId = @t.a.CustomerId,
            CustomerName = @t.a.Customer.FullName,
            DistrictId = @t.a.DistrictId,
            CustomerTypeId = @t.a.Customer.CustomerTypeId,
            DistrictName = @t.district.DistrictName,
            SectionId = @t.district.Province.SectionId,
            ProvinceId = @t.district.ProvinceId,
            PositionId = @t.a.PositionId,
            PositionName = @t.a.SupervisorPosition.PositionName
        } : null)).ToList();
    }

    public CustomerSupervisor GetCustomerSuperviorById(int id)
    {
        return (from e in db.CustomerSupervisors where e.Id == id select e).SingleOrDefault();
    }

    public List<vwCustomerSupervior> GetCustomerSupervisorById(int customerId)
    {
        return (from e in db.CustomerSupervisors
                where e.CustomerId == customerId
                select new vwCustomerSupervior
                {
                    Id = e.Id,
                    FullName = e.FullName,
                    Address = e.Address + ", " + e.Street + ", " + e.Ward,
                    Phone = e.Phone,
                    DistrictName = e.District.DistrictName,
                    PositionName = e.SupervisorPosition.PositionName
                }).ToList();
    }

    public bool InsertCustomerSupervisor(string fullName, string address, string street, string ward, string phone, 
        int customerId, int districtId, int positionId)
    {
        try
        {
            if (CheckExistedSuporvisor(-1, phone)) return false;

            var o = new CustomerSupervisor
                        {
                            FullName = fullName,
                            Address = address,
                            Street = street,
                            Ward = ward,
                            Phone = phone,
                            CustomerId = customerId,
                            DistrictId = districtId,
                            PositionId = positionId
                        };
            db.CustomerSupervisors.InsertOnSubmit(o);
            db.SubmitChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool CheckExistedSuporvisor(int id, string phone)
    {
        if (id == -1)
        {
            var o = (from e in db.CustomerSupervisors
                     where string.Compare(e.Phone, phone.Trim(), StringComparison.OrdinalIgnoreCase) == 0
                     select e).Count();
            return o > 0;
        }
        else
        {
            var o = (from e in db.CustomerSupervisors
                     where (string.Compare(e.Phone, phone.Trim(), StringComparison.OrdinalIgnoreCase) == 0)
                         && e.Id != id
                     select e).Count();
            return o > 0;
        }

    }

    public bool UpdateCustomerSupervisor(int id, string FullName, string Address, string Street, string Ward, string Phone, int CustomerId, int DistrictId, int PositionId)
    {
        try
        {
            if (CheckExistedSuporvisor(id, Phone)) return false;

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