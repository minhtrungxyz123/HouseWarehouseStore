using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace Master.Webapp.ApiClient
{
    public interface IArticleCategoryApiClient
    {
        public Task<bool> Create(ArticleCategoryModel request);

        public Task<bool> Edit(string? id, ArticleCategoryModel request);

        Task<ApiResult<Pagination<ArticleCategoryModel>>> Get(ArticleCategorySearchModel request);

        Task<ApiResult<ArticleCategoryModel>> GetById(string id);

        Task<bool> Delete(string id);
    }
}
