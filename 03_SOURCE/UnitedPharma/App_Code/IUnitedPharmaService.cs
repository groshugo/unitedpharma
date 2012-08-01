using System.ServiceModel;

namespace UnitedPharma
{
    [ServiceContract(Namespace = "UnitedPharma")]
    public interface IUnitedPharmaService
    {
        [OperationContract]
        AjaxServiceResult<string> AddPromotionBrowseHistoryForCustomer(string fullName, string customerType,
                                                                       string channel, int groupId, int regionId,
                                                                       int areaId, int localId, int promoId,
                                                                       string searchResults);
    }
}