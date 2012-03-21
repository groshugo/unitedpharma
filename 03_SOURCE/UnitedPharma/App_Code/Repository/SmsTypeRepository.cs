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

    public bool Add(string Name, string Syntax)
    {
        try
        {
            SmsType o = new SmsType();
            o.Name = Name;
            o.Syntax = Syntax;
            db.SmsTypes.InsertOnSubmit(o);
            db.SubmitChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool Edit(int id, string Name, string Syntax)
    {
        try
        {
            var o = (from e in db.SmsTypes where e.Id == id select e).SingleOrDefault();
            if (o != null)
            {
                o.Name = Name;
                o.Syntax = Syntax;
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

    
}
