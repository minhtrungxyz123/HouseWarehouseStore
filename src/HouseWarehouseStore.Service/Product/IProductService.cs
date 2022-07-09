using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;

namespace HouseWarehouseStore.Service
{
    public interface IProductService
    {
        Task<List<Product>> GetAll(bool showHidden = true);
        Task<ApiResult<Pagination<ProductModel>>> GetAllPaging(ProductSearchContext ctx);
        Task<ApiResult<Product>> GetProductDetail(string id);
    }
}