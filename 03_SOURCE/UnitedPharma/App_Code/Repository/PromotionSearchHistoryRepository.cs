using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

/// <summary>
/// Summary description for PromotionSearchHistoryRepository
/// </summary>
public class PromotionSearchHistoryRepository
{
    // Fields
    private UPIDataContext db = new UPIDataContext();

    // Methods
    public bool AddPromotionSearchHistory(PromotionSearchHistory promotionSearchHistory)
    {
        try
        {
            this.db.PromotionSearchHistories.InsertOnSubmit(promotionSearchHistory);
            this.db.SubmitChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool DeletePromotionSearchHistory(string promoHistoryId, out string selectedPhone)
    {
        try
        {
            selectedPhone = string.Empty;

            var guid = new Guid(promoHistoryId);
            var promo = (from o in db.PromotionSearchHistories where o.Id == guid select o).SingleOrDefault();

            if (promo != null)
            {
                selectedPhone = promo.SearchResults;
                this.db.PromotionSearchHistories.DeleteOnSubmit(promo);
                this.db.SubmitChanges();
                return true;
            }
            return false;
        }
        catch
        {
            selectedPhone = string.Empty;
            return false;
        }
    }

    public bool AddPromotionSearchHistory(PromotionHistoryFilterOption promotionHistoryFilterOption, string fullName,
        int customerTypeId, int channelId, int groupId, int regionId, int areaId, int localId, int promoId, string searchResults)
    {
        var promotionSearchHistory = CreatePromoSearchHistory(promotionHistoryFilterOption, fullName, customerTypeId, channelId, groupId, 
            regionId, areaId, localId, promoId, searchResults);

        return this.AddPromotionSearchHistory(promotionSearchHistory);
    }

    public PromotionSearchHistory CreatePromoSearchHistory(PromotionHistoryFilterOption promotionHistoryFilterOption, string fullName, 
        int customerTypeId, int channelId, int groupId, int regionId, int areaId, int localId, int promoId, string searchResults)
    {
        string searchLiteral = GetSearchHistoryLiteral((int)promotionHistoryFilterOption, fullName, customerTypeId, channelId,
            groupId, regionId, areaId, localId);

        string str = string.Empty;
        if (promotionHistoryFilterOption == PromotionHistoryFilterOption.Customer)
        {
            str = string.Format(Constant.PromotionHistoryTemplateString,
                new object[] { 0, fullName, customerTypeId, channelId, groupId, regionId, areaId, localId });
        }
        else
        {
            str = string.Format(Constant.PromotionHistoryTemplateString,
                 new object[] { 0, fullName, string.Empty, string.Empty, groupId, regionId, areaId, localId });
        }
        PromotionSearchHistory promotionSearchHistory = new PromotionSearchHistory
        {
            Id = Guid.NewGuid(),
            PromotionId = promoId,
            SearchCriteria = str,
            SearchResults = searchResults,
            SearchCriteriaLiteral = searchLiteral
        };
        return promotionSearchHistory;
    }



    public List<PromotionSearchHistory> GetSearchHistoryByPromoId(int promoId)
    {
        return (from e in this.db.PromotionSearchHistories
                where e.PromotionId == promoId
                orderby e.CreatedDate
                select e).ToList<PromotionSearchHistory>();
    }

    private string GetSearchHistoryLiteral(int searchOpt, string fullName, int customerTypeId, int channelId, int groupId,
        int regionId, int areaId, int localId)
    {
        var searchLiteral = new StringBuilder();

        searchLiteral.AppendFormat("Search on: {0}. ", searchOpt == 1 ? "Customer" : "Sales");

        if (!string.IsNullOrEmpty(fullName))
        {
            searchLiteral.AppendFormat("Fullname: {0}. ", fullName);
        }

        if (customerTypeId != 0)
        {
            var customerTypeObject = this.db.CustomerTypes.Single(ct => ct.Id == customerTypeId);
            if (customerTypeObject != null)
                searchLiteral.AppendFormat("Customer Type: {0}. ", customerTypeObject.TypeName);
        }

        if (channelId != 0)
        {
            var channelObject = this.db.Channels.Single(cn => cn.Id == channelId);
            if (channelObject != null)
                searchLiteral.AppendFormat("Channel: {0}. ", channelObject.ChannelName);
        }

        if (groupId != 0)
        {
            var groupObject = this.db.Groups.Single(cn => cn.Id == groupId);
            if (groupObject != null)
                searchLiteral.AppendFormat("Group: {0}. ", groupObject.GroupName);
        }

        if (regionId != 0)
        {
            var regionObject = this.db.Regions.Single(cn => cn.Id == regionId);
            if (regionObject != null)
                searchLiteral.AppendFormat("Region: {0}. ", regionObject.RegionName);
        }

        if (areaId != 0)
        {
            var areaObject = this.db.Areas.Single(cn => cn.Id == areaId);
            if (areaObject != null)
                searchLiteral.AppendFormat("Area: {0}. ", areaObject.AreaName);
        }

        if (localId != 0)
        {
            var localObject = this.db.Locals.Single(cn => cn.Id == localId);
            if (localObject != null)
                searchLiteral.AppendFormat("Local: {0}. ", localObject.LocalName);
        }

        return searchLiteral.ToString().TrimEnd(new char[] { '.', ' ' });
    }
}
