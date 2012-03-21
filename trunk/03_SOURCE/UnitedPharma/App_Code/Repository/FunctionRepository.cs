using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FunctionRepository
/// </summary>
public class FunctionRepository
{
    private UPIDataContext db;
	public FunctionRepository()
	{
        db = new UPIDataContext();
	}

    public List<Function> GetAll()
    {
        return (from e in db.Functions select e).ToList();
    }

    public List<vwFunction> GetAllViewFunction()
    {
        return (from a in db.Functions select new vwFunction { 
            Id = a.Id, FunctionName = a.FunctionName, 
            ParentFunctionId = GetParentFunctionId(a.ParentFunctionId), 
            Action = a.Action, 
            ParentFunctionName = GetParentFunctionName(a.ParentFunctionId) 
        }).ToList();
    }

    private int GetParentFunctionId(int? parentId)
    {
        return parentId == null ? 0 : (int)parentId;
    }

    private string GetParentFunctionName(int? parentId)
    {
        if (parentId == null)
        {
            return Constant.UNDEFINE;
        }
        else
        {
            return GetFunctionNameById((int)parentId);
        }

    }
    private string GetFunctionNameById(int parentId)
    {
        var o = (from e in db.Functions where e.Id == parentId select e).SingleOrDefault();
        if (o != null)
        {
            return o.FunctionName;
        }
        else
        {
            return String.Empty;
        }
    }
    public Function GetById(int id)
    {
        return (from e in db.Functions where e.Id == id select e).SingleOrDefault();
    }
    public bool Add(string FunctionName, int? ParentFunctionId, string Action)
    {
        try
        {
            Function o = new Function();
            o.FunctionName = FunctionName;
            if (ParentFunctionId != null)
            {
                o.ParentFunctionId = ParentFunctionId;
            }
            o.Action = Action;
            db.Functions.InsertOnSubmit(o);
            db.SubmitChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool Edit(int id, string FunctionName, int ParentFunctionId, string Action)
    {
        try
        {
            var o = (from e in db.Functions where e.Id == id select e).SingleOrDefault();
            if (o != null)
            {
                o.FunctionName = FunctionName;
                o.ParentFunctionId = ParentFunctionId;
                o.Action = Action;
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
        var o = (from e in db.Functions where e.Id == id select e).SingleOrDefault();
        if (o != null)
        {
            try
            {
                db.Functions.DeleteOnSubmit(o);
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

    public List<cbFunction> GetListParentFunction(int functionID)
    {
        //List<cbFunction> lst = new List<cbFunction>();
        //cbFunction first = new cbFunction();
        //first.Id = 0;
        //first.FunctionName = Constant.UNDEFINE;
        //lst.Add(first);
        //List<int> lstChildId = new List<int>();
        //GetListChildFunctionId(functionID, lstChildId);
        
        //if (lstChildId.Count == 0)
        //{
        //    var lstfunction = (from e in db.Functions where e.Id != functionID select e).ToList();
        //    if (lstfunction.Count > 0)
        //    {
        //        foreach (Function c in lstfunction)
        //        {
        //            cbFunction item = new cbFunction();
        //            item.Id = c.Id;
        //            item.FunctionName = c.FunctionName;
        //            lst.Add(item);
        //        }

        //    }
        //}
        //else
        //{
        //    var lstChannel = (from e in db.Functions where !lstChildId.Contains(e.Id) && e.Id != functionID select e).ToList();
        //    if (lstChannel.Count > 0)
        //    {
        //        foreach (Function c in lstChannel)
        //        {
        //            cbFunction item = new cbFunction();
        //            item.Id = c.Id;
        //            item.FunctionName = c.FunctionName;
        //            lst.Add(item);
        //        }
        //    }
        //}
        //return lst;
        List<cbFunction> lst = new List<cbFunction>();
        cbFunction first = new cbFunction();
        first.Id = 0;
        first.FunctionName = Constant.UNDEFINE;
        lst.Add(first);
        List<Function> lstFunc = (from e in db.Functions where e.ParentFunctionId == null select e).ToList();
        foreach (Function f in lstFunc)
        {
            cbFunction item = new cbFunction();
            item.Id = f.Id;
            item.FunctionName = f.FunctionName;
            lst.Add(item);
        }
        return lst;
    }

    public void GetListChildFunctionId(int FunctionID, List<int> lstChildId)
    {
        var lst = (from e in db.Functions where e.ParentFunctionId == FunctionID select e).ToList();
        if (lst.Count > 0)
        {
            foreach (Function c in lst)
            {
                lstChildId.Add(c.Id);
                GetListChildFunctionId(c.Id, lstChildId);
            }
        }
    }

    public string GetActionById(int id)
    {
        var o = (from e in db.Functions where e.Id == id select e).SingleOrDefault();
        if (o != null)
        {
            try
            {               
                return o.Action;
            }
            catch
            {
                return String.Empty;
            }
        }
        else
            return String.Empty;
    }
}