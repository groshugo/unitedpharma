using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UsefullinkRepository
/// </summary>
public class LocalsRepository
{
    private UPIDataContext db;
    public LocalsRepository()
	{
        db = new UPIDataContext();
	}

    public List<Local> GetAll()
    {
        return (from e in db.Locals select e).ToList(); 
    }

    public List<vwLocal> GetAllViewLocal()
    {
        return (from a in db.Locals select new vwLocal { Id = a.Id, UpiCode = a.UpiCode, LocalName = a.LocalName, Description = a.Description, AreaId = a.AreaId, AreaName = a.Area.AreaName, RegionId = a.Area.RegionId, RegionName = a.Area.Region.RegionName, GroupId = a.Area.Region.GroupId, GroupName = a.Area.Region.Group.GroupName }).ToList();
    }

    public Local GetById(int id)
    {
        return (from e in db.Locals where e.Id == id select e).SingleOrDefault();
    }
    public int GetLocalIdByName(string LocalName)
    {
        var a = from e in db.Locals where e.LocalName.Trim().ToLower() == LocalName.Trim().ToLower() select e.Id;
        if (a.Count() > 0)
            return a.SingleOrDefault();
        else
        {
            return InsertDefault();
        }
    }

    public int InsertDefault()
    {
        var a = from e in db.Locals where e.LocalName.Trim().ToLower() == "other" select e.Id;
        if (a.Count() > 0)
            return a.SingleOrDefault();
        else
        {
            Local o = new Local();
            o.UpiCode = "";
            o.LocalName = "Other";
            o.Description = "";
            db.Locals.InsertOnSubmit(o);
            db.SubmitChanges();
            return o.Id;
        }
    }
    public int Add(string upiCode, string localName, string description, int areaId)
    {
        try
        {
            if (CheckExistedLocal(-1, localName)) return -1;

            var o = new Local {UpiCode = upiCode, LocalName = localName, Description = description, AreaId = areaId};
            db.Locals.InsertOnSubmit(o);
            db.SubmitChanges();
            return o.Id;
        }
        catch
        {
            return -1;
        }
    }

    public int Import(string upiCode, string localName, string description, int areaId)
    {
        try
        {
            if (CheckExisted(localName) == false)
            {
                var o = new Local {UpiCode = upiCode, LocalName = localName, Description = description, AreaId = areaId};
                db.Locals.InsertOnSubmit(o);
                db.SubmitChanges();
                return o.Id;
            }
            var local = (from l in db.Locals where l.LocalName == localName select l).SingleOrDefault();

            if (local != null) return local.Id;

            return -1;
        }
        catch
        {
            return -1;
        }
    }

    public bool CheckExisted(string localName)
    {
        var o = (from e in db.Locals where e.LocalName.Trim().ToLower() == localName.Trim().ToLower() select e).Count();
        if (o > 0)
            return true;
        else
            return false;
    }

    public bool Edit(int id, string upiCode, string localName, string description, int areaId)
    {
        try
        {
            if (CheckExistedLocal(id, localName)) return false;

            var o = (from e in db.Locals where e.Id == id select e).SingleOrDefault();
            if (o != null)
            {                
                o.UpiCode = upiCode;
                o.LocalName = localName;
                o.Description = description;
                o.AreaId = areaId;
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
        var o = (from e in db.Locals where e.Id == id select e).SingleOrDefault();
        if (o != null)
        {
            try
            {
                db.Locals.DeleteOnSubmit(o);
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
    
    public object GetLocalByAreaId(int areaID)
    {
        return (from e in db.Locals where e.AreaId == areaID select e).ToList();
    }

    public int SelectLocalByAreaId(int areaID)
    {
        var o = from e in db.Locals where e.AreaId == areaID select e;
        if (o != null)
            return o.SingleOrDefault().AreaId;
        else
            return 0;
    }

    public bool AddSalesmenLocal(int salesmenId, int LocalId)
    {
        try
        {
            if (CheckExistSalesLocal(salesmenId, LocalId) == false)
            {
                SalesLocal r = new SalesLocal();
                r.SalesmenId = salesmenId;
                r.LocalId = LocalId;
                db.SalesLocals.InsertOnSubmit(r);
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
    public bool CheckExistSalesLocal(int salesmenId, int LocalId)
    {
        var o = from e in db.SalesLocals where e.LocalId == LocalId && e.SalesmenId == salesmenId select e;
        if (o.Count() > 0)
            return true;
        else
            return false;
    }
    public List<SalesLocal> GetLocalBySaleId(int SaleId)
    {
        return (from e in db.SalesLocals where e.SalesmenId == SaleId select e).ToList();
    }
    public List<Local> GetListLocal(int saleId)
    {
        var a = from s in db.Salesmens
                join sl in db.SalesLocals on s.Id equals sl.LocalId
                join l in db.Locals on sl.LocalId equals l.Id
                join c in db.Customers on l.Id equals c.LocalId
                where s.Id == saleId
                select l;
        return a.ToList();
    }

    public bool CheckExistedLocal(int id, string localName)
    {
        if (id == -1)
        {
            var o = (from e in db.Locals where e.LocalName.Trim().ToLower() == localName.Trim().ToLower() select e).Count();
            return o > 0;
        }
        else
        {
            var o = (from e in db.Locals
                     where e.LocalName.Trim().ToLower() == localName.Trim().ToLower()
                         && e.Id != id
                     select e).Count();
            return o > 0;
        }

    }
}
