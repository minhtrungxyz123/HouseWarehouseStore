using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace Master.Webapp.ApiClient
{
    public interface IAlbumApiClient
    {
        public Task<bool> Create(AlbumModel request);

        public Task<bool> CreateImage(FilesModel request, string albumId);

        public Task<bool> UpdateImage(FilesModel request, string albumId);

        public Task<bool> Edit(string id, AlbumModel request);

        Task<ApiResult<Pagination<AlbumModel>>> Get(AlbumSearchModel request);

        Task<ApiResult<AlbumModel>> GetById(string id);

        Task<bool> Delete(string id);

        Task<bool> DeleteDataFiles(string id);

        Task<List<FilesModel>> GetFilesAlbum(int take);

        Task<bool> DeleteFiles(string id);
    }
}