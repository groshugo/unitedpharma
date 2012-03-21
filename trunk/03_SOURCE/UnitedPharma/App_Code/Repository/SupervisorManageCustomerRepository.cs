using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UsefullinkRepository
/// </summary>
public class SupervisorManageCustomerRepository
{
    private UPIDataContext db;
    public SupervisorManageCustomerRepository()
    {
        db = new UPIDataContext();
    }

    public List<SupervisorManageCustomer> GetAll()
    {
        return (from e in db.SupervisorManageCustomers select e).ToList();
    }

    public List<vwSupervisorManageCustomer> GetAllView()
    {
        return (from a in db.SupervisorManageCustomers select new vwSupervisorManageCustomer { 
            Id = a.Id, 
            CustomerTypeId = a.Customer.CustomerTypeId,
            CustomerId = a.CustomerId, 
            CustomerName = a.Customer.FullName, 
            SupervisorId = a.SupervisorId,
            SupervisorName = a.CustomerSupervisor.FullName }).ToList();
    }    

    public bool Add(int cust, int supervisor)
    {
        try
        {
            SupervisorManageCustomer o = new SupervisorManageCustomer();
            o.CustomerId = cust;
            o.SupervisorId = supervisor;
            db.SupervisorManageCustomers.InsertOnSubmit(o);
            db.SubmitChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool Edit(int id, int cust, int supervisor)
    {
        try
        {
            var o = (from e in db.SupervisorManageCustomers where e.Id == id select e).SingleOrDefault();
            if (o != null)
            {
                o.CustomerId = cust;
                o.SupervisorId = supervisor;
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

    public bool Delete(int id)
    {
        var o = (from e in db.SupervisorManageCustomers where e.Id == id select e).SingleOrDefault();
        if (o != null)
        {
            try
            {
                db.SupervisorManageCustomers.DeleteOnSubmit(o);
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
