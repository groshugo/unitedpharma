using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PromotionSearchHistoryRepository
/// </summary>
public class PromotionSearchHistoryRepository
{
    private UPIDataContext db;
    public PromotionSearchHistoryRepository()
	{
        db = new UPIDataContext();
	}

    public List<PromotionSearchHistory> GetSearchHistoryByPromoId(int promoId)
    {
        return (from e in db.PromotionSearchHistories where e.PromotionId == promoId 
                select e).ToList();
    }

    public bool AddPromotionSearchHistory(PromotionHistoryFilterOption promotionHistoryFilterOption, string fullName, 
        string customerType, string channel, int groupId, int regionId, int areaId, int localId, int promoId, string searchResults)
    {
        var searchCriteria = string.Empty;
        if (promotionHistoryFilterOption == PromotionHistoryFilterOption.Customer)
        {
            searchCriteria = string.Format(Constant.CustomerPromotionHistoryTemplateString,
                                           (int)PromotionHistoryFilterOption.Customer,
                                           fullName, customerType, channel, groupId, regionId, areaId, localId);
        }
        else
        {
            searchCriteria = string.Format(Constant.CustomerPromotionHistoryTemplateString,
                                           (int)PromotionHistoryFilterOption.Sales,
                                           fullName, groupId, regionId, areaId, localId);
        }
        var promoHistory = new PromotionSearchHistory
                               {PromotionId = promoId, SearchCriteria = searchCriteria, SearchResults = searchResults};

        return AddPromotionSearchHistory(promoHistory);
    }

    public bool AddPromotionSearchHistory(PromotionSearchHistory promotionSearchHistory)
    {
        try
        {
            db.PromotionSearchHistories.InsertOnSubmit(promotionSearchHistory);
            db.SubmitChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
}