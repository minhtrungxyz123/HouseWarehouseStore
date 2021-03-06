using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseWarehouseStore.Service
{
    public class ProductService : IProductService
    {
        private readonly HouseWarehouseStoreDbContext _context;

        public ProductService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAll(bool showHidden = true)
        {
            return await _context.Products
                             .OrderByDescending(p => p.Name).Where(c => c.Home == showHidden)
                             .ToListAsync();
        }

        public async Task<ApiResult<Pagination<ProductModel>>> GetAllPaging(ProductSearchContext ctx)
        {
            var query = from pr in _context.Products
                        join c in _context.ProductCategories on pr.ProductCategorieId equals c.ProductCategorieId into pt
                        from tp in pt.DefaultIfEmpty()
                        join w in _context.Collections on pr.CollectionId equals w.CollectionId into wt
                        from tw in wt.DefaultIfEmpty()
                        join m in _context.Comments on pr.ProductId equals m.ProductId into mt
                        from tm in mt.DefaultIfEmpty()
                        select new { pr, tp, tw, tm };

            if (!string.IsNullOrEmpty(ctx.Keyword))
            {
                query = query.Where(x => x.pr.Name.Contains(ctx.Keyword)
                || x.pr.TitleMeta.Contains(ctx.Keyword));
            }
            if (!string.IsNullOrEmpty(ctx.Price.ToString()))
            {
                query = query.Where(x => x.pr.SaleOff >= ctx.Price);
            }

            var totalRecords = await query.CountAsync();

            var items = await query.Skip((ctx.PageIndex - 1) * ctx.PageSize)
                .Take(ctx.PageSize)
                .Select(u => new ProductModel()
                {
                    Name = u.pr.Name,
                    CollectionId = u.tw.Name,
                    Active = u.pr.Active,
                    TitleMeta = u.pr.TitleMeta,
                    Content = u.pr.Content,
                    StatusProduct = u.pr.StatusProduct,
                    Sort = u.pr.Sort,
                    Quantity = u.pr.Quantity,
                    BarCode = u.pr.BarCode,
                    Price = u.pr.Price,
                    Body = u.pr.Body,
                    CreateBy = u.pr.CreateBy,
                    CreateDate = u.pr.CreateDate,
                    Description = u.pr.Description,
                    Factory = u.pr.Factory,
                    Home = u.pr.Home,
                    Hot = u.pr.Hot,
                    Image = u.pr.Image,
                    SaleOff = u.pr.SaleOff,
                    QuyCach = u.pr.QuyCach,
                    GiftInfo = u.pr.GiftInfo,
                    ProductCategorieId = u.tp.Name,
                    ProductId = u.pr.ProductId,
                    DescriptionMeta = u.pr.DescriptionMeta,
                    star = u.tm.Star
                })
                .ToListAsync();

            var pagination = new Pagination<ProductModel>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageIndex = ctx.PageIndex,
                PageSize = ctx.PageSize,
            };

            return new ApiSuccessResult<Pagination<ProductModel>>(pagination);
        }

        public async Task<ApiResult<Product>> GetProductDetail(string id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Products
                            .OrderByDescending(p => p.Name)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.ProductId == id);

            var model = new Product()
            {
                CollectionId = item.CollectionId,
                Active = item.Active,
                ProductId = item.ProductId,
                Name = item.Name,
                TitleMeta = item.TitleMeta,
                BarCode = item.BarCode,
                Body = item.Body,
                Content = item.Content,
                CreateBy = item.CreateBy,
                CreateDate = item.CreateDate,
                Description = item.Description,
                DescriptionMeta = item.Description,
                Factory = item.Factory,
                GiftInfo = item.GiftInfo,
                Home = item.Home,
                Hot = item.Hot,
                Image = item.Image,
                Price = item.Price,
                ProductCategorieId = item.ProductCategorieId,
                Quantity = item.Quantity,
                QuyCach = item.QuyCach,
                SaleOff = item.SaleOff,
                Sort = item.Sort,
                StatusProduct = item.StatusProduct
            };
            return new ApiSuccessResult<Product>(model);
        }
    }
}