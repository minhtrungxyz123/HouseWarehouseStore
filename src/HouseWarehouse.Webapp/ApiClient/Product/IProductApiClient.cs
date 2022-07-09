using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace HouseWarehouse.Webapp.ApiClient
{
    public interface IProductApiClient
    {
        Task<List<ProductModel>> GetAll();
        Task<ApiResult<Pagination<ProductModel>>> Get(ProductSearchModel request);
        Task<ApiResult<ProductModel>> GetProductDetail(string id);
        Task<List<FilesModel>> GetFilesProduct(int take, string id);
    }
}