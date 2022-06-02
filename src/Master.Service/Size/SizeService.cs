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

        public async Task<ApiResult<Size>> GetByIdAsyn(int? id)
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
            };
            return new ApiSuccessResult<Size>(model);
        }

        public async Task<IEnumerable<Size>> GetAll()
        {
            return await _context.Sizes
                            .OrderByDescending(p => p.SizeProduct)
                            .ToListAsync();
        }

        public async Task<ApiResult<Pagination<Size>>> GetAllPaging(SizeSearchContext ctx)
        {
            var query = _context.Sizes.AsQueryable();
            if (!string.IsNullOrEmpty(ctx.Keyword))
            {
                query = query.Where(x => x.SizeProduct.Contains(ctx.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query.Skip((ctx.PageIndex - 1) * ctx.PageSize)
                .Take(ctx.PageSize)
                .Select(x => new Size()
                {
                    SizeProduct = x.SizeProduct,
                    SizeId = x.SizeId,
                }).ToListAsync();

            var pagedResult = new Pagination<Size>()
            {
                TotalRecords = totalRow,
                PageIndex = ctx.PageIndex,
                PageSize = ctx.PageSize,
                Items = data
            };
            return new ApiSuccessResult<Pagination<Size>>(pagedResult);
        }

        public async Task<Size?> GetById(int? id)
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
                SizeProduct = model.SizeProduct,
            };

            await _context.Sizes.AddAsync(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.SizeId
            };
        }

        public async Task<RepositoryResponse> Update(int? id, SizeModel model)
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

            _context.Sizes.Update(item);

            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = id
            };
        }

        public async Task<int> Delete(int? id)
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

        #endregion Method
    }
}