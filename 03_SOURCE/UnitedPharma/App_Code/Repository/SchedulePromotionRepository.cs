using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for SchedulePromotionRepository
/// </summary>
public class SchedulePromotionRepository
{
    private UPIDataContext db;
	public SchedulePromotionRepository()
	{
        db = new UPIDataContext();
	}
    public List<SchedulePromotion> GetAll()
    {
        return (from e in db.SchedulePromotions select e).ToList();
    }

    public List<SchedulePromotion> GetPromotions()
    {
        return (db.SchedulePromotions.Where(e => e.IsApprove.HasValue)).ToList();
    }

    public List<vwSchedulePromotion> GetAllViewSchedulePromotion()
    {
        return (from e in db.SchedulePromotions select new vwSchedulePromotion { 
            Id = e.Id,
            UPICode = e.UpiCode,
            Title = e.Title,
            SMSContent = e.SMSContent,
            WebContent = e.WebContent,
            StartDate = e.StartDate,
            EndDate=e.EndDate,
            AdministratorId = e.AdministratorId,
            AdministratorName = GetAdministratorName(e.AdministratorId),
            IsApprove = e.IsApprove,
            PhoneNumbers = e.PhoneNumbers,
            TotalPhoneNumber = TotalPhoneNumber(e.Id)
        }).ToList();
    }

    public List<vwSchedulePromotion> GetAllViewSchedulePromotion(string CustomerPhone)
    {
        return (from e in db.SchedulePromotions where e.PhoneNumbers.Contains(CustomerPhone) && e.IsApprove == true
                select new vwSchedulePromotion
                {
                    Id = e.Id,
                    UPICode = e.UpiCode,
                    Title = e.Title,
                    SMSContent = e.SMSContent,
                    WebContent = e.WebContent,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    AdministratorId = e.AdministratorId,
                    AdministratorName = GetAdministratorName(e.AdministratorId),
                    IsApprove = e.IsApprove,
                    PhoneNumbers = e.PhoneNumbers,
                    TotalPhoneNumber = TotalPhoneNumber(e.Id)
                }).ToList();
    }
    public DataTable GetListPhonePromotion(string CustomerPhone,int from)
    {
        List<vwSMS> promitionIdList = new List<vwSMS>();
        SMSObjRepository SRepo=new SMSObjRepository();
        List<vwSchedulePromotion> viewSchedule = new List<vwSchedulePromotion>();
        if (from == Constant.inbox)
            promitionIdList = SRepo.GetInboxSMS(CustomerPhone);
        else
        {
            if(from==Constant.outbox)
                promitionIdList = SRepo.GetOutboxSMS(CustomerPhone);
        }

        string protionId = "0,";
        foreach(var item in promitionIdList)
        {
            protionId += item.PromotionId + ",";
        }
        protionId=protionId.Substring(0, protionId.Length - 1);

        return GetPromotionByIdList(protionId);
    }
    public DataTable GetPromotionByIdList(string idList)
    {
        string sql = "select Id, Title from SchedulePromotion where id in (" + idList + ") group by Id, Title order by Id";
        Utility U = new Utility();
        return U.GetList(sql);
    }
    public int TotalPhoneNumber(int ScheduleId)
    {
        var smsList= (from e in db.SchedulePromotions where e.Id==ScheduleId select e.PhoneNumbers).FirstOrDefault();
        char[] separator = new char[] { ',' };
        string[] phoneList = smsList.Split(separator);
        return phoneList.Count();
    }
    public SchedulePromotion GetSchedulePromotionById(int Id)
    {
        return (from a in db.SchedulePromotions where a.Id==Id select a).SingleOrDefault();
    }

    public string GetAdministratorName(int? adminId)
    {
        return (from a in db.Administrators where a.Id==adminId select a.Fullname).SingleOrDefault();
    }

