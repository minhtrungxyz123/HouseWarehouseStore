using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace Master.Webapp.ApiClient
{
    public interface IArticlesApiClient
    {
        public Task<bool> Create(ArticleModel request);

        public Task<bool> CreateImage(FilesModel request, string articleId);

        public Task<bool> UpdateImage(FilesModel request, string articleId);

        public Task<bool> Edit(string? id, ArticleModel request);

        Task<ApiResult<Pagination<ArticleModel>>> Get(ArticleSearchModel request);

        Task<ApiResult<ArticleModel>> GetById(string id);

        Task<bool> Delete(string id);

        Task<bool> DeleteDataFiles(string id);

        Task<List<FilesModel>> GetFilesArticles(int take);

        Task<bool> DeleteFiles(string id);

        Task<List<ArticleModel>> GetAll();

        Task<IList<ArticleCategoryModel>> GetCategory(bool showHidden = true);
    }
}