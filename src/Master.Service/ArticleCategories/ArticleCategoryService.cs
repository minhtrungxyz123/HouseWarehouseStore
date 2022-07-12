using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;
using Microsoft.EntityFrameworkCore;

namespace Master.Service
{
    public class ArticleCategoryService : IArticleCategoryService
    {
        #region Fields

        private readonly HouseWarehouseStoreDbContext _context;

        public ArticleCategoryService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region List

        public async Task<ApiResult<ArticleCategory>> GetByIdAsyn(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.ArticleCategories
                            .OrderByDescending(p => p.CategoryName)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.ArticleCategoryId == id);

            var model = new ArticleCategory()
            {
                CategoryActive = item.CategoryActive,
                CategoryName = item.CategoryName,
                ArticleCategoryId = item.ArticleCategoryId,
                CategorySort = item.CategorySort,
                DescriptionMeta = item.DescriptionMeta,
                Hot = item.Hot,
                ParentId = item.ParentId,
                ShowHome = item.ShowHome,
                ShowMenu = item.ShowMenu,
                Slug = item.Slug,
                TitleMeta = item.TitleMeta,
                Url = item.Url,
            };
            return new ApiSuccessResult<ArticleCategory>(model);
        }

        public async Task<IEnumerable<ArticleCategory>> GetAll()
        {
            var getAll = await _context.ArticleCategories
                            .OrderByDescending(p => p.CategoryName)
                            .ToListAsync();
            return getAll;
        }

        public async Task<ApiResult<Pagination<ArticleCategory>>> GetAllPaging(ArticleCategorySearchContext ctx)
        {
            var query = _context.ArticleCategories.AsQueryable();
            if (!string.IsNullOrEmpty(ctx.Keyword))
            {
                query = query.Where(x => x.CategoryName.Contains(ctx.Keyword)
                || x.Url.Contains(ctx.Keyword)
                || x.DescriptionMeta.Contains(ctx.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query.Skip((ctx.PageIndex - 1) * ctx.PageSize)
                .Take(ctx.PageSize)
                .Select(x => new ArticleCategory()
                {
                    CategoryName = x.CategoryName,
                    Url = x.Url,
                    TitleMeta = x.TitleMeta,
                    Slug = x.Slug,
                    ShowMenu = x.ShowMenu,
                    ShowHome = x.ShowHome,
                    ParentId = x.ParentId,
                    Hot = x.Hot,
                    DescriptionMeta = x.DescriptionMeta,
                    ArticleCategoryId = x.ArticleCategoryId,
                    CategoryActive = x.CategoryActive,
                    CategorySort = x.CategorySort

                }).ToListAsync();

            var pagedResult = new Pagination<ArticleCategory>()
            {
                TotalRecords = totalRow,
                PageIndex = ctx.PageIndex,
                PageSize = ctx.PageSize,
                Items = data
            };
            return new ApiSuccessResult<Pagination<ArticleCategory>>(pagedResult);
        }

        public async Task<ArticleCategory?> GetById(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.ArticleCategories
                            .OrderByDescending(p => p.CategoryName)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.ArticleCategoryId == id);

            return item;
        }

        public IList<ArticleCategory> GetCategoryActive(bool showHidden = true)
        {
            var query = from p in _context.ArticleCategories.AsQueryable() select p;
            if (showHidden)
            {
                query = from p in query where p.CategoryActive select p;
            }
            query = from p in query orderby p.CategoryName select p;
            return query.ToList();
        }

        public IList<ArticleCategory> GetShowHome(bool showHidden = true)
        {
            var query = from p in _context.ArticleCategories.AsQueryable() select p;
            if (showHidden)
            {
                query = from p in query where p.ShowHome select p;
            }
            query = from p in query orderby p.CategoryName select p;
            return query.ToList();
        }

        public IList<ArticleCategory> GetShowMenu(bool showHidden = true)
        {
            var query = from p in _context.ArticleCategories.AsQueryable() select p;
            if (showHidden)
            {
                query = from p in query where p.ShowMenu select p;
            }
            query = from p in query orderby p.CategoryName select p;
            return query.ToList();
        }

        public IList<ArticleCategory> GetHot(bool showHidden = true)
        {
            var query = from p in _context.ArticleCategories.AsQueryable() select p;
            if (showHidden)
            {
                query = from p in query where p.Hot select p;
            }
            query = from p in query orderby p.CategoryName select p;
            return query.ToList();
        }

        #endregion List

        #region Method

        public async Task<RepositoryResponse> Create(ArticleCategoryModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            ArticleCategory item = new ArticleCategory()
            {
                CategoryName = model.CategoryName,
                ArticleCategoryId = Guid.NewGuid().ToString(),
                CategoryActive = model.CategoryActive,
                CategorySort = model.CategorySort,
                DescriptionMeta = model.DescriptionMeta,
                Hot = model.Hot,
                ParentId = model.ParentId,
                ShowHome = model.ShowHome,
                ShowMenu = model.ShowMenu,
                Slug = model.Slug,
                TitleMeta = model.TitleMeta,
                Url = model.Url
            };

            await _context.ArticleCategories.AddAsync(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.ArticleCategoryId,
            };
        }

        public async Task<RepositoryResponse> Update(string? id, ArticleCategoryModel model)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var item = await _context.ArticleCategories.FindAsync(id);
            item.CategoryName = model.CategoryName;
            item.CategoryActive = model.CategoryActive;
            item.CategorySort = model.CategorySort;
            item.DescriptionMeta = model.DescriptionMeta;
            item.Hot = model.Hot;
            item.ParentId = model.ParentId;
            item.ShowHome = model.ShowHome;
            item.ShowMenu = model.ShowMenu;
            item.Slug = model.Slug;
            item.TitleMeta = model.TitleMeta;
            item.Url = model.Url;

            _context.ArticleCategories.Update(item);

            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = id,
            };
        }

        public async Task<int> Delete(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.ArticleCategories.FindAsync(id);

            _context.ArticleCategories.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        #endregion Method
    }
}