    public bool InsertSchedulePromotion(string UpiCode, string Title, string SMSContent,string WebContent, DateTime StartDate, 
        DateTime EndDate, int AdministratorId, bool IsApprove, string PhoneNumbers)
    {
        try
        {
            SchedulePromotion SP = new SchedulePromotion();
            SP.UpiCode = UpiCode;
            SP.Title = Title;
            SP.SMSContent = SMSContent;
            SP.WebContent = WebContent;
            SP.StartDate = StartDate;
            SP.EndDate = EndDate;
            SP.AdministratorId = AdministratorId;
            SP.IsApprove = IsApprove;
            SP.PhoneNumbers = PhoneNumbers;
            db.SchedulePromotions.InsertOnSubmit(SP);
            db.SubmitChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool UpdateSchedulePromotion(int Id, string UpiCode, string Title,  string SMSContent,string WebContent, DateTime StartDate, DateTime EndDate, int AdministratorId, bool IsApprove)
    {
        try
        {
            var o = (from a in db.SchedulePromotions where a.Id == Id select a).SingleOrDefault();
            if (o != null)
            {
                o.UpiCode = UpiCode;
                o.Title = Title;
                o.SMSContent = SMSContent;
                o.WebContent = WebContent;
                o.StartDate = StartDate;
                o.EndDate = EndDate;
                o.AdministratorId = AdministratorId;
                o.IsApprove = IsApprove;
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

    public bool DeleteSchedulePromotion(int Id)
    {
        try
        {
            var o =(from a in db.SchedulePromotions where a.Id==Id select a).SingleOrDefault();
            if (o != null)
            {
                db.SchedulePromotions.DeleteOnSubmit(o);
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

    public string GetPhoneNumberOfSchedulePromotion(int Id)
    {
        try
        {
            var o = (from a in db.SchedulePromotions where a.Id == Id && a.IsApprove==true select a).SingleOrDefault();
            if (o != null)
            {
                
                return o.PhoneNumbers;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public List<vwSchedulePromotion> Filter(DateTime StartDate, DateTime EndDate, int Approve)
    {
        List<vwSchedulePromotion> List = new List<vwSchedulePromotion>();
        if(Approve==1)
            List = (from e in db.SchedulePromotions
                    where e.StartDate >= StartDate && e.EndDate <= EndDate && e.IsApprove == true
                    select new vwSchedulePromotion
                    {
                        Id = e.Id,
                        UPICode = e.UpiCode,
                        Title = e.Title,
                        SMSContent = e.SMSContent,
                        WebContent = e.WebContent,
                        StartDate = e.StartDate,
                        EndDate = e.EndDate,
                        AdministratorId = e.AdministratorId,
                        AdministratorName = GetAdministratorName(e.AdministratorId),
                        IsApprove = e.IsApprove,
                        PhoneNumbers = e.PhoneNumbers,
                        TotalPhoneNumber = TotalPhoneNumber(e.Id)
                    }).ToList();
        if(Approve==2)
            List = (from e in db.SchedulePromotions
                    where e.StartDate >= StartDate && e.EndDate <= EndDate
                    select new vwSchedulePromotion
                    {
                        Id = e.Id,
                        UPICode = e.UpiCode,
                        Title = e.Title,
                        SMSContent = e.SMSContent,
                        WebContent = e.WebContent,
                        StartDate = e.StartDate,
                        EndDate = e.EndDate,
                        AdministratorId = e.AdministratorId,
                        AdministratorName = GetAdministratorName(e.AdministratorId),
                        IsApprove = e.IsApprove,
                        PhoneNumbers = e.PhoneNumbers,
                        TotalPhoneNumber = TotalPhoneNumber(e.Id)
                    }).ToList();
        if(Approve==0)
            List = (from e in db.SchedulePromotions
                    where e.StartDate >= StartDate && e.EndDate <= EndDate && e.IsApprove == false
                    select new vwSchedulePromotion
                    {
                        Id = e.Id,
                        UPICode = e.UpiCode,
                        Title = e.Title,
                        SMSContent = e.SMSContent,
                        WebContent = e.WebContent,
                        StartDate = e.StartDate,
                        EndDate = e.EndDate,
                        AdministratorId = e.AdministratorId,
                        AdministratorName = GetAdministratorName(e.AdministratorId),
                        IsApprove = e.IsApprove,
                        PhoneNumbers = e.PhoneNumbers,
                        TotalPhoneNumber = TotalPhoneNumber(e.Id)
                    }).ToList();
        return List.ToList();
    }

    public bool ApproveSchedulePromotion(int Id)
    {
        SchedulePromotion a = db.SchedulePromotions.Single(e=> e.Id == Id);
        try
        {
            if (a != null)
            {
                a.IsApprove = true;
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
}