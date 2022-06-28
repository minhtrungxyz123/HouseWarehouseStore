using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace Master.Webapp.ApiClient
{
    public interface IFilesApiClient
    {
        Task<ApiResult<Pagination<FilesModel>>> Get(FileSearchModel request);
    }
}
