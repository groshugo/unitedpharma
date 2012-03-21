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
   

    public bool Add(string PositionName)
    {
        try
        {
            SupervisorPosition o = new SupervisorPosition();
            o.PositionName = PositionName;
            db.SupervisorPositions.InsertOnSubmit(o);
            db.SubmitChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool Edit(int id, string PositionName)
    {
        try
        {
            var o = (from e in db.SupervisorPositions where e.Id == id select e).SingleOrDefault();
            if (o != null)
            {
                o.PositionName = PositionName;                
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

    
}
