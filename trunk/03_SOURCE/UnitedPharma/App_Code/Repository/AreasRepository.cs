using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UsefullinkRepository
/// </summary>
public class AreasRepository
{
    private UPIDataContext db;
    public AreasRepository()
	{
        db = new UPIDataContext();
	}

    public List<Area> GetAll()
    {
        return (from e in db.Areas select e).ToList(); 
    }

    public List<vwArea> GetAllViewArea()
    {
        return (from a in db.Areas select new vwArea{ Id = a.Id, UpiCode = a.UpiCode, AreaName = a.AreaName, Description = a.Description, RegionId = a.RegionId, RegionName = a.Region.RegionName, GroupId = a.Region.GroupId, GroupName = a.Region.Group.GroupName } ).ToList();
    }

    public Area GetById(int id)
    {
        return (from e in db.Areas where e.Id == id select e).SingleOrDefault();
    }
    public int GetAreaIdByName(string AreaName)
    {
        var a = from e in db.Areas where e.AreaName.Trim().ToLower() == AreaName.Trim().ToLower() select e.Id;
        if (a.Count() > 0)
            return a.SingleOrDefault();
        else
        {
            return InsertDefault();
        }
    }
    public int InsertDefault()
    {
        var a = from e in db.Areas where e.AreaName.Trim().ToLower() == "other" select e.Id;
        if (a.Count() > 0)
            return a.SingleOrDefault();
        else
        {
            Area o = new Area();
            o.UpiCode = "";
            o.AreaName = "Other";
            o.Description = "";
            db.Areas.InsertOnSubmit(o);
            db.SubmitChanges();
            return o.Id;
        }
    }
    public int Add(string upiCode, string areaName, string description, int regionId)
    {
        try
        {
            if (CheckExistedArea(-1, areaName)) return -1;

            var o = new Area {UpiCode = upiCode, AreaName = areaName, Description = description, RegionId = regionId};
            db.Areas.InsertOnSubmit(o);
            db.SubmitChanges();
            return o.Id;
        }
        catch
        {
            return -1;
        }
    }

    public int Import(string UPICode, string AreaName, string Description, int RegionId)
    {
        try
        {
            if (CheckExistedArea(AreaName) == false)
            {
                Area o = new Area();
                o.UpiCode = UPICode;
                o.AreaName = AreaName;
                o.Description = Description;
                o.RegionId = RegionId;
                db.Areas.InsertOnSubmit(o);
                db.SubmitChanges();
                return o.Id;
            }

            var area = (from a in db.Areas where a.AreaName == AreaName select a).SingleOrDefault();

            if (area != null) return area.Id;

            return -1;
        }
        catch
        {
            return -1;
        }
    }

    public bool CheckExistedArea(string AreaName)
    {
        var o = (from e in db.Areas where e.AreaName.Trim().ToLower() == AreaName.Trim().ToLower() select e).Count();
        if (o > 0)
            return true;
        else
            return false;

    }
    public bool Edit(int id, string UPICode, string AreaName, string Description, int RegionId)
    {
        try
        {
            if (CheckExistedArea(id, AreaName)) return false;

            var o = (from e in db.Areas where e.Id == id select e).SingleOrDefault();
            if (o != null)
            {                
                o.UpiCode = UPICode;
                o.AreaName = AreaName;
                o.Description = Description;
                o.RegionId = RegionId;
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
        var o = (from e in db.Areas where e.Id == id select e).SingleOrDefault();
        if (o != null)
        {
            try
            {
                db.Areas.DeleteOnSubmit(o);
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

    public object GetAreaByRegionId(int rId)
    {
        return (from e in db.Areas where e.RegionId == rId select e).ToList();
    }

    public int SelectAreaByRegionId(int rId)
    {
        var o = from e in db.Areas where e.RegionId == rId select e;
        if (o != null)
            return o.SingleOrDefault().Id;
        else
            return 0;

    }

    public bool AddSalesmenArea(int salesmenId, int areaId)
    {
        try
        {
            if (CheckExistSalesArea(salesmenId, areaId) == false)
            {
                SalesArea r = new SalesArea();
                r.SalesmenId = salesmenId;
                r.AreaId = areaId;
                db.SalesAreas.InsertOnSubmit(r);
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
    public bool CheckExistSalesArea(int salesmenId, int areaId)
    {
        var o = from e in db.SalesAreas where e.AreaId == areaId && e.SalesmenId == salesmenId select e;
        if (o.Count() > 0)
            return true;
        else
            return false;
    }
    public List<SalesArea> GetAreaBySaleId(int SaleId)
    {
        return (from e in db.SalesAreas where e.SalesmenId == SaleId select e).ToList();
    }
    LocalsRepository LRepo = new LocalsRepository();
    public List<Area> GetListAreaByLocalId(int RegionId, int saleId)
    {
        var l = GetAreaBySaleId(saleId);
        List<Area> lst = new List<Area>();
        foreach (var item in l)
        {
            var e = (from a in db.Areas where a.Id == item.AreaId && a.RegionId == RegionId select a).ToList();
            if (e != null)
                lst.AddRange(e);
        }
        return lst;
    }

    public bool CheckExistedArea(int id, string areaName)
    {
        if (id == -1)
        {
            var o = (from e in db.Areas where e.AreaName.Trim().ToLower() == areaName.Trim().ToLower() select e).Count();
            return o > 0;
        }
        else
        {
            var o = (from e in db.Areas
                     where e.AreaName.Trim().ToLower() == areaName.Trim().ToLower()
                         && e.Id != id
                     select e).Count();
            return o > 0;
        }

    }
}
