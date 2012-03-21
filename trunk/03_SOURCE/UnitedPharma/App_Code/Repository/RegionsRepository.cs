using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UsefullinkRepository
/// </summary>
public class RegionsRepository
{
    private UPIDataContext db;
    public RegionsRepository()
	{
        db = new UPIDataContext();
	}

    public List<Region> GetAll()
    {
        return (from e in db.Regions select e).ToList(); 
    }

    public List<vwRegion> GetAllViewRegion()
    {
        return (from a in db.Regions select new vwRegion { Id = a.Id, UpiCode = a.UpiCode, RegionName = a.RegionName, Description = a.Description, GroupId = a.GroupId, GroupName = a.Group.GroupName }).ToList();
    }

    public Region GetById(int id)
    {
        return (from e in db.Regions where e.Id == id select e).SingleOrDefault();
    }
    public int GetRegionIdByName(string RegionName)
    {
        var a = from e in db.Regions where e.RegionName.Trim().ToLower() == RegionName.Trim().ToLower() select e.Id;
        if (a.Count() > 0)
            return a.SingleOrDefault();
        else
        {
            return InsertDefault();
        }
    }
    public int InsertDefault()
    {
        var a = from e in db.Regions where e.RegionName.Trim().ToLower() == "other" select e.Id;
        if (a.Count() > 0)
            return a.SingleOrDefault();
        else
        {
            Region o = new Region();
            o.UpiCode = "";
            o.RegionName = "Other";
            o.Description = "";
            db.Regions.InsertOnSubmit(o);
            db.SubmitChanges();
            return o.Id;
        }
    }
    public string GetRegionByAreaId(int AreaId)
    {
        string result = string.Empty;
        var areaIdList = (from e in db.Areas where e.Id == AreaId select e.RegionId).ToList();
        foreach (var item in areaIdList)
        {
            result += item + ",";
        }
        return result.Substring(0, result.Length - 1);
    }
    public bool Add(string UPICode, string RegionName, string Description, int GroupId)
    {
        try
        {
            if (CheckExist(RegionName) == false)
            {
                Region o = new Region();
                o.UpiCode = UPICode;
                o.RegionName = RegionName;
                o.Description = Description;
                o.GroupId = GroupId;
                db.Regions.InsertOnSubmit(o);
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
    public bool CheckExist(string RegionName)
    {
        var o = (from e in db.Regions where e.RegionName.Trim().ToLower() == RegionName.Trim().ToLower() select e).Count();
        if (o > 0)
            return true;
        else
            return false;
    }
    public bool Edit(int id, string UPICode, string RegionName, string Description, int GroupId)
    {
        try
        {
            var o = (from e in db.Regions where e.Id == id select e).SingleOrDefault();
            if (o != null)
            {
                o.RegionName = RegionName;
                o.UpiCode = UPICode;                
                o.Description = Description;
                o.GroupId = GroupId;
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
        var o = (from e in db.Regions where e.Id == id select e).SingleOrDefault();
        if (o != null)
        {
            try
            {
                db.Regions.DeleteOnSubmit(o);
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

    public object GetRegionByGroupId(int GroupId)
    {
        return (from e in db.Regions where e.GroupId==GroupId select e).ToList();
    }
    public object GetRegionListByGroupId(int GroupId, int saleId)
    {
        var a = from e in db.Regions
                join r in db.SalesRegions on e.Id equals r.SalesmenId
                where e.GroupId == GroupId && r.SalesmenId == saleId
                select e;
        return a.ToList();
    }
    public int SelectRegionByGroupId(int gId)
    {
        var t=from g in db.Regions where g.Id==gId select g;
        if (t != null)
            return t.SingleOrDefault().Id;
        else
            return 0;
    }

    public bool AddSalesmenRegion(int salesmenId, int regionId)
    {
        try
        {
            if (CheckExistedSalesRegion(salesmenId, regionId) == false)
            {
                SalesRegion r = new SalesRegion();
                r.SalesmenId = salesmenId;
                r.RegionId = regionId;
                db.SalesRegions.InsertOnSubmit(r);
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
    public bool CheckExistedSalesRegion(int salesmenId, int regionId)
    {
        var o=from e in db.SalesRegions where e.RegionId==regionId && e.SalesmenId==salesmenId select e;
        if (o.Count() > 0)
            return true;
        else
            return false;
    }
    public List<SalesRegion> GetRegionBySaleId(int SaleId)
    {
        return (from e in db.SalesRegions where e.SalesmenId == SaleId select e).ToList();
    }
    AreasRepository ARepo = new AreasRepository();
    public List<Region> GetListRegionByAreaId(int saleId, int GroupId)
    {
        var l = GetRegionBySaleId(saleId);
        List<Region> lst = new List<Region>();
        foreach (var item in l)
        {
            var e = (from a in db.Regions where a.Id == item.RegionId && a.GroupId == GroupId select a).ToList();
            if (e != null)
                lst.AddRange(e);
        }
        return lst;
    }

}
