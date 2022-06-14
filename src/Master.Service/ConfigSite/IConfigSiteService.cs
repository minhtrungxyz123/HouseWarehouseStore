using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;

namespace Master.Service
{
    public interface IConfigSiteService
    {
        Task<IEnumerable<ConfigSite>> GetAll();

        Task<ApiResult<Pagination<ConfigSiteModel>>> GetAllPaging(ConfigSiteSearchContext ctx);

        Task<ConfigSite> GetById(string? id);

        Task<RepositoryResponse> Create(ConfigSiteModel model);

        Task<RepositoryResponse> Update(string? id, ConfigSiteModel model);

        Task<int> Delete(string? id);

        Task<ApiResult<ConfigSite>> GetByIdAsyn(string? id);
    }
}