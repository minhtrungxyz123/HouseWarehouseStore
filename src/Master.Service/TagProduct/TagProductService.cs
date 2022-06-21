using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;
using Microsoft.EntityFrameworkCore;

namespace Master.Service
{
    public class TagProductService : ITagProductService
    {
        #region Fields

        private readonly HouseWarehouseStoreDbContext _context;

        public TagProductService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region List

        public async Task<ApiResult<TagProduct>> GetByIdAsyn(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.TagProducts
                            .OrderByDescending(p => p.ProductId)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.TagId == id);

            var model = new TagProduct()
            {
                TagId = item.TagId,
                ProductId = item.ProductId
            };
            return new ApiSuccessResult<TagProduct>(model);
        }

        public async Task<IEnumerable<TagProduct>> GetAll()
        {
            return await _context.TagProducts
                            .OrderByDescending(p => p.TagId)
                            .ToListAsync();
        }

        public async Task<ApiResult<Pagination<TagProductModel>>> GetAllPaging(TagProductSearchContext ctx)
        {
            var query = from pr in _context.TagProducts
                        join c in _context.Products on pr.ProductId equals c.ProductId into pt
                        from tp in pt.DefaultIfEmpty()
                        join w in _context.Tags on pr.TagId equals w.TagId into wt
                        from tw in wt.DefaultIfEmpty()
                        select new { pr, tp, tw };

            if (!string.IsNullOrEmpty(ctx.Keyword))
            {
                query = query.Where(x => x.pr.TagId.Contains(ctx.Keyword)
                || x.pr.ProductId.Contains(ctx.Keyword));
            }

            var totalRecords = await query.CountAsync();

            var items = await query.Skip((ctx.PageIndex - 1) * ctx.PageSize)
                .Take(ctx.PageSize)
                .Select(u => new TagProductModel()
                {
                    ProductId = u.pr.ProductId,
                    TagId = u.pr.TagId,
                    ProductName = u.tp.Name,
                    TagName = u.tw.Name
                })
                .ToListAsync();

            var pagination = new Pagination<TagProductModel>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageIndex = ctx.PageIndex,
                PageSize = ctx.PageSize,
            };

            return new ApiSuccessResult<Pagination<TagProductModel>>(pagination);
        }

        public async Task<TagProduct?> GetById(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.TagProducts
                            .OrderByDescending(p => p.ProductId)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.TagId == id);

            return item;
        }

        #endregion List

        #region Method

        public async Task<RepositoryResponse> Create(TagProductModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            TagProduct item = new TagProduct()
            {
                ProductId = model.ProductId,
                TagId = model.TagId
            };

            await _context.TagProducts.AddAsync(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.TagId,
            };
        }

        public async Task<int> Delete(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.TagProducts.FindAsync(id);

            _context.TagProducts.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        #endregion Method
    }
}