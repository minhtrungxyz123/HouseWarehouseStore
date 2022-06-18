using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;

namespace Master.Service
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategory>> GetAll();

        Task<ApiResult<Pagination<ProductCategoryModel>>> GetAllPaging(ProductCategorySearchContext ctx);

        Task<ProductCategory> GetById(string? id);

        Task<RepositoryResponse> Create(ProductCategoryModel model);

        Task<RepositoryResponse> Update(string? id, ProductCategoryModel model);

        Task<int> Delete(string? id);

        Task<ApiResult<ProductCategory>> GetByIdAsyn(string? id);

        IList<ProductCategory> GetActive(bool showHidden = true);

        IList<ProductCategory> GetHome(bool showHidden = true);

    }
}