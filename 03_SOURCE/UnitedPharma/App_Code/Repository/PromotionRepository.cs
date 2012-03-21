using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PromotionRepository
/// </summary>
public class PromotionRepository
{
    private UPIDataContext db;
	public PromotionRepository()
	{
        db = new UPIDataContext();
	}

    public List<Promotion> GetAll()
    {
        return (from e in db.Promotions select e).ToList();
    }

    public List<Promotion> GetAllPromotionForAdmin(int AdminId)
    {
        return (from e in db.Promotions where e.AdministratorId == AdminId select e).ToList();
    }

    public Promotion GetPromotionById(int id)
    {
        return (from e in db.Promotions where e.Id == id select e).SingleOrDefault();
    }    

    public bool Insert(string UPICode, string Title, string Content, DateTime StartDate, DateTime EndDate, int AdminId)
    {
        try
        {
            Promotion o = new Promotion();
            o.UpiCode = UPICode;
            o.Title = Title;
            o.Content = Content;
            o.StartDate = StartDate;
            o.EndDate = EndDate;
            o.AdministratorId = AdminId;
            db.Promotions.InsertOnSubmit(o);
            db.SubmitChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool Update(int id, string UPICode, string Title, string Content, DateTime StartDate, DateTime EndDate)
    {
        try
        {
            var o = (from e in db.Promotions where e.Id == id select e).SingleOrDefault();
            if (o != null)
            {
                o.UpiCode = UPICode;
                o.Title = Title;
                o.Content = Content;
                o.StartDate = StartDate;
                o.EndDate = EndDate;
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
        var o = (from e in db.Promotions where e.Id == id select e).SingleOrDefault();
        if (o != null)
        {
            try
            {
                db.Promotions.DeleteOnSubmit(o);
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

    public List<Promotion> GetPromotionForCustomer(string custPhone)
    {
        var lstPromotionId = (from e in db.SmsObjs where e.ReceiverNumber.Contains(custPhone) select e.PromotionId).Distinct();
        if (lstPromotionId.Count() == 0)
        {
            return null;
        }
        else
        {
            List<Promotion> rs = new List<Promotion>();
            foreach (int id in lstPromotionId)
            {
                rs.Add(GetPromotionById(id));
            }
            return rs;
        }
    }
}