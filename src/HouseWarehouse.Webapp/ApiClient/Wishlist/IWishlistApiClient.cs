using HouseWarehouseStore.Models;

namespace HouseWarehouse.Webapp.ApiClient
{
    public interface IWishlistApiClient
    {
        Task<List<ProductLikeModel>> GetAll(string idProd, string idMember);
    }
}
