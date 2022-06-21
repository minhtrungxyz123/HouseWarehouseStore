using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace Master.Webapp.ApiClient
{
    public interface IProductApiClient
    {
        public Task<bool> Create(ProductModel request);

        public Task<bool> CreateImage(FilesModel request, string productId);

        public Task<bool> UpdateImage(FilesModel request, string productId);

        public Task<bool> Edit(string? id, ProductModel request);

        Task<ApiResult<Pagination<ProductModel>>> Get(ProductSearchModel request);

        Task<ApiResult<ProductModel>> GetById(string id);

        Task<bool> Delete(string id);

        Task<bool> DeleteDataFiles(string id);

        Task<List<FilesModel>> GetFilesProduct(int take);

        Task<bool> DeleteFiles(string id);

        Task<List<ProductModel>> GetAll();

        Task<IList<ProductModel>> GetActive(bool showHidden = true);

    }
}