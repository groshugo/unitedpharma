using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UsefullinkRepository
/// </summary>
public class RolesRepository
{
    private UPIDataContext db;
    public RolesRepository()
	{
        db = new UPIDataContext();
	}

    public List<Role> GetAll()
    {
        return (from e in db.Roles select e).ToList(); 
    }

    public Role GetById(int id)
    {
        return (from e in db.Roles where e.Id == id select e).SingleOrDefault();
    }
    public int GetRoleIdByName(string RoleName)
    {
        var a = from e in db.Roles where e.RoleName.Trim().ToLower() == RoleName.Trim().ToLower() select e.Id;
        if (a.Count() > 0)
            return a.SingleOrDefault();
        else
        {
            return InsertDefault();
        }
    }
    public int InsertDefault()
    {
        var a = from e in db.Roles where e.RoleName.Trim().ToLower() == "other" select e.Id;
        if (a.Count() > 0)
            return a.SingleOrDefault();
        else
        {
            Role o = new Role();
            o.RoleName = "Other";
            db.Roles.InsertOnSubmit(o);
            db.SubmitChanges();
            return o.Id;
        }
    }
    public bool Add(string RoleName)
    {
        try
        {
            if (CheckExistedRole(RoleName)==false)
            {
                Role o = new Role();
                o.RoleName = RoleName;
                db.Roles.InsertOnSubmit(o);
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
    public bool CheckExistedRole(string RoleName)
    {
        var o = (from e in db.Roles where e.RoleName.Trim().ToLower() == RoleName.Trim().ToLower() select e).Count();
        if (o > 0)
            return true;
        else
            return false;
    }
    public bool Edit(int id, string RoleName)
    {
        try
        {
            var o = (from e in db.Roles where e.Id == id select e).SingleOrDefault();
            if (o != null)
            {
                o.RoleName = RoleName;
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
        var o = (from e in db.Roles where e.Id == id select e).SingleOrDefault();
        if (o != null)
        {
            try
            {
                db.Roles.DeleteOnSubmit(o);
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
