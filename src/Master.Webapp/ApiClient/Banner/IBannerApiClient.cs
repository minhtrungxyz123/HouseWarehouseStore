using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace Master.Webapp.ApiClient
{
    public interface IBannerApiClient
    {
        public Task<bool> Create(BannerModel request);

        public Task<bool> CreateImage(FilesModel request, string bannerId);

        public Task<bool> UpdateImage(FilesModel request, string bannerId);

        public Task<bool> Edit(string? id, BannerModel request);

        Task<ApiResult<Pagination<BannerModel>>> Get(BannerSearchModel request);

        Task<ApiResult<BannerModel>> GetById(string id);

        Task<bool> Delete(string id);

        Task<bool> DeleteDataFiles(string id);

        Task<List<FilesModel>> GetFilesBanner(int take);

        Task<bool> DeleteFiles(string id);

        Task<List<BannerModel>> GetAll();
    }
}
