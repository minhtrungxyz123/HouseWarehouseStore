using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;
using Microsoft.EntityFrameworkCore;

namespace Master.Service
{
    public class SizeService : ISizeService
    {
        #region Fields

        private readonly HouseWarehouseStoreDbContext _context;

        public SizeService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region List

        public async Task<ApiResult<Size>> GetByIdAsyn(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Sizes
                            .OrderByDescending(p => p.SizeProduct)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.SizeId == id);

            var model = new Size()
            {
                SizeId = item.SizeId,
                SizeProduct = item.SizeProduct,
                ProductId = item.ProductId,
            };
            return new ApiSuccessResult<Size>(model);
        }

        public async Task<IEnumerable<Size>> GetAll()
        {
            return await _context.Sizes
                            .OrderByDescending(p => p.SizeProduct)
                            .ToListAsync();
        }

        public async Task<ApiResult<Pagination<SizeModel>>> GetAllPaging(SizeSearchContext ctx)
        {
            var query = from pr in _context.Sizes
                        join c in _context.Products on pr.ProductId equals c.ProductId into pt
                        from tp in pt.DefaultIfEmpty()
                        select new { pr, tp };

            if (!string.IsNullOrEmpty(ctx.Keyword))
            {
                query = query.Where(x => x.pr.SizeProduct.Contains(ctx.Keyword));
            }

            var totalRecords = await query.CountAsync();

            var items = await query.Skip((ctx.PageIndex - 1) * ctx.PageSize)
                .Take(ctx.PageSize)
                .Select(u => new SizeModel()
                {
                    ProductId = u.tp.Name,
                    SizeId = u.pr.SizeId,
                    SizeProduct = u.pr.SizeProduct
                })
                .ToListAsync();

            var pagination = new Pagination<SizeModel>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageIndex = ctx.PageIndex,
                PageSize = ctx.PageSize,
            };

            return new ApiSuccessResult<Pagination<SizeModel>>(pagination);
        }

        public async Task<Size?> GetById(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Sizes
                            .OrderByDescending(p => p.SizeProduct)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.SizeId == id);

            return item;
        }

        #endregion List

        #region Method

        public async Task<RepositoryResponse> Create(SizeModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            Size item = new Size()
            {
                ProductId = model.ProductId,
                SizeProduct = model.SizeProduct,
                SizeId = Guid.NewGuid().ToString(),
            };

            await _context.Sizes.AddAsync(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.SizeId
            };
        }

        public async Task<RepositoryResponse> Update(string? id, SizeModel model)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var item = await _context.Sizes.FindAsync(id);
            item.SizeProduct = model.SizeProduct;
            item.ProductId = model.ProductId;

            _context.Sizes.Update(item);

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

            var item = await _context.Sizes.FindAsync(id);

            _context.Sizes.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        public IList<Size> GetActive()
        {
            var query = from p in _context.Sizes.AsQueryable() select p;
            query = from p in query orderby p.SizeProduct select p;
            return query.ToList();
        }

        #endregion Method
    }
}