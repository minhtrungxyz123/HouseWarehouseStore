using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace Master.Webapp.ApiClient
{
    public interface IConfigSiteApiClient
    {
        public Task<bool> Create(ConfigSiteModel request);

        public Task<bool> CreateImage(FilesModel request, string configSiteId);

        public Task<bool> UpdateImage(FilesModel request, string configSiteId);

        public Task<bool> Edit(string? id, ConfigSiteModel request);

        Task<ApiResult<Pagination<ConfigSiteModel>>> Get(ConfigSiteSearchModel request);

        Task<ApiResult<ConfigSiteModel>> GetById(string id);

        Task<bool> Delete(string id);

        Task<bool> DeleteDataFiles(string id);

        Task<List<FilesModel>> GetFilesConfigSite(int take);

        Task<bool> DeleteFiles(string id);

        Task<List<ConfigSiteModel>> GetAll();
    }
}
