using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;
using Microsoft.EntityFrameworkCore;

namespace Master.Service
{
    public class ProductLikeService : IProductLikeService
    {
        #region Fields

        private readonly HouseWarehouseStoreDbContext _context;

        public ProductLikeService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region List

        public async Task<ApiResult<ProductLike>> GetByIdAsyn(string id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.ProductLikes
                            .OrderByDescending(p => p.MemberId)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.ProductLikeId == id);

            var model = new ProductLike()
            {
                MemberId = item.ProductId,
                ProductId = item.MemberId,
                ProductLikeId = item.ProductLikeId,
                ProductsProductId = item.ProductsProductId
            };
            return new ApiSuccessResult<ProductLike>(model);
        }

        public async Task<IEnumerable<ProductLike>> GetAll()
        {
            return await _context.ProductLikes
                            .OrderByDescending(p => p.ProductLikeId)
                            .ToListAsync();
        }

        public async Task<ApiResult<Pagination<ProductLikeModel>>> GetAllPaging(ProductLikeSearchContext ctx)
        {
            var query = from pr in _context.ProductLikes
                        join c in _context.Members on pr.MemberId equals c.Id into pt
                        from tp in pt.DefaultIfEmpty()
                        join w in _context.Products on pr.ProductId equals w.ProductId into wt
                        from tw in wt.DefaultIfEmpty()
                        select new { pr, tp, tw };

            if (!string.IsNullOrEmpty(ctx.Keyword))
            {
                query = query.Where(x => x.pr.MemberId.Contains(ctx.Keyword)
                || x.pr.ProductId.Contains(ctx.Keyword));
            }

            var totalRecords = await query.CountAsync();

            var items = await query.Skip((ctx.PageIndex - 1) * ctx.PageSize)
                .Take(ctx.PageSize)
                .Select(u => new ProductLikeModel()
                {
                    ProductId = u.pr.ProductId,
                    MemberId = u.tp.Fullname,
                    ProductLikeId = u.pr.ProductLikeId,
                    ProductName = u.tw.Name,
                    Quantity = u.tw.SaleOff,
                    ProductsProductId = u.pr.ProductsProductId
                })
                .ToListAsync();

            var pagination = new Pagination<ProductLikeModel>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageIndex = ctx.PageIndex,
                PageSize = ctx.PageSize,
            };

            return new ApiSuccessResult<Pagination<ProductLikeModel>>(pagination);
        }

        public async Task<ProductLike?> GetById(string id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.ProductLikes
                            .OrderByDescending(p => p.ProductId)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.ProductLikeId == id);

            return item;
        }

        #endregion List
    }
}