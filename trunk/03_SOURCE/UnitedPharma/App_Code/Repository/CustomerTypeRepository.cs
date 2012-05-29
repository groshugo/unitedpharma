using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UsefullinkRepository
/// </summary>
public class CustomerTypeRepository
{
    private UPIDataContext db;
    public CustomerTypeRepository()
	{
        db = new UPIDataContext();
	}

    public List<CustomerType> GetAll()
    {
        return (from e in db.CustomerTypes select e).ToList(); 
    }

    public CustomerType GetById(int id)
    {
        return (from e in db.CustomerTypes where e.Id == id select e).SingleOrDefault();
    }
    public int GetCustomerTypeIdByName(string TypeName)
    {
        var a = from e in db.CustomerTypes where e.TypeName.Trim().ToLower() == TypeName.Trim().ToLower() select e.Id;
        if (a.Count() > 0)
            return a.SingleOrDefault();
        else
        {
            return InsertDefault();
        }
    }
    public int InsertDefault()
    {
        var a = from e in db.CustomerTypes where e.TypeName.Trim().ToLower() == "other" select e.Id;
        if (a.Count() > 0)
            return a.SingleOrDefault();
        else
        {
            CustomerType o = new CustomerType();
            o.UpiCode = "";
            o.TypeName = "Other";
            db.CustomerTypes.InsertOnSubmit(o);
            db.SubmitChanges();
            return o.Id;
        }
    }
    public bool Add(string upiCode, string typeName)
    {
        try
        {
            if (CheckExistedType(-1, typeName)) return false;

            var o = new CustomerType();
            o.UpiCode = upiCode;
            o.TypeName = typeName;
            db.CustomerTypes.InsertOnSubmit(o);
            db.SubmitChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool CheckExistedCustomerType(string CustomerTypeName)
    {
        var o = (from e in db.CustomerTypes where e.TypeName.Trim().ToLower() == CustomerTypeName.Trim().ToLower() select e).Count();
        if (o > 0)
            return true;
        else
            return false;
    }
    public bool Edit(int id, string upiCode, string typeName)
    {
        try
        {
            if (CheckExistedType(id, typeName)) return false;

            var o = (from e in db.CustomerTypes where e.Id == id select e).SingleOrDefault();
            if (o != null)
            {
                o.UpiCode = upiCode;
                o.TypeName = typeName;               
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
    
    public bool Delete(int id)
    {
        var o = (from e in db.CustomerTypes where e.Id == id select e).SingleOrDefault();
        if (o != null)
        {
            try
            {
                db.CustomerTypes.DeleteOnSubmit(o);
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

    public bool CheckExistedType(int id, string typeName)
    {
        if (id == -1)
        {
            var o = (from e in db.CustomerTypes where e.TypeName.Trim().ToLower() == typeName.Trim().ToLower() select e).Count();
            return o > 0;
        }
        else
        {
            var o = (from e in db.CustomerTypes
                     where e.TypeName.Trim().ToLower() == typeName.Trim().ToLower()
                         && e.Id != id
                     select e).Count();
            return o > 0;
        }

    }
}
