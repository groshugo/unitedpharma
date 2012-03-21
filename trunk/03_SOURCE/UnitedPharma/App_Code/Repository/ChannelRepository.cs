using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ChannelRepository
/// </summary>
public class ChannelRepository
{
    private UPIDataContext db;
    public ChannelRepository()
    {
        db = new UPIDataContext();
    }

    public List<Channel> GetAll()
    {
        return (from e in db.Channels select e).ToList();
    }

    public Channel GetChannelById(int id)
    {
        return (from e in db.Channels where e.Id == id select e).SingleOrDefault();
    }

    public List<vwChannel> GetAllViewChannel()
    {
        return (from a in db.Channels select new vwChannel { 
            Id = a.Id, 
            UpiCode = a.UpiCode, 
            ChannelName = a.ChannelName, 
            ParentChannelId = GetParentChannelId(a.ParentChannelId),
            ParentChannelName = GetParentChannelName(a.ParentChannelId) 
        }).ToList();
    }

    private int GetParentChannelId(int? parentId)
    {
        return parentId == null ? 0 : (int)parentId;
    }

    private string GetParentChannelName(int? parentId)
    {
        if (parentId == null)
        {
            return Constant.UNDEFINE;
        }
        else
        {
            return GetChannelNameById((int)parentId);
        }

    }
    public int GetChannelIdByName(string ChannelName)
    {
        var a = from e in db.Channels where e.ChannelName.Trim().ToLower() == ChannelName.Trim().ToLower() select e.Id;
        if (a.Count() > 0)
            return a.SingleOrDefault();
        else
        {
            return InsertDefault();
        }
    }
    public int InsertDefault()
    {
        var a = from e in db.Channels where e.ChannelName.Trim().ToLower() == "other" select e.Id;
        if (a.Count() > 0)
            return a.SingleOrDefault();
        else
        {
            Channel o = new Channel();
            o.UpiCode = "";
            o.ChannelName = "Other";
            o.ParentChannelId = 0;
            db.Channels.InsertOnSubmit(o);
            db.SubmitChanges();
            return o.Id;
        }
    }
    private string GetChannelNameById(int parentId)
    {
        var o = (from e in db.Channels where e.Id == parentId select e).SingleOrDefault();
        if (o != null)
        {
            return o.ChannelName;
        }
        else
        {
            return String.Empty;
        }
    }

    public bool Insert(string UPICode, string ChannelName, int? ParentChannelId)
    {
        try
        {
            if (CheckExistedChannel(ChannelName) == false)
            {
                Channel o = new Channel();
                o.UpiCode = UPICode;
                o.ChannelName = ChannelName;
                if (ParentChannelId != null)
                {
                    o.ParentChannelId = ParentChannelId;
                }
                db.Channels.InsertOnSubmit(o);
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
    public bool CheckExistedChannel(string ChannelName)
    {
        var o = (from e in db.Channels where e.ChannelName.Trim().ToLower() == ChannelName.Trim().ToLower() select e).Count();
        if (o > 0)
            return true;
        else
            return false;
    }
    public bool Update(int id, string UPICode, string ChannelName, int? ParentChannelId)
    {
        try
        {
            var o = (from e in db.Channels where e.Id == id select e).SingleOrDefault();
            if (o != null)
            {
                o.UpiCode = UPICode;
                o.ChannelName = ChannelName;
                if (ParentChannelId != null)
                {
                    o.ParentChannelId = ParentChannelId;
                }
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
        var o = (from e in db.Channels where e.Id == id select e).SingleOrDefault();
        if (o != null)
        {
            try
            {
                db.Channels.DeleteOnSubmit(o);
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

    public List<cbChannel> GetListParentChannel(int channelID)
    {
        List<cbChannel> lst = new List<cbChannel>();
        cbChannel first = new cbChannel();
        first.Id = 0;
        first.ChannelName = Constant.UNDEFINE;
        lst.Add(first);
        List<int> lstChildId = new List<int>();
        GetListChildChannelId(channelID, lstChildId);
        //channel don't have child channel
        if (lstChildId.Count == 0)
        {
            var lstChannel = (from e in db.Channels where e.Id != channelID select e).ToList();
            if (lstChannel.Count > 0)
            {
                foreach (Channel c in lstChannel)
                {
                    cbChannel item = new cbChannel();
                    item.Id = c.Id;
                    item.ChannelName = c.ChannelName;
                    lst.Add(item);
                }

            }
        }
        else // channel have child, and child channel can't show in parent channel combobox
        {
            var lstChannel = (from e in db.Channels where !lstChildId.Contains(e.Id) && e.Id != channelID select e).ToList();
            if (lstChannel.Count > 0)
            {
                foreach (Channel c in lstChannel)
                {
                    cbChannel item = new cbChannel();
                    item.Id = c.Id;
                    item.ChannelName = c.ChannelName;
                    lst.Add(item);
                }
            }
        }
        return lst;
    }

    public void GetListChildChannelId(int channelID, List<int> lstChildId)
    {
        var lst = (from e in db.Channels where e.ParentChannelId == channelID select e).ToList();
        if (lst.Count > 0)
        {
            foreach (Channel c in lst)
            {
                lstChildId.Add(c.Id);
                GetListChildChannelId(c.Id, lstChildId);
            }
        }
    }

    public bool CheckChannelRoot(int ChannelId)
    {
        var o = (from e in db.Channels where e.Id == ChannelId && e.ParentChannelId==0 select e.ParentChannelId).SingleOrDefault();
        if (o != null)
            return true;
        else
            return false;

    }
}