using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UsefullinkRepository
/// </summary>
public class DistrictsRepository
{
    private UPIDataContext db;
    ProvincesRepository PRepo = new ProvincesRepository();
    public DistrictsRepository()
	{
        db = new UPIDataContext();
	}

    public List<District> GetAll()
    {
        return (from e in db.Districts select e).ToList(); 
    }

    public List<vwDistrict> GetAllViewDistrict()
    {
        return (from a in db.Districts select new vwDistrict { Id = a.Id, DistrictName = a.DistrictName, ProvinceId = a.ProvinceId, ProvinceName = a.Province.ProvinceName, SectionId = a.Province.SectionId, SectionName = a.Province.Section.SectionName }).ToList();
    }

    public District GetById(int id)
    {
        return (from e in db.Districts where e.Id == id select e).SingleOrDefault();
    }
    public int GetDistrictIdByName(string DistrictName)
    {
        var a = from e in db.Districts where e.DistrictName.Trim().ToLower() == DistrictName.Trim().ToLower() select e.Id;
        if (a.Count() > 0)
            return a.SingleOrDefault();
        else
        {
            return InsertDefault();
        }
    }
    public int InsertDefault()
    {
        var a = from e in db.Districts where e.DistrictName.Trim().ToLower() == "other" select e.Id;
        if (a.Count() > 0)
            return a.SingleOrDefault();
        else
        {
            District o = new District();
            o.DistrictName = "Other";
            o.ProvinceId = PRepo.GetProvinceIdByName("");
            db.Districts.InsertOnSubmit(o);
            db.SubmitChanges();
            return o.Id;
        }
    }
    public bool Add(string DistrictName, int ProvinceId)
    {
        try
        {
            if (CheckExisted(DistrictName) == false)
            {
                District o = new District();
                o.DistrictName = DistrictName;
                o.ProvinceId = ProvinceId;
                db.Districts.InsertOnSubmit(o);
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
    public bool CheckExisted(string DistrictName)
    {
        var o = (from e in db.Districts where e.DistrictName.Trim().ToLower() == DistrictName.Trim().ToLower() select e).Count();
        if (o > 0)
            return true;
        else
            return false;
    }
    public bool Edit(int id, string DistrictName, int ProvinceId)
    {
        try
        {
            var o = (from e in db.Districts where e.Id == id select e).SingleOrDefault();
            if (o != null)
            {
                o.DistrictName = DistrictName;
                o.ProvinceId = ProvinceId;
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
        var o = (from e in db.Districts where e.Id == id select e).SingleOrDefault();
        if (o != null)
        {
            try
            {
                db.Districts.DeleteOnSubmit(o);
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
    public object GetDistrictByProvinceId(int gId)
    {
        return (from e in db.Districts where e.ProvinceId == gId select e).ToList();
    }

    public int Import(string DistrictName, int ProvinceId)
    {
        try
        {
            var district =
                (from e in db.Districts where e.DistrictName.Trim().ToLower() == DistrictName.Trim().ToLower() select e)
                    .SingleOrDefault();

            if (district == null)
            {
                District o = new District();
                o.DistrictName = DistrictName;
                o.ProvinceId = ProvinceId;
                db.Districts.InsertOnSubmit(o);
                db.SubmitChanges();
                return o.Id;
            }

            return district.Id;
        }
        catch
        {
            return -1;
        }
    }
}
