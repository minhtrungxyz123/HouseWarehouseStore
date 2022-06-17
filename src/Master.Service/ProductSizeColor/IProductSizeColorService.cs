using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;

namespace Master.Service
{
    public interface IProductSizeColorService
    {
        Task<IEnumerable<ProductSizeColor>> GetAll();

        Task<ApiResult<Pagination<ProductSizeColor>>> GetAllPaging(ProductSizeColorSearchContext ctx);

        Task<ProductSizeColor> GetById(string? id);

        Task<RepositoryResponse> Create(ProductSizeColorModel model);

        Task<RepositoryResponse> Update(string? id, ProductSizeColorModel model);

        Task<int> Delete(string? id);

        Task<ApiResult<ProductSizeColor>> GetByIdAsyn(string? id);
    }
}