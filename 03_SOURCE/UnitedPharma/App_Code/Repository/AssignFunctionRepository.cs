using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AssignFunctionRepository
/// </summary>
public class AssignFunctionRepository
{
    private UPIDataContext db;
    public AssignFunctionRepository()
    {
        db = new UPIDataContext();
    }

    public List<AssignFunction> GetAll()
    {
        return (from e in db.AssignFunctions select e).ToList();
    }

    public List<int> GetListAssignedFunctionId(int AdminId)
    {
        return (from a in db.AssignFunctions
                where a.AdministratorId == AdminId
                select a.FunctionId).ToList();
    }

    public List<Function> GetListAssignedFunction(int AdminId)
    {
        List<int> listAssignId = GetListAssignedFunctionId(AdminId);
        return (from e in db.Functions where listAssignId.Contains(e.Id) select e).ToList();
        
    }    

    public List<Function> GetListNotAssignFunction(int AdminId)
    {
        List<int> listAssignId = GetListAssignedFunctionId(AdminId);
        return (from e in db.Functions where !listAssignId.Contains(e.Id) select e).ToList();
    }

    public string FunctionName(int? functionId)
    {
        if (functionId == null)
            return Constant.UNDEFINE;
        else
            return GetFunctionNameById((int)functionId);
    }

    public string GetFunctionNameById(int functionId)
    {
        var o = (from e in db.Functions where e.Id == functionId select e).SingleOrDefault();
        if (o != null)
            return o.FunctionName;
        else return String.Empty;
    }



    public bool Add(int FunctionId, int? AdministratorId)
    {
        try
        {
            AssignFunction o = new AssignFunction();
            o.FunctionId = FunctionId;
            o.AdministratorId = AdministratorId;
            db.AssignFunctions.InsertOnSubmit(o);
            db.SubmitChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool Edit(int id, int FunctionId, int AdministratorId, int SalesmenId, int CustomerId)
    {
        try
        {
            var o = (from e in db.AssignFunctions where e.Id == id select e).SingleOrDefault();
            if (o != null)
            {
                o.FunctionId = FunctionId;
                o.AdministratorId = AdministratorId;
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
        var o = (from e in db.AssignFunctions where e.Id == id select e).SingleOrDefault();
        if (o != null)
        {
            try
            {
                db.AssignFunctions.DeleteOnSubmit(o);
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

    public bool UpdateAssignFunction(int FunctionId, int AdminId)
    {
        var o = (from e in db.AssignFunctions where e.FunctionId == FunctionId && e.AdministratorId == AdminId select e).SingleOrDefault();
        if (o != null)
        {
            try
            {
                db.AssignFunctions.DeleteOnSubmit(o);
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
            return Add(FunctionId, AdminId);
        }
    }

    public bool CheckPermission(int adminId, string action)
    {
        var o = (from e in db.AssignFunctions where e.AdministratorId == adminId && string.Equals(e.Function.Action.ToLower(), action) select e).SingleOrDefault();
        return (o == null) ? false : true;
    }
}