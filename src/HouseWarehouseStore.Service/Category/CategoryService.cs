using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseWarehouseStore.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly HouseWarehouseStoreDbContext _context;

        public CategoryService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductCategory>> GetAll(bool showHidden = true)
        {
            return await _context.ProductCategories
                             .OrderByDescending(p => p.Name).Where(p => p.Active == showHidden)
                             .ToListAsync();
        }

        public async Task<ApiResult<Pagination<ProductCategoryModel>>> GetAllPaging(CategorySearchContext ctx)
        {
            var query = from pr in _context.ProductCategories
                        join c in _context.Products on pr.ProductCategorieId equals c.ProductCategorieId into pt
                        from tp in pt.DefaultIfEmpty()
                        where pr.ProductCategorieId == ctx.CategoryId
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
                    Body = u.pr.Body,
                    CoverImage = u.pr.CoverImage,
                    DescriptionMeta = u.pr.DescriptionMeta,
                    Home = u.pr.Home,
                    Icon = u.pr.Icon,
                    Url = u.pr.Url,
                    Image = u.pr.Image,
                    ParentId = u.pr.ParentId,
                    ProductCategorieId = u.pr.ProductCategorieId,
                    Soft = u.pr.Soft,
                    TitleMeta = u.pr.TitleMeta,
                    ProductId = u.tp.ProductId,
                    ProductName = u.tp.Name,
                    SaleOff = u.tp.SaleOff
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
    }
}