using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class UnitedPharmaService
{
    // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
    // To create an operation that returns XML,
    //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
    //     and include the following line in the operation body:
    //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
    [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    public double CostOfSandwiches(int quantity)
    {
        return 1.25 * quantity;
    }

    [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    public bool AddPromotionBrowseHistory(int searchOption, string fullName, int customerTypeId, int channelId, int groupId, int regionId, 
        int areaId, int localId, int promoId, string searchResults)
    {
        if (HttpContext.Current.Session["objLogin"] == null) return false;

        bool flag = false;
        var promoHistoryRepo = new PromotionSearchHistoryRepository();

        PromotionHistoryFilterOption promotionHistoryFilterOption = RefineParameters(searchOption, ref customerTypeId, ref channelId);

        if (promoId == -1)
        {
            var promotionSearchHistory = promoHistoryRepo.CreatePromoSearchHistory(promotionHistoryFilterOption, fullName,
                    customerTypeId, channelId, groupId, regionId, areaId, localId, promoId, searchResults);

            AddPromotionSearchHIstoryToSession(promotionSearchHistory);
            flag = true;
        }
        else
        {
            flag = promoHistoryRepo.AddPromotionSearchHistory(promotionHistoryFilterOption, fullName, customerTypeId, channelId, groupId,
                regionId, areaId, localId, promoId, searchResults);
        }               

        return flag;
    }

    [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    public AjaxServiceResult<List<PromotionHistoryResult>> GetPromotionBrowseHistory(int promoId)
    {
        var result = new AjaxServiceResult<List<PromotionHistoryResult>>();

        if (HttpContext.Current.Session["objLogin"] == null) return result;

        if (promoId == -1)
        {
            var session = HttpContext.Current.Session;

            var historyResult = session[Constant.PromotionHistorySessionName] as List<PromotionHistoryResult>;
            
            result.Data = historyResult == null ? new List<PromotionHistoryResult>() : historyResult;            
            result.Status = AjaxServiceStatus.Success;

            return result;
        } 

        try
        {
            var promoHistoryRepo = new PromotionSearchHistoryRepository();
            var listPromoHistories = promoHistoryRepo.GetSearchHistoryByPromoId(promoId);

            var listResults = new List<PromotionHistoryResult>();

            foreach (PromotionSearchHistory history in listPromoHistories)
            {
                listResults.Add(ConvertFromPromotionSearchHistoryToPromotionHistoryResult(history));
            }

            result.Status = AjaxServiceStatus.Success;
            result.Data = listResults;
        }
        catch
        {
            result.Data = new List<PromotionHistoryResult>();
            result.Status = AjaxServiceStatus.Fail;
        }

        return result;
    }

    [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    public AjaxServiceResult<string> RemovePromotionHistory(string promoHistoryId, int promoId)
    {
        var result = new AjaxServiceResult<string>();

        if (HttpContext.Current.Session["objLogin"] == null) return result;

        var selectedPhone = string.Empty;
        var promoHistoryRepo = new PromotionSearchHistoryRepository();

        if (promoId == -1)
        {
            var session = HttpContext.Current.Session;
            var listPromoHis = session[Constant.PromotionHistorySessionName] as List<PromotionHistoryResult>;
            if (listPromoHis != null)
            {
                var deletedItem = listPromoHis.Find(e => e.Id == promoHistoryId);
                if (deletedItem != null)
                {
                    listPromoHis.Remove(deletedItem);
                    session[Constant.PromotionHistorySessionName] = listPromoHis;

                    result.Status = AjaxServiceStatus.Success;
                    result.Data = deletedItem.SearchResutls;
                }
            }
        }
        else
        {
            result.Status = promoHistoryRepo.DeletePromotionSearchHistory(promoHistoryId, out selectedPhone) ?
            AjaxServiceStatus.Success : AjaxServiceStatus.Fail;
            result.Data = selectedPhone;
        }        

        return result;
    }

    #region private

    private static PromotionHistoryResult ConvertFromPromotionSearchHistoryToPromotionHistoryResult(PromotionSearchHistory history)
    {
        PromotionHistoryResult hisResult = new PromotionHistoryResult();
        hisResult.Id = history.Id.ToString();
        hisResult.SearchCriteria = history.SearchCriteria;
        hisResult.SearchCriteriaLiteral = history.SearchCriteriaLiteral;
        hisResult.SearchResutls = history.SearchResults;
        return hisResult;
    }

    private static void AddPromotionSearchHIstoryToSession(PromotionSearchHistory promotionSearchHistory)
    {
        var session = HttpContext.Current.Session;
        List<PromotionHistoryResult> listPromoHis = session[Constant.PromotionHistorySessionName] as List<PromotionHistoryResult>;
        if (listPromoHis == null)
        {
            listPromoHis = new List<PromotionHistoryResult>();
        }

        listPromoHis.Add(ConvertFromPromotionSearchHistoryToPromotionHistoryResult(promotionSearchHistory));
        session[Constant.PromotionHistorySessionName] = listPromoHis;
    }

    private static PromotionHistoryFilterOption RefineParameters(int searchOption, ref int customerTypeId, ref int channelId)
    {
        PromotionHistoryFilterOption promotionHistoryFilterOption;
        if (searchOption == 1)
        {
            promotionHistoryFilterOption = PromotionHistoryFilterOption.Customer;
        }
        else
        {
            promotionHistoryFilterOption = PromotionHistoryFilterOption.Sales;
            customerTypeId = 0;
            channelId = 0;
        }
        return promotionHistoryFilterOption;
    }

    #endregion
}

[DataContract]
public class AjaxServiceResult<T>
{
    [DataMember]
    public T Data { get; set; }
    [DataMember]
    public string ErrorMessage { get; set; }
    [DataMember]
    public AjaxServiceStatus Status { get; set; }
}

[DataContract]
public class PromotionHistoryResult
{
    [DataMember]
    public string Id { get; set; }

    [DataMember]
    public string SearchCriteria { get; set; }

    [DataMember]
    public string SearchCriteriaLiteral { get; set; }
    
    [DataMember]
    public string SearchResutls { get; set; }
}


