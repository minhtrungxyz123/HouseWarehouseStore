using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;
using Microsoft.EntityFrameworkCore;

namespace Master.Service
{
    public class ArticleService : IArticleService
    {
        #region Fields

        private readonly HouseWarehouseStoreDbContext _context;

        public ArticleService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region List

        public async Task<ApiResult<Article>> GetByIdAsyn(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Articles
                            .OrderByDescending(p => p.Description)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            var model = new Article()
            {
                Description = item.Description,
                TitleMeta = item.TitleMeta,
                Hot = item.Hot,
                ArticleCategoryId = item.ArticleCategoryId,
                Home = item.Home,
                Active = item.Active,
                Body = item.Body,
                CreateDate = item.CreateDate,
                DescriptionMetaTitle = item.DescriptionMetaTitle,
                Image = item.Image,
                KeyWord = item.KeyWord,
                Subject = item.Subject,
                Url = item.Url,
                View = item.View
            };
            return new ApiSuccessResult<Article>(model);
        }

        public async Task<IEnumerable<Article>> GetAll()
        {
            return await _context.Articles
                            .OrderByDescending(p => p.Description)
                            .ToListAsync();
        }

        public async Task<ApiResult<Pagination<ArticleModel>>> GetAllPaging(ArticleSearchContext ctx)
        {
            var query = from pr in _context.Articles
                        join c in _context.ArticleCategories on pr.ArticleCategoryId equals c.ArticleCategoryId into pt
                        from tp in pt.DefaultIfEmpty()
                        select new { pr, tp };

            if (!string.IsNullOrEmpty(ctx.Keyword))
            {
                query = query.Where(x => x.pr.Description.Contains(ctx.Keyword)
                || x.pr.TitleMeta.Contains(ctx.Keyword));
            }

            var totalRecords = await query.CountAsync();

            var items = await query.Skip((ctx.PageIndex - 1) * ctx.PageSize)
                .Take(ctx.PageSize)
                .Select(u => new ArticleModel()
                {
                    Description = u.pr.Description,
                    Id = u.pr.Id,
                    Active = u.pr.Active,
                    Home = u.pr.Home,
                    Hot = u.pr.Hot,
                    Image = u.pr.Image,
                    ArticleCategoryId = u.tp.CategoryName,
                    Body = u.pr.Body,
                    CreateDate = u.pr.CreateDate,
                    DescriptionMetaTitle = u.pr.DescriptionMetaTitle,
                    KeyWord = u.pr.KeyWord,
                    Subject = u.pr.Subject,
                    TitleMeta = u.pr.TitleMeta,
                    Url = u.pr.Url,
                    View = u.pr.View,
                })
                .ToListAsync();

            var pagination = new Pagination<ArticleModel>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageIndex = ctx.PageIndex,
                PageSize = ctx.PageSize,
            };

            return new ApiSuccessResult<Pagination<ArticleModel>>(pagination);
        }

        public async Task<Article?> GetById(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Articles
                            .OrderByDescending(p => p.Description)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            return item;
        }

        public IList<Article> GetActive(bool showHidden = true)
        {
            var query = from p in _context.Articles.AsQueryable() select p;
            if (showHidden)
            {
                query = from p in query where p.Active select p;
            }
            query = from p in query orderby p.Description select p;
            return query.ToList();
        }

        public IList<Article> GetHome(bool showHidden = true)
        {
            var query = from p in _context.Articles.AsQueryable() select p;
            if (showHidden)
            {
                query = from p in query where p.Home select p;
            }
            query = from p in query orderby p.Description select p;
            return query.ToList();
        }

        public IList<Article> GetHot(bool showHidden = true)
        {
            var query = from p in _context.Articles.AsQueryable() select p;
            if (showHidden)
            {
                query = from p in query where p.Hot select p;
            }
            query = from p in query orderby p.Hot select p;
            return query.ToList();
        }

        #endregion List

        #region Method

        public async Task<RepositoryResponse> Create(ArticleModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            Article item = new Article()
            {
                Id = Guid.NewGuid().ToString(),
                TitleMeta = model.TitleMeta,
                Hot = model.Hot,
                Description = model.Description,
                View = model.View,
                Url = model.Url,
                Subject = model.Subject,
                KeyWord = model.KeyWord,
                DescriptionMetaTitle = model.DescriptionMetaTitle,
                Active = model.Active,
                ArticleCategoryId = model.ArticleCategoryId,
                Body = model.Body,
                CreateDate = model.CreateDate,
                Home = model.Home,
                Image = model.Image
            };

            await _context.Articles.AddAsync(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.Id
            };
        }

        public async Task<RepositoryResponse> Update(string? id, ArticleModel model)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var item = await _context.Articles.FindAsync(id);
            item.DescriptionMetaTitle = model.DescriptionMetaTitle;
            item.TitleMeta = model.TitleMeta;
            item.Hot = model.Hot;
            item.Description = model.Description;
            item.View = model.View;
            item.Url = model.Url;
            item.Subject = model.Subject;
            item.KeyWord = model.KeyWord;
            item.DescriptionMetaTitle = model.DescriptionMetaTitle;
            item.Active = model.Active;
            item.ArticleCategoryId = model.ArticleCategoryId;
            item.Body = model.Body;
            item.CreateDate = model.CreateDate;
            item.Home = model.Home;
            item.Image = model.Image;

            _context.Articles.Update(item);

            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = id
            };
        }

        public async Task<int> Delete(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Articles.FindAsync(id);

            _context.Articles.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        #endregion Method
    }
}