using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace Master.Webapp.ApiClient
{
    public interface IArticlesApiClient
    {
        public Task<bool> Create(ArticleModel request);

        public Task<bool> CreateImage(FilesModel request, string articlesId);

        public Task<bool> UpdateImage(FilesModel request, string articlesId);

        public Task<bool> Edit(string? id, ArticleModel request);

        Task<ApiResult<Pagination<ArticleModel>>> Get(ArticleSearchModel request);

        Task<ApiResult<ArticleModel>> GetById(string id);

        Task<bool> Delete(string id);

        Task<bool> DeleteDataFiles(string id);

        Task<List<FilesModel>> GetFilesArticle(int take);

        Task<bool> DeleteFiles(string id);
    }
}