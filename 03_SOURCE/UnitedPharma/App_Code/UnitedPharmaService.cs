using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace UnitedPharma
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class UnitedPharmaService : IUnitedPharmaService
    {
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public AjaxServiceResult<string> AddPromotionBrowseHistoryForCustomer(string fullName, string customerType,
                                                                              string channel,
                                                                              int groupId, int regionId, int areaId,
                                                                              int localId, int promoId,
                                                                              string searchResults)
        {
            var ajaxResult = new AjaxServiceResult<string>();

            var repo = new PromotionSearchHistoryRepository();
            var result = repo.AddPromotionSearchHistory(PromotionHistoryFilterOption.Customer, fullName, customerType,
                                                        channel,
                                                        groupId, regionId, areaId, localId, promoId, searchResults);

            ajaxResult.Status = result ? AjaxServiceStatus.Success : AjaxServiceStatus.Fail;
            ajaxResult.Data = "Test";

            return ajaxResult;
        }
    }
}
