using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseWarehouseStore.Service
{
    public class ProductLikeService : IProductLikeService
    {
        private readonly HouseWarehouseStoreDbContext _context;

        public ProductLikeService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductLikeModel>> GetAll(string idProd, string idMember)
        {
            var banner = from x in _context.ProductLikes
                         where x.ProductId == idProd && x.MemberId == idMember
                         select new ProductLikeModel()
                         {
                             MemberId = x.MemberId,
                             ProductId = x.ProductId,
                             ProductLikeId = x.ProductLikeId,
                             ProductsProductId = x.ProductsProductId
                         };

            return banner.ToList();
        }

        public async Task<ApiResult<Pagination<ProductLikeModel>>> GetAllPaging(ProductLikeSearchContext ctx)
        {
            var query = from pr in _context.ProductLikes
                        where pr.MemberId == ctx.IdMember
                        select new { pr };

            if (!string.IsNullOrEmpty(ctx.Keyword))
            {
                query = query.Where(x => x.pr.MemberId.Contains(ctx.Keyword)
                || x.pr.ProductsProductId.Contains(ctx.Keyword));
            }

            var totalRecords = await query.CountAsync();

            var items = await query.Skip((ctx.PageIndex - 1) * ctx.PageSize)
                .Take(ctx.PageSize)
                .Select(u => new ProductLikeModel()
                {
                    ProductId = u.pr.ProductId,
                    MemberId = u.pr.MemberId,
                    ProductLikeId = u.pr.ProductId,
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
    }
}