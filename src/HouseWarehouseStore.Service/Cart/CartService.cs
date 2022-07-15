using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseWarehouseStore.Service
{
    public class CartService : ICartService
    {
        private readonly HouseWarehouseStoreDbContext _context;

        public CartService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<Pagination<CartModel>>> GetAllPaging(CartSearchContext ctx)
        {
            var query = from pr in _context.Carts
                        join c in _context.Products on pr.ProductId equals c.ProductId into pt
                        from tp in pt.DefaultIfEmpty()
                        from m in _context.Members
                        where m.Email == ctx.MemberId && tp.ProductId == ctx.ProductId
                        select new { pr, tp };

            if (!string.IsNullOrEmpty(ctx.Keyword))
            {
                query = query.Where(x => x.pr.Price.ToString().Contains(ctx.Keyword)
                || x.pr.ProductId.Contains(ctx.Keyword));
            }

            var totalRecords = await query.CountAsync();

            var items = await query.Skip((ctx.PageIndex - 1) * ctx.PageSize)
                .Take(ctx.PageSize)
                .Select(u => new CartModel()
                {
                    ProductId = u.pr.ProductId,
                    Price = u.pr.Price,
                    CartId = u.pr.CartId,
                    Color = u.pr.Color,
                    Count = u.pr.Count,
                    DateCreated = u.pr.DateCreated,
                    RecordId = u.pr.RecordId,
                    Size = u.pr.Size
                })
                .ToListAsync();

            var pagination = new Pagination<CartModel>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageIndex = ctx.PageIndex,
                PageSize = ctx.PageSize,
            };

            return new ApiSuccessResult<Pagination<CartModel>>(pagination);
        }
    }
}