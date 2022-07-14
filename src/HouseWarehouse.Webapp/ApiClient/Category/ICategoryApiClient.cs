using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace HouseWarehouse.Webapp.ApiClient
{
    public interface ICategoryApiClient
    {
        Task<List<ProductCategoryModel>> GetAll(bool showHidden = true);

        Task<ApiResult<Pagination<ProductCategoryModel>>> Get(ProductCategorySearchModel request);
    }
}