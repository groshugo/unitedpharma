using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SalesmanRepository
/// </summary>
public class SalesmanRepository
{
    private UPIDataContext db;
    public SalesmanRepository()
    {
        db = new UPIDataContext();
    }
    public List<Salesmen> GetAll()
    {
        return (from e in db.Salesmens select e).ToList();
    }
    public List<vwSalemen> GetAllViewSales()
    {
        return (from a in db.Salesmens
                orderby a.RoleId
                select new vwSalemen
                {
                    Id = a.Id,
                    UpiCode = a.UpiCode,
                    FullName = a.FullName,
                    Phone = a.Phone,
                    RoleId = a.RoleId,
                    RoleName = a.Role.RoleName,
                    SmsQuota = a.SmsQuota,
                    SmsUsed = a.SmsUsed,
                    ExpiredDate = a.ExpiredDate
                }).ToList();
    }

    public List<vwDashboard> GetSaleToDashboard()
    {
        return (from e in db.Salesmens
                select new vwDashboard
                {
                    Phone = e.Phone,
                    FullName = e.FullName,
                    Role = e.Role.RoleName
                }).ToList();
    }

    public Salesmen GetById(int id)
    {
        return (from e in db.Salesmens where e.Id == id select e).SingleOrDefault();
    }
    public int GetSalesmenIdByName(string FullName)
    {
        return (from e in db.Salesmens where e.FullName.Trim().ToLower() == FullName.Trim().ToLower() select e.Id).SingleOrDefault();
    }
    public bool Add(string UpiCode, string FullName, string Phone, int RoleId, int SmsQuota, DateTime ExpiredDate,
        int groupId, int regionId, int areaId, int localId)
    {
        try
        {
            if (CheckExistedSalesmen(FullName) == false)
            {
                Salesmen o = new Salesmen();
                o.UpiCode = UpiCode;
                o.FullName = FullName;
                o.Phone = Phone;
                o.RoleId = RoleId;
                o.SmsQuota = SmsQuota;
                o.ExpiredDate = ExpiredDate;
                db.Salesmens.InsertOnSubmit(o);
                db.SubmitChanges();

                int salesMenId = o.Id;
                if(groupId != 0)
                {
                    // insert Group to SaleGroup
                    SalesGroup salesGroup = new SalesGroup();
                    salesGroup.GroupId = groupId;
                    salesGroup.SalesmenId = salesMenId;
                    db.SalesGroups.InsertOnSubmit(salesGroup);
                    db.SubmitChanges();
                }

                if(regionId != 0)
                {
                    // insert region
                    SalesRegion salesRegion = new SalesRegion();
                    salesRegion.SalesmenId = salesMenId;
                    salesRegion.RegionId = regionId;
                    db.SalesRegions.InsertOnSubmit(salesRegion);
                    db.SubmitChanges();
                }

                if (areaId != 0)
                {
                    // insert area
                    SalesArea salesArea = new SalesArea();
                    salesArea.SalesmenId = salesMenId;
                    salesArea.AreaId = areaId;
                    db.SalesAreas.InsertOnSubmit(salesArea);
                    db.SubmitChanges();
                }

                if (localId != 0)
                {
                    // insert area
                    SalesLocal salesLocal = new SalesLocal();
                    salesLocal.SalesmenId = salesMenId;
                    salesLocal.LocalId = localId;
                    db.SalesLocals.InsertOnSubmit(salesLocal);
                    db.SubmitChanges();
                }

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

    public bool Add(string UpiCode, string FullName, string Phone, int RoleId, int SmsQuota, DateTime ExpiredDate)
    {
        return Add(UpiCode, FullName, Phone, RoleId, SmsQuota, ExpiredDate, 0, 0, 0, 0);
    }

    public bool CheckExistedSalesmen(string FullName)
    {
        var o = (from e in db.Salesmens where e.FullName.Trim().ToLower() == FullName.Trim().ToLower() select e).Count();
        if (o > 0)
            return true;
        else
            return false;
    }
    public bool Edit(int id, string UpiCode, string FullName, string Phone, int RoleId, int SmsQuota, DateTime ExpiredDate)
    {
        try
        {
            var o = (from e in db.Salesmens where e.Id == id select e).SingleOrDefault();
            if (o != null)
            {
                o.UpiCode = UpiCode;
                o.FullName = FullName;
                o.Phone = Phone;
                o.RoleId = RoleId;
                o.SmsQuota = SmsQuota;
                o.ExpiredDate = ExpiredDate;
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
        var o = (from e in db.Salesmens where e.Id == id select e).SingleOrDefault();
        if (o != null)
        {
            try
            {
                db.Salesmens.DeleteOnSubmit(o);
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
    public bool EditQuota(int id, int Quota, DateTime ExpiredDate)
    {
        try
        {
            var o = (from e in db.Salesmens where e.Id == id select e).SingleOrDefault();
            if (o != null)
            {
                o.SmsQuota = Quota;
                o.ExpiredDate = ExpiredDate;
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

    public List<Salesmen> FilterSalesmenByName(string name)
    {
        return (from e in db.Salesmens where e.FullName.Contains(name) select e).ToList();
    }

    public List<Salesmen> GetSaleGroup(int gid, bool isManager)
    {
        List<int> lstId = (from e in db.SalesGroups where e.GroupId == gid select e.SalesmenId).ToList();
        if (isManager)
        {
            return (from e in db.Salesmens join r in db.Roles on e.RoleId equals r.Id 
                    where lstId.Contains(e.Id) 
                    select e).ToList();
        }
        else
        {
            return (from e in db.Salesmens
                    join r in db.Roles on e.RoleId equals r.Id
                    where !lstId.Contains(e.Id) select e).ToList();
        }
    }
    public object GetSaleRegion(int rid, bool isManager)
    {
        List<int> lstId = (from e in db.SalesRegions where e.RegionId == rid select e.SalesmenId).ToList();
        if (isManager)
        {
            return (from e in db.Salesmens join r in db.Roles on e.RoleId equals r.Id where lstId.Contains(e.Id) select e).ToList();
        }
        else
        {
            return (from e in db.Salesmens join r in db.Roles on e.RoleId equals r.Id where !lstId.Contains(e.Id) select e).ToList();
        }
    }
    public object GetSaleArea(int aid, bool isManager)
    {
        List<int> lstId = (from e in db.SalesAreas where e.AreaId == aid select e.SalesmenId).ToList();
        if (isManager)
        {
            return (from e in db.Salesmens join r in db.Roles on e.RoleId equals r.Id where lstId.Contains(e.Id) select e).ToList();
        }
        else
        {
            return (from e in db.Salesmens join r in db.Roles on e.RoleId equals r.Id where !lstId.Contains(e.Id) select e).ToList();
        }
    }
    public object GetSaleLocal(int lid, bool isManager)
    {
        List<int> lstId = (from e in db.SalesLocals where e.LocalId == lid select e.SalesmenId).ToList();
        if (isManager)
        {
            return (from e in db.Salesmens join r in db.Roles on e.RoleId equals r.Id where lstId.Contains(e.Id) select e).ToList();
        }
        else
        {
            return (from e in db.Salesmens join r in db.Roles on e.RoleId equals r.Id where !lstId.Contains(e.Id) select e).ToList();
        }
    }
    public bool UpdateSaleGroup(int salesmenId, int groupId)
    {
        var o = (from e in db.SalesGroups where e.SalesmenId == salesmenId && e.GroupId == groupId select e).SingleOrDefault();
        if (o != null)
        {
            try
            {
                db.SalesGroups.DeleteOnSubmit(o);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        else
        {
            try
            {
                SalesGroup sg = new SalesGroup();
                sg.GroupId = groupId;
                sg.SalesmenId = salesmenId;
                db.SalesGroups.InsertOnSubmit(sg);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
    public bool UpdateSaleRegion(int salesmenId, int regionId)
    {
        var o = (from e in db.SalesRegions where e.SalesmenId == salesmenId && e.RegionId == regionId select e).SingleOrDefault();
        if (o != null)
        {
            try
            {
                db.SalesRegions.DeleteOnSubmit(o);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        else
        {
            try
            {
                SalesRegion sr = new SalesRegion();
                sr.RegionId = regionId;
                sr.SalesmenId = salesmenId;
                db.SalesRegions.InsertOnSubmit(sr);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
    public bool UpdateSaleArea(int salesmenId, int areaId)
    {
        var o = (from e in db.SalesAreas where e.SalesmenId == salesmenId && e.AreaId == areaId select e).SingleOrDefault();
        if (o != null)
        {
            try
            {
                db.SalesAreas.DeleteOnSubmit(o);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        else
        {
            try
            {
                SalesArea sr = new SalesArea();
                sr.AreaId = areaId;
                sr.SalesmenId = salesmenId;
                db.SalesAreas.InsertOnSubmit(sr);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
    public bool UpdateSaleLocal(int salesmenId, int localId)
    {
        var o = (from e in db.SalesLocals where e.SalesmenId == salesmenId && e.LocalId == localId select e).SingleOrDefault();
        if (o != null)
        {
            try
            {
                db.SalesLocals.DeleteOnSubmit(o);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        else
        {
            try
            {
                SalesLocal sr = new SalesLocal();
                sr.LocalId = localId;
                sr.SalesmenId = salesmenId;
                db.SalesLocals.InsertOnSubmit(sr);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
    public bool CheckSalemenByPhoneNumber(string phone)
    {
        try
        {
            var o = from e in db.Salesmens where e.Phone == phone select e;
            if (o.Count() > 0)
                return true;
            else
                return false;
        }
        catch
        {
            return false;
        }
    }

    public Salesmen GetSalemenByPhoneNumber(string phone)
    {
        return (from e in db.Salesmens where e.Phone == phone select e).SingleOrDefault();
    }

    public Salesmen GetSalemenById(int id)
    {
        return (from e in db.Salesmens where e.Id == id select e).SingleOrDefault();
    }

    public Salesmen CheckLogin(string phone)
    {
        return (from e in db.Salesmens where String.Equals(phone, e.Phone) select e).SingleOrDefault();
    }

    public ObjLogin CheckLoginSerialize(string phone)
    {
        return (from e in db.Salesmens where String.Equals(phone, e.Phone) select new ObjLogin { Id = e.Id, Fullname = e.FullName, Phone = e.Phone }).SingleOrDefault();
    }

    public List<vwSalesGroup> GetSalesGroupById(int groupId)
    {
        return (from e in db.SalesGroups
                where e.GroupId == groupId
                select new vwSalesGroup
                {
                    Id = e.Id,
                    SalesmenId = e.SalesmenId,
                    GroupId = e.GroupId,
                    GroupName = e.Group.GroupName
                }).ToList();
    }

    public List<vwSalesRegion> GetAllSalesRegion()
    {
        return (from e in db.SalesRegions
                select new vwSalesRegion
                {
                    Id = e.Id,
                    RegionId = e.RegionId,
                    RegionName = e.Region.RegionName,
                    SalesmenId = e.SalesmenId
                }).ToList();
    }

    public List<vwSalesArea> GetAllSalesArea()
    {
        return (from e in db.SalesAreas
                select new vwSalesArea
                {
                    Id = e.Id,
                    AreaId = e.AreaId,
                    AreaName = e.Area.AreaName,
                    SalesmenId = e.SalesmenId
                }).ToList();
    }

    public List<vwSalesLocal> GetAllSalesLocal()
    {
        return (from e in db.SalesLocals
                select new vwSalesLocal
                {
                    Id = e.Id,
                    SalesmenId = e.SalesmenId,
                    LocalId = e.LocalId,
                    LocalName = e.Local.LocalName
                }).ToList();
    }

    public List<int> GetLocalsByAreaId(int areaId)
    {
        return (from e in db.Locals where e.AreaId == areaId select e.Id).ToList();
    }

    public List<int> GetAreasByRegionId(int regionId)
    {
        return (from e in db.Areas where e.RegionId == regionId select e.Id).ToList();
    }

    public List<int> GetRegionsByGroupId(int groupId)
    {
        return (from e in db.Regions where e.GroupId == groupId select e.Id).ToList();
    }

    public void GetSalesmenByGroupId(int groupId, List<Salesmen> lstRs)
    {
        var lstTmp = from e in db.SalesGroups where e.GroupId == groupId select e.Salesmen;
        if (lstTmp.Count() > 0)
        {
            foreach (Salesmen sm in lstTmp)
            {
                if (!lstRs.Contains(sm))
                    lstRs.Add(sm);
            }
        }
    }

    public void GetSalesmenByRegionId(int regionId, List<Salesmen> lstRs)
    {
        var lstTmp = from e in db.SalesRegions where e.RegionId == regionId select e.Salesmen;
        if (lstTmp.Count() > 0)
        {
            foreach (Salesmen sm in lstTmp)
            {
                if (!lstRs.Contains(sm))
                    lstRs.Add(sm);
            }
        }
    }

    public void GetSalesmenByAreaId(int areaId, List<Salesmen> lstRs)
    {
        var lstTmp = from e in db.SalesAreas where e.AreaId == areaId select e.Salesmen;
        if (lstTmp.Count() > 0)
        {
            foreach (Salesmen sm in lstTmp)
            {
                if (!lstRs.Contains(sm))
                    lstRs.Add(sm);
            }
        }
    }

    public void GetSalesmenByLocalId(int localId, List<Salesmen> lstRs)
    {
        var lstTmp = from e in db.SalesLocals where e.LocalId == localId select e.Salesmen;
        if (lstTmp.Count() > 0)
        {
            foreach (Salesmen sm in lstTmp)
            {
                if (!lstRs.Contains(sm))
                    lstRs.Add(sm);
            }
        }
    }

    public void GetCustomersByLocalId(int localId, List<Customer> lstRs)
    {
        var lstTmp = (from e in db.Customers where e.LocalId == localId select e).Distinct();
        if (lstTmp.Count() > 0)
        {
            foreach (Customer sm in lstTmp)
            {
                lstRs.Add(sm);
            }
            lstRs.Distinct();
        }
    }

    public List<Salesmen> GetSaleContact(int saleId)
    {
        Salesmen sale = (from e in db.Salesmens where e.Id == saleId select e).SingleOrDefault();
        if (sale != null)
        {
            List<Salesmen> lst = new List<Salesmen>();
            //Is Sale_Local
            if (sale.SalesLocals.Count > 0)
            {
                foreach (SalesLocal sl in sale.SalesLocals)
                {
                    GetSalesmenByAreaId(sl.Local.AreaId, lst);
                    GetSalesmenByRegionId(sl.Local.Area.RegionId, lst);
                    GetSalesmenByGroupId(sl.Local.Area.Region.GroupId, lst);
                }
            }
            else if (sale.SalesAreas.Count > 0)
            {
                foreach (SalesArea sa in sale.SalesAreas)
                {
                    GetSalesmenByRegionId(sa.Area.RegionId, lst);
                    GetSalesmenByGroupId(sa.Area.Region.GroupId, lst);
                    if (sa.Area.Locals.Count > 0)
                    {
                        foreach (Local lc in sa.Area.Locals)
                        {
                            GetSalesmenByLocalId(lc.Id, lst);
                        }
                    }
                }
            }
            else if (sale.SalesRegions.Count > 0) //Is Sale_Region
            {
                foreach (SalesRegion sr in sale.SalesRegions)
                {
                    GetSalesmenByGroupId(sr.Region.GroupId, lst);

                    if (sr.Region.Areas.Count > 0)
                    {
                        foreach (Area ar in sr.Region.Areas)//Add Sale_Area
                        {
                            GetSalesmenByAreaId(ar.Id, lst);
                            if (ar.Locals.Count > 0)
                            {
                                foreach (Local lc in ar.Locals)
                                {
                                    GetSalesmenByLocalId(lc.Id, lst);
                                }
                            }
                        }
                    }
                }
            }
            else //Is Sale_Group
            {
                foreach (SalesGroup sg in sale.SalesGroups)
                {
                    if (sg.Group.Regions.Count > 0)
                    {
                        foreach (Region rg in sg.Group.Regions)
                        {
                            GetSalesmenByRegionId(rg.Id, lst);
                            if (rg.Areas.Count > 0)
                            {
                                foreach (Area ar in rg.Areas)
                                {
                                    GetSalesmenByAreaId(ar.Id, lst);
                                    if (ar.Locals.Count > 0)
                                    {
                                        foreach (Local lc in ar.Locals)
                                        {
                                            GetSalesmenByLocalId(lc.Id, lst);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return lst;
        }
        else
        {
            return null;
        }
    }

    public List<Customer> GetCustomerContact(int saleId)
    {
        Salesmen sale = (from e in db.Salesmens where e.Id == saleId select e).SingleOrDefault();
        if (sale != null)
        {
            List<Customer> lst = new List<Customer>();
            List<Customer> lstTemp;
            if (sale.SalesLocals.Count > 0)
            {
                foreach (SalesLocal slc in sale.SalesLocals)
                {
                    GetCustomersByLocalId(slc.LocalId, lst);
                }
            }
            else if (sale.SalesAreas.Count > 0)
            {
                foreach (SalesArea sa in sale.SalesAreas)
                {
                    if (sa.Area.Locals.Count > 0)
                    {
                        foreach (Local lc in sa.Area.Locals)
                        {
                            GetCustomersByLocalId(lc.Id, lst);
                        }
                    }
                }
            }
            else if (sale.SalesRegions.Count > 0) //Is Sale_Region
            {
                foreach (SalesRegion sr in sale.SalesRegions)
                {
                    if (sr.Region.Areas.Count > 0)
                    {
                        foreach (Area ar in sr.Region.Areas)
                        {
                            if (ar.Locals.Count > 0)
                            {
                                foreach (Local lc in ar.Locals)
                                {
                                    GetCustomersByLocalId(lc.Id, lst);
                                }
                            }
                        }
                    }
                }
            }
            else //Is Sale_Group
            {
                foreach (SalesGroup sg in sale.SalesGroups)
                {
                    if (sg.Group.Regions.Count > 0)
                    {
                        foreach (Region rg in sg.Group.Regions)
                        {
                            if (rg.Areas.Count > 0)
                            {
                                foreach (Area ar in rg.Areas)
                                {
                                    if (ar.Locals.Count > 0)
                                    {
                                        foreach (Local lc in ar.Locals)
                                        {
                                            GetCustomersByLocalId(lc.Id, lst);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return lst;
        }
        else
            return null;
    }
    
    public List<Salesmen> FilterSaleContact(int saleId, int typeFilter, string valueFilter)
    {
        List<Salesmen> source = GetSaleContact(saleId);
        List<Salesmen> result = new List<Salesmen>();
        switch (typeFilter)
        {
            case 0: result = (from e in source where e.FullName.Contains(valueFilter) select e).ToList(); break;
            case 1: result = (from e in source where e.Phone.Contains(valueFilter) select e).ToList(); break;
        }
        return result;
    }

    public List<Customer> FilterCustomerContact(int saleId, int typeFilter, string valueFilter)
    {
        List<Customer> source = GetCustomerContact(saleId);
        List<Customer> result = new List<Customer>();
        switch (typeFilter)
        {
            case 0: result = (from e in source where e.FullName.Contains(valueFilter) select e).ToList(); break;
            case 1: result = (from e in source where e.Phone.Contains(valueFilter) select e).ToList(); break;
        }
        return result;
    }

    public List<vwSalemen> GetvwSaleContact(int saleId)
    {
        List<Salesmen> source = GetSaleContact(saleId);
        return (from e in source select new vwSalemen 
                    { Id = e.Id, Phone = e.Phone, FullName = e.FullName, UpiCode = e.UpiCode, 
                        RoleId = e.RoleId, RoleName = e.Role.RoleName }).ToList();
    }

    public List<vwSalemen> FiltervwSaleContact(int saleId, string valueFilter)
    {
        List<vwSalemen> source = GetvwSaleContact(saleId);        
        return  (from e in source where e.FullName.Contains(valueFilter) select e).ToList();
        
    }

    public List<Salesmen> GetSalesmenByRoleId(int roleId)
    {
        return (from e in db.Salesmens where e.RoleId == roleId select e).ToList();
    }

    public List<Salesmen> GetSalesmenByRoleIdAndManagerId(int roleId, int managerId)
    {
        return (from e in db.Salesmens where e.RoleId == roleId  
                    && e.SalesmenManagerId == managerId select e).ToList();
    }
}