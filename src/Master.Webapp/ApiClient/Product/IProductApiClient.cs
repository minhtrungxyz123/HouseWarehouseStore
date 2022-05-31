using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace Master.Webapp.ApiClient
{
    public interface IProductApiClient
    {
        public Task<bool> Create(ProductModel request);

        public Task<bool> Edit(int? id, ProductModel request);

        Task<ApiResult<Pagination<ProductModel>>> Get(ProductSearchModel request);

        Task<ApiResult<ProductModel>> GetById(int id);

        Task<bool> Delete(int id);
    }
}
