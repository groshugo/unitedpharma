using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UsefullinkRepository
/// </summary>
/// 
public class AdministratorRepository  
{
    private UPIDataContext db;
    public AdministratorRepository()
	{
        db = new UPIDataContext();
	}    

    public List<Administrator> GetAll()
    {
        return (from e in db.Administrators select e).ToList(); 
    }

    public List<vwDashboard> GetAdminToDashboard()
    {
        return (from e in db.Administrators select new vwDashboard {
            Phone=e.Phone,
            FullName=e.Fullname,
            Role="Admin"
        }).ToList();
    }

    public Administrator GetById(int id)
    {
        return (from e in db.Administrators where e.Id == id select e).SingleOrDefault();
    }

    public Administrator CheckLogin(string phone, string password)
    {
        return (from e in db.Administrators where String.Equals(phone, e.Phone) && String.Equals(password, e.Password) select e).SingleOrDefault();
    }

    public ObjLogin CheckLoginSerialize(string phone, string password)
    {
        return (from e in db.Administrators where String.Equals(phone, e.Phone) && String.Equals(password, e.Password) select new ObjLogin { Id = e.Id, Fullname = e.Fullname, Phone = e.Phone,AllowApprove=e.AllowApprove }).SingleOrDefault();
    }

    public bool Add(string upiCode, string fullname, string password, string phone)
    {
        try
        {
            if (!IsAdminExisted(-1, upiCode, phone)) return false;

            var o = new Administrator
            {
                UpiCode = upiCode,
                Fullname = fullname,
                Phone = phone,
                Password = password,
                AllowApprove = false
            };
            db.Administrators.InsertOnSubmit(o);
            db.SubmitChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool Edit(int id, string upiCode, string fullname, string password, string phone)
    {
        try
        {
            if (!IsAdminExisted(id, upiCode, phone)) return false;

            var o = (from e in db.Administrators where e.Id == id select e).SingleOrDefault();
            if (o != null)
            {
                o.UpiCode = upiCode;
                o.Fullname = fullname;
                o.Phone = phone;
                o.Password = password;             
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

    private bool IsAdminExisted(int id, string upiCode, string phone)
    {
        if(id == -1)
        {
            var oForChecking = (from e in db.Administrators
                                where (e.Phone == phone || e.UpiCode == upiCode)
                                select e).SingleOrDefault();
            return oForChecking == null;
        }
        else
        {
            var oForChecking = (from e in db.Administrators
                                where e.Id != id &&
                                      (e.Phone == phone || e.UpiCode == upiCode)
                                select e).SingleOrDefault();
            return oForChecking == null;
        }
        
    }

    public bool Delete(int id)
    {
        var o = (from e in db.Administrators where e.Id == id select e).SingleOrDefault();
        if (o != null)
        {
            try
            {
                db.Administrators.DeleteOnSubmit(o);
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

    public bool GetAdminByPhoneNumber(string phone)
    {
        try
        {
            var o = from e in db.Administrators where e.Phone == phone select e;
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

    public List<Administrator> GetAdminApprove(bool IsApprove)
    {
        return (from e in db.Administrators where e.AllowApprove == IsApprove select e).ToList();
    }
}
