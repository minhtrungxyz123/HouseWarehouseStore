using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Models;
using Microsoft.EntityFrameworkCore;

namespace Master.Service
{
    public class CommentService : ICommentService
    {
        #region Fields

        private readonly HouseWarehouseStoreDbContext _context;

        public CommentService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region List

        public async Task<ApiResult<Pagination<CommentModel>>> GetAllPaging(CommentSearchContext ctx)
        {
            var query = from pr in _context.Comments
                        join c in _context.Products on pr.ProductId equals c.ProductId into pt
                        from tp in pt.DefaultIfEmpty()
                        select new { pr, tp };

            if (!string.IsNullOrEmpty(ctx.Keyword))
            {
                query = query.Where(x => x.pr.CustomerName.Contains(ctx.Keyword)
                || x.pr.Profession.Contains(ctx.Keyword));
            }

            var totalRecords = await query.CountAsync();

            var items = await query.Skip((ctx.PageIndex - 1) * ctx.PageSize)
                .Take(ctx.PageSize)
                .Select(u => new CommentModel()
                {
                    CustomerName = u.pr.CustomerName,
                    Profession = u.pr.Profession,
                    ProductId = u.tp.Name,
                    Star = u.pr.Star,
                    CommentId = u.pr.CommentId,
                    Image = u.pr.Image,
                    Contents = u.pr.Contents,
                })
                .ToListAsync();

            var pagination = new Pagination<CommentModel>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageIndex = ctx.PageIndex,
                PageSize = ctx.PageSize,
            };

            return new ApiSuccessResult<Pagination<CommentModel>>(pagination);
        }

        #endregion List

        #region Method

        public async Task<int> Delete(string id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Comments.FindAsync(id);

            _context.Comments.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        #endregion Method
    }
}