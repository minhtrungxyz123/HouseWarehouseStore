using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace Master.Webapp.ApiClient
{
    public interface IProductCategoryApiCient
    {
        public Task<bool> Create(ProductCategoryModel request);

        public Task<bool> CreateImage(FilesModel request, string productCategoryId);

        public Task<bool> CreateImageConver(FilesModel request, string productCategoryId);

        public Task<bool> UpdateImage(FilesModel request, string productCategoryId);

        public Task<bool> UpdateImageCover(FilesModel request, string productCategoryId);

        public Task<bool> Edit(string? id, ProductCategoryModel request);

        Task<ApiResult<Pagination<ProductCategoryModel>>> Get(ProductCategorySearchModel request);

        Task<ApiResult<ProductCategoryModel>> GetById(string id);

        Task<bool> Delete(string id);

        Task<bool> DeleteDataFiles(string id);

        Task<bool> DeleteDataFilesCover(string id);

        Task<List<FilesModel>> GetFilesProductCategory(int take);

        Task<List<FilesModel>> GetFilesCoverProductCategory(int take);

        Task<bool> DeleteFiles(string id);

        Task<bool> DeleteFilesCover(string id);

        Task<List<ProductCategoryModel>> GetAll();
    }
}
