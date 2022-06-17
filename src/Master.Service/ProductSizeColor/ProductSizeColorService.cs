using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Service
{
    public class ProductSizeColorService : IProductSizeColorService
    {
        #region Fields

        private readonly HouseWarehouseStoreDbContext _context;

        public ProductSizeColorService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region List

        public async Task<ApiResult<ProductSizeColor>> GetByIdAsyn(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.ProductSizeColors
                            .OrderByDescending(p => p.ProductId)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            var model = new ProductSizeColor()
            {
                Id = item.Id,
                ProductId = item.ProductId,
                ColorId = item.ColorId,
                ProductsProductId = item.ProductsProductId,
                SizeId = item.SizeId
            };
            return new ApiSuccessResult<ProductSizeColor>(model);
        }

        public async Task<IEnumerable<ProductSizeColor>> GetAll()
        {
            return await _context.ProductSizeColors
                            .OrderByDescending(p => p.ProductId)
                            .ToListAsync();
        }

        public async Task<ApiResult<Pagination<ProductSizeColor>>> GetAllPaging(ProductSizeColorSearchContext ctx)
        {
            var query = _context.ProductSizeColors.AsQueryable();
            if (!string.IsNullOrEmpty(ctx.Keyword))
            {
                query = query.Where(x => x.ProductId.Contains(ctx.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query.Skip((ctx.PageIndex - 1) * ctx.PageSize)
                .Take(ctx.PageSize)
                .Select(x => new ProductSizeColor()
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
