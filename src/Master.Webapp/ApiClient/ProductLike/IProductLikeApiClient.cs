using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace Master.Webapp.ApiClient
{
    public interface IProductLikeApiClient
    {
        Task<ApiResult<Pagination<ProductLikeModel>>> Get(ProductLikeSearchModel request);
    }
}