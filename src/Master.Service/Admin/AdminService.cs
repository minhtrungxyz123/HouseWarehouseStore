using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;
using Microsoft.EntityFrameworkCore;

namespace Master.Service
{
    public class AdminService : IAdminService
    {
        #region Fields

        private readonly HouseWarehouseStoreDbContext _context;

        public AdminService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region List

        public async Task<ApiResult<Admin>> GetByIdAsyn(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Admins
                            .OrderByDescending(p => p.Username)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.AdminId == id);

            var model = new Admin()
            {
                Username = item.Username,
                Active = item.Active,
                AdminId = item.AdminId,
                Password = item.Password,
                Role = item.Role
            };
            return new ApiSuccessResult<Admin>(model);
        }

        public async Task<IEnumerable<Admin>> GetAll()
        {
            return await _context.Admins
                            .OrderByDescending(p => p.Username)
                            .ToListAsync();
        }

        public async Task<ApiResult<Pagination<Admin>>> GetAllPaging(AdminSearchContext ctx)
        {
            var query = _context.Admins.AsQueryable();
            if (!string.IsNullOrEmpty(ctx.Keyword))
            {
                query = query.Where(x => x.Username.Contains(ctx.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query.Skip((ctx.PageIndex - 1) * ctx.PageSize)
                .Take(ctx.PageSize)
                .Select(x => new Admin()
                {
                    Username = x.Username,
                    Password = x.Password,
                    AdminId = x.AdminId,
                    Role = x.Role,
                    Active = x.Active
                }).ToListAsync();

            var pagedResult = new Pagination<Admin>()
            {
                TotalRecords = totalRow,
                PageIndex = ctx.PageIndex,
                PageSize = ctx.PageSize,
                Items = data
            };
            return new ApiSuccessResult<Pagination<Admin>>(pagedResult);
        }

        public async Task<Admin?> GetById(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Admins
                            .OrderByDescending(p => p.Username)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.AdminId == id);

            return item;
        }

        public IList<Admin> GetMvcListItems(bool showHidden = true)
        {
            var query = from p in _context.Admins.AsQueryable() select p;
            if (showHidden)
            {
                query = from p in query where p.Active select p;
            }
            query = from p in query orderby p.Username select p;
            return query.ToList();
        }

        #endregion List

        #region Method

        public async Task<RepositoryResponse> Create(AdminModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            Admin item = new Admin()
            {
                Username = model.Username,
                Active = model.Active,
                Password = model.Password,
                Role = model.Role
            };
            model.AdminId = Guid.NewGuid().ToString();

            await _context.Admins.AddAsync(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.AdminId.ToString(),
            };
        }

        public async Task<RepositoryResponse> Update(string? id, AdminModel model)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var item = await _context.Admins.FindAsync(id);
            item.Username = model.Username;
            item.Active = model.Active;
            item.Password = model.Password;
            item.Role = model.Role;

            _context.Admins.Update(item);

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

            var item = await _context.Admins.FindAsync(id);

            _context.Admins.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        #endregion Method
    }
}