using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SupervisorRepository
/// </summary>
public class SupervisorRepository
{
    private UPIDataContext db;
	public SupervisorRepository()
	{
        db = new UPIDataContext();
	}

    public List<CustomerSupervisor> GetAllCustomerSupervisor()
    {
        return (from e in db.CustomerSupervisors select e).ToList();
    }    

    public CustomerSupervisor GetSupervisorById(int id)
    {
        return (from e in db.CustomerSupervisors where e.Id == id select e).SingleOrDefault();
    }

    public bool InsertSupervisor(string FullName, string Address, string Street, string Ward, string Phone, int CustomerId, int DistrictId)
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
            db.CustomerSupervisors.InsertOnSubmit(o);
            db.SubmitChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool EditSupervisor(int id, string FullName, string Address, string Street, string Ward, string Phone, int CustomerId, int DistrictId)
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

    public bool DeleteSupervisor(int id)
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