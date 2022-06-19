using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;
using Microsoft.EntityFrameworkCore;

namespace Master.Service
{
    public class ProductCategoryService : IProductCategoryService
    {
        #region Fields

        private readonly HouseWarehouseStoreDbContext _context;

        public ProductCategoryService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region List

        public async Task<ApiResult<ProductCategory>> GetByIdAsyn(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.ProductCategories
                            .OrderByDescending(p => p.Name)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.ProductCategorieId == id);

            var model = new ProductCategory()
            {
                Name = item.Name,
                Active = item.Active,
                Body = item.Body,
                Home = item.Home,
                Image = item.Image,
                TitleMeta = item.TitleMeta,
                ProductCategorieId = item.ProductCategorieId,
                Url = item.Url,
                ParentId = item.ParentId,
                CoverImage = item.CoverImage,
                DescriptionMeta = item.DescriptionMeta,
                Soft = item.Soft
            };
            return new ApiSuccessResult<ProductCategory>(model);
        }

        public async Task<IEnumerable<ProductCategory>> GetAll()
        {
            return await _context.ProductCategories
                            .OrderByDescending(p => p.Name)
                            .ToListAsync();
        }

        public async Task<ApiResult<Pagination<ProductCategoryModel>>> GetAllPaging(ProductCategorySearchContext ctx)
        {
            var query = from pr in _context.ProductCategories
                        join c in _context.ProductCategories on pr.ParentId equals c.ProductCategorieId into pt
                        from tp in pt.DefaultIfEmpty()
                        select new { pr, tp };

            if (!string.IsNullOrEmpty(ctx.Keyword))
            {
                query = query.Where(x => x.pr.Name.Contains(ctx.Keyword)
                || x.pr.TitleMeta.Contains(ctx.Keyword));
            }

            var totalRecords = await query.CountAsync();

            var items = await query.Skip((ctx.PageIndex - 1) * ctx.PageSize)
                .Take(ctx.PageSize)
                .Select(u => new ProductCategoryModel()
                {
                    Name = u.pr.Name,
                    Active = u.pr.Active,
                    TitleMeta = u.pr.TitleMeta,
                    Body = u.pr.Body,
                    Home = u.pr.Home,
                    Image = u.pr.Image,
                    Soft = u.pr.Soft,
                    DescriptionMeta = u.pr.DescriptionMeta,
                    CoverImage = u.pr.CoverImage,
                    ParentId = u.tp.Name,
                    Url = u.pr.Url,
                    ProductCategorieId = u.pr.ProductCategorieId
                })
                .ToListAsync();

            var pagination = new Pagination<ProductCategoryModel>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageIndex = ctx.PageIndex,
                PageSize = ctx.PageSize,
            };

            return new ApiSuccessResult<Pagination<ProductCategoryModel>>(pagination);
        }

        public async Task<ProductCategory?> GetById(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.ProductCategories
                            .OrderByDescending(p => p.Name)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.ProductCategorieId == id);

            return item;
        }

        public IList<ProductCategory> GetActive(bool showHidden = true)
        {
            var query = from p in _context.ProductCategories.AsQueryable() select p;
            if (showHidden)
            {
                query = from p in query where p.Active select p;
            }
            query = from p in query orderby p.Name select p;
            return query.ToList();
        }

        public IList<ProductCategory> GetHome(bool showHidden = true)
        {
            var query = from p in _context.ProductCategories.AsQueryable() select p;
            if (showHidden)
            {
                query = from p in query where p.Home select p;
            }
            query = from p in query orderby p.Name select p;
            return query.ToList();
        }

        #endregion List

        #region Method

        public async Task<RepositoryResponse> Create(ProductCategoryModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            ProductCategory item = new ProductCategory()
            {
                ProductCategorieId = model.ProductCategorieId,
                Name = model.Name,
                Active = model.Active,
                Body = model.Body,
                Home = model.Home,
                Image = model.Image,
                TitleMeta = model.TitleMeta,
                ParentId = model.ParentId,
                Url = model.Url,
                CoverImage = model.CoverImage,
                DescriptionMeta = model.DescriptionMeta,
                Soft = model.Soft
            };

            await _context.ProductCategories.AddAsync(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.ProductCategorieId
            };
        }

        public async Task<RepositoryResponse> Update(string? id, ProductCategoryModel model)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var item = await _context.ProductCategories.FindAsync(id);
            item.Name = model.Name;
            item.Active = model.Active;
            item.Body = model.Body;
            item.Home = model.Home;
            item.Image = model.Image;
            item.TitleMeta = model.TitleMeta;
            item.ParentId = model.ParentId;
            item.Url = model.Url;
            item.CoverImage = model.CoverImage;
            item.DescriptionMeta = model.DescriptionMeta;
            item.Soft = model.Soft;

            _context.ProductCategories.Update(item);

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

            var item = await _context.ProductCategories.FindAsync(id);

            _context.ProductCategories.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        #endregion Method
    }
}