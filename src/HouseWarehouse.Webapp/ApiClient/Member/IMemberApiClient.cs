using HouseWarehouseStore.Models;

namespace HouseWarehouse.Webapp.ApiClient
{
    public interface IMemberApiClient
    {
        Task<MemberModel> GetCheckActive(string id, bool showHidden = true);
    }
}
