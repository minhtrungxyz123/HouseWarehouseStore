using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;

namespace Master.Service
{
    public interface IArticleService
    {
        Task<IEnumerable<Article>> GetAll();

        Task<ApiResult<Pagination<ArticleModel>>> GetAllPaging(ArticleSearchContext ctx);

        Task<Article> GetById(string? id);

        Task<RepositoryResponse> Create(ArticleModel model);

        Task<RepositoryResponse> Update(string? id, ArticleModel model);

        Task<int> Delete(string? id);

        Task<ApiResult<Article>> GetByIdAsyn(string? id);

        IList<Article> GetActive(bool showHidden = true);

        IList<Article> GetHome(bool showHidden = true);

        IList<Article> GetHot(bool showHidden = true);
    }
}