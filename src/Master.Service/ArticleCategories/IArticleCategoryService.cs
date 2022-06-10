using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;

namespace Master.Service
{
    public interface IArticleCategoryService
    {
        Task<IEnumerable<ArticleCategory>> GetAll();

        Task<ApiResult<Pagination<ArticleCategory>>> GetAllPaging(ArticleCategorySearchContext ctx);

        Task<ArticleCategory> GetById(string? id);

        Task<RepositoryResponse> Create(ArticleCategoryModel model);

        Task<RepositoryResponse> Update(string? id, ArticleCategoryModel model);

        Task<int> Delete(string? id);

        Task<ApiResult<ArticleCategory>> GetByIdAsyn(string? id);

        IList<ArticleCategory> GetCategoryActive(bool showHidden = true);

        IList<ArticleCategory> GetShowHome(bool showHidden = true);

        IList<ArticleCategory> GetShowMenu(bool showHidden = true);

        IList<ArticleCategory> GetHot(bool showHidden = true);
    }
}