using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class SMSObjRepository
{
    private UPIDataContext db;
    private List<vwSMS> lstVwSms;
    public SMSObjRepository()
    {
        db = new UPIDataContext();
        lstVwSms = GetAllView();
    }

    public List<SmsObj> GetAll()
    {
        return (from e in db.SmsObjs select e).ToList();
    }

    public List<vwSMS> GetAllView()
    {
        return (from e in db.SmsObjs
                where e.IsDeleted == false && e.IsSendSuccess == true
                select new vwSMS
                {
                    Id = e.Id,
                    SMSCode = e.SMSCode,
                    SmsParentId = e.ParentSmsId,
                    SenderPhone = e.SenderNumber,
                    SenderType = e.SenderType,
                    ReceiverPhone = e.ReceiverNumber,
                    ReceiverType = e.ReceiverType,
                    Date = e.Date,
                    Subject = e.Subject,
                    Content = e.Content,
                    IsSendSuccess = e.IsSendSuccess,
                    IsDelete = e.IsDeleted,
                    IsRead = e.IsRead,
                    SmsTypeId = e.SmsTypeId,
                    PromotionId = e.PromotionId,
                    SenderId = GetId(e.SenderType, e.SenderNumber),
                    SenderName = GetName(e.SenderType, e.SenderNumber),
                    ReceiverId = GetId(e.ReceiverType, e.ReceiverNumber),
                    ReceiverName = GetName(e.ReceiverType, e.ReceiverNumber)
                }).ToList();
    }

    public List<vwSMS> GetInboxSMS(string Phone)
    {
        return (from e in db.SmsObjs
                where e.ReceiverNumber.Contains(Phone) && e.IsDeleted == false && e.IsSendSuccess == true
                orderby e.Id descending
                select  new vwSMS
                {
                    Id = e.Id,
                    SMSCode = e.SMSCode,
                    SmsParentId = e.ParentSmsId,
                    SenderPhone = e.SenderNumber,
                    SenderType = e.SenderType,
                    ReceiverPhone = e.ReceiverNumber,
                    ReceiverType = e.ReceiverType,
                    Date = e.Date,
                    CountParentSMS = CountParentSMS1(e.Id),
                    Subject = CountParentSMS1WithSubject(e.Id,e.Subject),
                    Content = e.Content,
                    IsSendSuccess = e.IsSendSuccess,
                    IsDelete = e.IsDeleted,
                    IsRead = e.IsRead,
                    SmsTypeId = e.SmsTypeId,
                    PromotionId = e.PromotionId,
                    SenderId = GetId(e.SenderType, e.SenderNumber),
                    SenderName = GetName(e.SenderType, e.SenderNumber),
                    ReceiverId = GetId(e.ReceiverType, e.ReceiverNumber),
                    ReceiverName = GetName(e.ReceiverType, e.ReceiverNumber)
                }).ToList();
    }

    public List<vwSMS> GetInboxSMSByDate(string Phone, string dt)
    {
        var lst = GetInboxSMS(Phone);
        return (from sms in lst where ((DateTime)sms.Date).ToShortDateString().Replace("/", "") == dt select sms).ToList();
    }

    public List<vwSMS> GetOutboxSMS(string Phone)
    {
        return (from e in db.SmsObjs
                where String.Equals(e.SenderNumber, Phone) && e.IsDeleted == false && e.IsSendSuccess == true
                orderby e.Date descending
                select new vwSMS
                {
                    Id = e.Id,
                    SMSCode = e.SMSCode,
                    SmsParentId = e.ParentSmsId,
                    SenderPhone = e.SenderNumber,
                    SenderType = e.SenderType,
                    ReceiverPhone = e.ReceiverNumber,
                    ReceiverType = e.ReceiverType,
                    Date = e.Date,
                    Subject = e.Subject,
                    Content = e.Content,
                    IsSendSuccess = e.IsSendSuccess,
                    IsDelete = e.IsDeleted,
                    IsRead = e.IsRead,
                    SmsTypeId = e.SmsTypeId,
                    PromotionId = e.PromotionId,
                    SenderId = GetId(e.SenderType, e.SenderNumber),
                    SenderName = GetName(e.SenderType, e.SenderNumber),
                    ReceiverId = GetId(e.ReceiverType, e.ReceiverNumber),
                    ReceiverName = GetName(e.ReceiverType, e.ReceiverNumber)
                }).ToList();
    }

    public List<vwSMS> GetDeletedSMS(string Phone)
    {
        return (from e in db.SmsObjs
                where e.SenderNumber==Phone || e.ReceiverNumber.Contains(Phone) && e.IsDeleted == true && e.IsSendSuccess == true
                orderby e.Date descending
                select new vwSMS
                {
                    Id = e.Id,
                    SMSCode = e.SMSCode,
                    SmsParentId = e.ParentSmsId,
                    SenderPhone = e.SenderNumber,
                    SenderType = e.SenderType,
                    ReceiverPhone = e.ReceiverNumber,
                    ReceiverType = e.ReceiverType,
                    Date = e.Date,
                    Subject = e.Subject,
                    Content = e.Content,
                    IsSendSuccess = e.IsSendSuccess,
                    IsDelete = e.IsDeleted,
                    IsRead = e.IsRead,
                    SmsTypeId = e.SmsTypeId,
                    PromotionId = e.PromotionId,
                    SenderId = GetId(e.SenderType, e.SenderNumber),
                    SenderName = GetName(e.SenderType, e.SenderNumber),
                    ReceiverId = GetId(e.ReceiverType, e.ReceiverNumber),
                    ReceiverName = GetName(e.ReceiverType, e.ReceiverNumber)
                }).ToList();
    }

    public List<vwContact> GetMyContact(int type, int id)
    {
        List<vwContact> rs = null;
        switch (type)
        {
            //admin
            case 0: rs = GetMyContactForAdmin(id); break;
            case 1: rs = GetMyContactForSale(id); break;
            case 2: rs = GetMyContactForCustomer(id); break;
        }
        return rs;
    }

    private List<vwContact> GetMyContactForAdmin(int id)
    {
        List<vwContact> lst = new List<vwContact>();
        var contactADM = (from e in db.Administrators where e.Id != id select new vwContact { Id = e.Id, FullName = e.Fullname, Phone = e.Phone, Type = 0 });
        foreach (vwContact i in contactADM)
        {
            lst.Add(i);
        }
        var contactSALE = (from e in db.Salesmens select new vwContact { Id = e.Id, FullName = e.FullName, Phone = e.Phone, Type = 1 });
        foreach (vwContact i in contactSALE)
        {
            lst.Add(i);
        }
        return lst;
    }

    private List<vwContact> GetMyContactForSale(int id)
    {
        return (from e in db.Salesmens where e.Id != id select new vwContact { Id = e.Id, FullName = e.FullName, Phone = e.Phone, Type = 1 }).ToList();
    }

    private List<vwContact> GetMyContactForCustomer(int id)
    {
        return null;
    }

    public List<vwContact> GetCustomerContact()
    {
        return (from e in db.Customers select new vwContact { Id = e.Id, FullName = e.FullName, Phone = e.Phone, Type = 2 }).ToList();
    }

    private string GetName(int? type, string Phone)
    {
        string name = "";

        if (string.IsNullOrEmpty(Phone)) return string.Empty;

        switch (type)
        {
            case 0: name = (from e in db.Administrators where String.Equals(e.Phone, Phone) select e.Fullname).SingleOrDefault(); break;
            case 1: name = (from e in db.Salesmens where String.Equals(e.Phone, Phone) select e.FullName).SingleOrDefault(); break;
            case 2: name = (from e in db.Customers where String.Equals(e.Phone, Phone) select e.FullName).SingleOrDefault(); break;
        }
        return name;
    }

    private int GetId(int? type, string Phone)
    {
        int Id = 0;
        
        if (string.IsNullOrEmpty(Phone)) return Id;

        switch (type)
        {
            case 0: Id = (from e in db.Administrators where String.Equals(e.Phone, Phone) select e.Id).SingleOrDefault(); break;
            case 1: Id = (from e in db.Salesmens where String.Equals(e.Phone, Phone) select e.Id).SingleOrDefault(); break;
            case 2: Id = (from e in db.Customers where String.Equals(e.Phone, Phone) select e.Id).SingleOrDefault(); break;
        }
        return Id;
    }

    public SmsObj GetById(int id)
    {
        return (from e in db.SmsObjs where e.Id == id select e).SingleOrDefault();
    }

    public bool InsertSMS(string SMSCode, int SmsParentId, string senderNumber, int SenderType, string ReceiverPhone, int ReceiverType, 
        DateTime Date, string Subject, string Content,
        bool IsSendSuccess, bool IsDelete, bool IsRead, int SmsTypeId, int PromotionId)
    {
        try
        {
            SmsObj sms = new SmsObj();
            sms.SMSCode = SMSCode;
            sms.SenderNumber = senderNumber;
            sms.ParentSmsId = SmsParentId;
            sms.SenderType = SenderType;
            sms.ReceiverNumber = ReceiverPhone;
            sms.ReceiverType = ReceiverType;
            sms.Date = Date;
            sms.Subject = Subject;
            sms.Content = Content;
            sms.IsSendSuccess = IsSendSuccess;
            sms.IsDeleted = IsDelete;
            sms.IsRead = IsRead;
            sms.SmsTypeId = SmsTypeId;
            sms.PromotionId = PromotionId;
            db.SmsObjs.InsertOnSubmit(sms);
            db.SubmitChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool Edit(int id, string SenderNumber, string ReceiverNumber, DateTime Date, string Content, bool IsSendSuccess, bool IsRead, bool Closed, int SmsTypeId,
        int SalesmenIdSender, int SalesmenIdReceiver, int CustomerIdSender, int CustomerIdReceiver)
    {
        try
        {
            var o = (from e in db.SmsObjs where e.Id == id select e).SingleOrDefault();
            if (o != null)
            {
                o.SenderNumber = SenderNumber;
                o.ReceiverNumber = ReceiverNumber;
                o.Date = Date;
                o.Content = Content;
                o.IsSendSuccess = IsSendSuccess;
                o.IsRead = IsRead;
                o.SmsTypeId = SmsTypeId;
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
        var o = (from e in db.SmsObjs where e.Id == id select e).SingleOrDefault();
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

    public bool DeleteFromTrash(int id)
    {
        var o = (from e in db.SmsObjs where e.Id == id select e).SingleOrDefault();
        if (o != null)
        {
            try
            {
                db.SmsObjs.DeleteOnSubmit(o);
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
        return true;
    }

    public List<vwSMS> FilterInboxSMS(int typeFilter, string value, string Phone)
    {
        var lstSMS = GetInboxSMS(Phone);
        switch (typeFilter)
        {
            case 0:
                return (from sms in lstSMS where sms.SenderName != null && sms.SenderName.ToLower().Contains(value.ToLower()) select sms).ToList();
            case 1:
                return (from sms in lstSMS where sms.SenderPhone != null && sms.SenderPhone.ToLower().Contains(value.ToLower()) select sms).ToList();
            default:
                return (from sms in lstSMS where sms.Subject != null && sms.Subject.ToLower().Contains(value.ToLower()) select sms).ToList();
        }
    }

    public List<vwSMS> FilterOutboxSMS(int typeFilter, string value, string Phone)
    {
        var lstSMS = GetOutboxSMS(Phone);
        if (typeFilter == 0) //From
        {
            return (from sms in lstSMS where sms.ReceiverName.ToLower().Contains(value.ToLower()) select sms).ToList();
        }
        else if (typeFilter == 1)//Phone
        {
            return (from sms in lstSMS where sms.ReceiverPhone.ToLower().Contains(value.ToLower()) select sms).ToList();
        }
        else
            return (from sms in lstSMS where sms.Subject.ToLower().Contains(value.ToLower()) select sms).ToList();
    }

    public List<vwSMS> FilterDeletedSMS(int typeFilter, string value, string Phone)
    {
        var lstSMS = GetDeletedSMS(Phone);
        if (typeFilter == 0) //From
        {
            return (from sms in lstSMS where sms.SenderName.ToLower().Contains(value.ToLower()) select sms).ToList();
        }
        else if (typeFilter == 1) //To
        {
            return (from sms in lstSMS where sms.ReceiverName.ToLower().Contains(value.ToLower()) select sms).ToList();
        }
        else
            return (from sms in lstSMS where sms.Subject.ToLower().Contains(value.ToLower()) select sms).ToList();
    }

    public List<vwSMS> FilterInboxSMSByDate(int typeFilter, string value, string Phone)
    {
        var lstSMS = GetInboxSMS(Phone);
        if (typeFilter == 0) //From
        {
            return (from sms in lstSMS where sms.SenderName.ToLower().Contains(value.ToLower()) select sms).ToList();
        }
        else if (typeFilter == 1)//Phone
        {
            return (from sms in lstSMS where sms.SenderPhone.ToLower().Contains(value.ToLower()) select sms).ToList();
        }
        else
            return (from sms in lstSMS where sms.Subject.ToLower().Contains(value.ToLower()) select sms).ToList();
    }

    public List<vwContact> FileterMyContact(string t, int typeFilter, string value, int userType, int id)
    {
        List<vwContact> lstContact = null;
        List<vwContact> lstRs = null;
        if (t == "1")
        {
            lstContact = GetMyContact(userType, id);
        }
        else
        {
            lstContact = GetCustomerContact();
        }
        switch (typeFilter)
        {
            case 0: lstRs = (from e in lstContact where e.FullName.Contains(value) select e).ToList(); break;
            case 1: lstRs = (from e in lstContact where e.Phone.Contains(value) select e).ToList(); break;
        }
        return lstRs;
    }

    public void GetListParentSMS(int SmsId, List<vwSMS> rs)
    {
        vwSMS sms = (from e in lstVwSms where e.Id == SmsId select e).SingleOrDefault();
        if (sms != null)
        {
            rs.Add(sms);
            if (sms.SmsParentId != 0)
            {
                GetListParentSMS((int)sms.SmsParentId, rs);
            }
        }
    }

    private int? CountParentSMS1(int SmsId)
    {
        List<vwSMS> rs = new List<vwSMS>();
        GetListParentSMS(SmsId, rs);
        return rs.Count;
    }

    private string CountParentSMS1WithSubject(int SmsId,string subject)
    {
        List<vwSMS> rs = new List<vwSMS>();
        GetListParentSMS(SmsId, rs);
        return subject+" ("+rs.Count.ToString()+")";
    }

    public bool CheckOwner(string Phone, int SmsId)
    {
        var o = (from e in db.SmsObjs where (String.Equals(e.SenderNumber, Phone) || e.ReceiverNumber.Contains(Phone)) && e.Id == SmsId select e).SingleOrDefault();
        return (o == null) ? false : true;
    }

    public void SetIsRead(int smsId)
    {
        var o = (from e in db.SmsObjs where e.Id == smsId select e).SingleOrDefault();
        if (o != null)
        {
            try
            {
                o.IsRead = true;
                db.SubmitChanges();
            }
            catch
            {

            }
        }
    }

    public void SetIsSendSuccess(int smsId)
    {
        var o = (from e in db.SmsObjs where e.Id == smsId select e).SingleOrDefault();
        if (o != null)
        {
            try
            {
                o.IsSendSuccess = true;
                db.SubmitChanges();
            }
            catch
            {

            }
        }
    }
    public List<vwSMS> GetSMSById(int smsId)
    {
        return (from e in db.SmsObjs
                where e.Id == smsId
                select new vwSMS
                {
                    Id = e.Id,
                    SMSCode = e.SMSCode,
                    SmsParentId = e.ParentSmsId,
                    SenderPhone = e.SenderNumber,
                    SenderType = e.SenderType,
                    ReceiverPhone = e.ReceiverNumber,
                    ReceiverType = e.ReceiverType,
                    Date = e.Date,
                    CountParentSMS = CountParentSMS1(e.Id),
                    Subject = e.Subject,
                    Content = e.Content,
                    IsSendSuccess = e.IsSendSuccess,
                    IsDelete = e.IsDeleted,
                    IsRead = e.IsRead,
                    SmsTypeId = e.SmsTypeId,
                    PromotionId = e.PromotionId,
                    SenderId = GetId(e.SenderType, e.SenderNumber),
                    SenderName = GetName(e.SenderType, e.SenderNumber),
                    ReceiverId = GetId(e.ReceiverType, e.ReceiverNumber),
                    ReceiverName = GetName(e.ReceiverType, e.ReceiverNumber)
                }).ToList();
    }
    public int CountSMSUnread(int adminId,string receiveNumber)
    {
        return (from e in db.SmsObjs where e.IsRead==false && e.ReceiverNumber==receiveNumber select e).Count();
    }

    public List<vwSMS> SMSFailure(string Phone)
    {
        return (from e in db.SmsObjs where String.Equals(e.SenderNumber, Phone) && e.IsSendSuccess==false select new vwSMS{
            Id = e.Id,
            SMSCode = e.SMSCode,
            SmsParentId = e.ParentSmsId,
            SenderPhone = e.SenderNumber,
            SenderType = e.SenderType,
            ReceiverPhone = e.ReceiverNumber,
            ReceiverType = e.ReceiverType,
            Date = e.Date,
            CountParentSMS = CountParentSMS1(e.Id),
            Subject = e.Subject,
            Content = e.Content,
            IsSendSuccess = e.IsSendSuccess,
            IsDelete = e.IsDeleted,
            IsRead = e.IsRead,
            SmsTypeId = e.SmsTypeId,
            PromotionId = e.PromotionId,
            SenderId = GetId(e.SenderType, e.SenderNumber),
            SenderName = GetName(e.SenderType, e.SenderNumber),
            ReceiverId = GetId(e.ReceiverType, e.ReceiverNumber),
            ReceiverName = GetName(e.ReceiverType, e.ReceiverNumber)
        }).ToList();
    }

    public List<vwSMS> GetSMSByPromotionId(string Phone,int PromotionId)
    {
        return (from e in db.SmsObjs
                where e.ReceiverNumber.Contains(Phone) && e.IsDeleted == false && e.IsSendSuccess == true && e.PromotionId == PromotionId
                orderby e.Id descending
                select new vwSMS
                {
                    Id = e.Id,
                    SMSCode = e.SMSCode,
                    SmsParentId = e.ParentSmsId,
                    SenderPhone = e.SenderNumber,
                    SenderType = e.SenderType,
                    ReceiverPhone = e.ReceiverNumber,
                    ReceiverType = e.ReceiverType,
                    Date = e.Date,
                    CountParentSMS = CountParentSMS1(e.Id),
                    Subject = CountParentSMS1WithSubject(e.Id, e.Subject),
                    Content = e.Content,
                    IsSendSuccess = e.IsSendSuccess,
                    IsDelete = e.IsDeleted,
                    IsRead = e.IsRead,
                    SmsTypeId = e.SmsTypeId,
                    PromotionId = e.PromotionId,
                    SenderId = GetId(e.SenderType, e.SenderNumber),
                    SenderName = GetName(e.SenderType, e.SenderNumber),
                    ReceiverId = GetId(e.ReceiverType, e.ReceiverNumber),
                    ReceiverName = GetName(e.ReceiverType, e.ReceiverNumber)
                }).ToList();
    }

    public List<vwSMS> GetOutboxSMSByPromotionId(string Phone, int PromotionId)
    {
        return (from e in db.SmsObjs
                where String.Equals(e.SenderNumber, Phone) && e.IsDeleted == false && e.IsSendSuccess == true && e.PromotionId==PromotionId
                orderby e.Date descending
                select new vwSMS
                {
                    Id = e.Id,
                    SMSCode = e.SMSCode,
                    SmsParentId = e.ParentSmsId,
                    SenderPhone = e.SenderNumber,
                    SenderType = e.SenderType,
                    ReceiverPhone = e.ReceiverNumber,
                    ReceiverType = e.ReceiverType,
                    Date = e.Date,
                    Subject = e.Subject,
                    Content = e.Content,
                    IsSendSuccess = e.IsSendSuccess,
                    IsDelete = e.IsDeleted,
                    IsRead = e.IsRead,
                    SmsTypeId = e.SmsTypeId,
                    PromotionId = e.PromotionId,
                    SenderId = GetId(e.SenderType, e.SenderNumber),
                    SenderName = GetName(e.SenderType, e.SenderNumber),
                    ReceiverId = GetId(e.ReceiverType, e.ReceiverNumber),
                    ReceiverName = GetName(e.ReceiverType, e.ReceiverNumber)
                }).ToList();
    }
}