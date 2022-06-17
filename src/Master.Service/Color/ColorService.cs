using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;
using Microsoft.EntityFrameworkCore;

namespace Master.Service
{
    public class ColorService : IColorService
    {
        #region Fields

        private readonly HouseWarehouseStoreDbContext _context;

        public ColorService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region List

        public async Task<ApiResult<Color>> GetByIdAsyn(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Colors
                            .OrderByDescending(p => p.NameColor)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.ColorId == id);

            var model = new Color()
            {
                NameColor = item.NameColor,
                ColorId = item.ColorId,
                Code = item.Code
            };
            return new ApiSuccessResult<Color>(model);
        }

        public async Task<IEnumerable<Color>> GetAll()
        {
            return await _context.Colors
                            .OrderByDescending(p => p.NameColor)
                            .ToListAsync();
        }

        public async Task<ApiResult<Pagination<Color>>> GetAllPaging(ColorSearchContext ctx)
        {
            var query = _context.Colors.AsQueryable();
            if (!string.IsNullOrEmpty(ctx.Keyword))
            {
                query = query.Where(x => x.NameColor.Contains(ctx.Keyword)
                || x.Code.Contains(ctx.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query.Skip((ctx.PageIndex - 1) * ctx.PageSize)
                .Take(ctx.PageSize)
                .Select(x => new Color()
                {
                    NameColor = x.NameColor,
                    ColorId = x.ColorId,
                    Code = x.Code
                }).ToListAsync();

            var pagedResult = new Pagination<Color>()
            {
                TotalRecords = totalRow,
                PageIndex = ctx.PageIndex,
                PageSize = ctx.PageSize,
                Items = data
            };
            return new ApiSuccessResult<Pagination<Color>>(pagedResult);
        }

        public async Task<Color?> GetById(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Colors
                            .OrderByDescending(p => p.NameColor)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.ColorId == id);

            return item;
        }

        #endregion List

        #region Method

        public async Task<RepositoryResponse> Create(ColorModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            Color item = new Color()
            {
                NameColor = model.NameColor,
                Code = model.Code,
                ColorId = Guid.NewGuid().ToString(),
            };

            await _context.Colors.AddAsync(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.ColorId.ToString(),
            };
        }

        public async Task<RepositoryResponse> Update(string? id, ColorModel model)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var item = await _context.Colors.FindAsync(id);
            item.NameColor = model.NameColor;
            item.Code = model.Code;

            _context.Colors.Update(item);

            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = id.ToString(),
            };
        }

        public async Task<int> Delete(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Colors.FindAsync(id);

            _context.Colors.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        #endregion Method
    }
}