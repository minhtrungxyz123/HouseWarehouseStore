using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;

namespace Master.Service
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();

        Task<ApiResult<Pagination<ProductModel>>> GetAllPaging(ProductSearchContext ctx);

        Task<Product> GetById(string? id);

        Task<RepositoryResponse> Create(ProductModel model);

        Task<RepositoryResponse> Update(string? id, ProductModel model);

        Task<int> Delete(string? id);

        Task<ApiResult<Product>> GetByIdAsyn(string? id);

        IList<Product> GetActive(bool showHidden = true);

        IList<Product> GetHome(bool showHidden = true);

        IList<Product> GetHot(bool showHidden = true);

        IList<Product> GetStatusProduct(bool showHidden = true);
    }
}