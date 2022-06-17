using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<ApiResult<Pagination<ProductSizeColorModel>>> GetAllPaging(ProductSizeColorSearchContext request)
        {
            var query = from pr in _context.ProductSizeColors
                        join c in _context.Colors on pr.ColorId equals c.ColorId into pt
                        from tp in pt.DefaultIfEmpty()
                        join w in _context.Sizes on pr.SizeId equals w.SizeId into wt
                        from tw in wt.DefaultIfEmpty()
                        join i in _context.Products on pr.ProductId equals i.ProductId into it
                        from ti in it.DefaultIfEmpty()
                        select new { pr, tp, tw, ti };

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.pr.ProductsProductId.Contains(request.Keyword));
            }

            if (!string.IsNullOrEmpty(request.SizeId))
            {
                query = query.Where(x => x.pr.SizeId == request.SizeId);
            }

            if (!string.IsNullOrEmpty(request.ColorId))
            {
                query = query.Where(x => x.pr.ColorId == request.ColorId);
            }

            if (!string.IsNullOrEmpty(request.ProductId))
            {
                query = query.Where(x => x.pr.ProductId == request.ProductId);
            }

            var totalRecords = await query.CountAsync();

            var items = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(u => new ProductSizeColorModel()
                {
                    Id = u.pr.Id,
                    SizeId = u.tw.SizeProduct,
                    ColorId = u.tp.NameColor,
                    ProductsProductId = u.pr.ProductsProductId,
                    ProductId = u.ti.Name
                })
                .ToListAsync();

            var pagination = new Pagination<ProductSizeColorModel>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            };

            return new ApiSuccessResult<Pagination<ProductSizeColorModel>>(pagination);
        }

        public async Task<ProductSizeColor?> GetById(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.ProductSizeColors
                            .OrderByDescending(p => p.ProductId)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            return item;
        }

        #endregion List

        #region Method

        public async Task<RepositoryResponse> Create(ProductSizeColorModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            ProductSizeColor item = new ProductSizeColor()
            {
                ProductId = model.ProductId,
                ProductsProductId = model.ProductsProductId,
                SizeId = model.SizeId,
                Id = Guid.NewGuid().ToString(),
                ColorId = model.ColorId,
            };

            await _context.ProductSizeColors.AddAsync(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.Id
            };
        }

        public async Task<RepositoryResponse> Update(string? id, ProductSizeColorModel model)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var item = await _context.ProductSizeColors.FindAsync(id);
            item.ProductId = model.ProductId;
            item.ProductsProductId = model.ProductsProductId;
            item.SizeId = model.SizeId;
            item.ColorId = model.ColorId;

            _context.ProductSizeColors.Update(item);

            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = id,
            };
        }

        public async Task<int> Delete(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.ProductSizeColors.FindAsync(id);

            _context.ProductSizeColors.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        #endregion Method
    }
}