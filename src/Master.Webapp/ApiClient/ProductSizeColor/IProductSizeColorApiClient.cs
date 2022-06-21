using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace Master.Webapp.ApiClient
{
    public interface IProductSizeColorApiClient
    {
        public Task<bool> Create(ProductSizeColorModel request);

        public Task<bool> Edit(string? id, ProductSizeColorModel request);

        Task<ApiResult<Pagination<ProductSizeColorModel>>> Get(ProductSizeColorSearchModel request);

        Task<ApiResult<ProductSizeColorModel>> GetById(string id);

        Task<bool> Delete(string id);
    }
}