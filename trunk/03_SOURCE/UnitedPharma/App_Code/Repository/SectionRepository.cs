using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UsefullinkRepository
/// </summary>
public class SectionRepository
{
    private UPIDataContext db;
    public SectionRepository()
	{
        db = new UPIDataContext();
	}

    public List<Section> GetAll()
    {
        return (from e in db.Sections select e).ToList(); 
    }

    public Section GetById(int id)
    {
        return (from e in db.Sections where e.Id == id select e).SingleOrDefault();
    }
    public int GetSectionIdByName(string SectionName)
    {
        var a = from e in db.Sections where e.SectionName.Trim().ToLower() == SectionName.Trim().ToLower() select e.Id;
        if (a.Count() > 0)
            return a.SingleOrDefault();
        else
        {
            return InsertDefault();
        }
    }
    public int InsertDefault()
    {
        var a = from e in db.Sections where e.SectionName.Trim().ToLower() == "other" select e.Id;
        if (a.Count() > 0)
            return a.SingleOrDefault();
        else
        {
            Section o = new Section();
            o.SectionName = "Other";
            db.Sections.InsertOnSubmit(o);
            db.SubmitChanges();
            return o.Id;
        }
    }
    public bool Add(string SectionName)
    {
        try
        {
            if (CheckExisted(SectionName) == false)
            {
                Section o = new Section();
                o.SectionName = SectionName;
                db.Sections.InsertOnSubmit(o);
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
    public bool CheckExisted(string SectionName)
    {
        var o = (from e in db.Sections where e.SectionName.Trim().ToLower() == SectionName.Trim().ToLower() select e).Count();
        if (o > 0)
            return true;
        else
            return false;
    }
    public bool Edit(int id, string SectionName)
    {
        try
        {
            var o = (from e in db.Sections where e.Id == id select e).SingleOrDefault();
            if (o != null)
            {
                o.SectionName = SectionName;                
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
        var o = (from e in db.Sections where e.Id == id select e).SingleOrDefault();
        if (o != null)
        {
            try
            {
                db.Sections.DeleteOnSubmit(o);
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
