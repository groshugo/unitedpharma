using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UsefullinkRepository
/// </summary>
public class DashboardRepository
{
    private UPIDataContext db;
    public DashboardRepository()
	{
        db = new UPIDataContext();
	}

    public List<Dashboard> GetAll()
    {
        return (from e in db.Dashboards select e).ToList(); 
    }

    public Dashboard GetById(int id)
    {
        return (from e in db.Dashboards where e.Id == id select e).SingleOrDefault();
    }

    public List<Dashboard> GetAllForSalemens(string ReceiverPhoneNumber)
    {
        return (from e in db.Dashboards where e.ReceiverPhoneNumber == ReceiverPhoneNumber && e.IsDeleted == false select e).ToList();
    }

    public int CountDashboardForsalemens(string ReceiverPhoneNumber)
    {
        return (from e in db.Dashboards where e.ReceiverPhoneNumber == ReceiverPhoneNumber && e.IsDeleted == false && e.IsRead==false select e).Count();
    }

    public bool Add(string Title, string Content, string ReceiverPhoneNumber,string SenderPhoneNumber, string filePath)
    {
        try
        {
            Dashboard o = new Dashboard();
            o.Title = Title;
            o.Content = Content;
            o.CreateDate = DateTime.Now;
            o.UpdateDate = DateTime.Now;
            o.IsDeleted = false;
            o.IsRead = false;
            o.ReceiverPhoneNumber = ReceiverPhoneNumber;
            o.SenderPhoneNumber = SenderPhoneNumber;
            o.AttachedFileName = filePath;
            db.Dashboards.InsertOnSubmit(o);
            db.SubmitChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool Edit(int id, string Title, string Content)
    {
        try
        {
            var o = (from e in db.Dashboards where e.Id == id select e).SingleOrDefault();
            if (o != null)
            {
                o.Title = Title;
                o.Content = Content;                
                o.UpdateDate = DateTime.Now;
                o.IsRead = false;
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
    public void UpdateStatus(int Id)
    {
        try
        {
            var o = (from e in db.Dashboards where e.Id == Id select e).SingleOrDefault();
            if (o != null)
            {
                o.IsRead = true;
                db.SubmitChanges();
            }
        }
        catch
        {
        }
    }
    public bool Delete(int id)
    {
        var o = (from e in db.Dashboards where e.Id == id select e).SingleOrDefault();
        if (o != null)
        {
            try
            {
                o.IsDeleted = true;
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
