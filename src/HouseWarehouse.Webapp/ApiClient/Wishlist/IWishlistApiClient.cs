using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace HouseWarehouse.Webapp.ApiClient
{
    public interface IWishlistApiClient
    {
        Task<List<ProductLikeModel>> GetAll(string idProd, string idMember);

        Task<ApiResult<Pagination<ProductLikeModel>>> Get(ProductLikeSearchModel request);

        public Task<bool> Create(ProductLikeModel request);
    }
}