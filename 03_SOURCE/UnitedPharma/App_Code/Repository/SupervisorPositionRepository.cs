using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UsefullinkRepository
/// </summary>
public class SupervisorPositionRepository
{
    private UPIDataContext db;
    public SupervisorPositionRepository()
	{
        db = new UPIDataContext();
	}

    public List<SupervisorPosition> GetAll()
    {
        return (from e in db.SupervisorPositions select e).ToList(); 
    }
   

    public bool Add(string positionName)
    {
        try
        {
            if (CheckExistedPosition(-1, positionName)) return false;

            var o = new SupervisorPosition {PositionName = positionName};
            db.SupervisorPositions.InsertOnSubmit(o);
            db.SubmitChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool Edit(int id, string positionName)
    {
        try
        {
            if (CheckExistedPosition(id, positionName)) return false;

            var o = (from e in db.SupervisorPositions where e.Id == id select e).SingleOrDefault();
            if (o != null)
            {
                o.PositionName = positionName;                
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
        var o = (from e in db.SupervisorPositions where e.Id == id select e).SingleOrDefault();
        if (o != null)
        {
            try
            {
                db.SupervisorPositions.DeleteOnSubmit(o);
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

    public bool CheckExistedPosition(int id, string posName)
    {
        if(id == -1)
        {
            var o = (from e in db.SupervisorPositions where e.PositionName.Trim().ToLower() == posName.Trim().ToLower() select e).Count();
            return o > 0;
        }
        else
        {
            var o = (from e in db.SupervisorPositions where e.PositionName.Trim().ToLower() == posName.Trim().ToLower() 
                     && e.Id != id select e).Count();
            return o > 0;
        }
        
    }
}
