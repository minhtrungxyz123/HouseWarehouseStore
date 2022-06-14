using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;
using Microsoft.EntityFrameworkCore;

namespace Master.Service
{
    public class VoucherService : IVoucherService
    {
        #region Fields

        private readonly HouseWarehouseStoreDbContext _context;

        public VoucherService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region List

        public async Task<ApiResult<Voucher>> GetByIdAsyn(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Vouchers
                            .OrderByDescending(p => p.Name)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            var model = new Voucher()
            {
                Id = item.Id,
                Name = item.Name,
                Active = item.Active,
                Code = item.Code,
                Condition = item.Condition,
                PriceDown = item.PriceDown,
                PriceUp = item.PriceUp,
                ReductionMax = item.ReductionMax,
                SumUse = item.SumUse,
                Type = item.Type,
                Value = item.Value
            };
            return new ApiSuccessResult<Voucher>(model);
        }

        public async Task<IEnumerable<Voucher>> GetAll()
        {
            return await _context.Vouchers
                            .OrderByDescending(p => p.Name)
                            .ToListAsync();
        }

        public async Task<ApiResult<Pagination<Voucher>>> GetAllPaging(VoucherSearchContext ctx)
        {
            var query = _context.Vouchers.AsQueryable();
            if (!string.IsNullOrEmpty(ctx.Keyword))
            {
                query = query.Where(x => x.Name.Contains(ctx.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query.Skip((ctx.PageIndex - 1) * ctx.PageSize)
                .Take(ctx.PageSize)
                .Select(x => new Voucher()
                {
                    Name = x.Name,
                    Id = x.Id,
                    Value = x.Value,
                    PriceDown = x.PriceDown,
                    ReductionMax = x.ReductionMax,
                    Type = x.Type,
                    SumUse = x.SumUse,
                    Active = x.Active,
                    Code = x.Code,
                    Condition = x.Condition,
                    PriceUp = x.PriceUp
                }).ToListAsync();

            var pagedResult = new Pagination<Voucher>()
            {
                TotalRecords = totalRow,
                PageIndex = ctx.PageIndex,
                PageSize = ctx.PageSize,
                Items = data
            };
            return new ApiSuccessResult<Pagination<Voucher>>(pagedResult);
        }

        public async Task<Voucher?> GetById(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Vouchers
                            .OrderByDescending(p => p.Name)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            return item;
        }

        public IList<Voucher> GetActive(bool showHidden = true)
        {
            var query = from p in _context.Vouchers.AsQueryable() select p;
            if (showHidden)
            {
                query = from p in query where p.Active select p;
            }
            query = from p in query orderby p.Name select p;
            return query.ToList();
        }

        public IList<Voucher> GetType(bool showHidden = true)
        {
            var query = from p in _context.Vouchers.AsQueryable() select p;
            if (showHidden)
            {
                query = from p in query where p.Type select p;
            }
            query = from p in query orderby p.Name select p;
            return query.ToList();
        }

        public IList<Voucher> GetCondition(bool showHidden = true)
        {
            var query = from p in _context.Vouchers.AsQueryable() select p;
            if (showHidden)
            {
                query = from p in query where p.Condition select p;
            }
            query = from p in query orderby p.Name select p;
            return query.ToList();
        }

        #endregion List

        #region Method

        public async Task<RepositoryResponse> Create(VoucherModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            Voucher item = new Voucher()
            {
                Name = model.Name,
                Id = Guid.NewGuid().ToString(),
                PriceUp = model.PriceUp,
                Condition = model.Condition,
                Code = model.Code,
                Active  = model.Active,
                SumUse = model.SumUse,
                PriceDown =model.PriceDown,
                ReductionMax = model.ReductionMax,
                Type = model.Type,
                Value=model.Value
            };

            await _context.Vouchers.AddAsync(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.Id,
            };
        }

        public async Task<RepositoryResponse> Update(string? id, VoucherModel model)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var item = await _context.Vouchers.FindAsync(id);
            item.Name = model.Name;
            item.PriceUp = model.PriceUp;
            item.Condition = model.Condition;
            item.Code = model.Code;
            item.Active = model.Active;
            item.SumUse = model.SumUse;
            item.PriceDown = model.PriceDown;
            item.ReductionMax = model.ReductionMax;
            item.Type = model.Type;
            item.Value = model.Value;

            _context.Vouchers.Update(item);

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

            var item = await _context.Vouchers.FindAsync(id);

            _context.Vouchers.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        #endregion Method
    }
}