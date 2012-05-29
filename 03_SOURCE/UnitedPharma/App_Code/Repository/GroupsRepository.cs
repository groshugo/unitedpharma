using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UsefullinkRepository
/// </summary>
public class GroupsRepository
{
    private UPIDataContext db;
    public GroupsRepository()
	{
        db = new UPIDataContext();
	}

    public List<Group> GetAll()
    {
        return (from e in db.Groups select e).ToList(); 
    }

    public Group GetById(int id)
    {
        return (from e in db.Groups where e.Id == id select e).SingleOrDefault();
    }

    public int GetGroupIdByName(string GroupName)
    {
        var a=from e in db.Groups where e.GroupName.Trim().ToLower() == GroupName.Trim().ToLower() select e.Id;
        if (a.Count() > 0)
            return a.SingleOrDefault();
        else
        {
            return InsertDefault();
        }
    }
    public int InsertDefault()
    {
        var a = from e in db.Groups where e.GroupName.Trim().ToLower() == "other" select e.Id;
        if (a.Count() > 0)
            return a.SingleOrDefault();
        else
        {
            Group o = new Group();
            o.UpiCode = "";
            o.GroupName = "Other";
            o.Description = "";
            db.Groups.InsertOnSubmit(o);
            db.SubmitChanges();
            return o.Id;
        }
    }

    public int Add(string upiCode, string groupName, string description)
    {
        try
        {
            if (CheckExistedGroup(-1, groupName)) return -1;

            var o = new Group { UpiCode = upiCode, GroupName = groupName, Description = description };
            db.Groups.InsertOnSubmit(o);
            db.SubmitChanges();

            return o.Id;
        }
        catch
        {
            return -1;
        }
    }

    public bool CheckExistedGroup(string GroupName)
    {
        var o = (from e in db.Groups where e.GroupName.Trim().ToLower() == GroupName.Trim().ToLower() select e).Count();
        if (o > 0)
            return true;
        else
            return false;
    }
    public bool Edit(int id, string upiCode, string groupName, string description)
    {
        try
        {
            if(CheckExistedGroup(id, groupName)) return false;

            var o = (from e in db.Groups where e.Id == id select e).SingleOrDefault();
            if (o != null)
            {
                o.GroupName = groupName;
                o.UpiCode = upiCode;                
                o.Description = description;                
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
        var o = (from e in db.Groups where e.Id == id select e).SingleOrDefault();
        if (o != null)
        {
            try
            {
                db.Groups.DeleteOnSubmit(o);
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

    public bool AddSalesmenGroup(int salesmenId, int groupId)
    {
        try
        {
            if (CheckExistSalesGroup(salesmenId, groupId) == false)
            {
                SalesGroup r = new SalesGroup();
                r.SalesmenId = salesmenId;
                r.GroupId = groupId;
                db.SalesGroups.InsertOnSubmit(r);
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
    public bool CheckExistSalesGroup(int salesmenId, int groupId)
    {
        var o = from e in db.SalesGroups where e.GroupId == groupId && e.SalesmenId == salesmenId select e;
        if (o.Count() > 0)
            return true;
        else
            return false;
    }
    public List<SalesGroup> GetGroupBySaleId(int SaleId)
    {
        return (from e in db.SalesGroups where e.SalesmenId == SaleId select e).ToList();        
    }
    public List<Group> GetListGroupBySaleId(int SaleId)
    {
        var a = from e in db.SalesGroups
                join g in db.Groups on e.GroupId equals g.Id
                where e.SalesmenId == SaleId
                select g;
        return a.ToList();
    }
    RegionsRepository RRepo = new RegionsRepository();
    public List<Group> GetListGroupByRegionId(int saleId)
    {
        var l = GetGroupBySaleId(saleId);
        List<Group> lst = new List<Group>();
        foreach (var item in l)
        {
            var e = (from a in db.Groups where a.Id == item.GroupId select a).ToList();
            if (e != null)
                lst.AddRange(e);
        }
        return lst;
    }

    public Group GetByName(string groupName)
    {
        if (groupName == null) throw new ArgumentNullException("groupName");

        return (from e in db.Groups where e.GroupName == groupName select e).SingleOrDefault();
    }

    public bool CheckExistedGroup(int id, string groupName)
    {
        if (id == -1)
        {
            var o = (from e in db.Groups where e.GroupName.Trim().ToLower() == groupName.Trim().ToLower() select e).Count();
            return o > 0;
        }
        else
        {
            var o = (from e in db.Groups
                     where e.GroupName.Trim().ToLower() == groupName.Trim().ToLower()
                         && e.Id != id
                     select e).Count();
            return o > 0;
        }

    }

    public int Import(string upiCode, string groupName, string description)
    {
        try
        {
            var g = GetByName(groupName);

            if (g == null)
            {
                var o = new Group { UpiCode = upiCode, GroupName = groupName, Description = description };
                db.Groups.InsertOnSubmit(o);
                db.SubmitChanges();

                return o.Id;
            }

            return g.Id;
        }
        catch
        {
            return -1;
        }
    }
}
