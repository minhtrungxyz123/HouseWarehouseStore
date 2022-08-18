using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;

namespace Master.Service
{
    public interface IAlbumSevice
    {
        Task<IEnumerable<Album>> GetAll();

        Task<ApiResult<Pagination<AlbumModel>>> GetAllPaging(AlbumSearchContext ctx);

        Task<Album> GetById(string id);

        Task<RepositoryResponse> Create(AlbumModel model);

        Task<RepositoryResponse> Update(string id, AlbumModel model);

        Task<int> Delete(string? id);

        Task<ApiResult<Album>> GetByIdAsyn(string? id);

        IList<Album> GetActive(bool showHidden = true);

        IList<Album> GetHome(bool showHidden = true);
    }
}