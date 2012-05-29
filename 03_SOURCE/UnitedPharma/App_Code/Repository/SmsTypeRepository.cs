using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UsefullinkRepository
/// </summary>
public class SmsTypeRepository
{
    private UPIDataContext db;
    public SmsTypeRepository()
	{
        db = new UPIDataContext();
	}

    public List<SmsType> GetAll()
    {
        return (from e in db.SmsTypes select e).ToList(); 
    }

    public SmsType GetById(int id)
    {
        return (from e in db.SmsTypes where e.Id == id select e).SingleOrDefault();
    }

    public bool Add(string name, string syntax)
    {
        try
        {
            if (CheckExistedSmsName(-1, name, syntax)) return false;

            var o = new SmsType {Name = name, Syntax = syntax};
            db.SmsTypes.InsertOnSubmit(o);
            db.SubmitChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool Edit(int id, string name, string syntax)
    {
        try
        {
            if (CheckExistedSmsName(id, name, syntax)) return false;

            var o = (from e in db.SmsTypes where e.Id == id select e).SingleOrDefault();
            if (o != null)
            {
                o.Name = name;
                o.Syntax = syntax;
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
        var o = (from e in db.SmsTypes where e.Id == id select e).SingleOrDefault();
        if (o != null)
        {
            try
            {
                db.SmsTypes.DeleteOnSubmit(o);
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

    public bool CheckExistedSmsName(int id, string smsName, string syntax)
    {
        if (id == -1)
        {
            var o = (from e in db.SmsTypes where e.Name.Trim().ToLower() == smsName.Trim().ToLower() ||
                         e.Syntax.Trim().ToLower() == syntax.Trim().ToLower()
                     select e).Count();
            return o > 0;
        }
        else
        {
            var o = (from e in db.SmsTypes
                     where (e.Name.Trim().ToLower() == smsName.Trim().ToLower() || 
                     e.Syntax.Trim().ToLower() == syntax.Trim().ToLower())
                         && e.Id != id
                     select e).Count();
            return o > 0;
        }

    }
    
}
