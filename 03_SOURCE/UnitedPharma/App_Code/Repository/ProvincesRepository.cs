using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UsefullinkRepository
/// </summary>
public class ProvincesRepository
{
    private UPIDataContext db;
    SectionRepository SRepo = new SectionRepository();
    public ProvincesRepository()
	{
        db = new UPIDataContext();
	}

    public List<Province> GetAll()
    {
        return (from e in db.Provinces select e).ToList(); 
    }

    public List<vwProvince> GetAllViewProvince()
    {
        return (from a in db.Provinces select new vwProvince { Id = a.Id, ProvinceName = a.ProvinceName, SectionId = a.SectionId, SectionName = a.Section.SectionName }).ToList();
    }

    public Province GetById(int id)
    {
        return (from e in db.Provinces where e.Id == id select e).SingleOrDefault();
    }
    public int GetProvinceIdByName(string ProvinceName)
    {
        var a = from e in db.Provinces where e.ProvinceName.Trim().ToLower() == ProvinceName.Trim().ToLower() select e.Id;
        if (a.Count() > 0)
            return a.SingleOrDefault();
        else
        {
            return InsertDefault();
        }
    }
    public int InsertDefault()
    {
        var a = from e in db.Provinces where e.ProvinceName.Trim().ToLower() == "other" select e.Id;
        if (a.Count() > 0)
            return a.SingleOrDefault();
        else
        {
            Province o = new Province();
            o.ProvinceName = "Other";
            o.SectionId = SRepo.GetSectionIdByName("");
            db.Provinces.InsertOnSubmit(o);
            db.SubmitChanges();
            return o.Id;
        }
    }
    public bool Add( string ProvinceName, int SectionId)
    {
        try
        {
            if (CheckExisted(ProvinceName) == false)
            {
                Province o = new Province();
                o.ProvinceName = ProvinceName;
                o.SectionId = SectionId;
                db.Provinces.InsertOnSubmit(o);
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
    public bool CheckExisted(string ProvinceName)
    {
        var o = (from e in db.Provinces where e.ProvinceName.Trim().ToLower() == ProvinceName.Trim().ToLower() select e).Count();
        if (o > 0)
            return true;
        else
            return false;
    }
    public bool Edit(int id, string ProvinceName, int SectionId)
    {
        try
        {
            var o = (from e in db.Provinces where e.Id == id select e).SingleOrDefault();
            if (o != null)
            {
                o.ProvinceName = ProvinceName;
                o.SectionId = SectionId;
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
        var o = (from e in db.Provinces where e.Id == id select e).SingleOrDefault();
        if (o != null)
        {
            try
            {
                db.Provinces.DeleteOnSubmit(o);
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

    public object GetProvinceBySectionId(int gId)
    {
        return (from e in db.Provinces where e.SectionId == gId select e).ToList();
    }
}
