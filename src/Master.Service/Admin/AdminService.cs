using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.Dapper;
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
        private readonly IDapper _dapper;

        public AdminService(HouseWarehouseStoreDbContext context,
            IDapper dapper)
        {
            _context = context;
            _dapper = dapper;
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
                            .FirstOrDefaultAsync(p => p.Id == id);

            var model = new Admin()
            {
                Username = item.Username,
                Active = item.Active,
                Id = item.Id,
                Password = item.Password,
                Role = item.Role,
                Position = item.Position,
                Sex = item.Sex,
                CreateDate = item.CreateDate,
                FullName = item.FullName,
                Image = item.Image,
                Address = item.Address,
                Age = item.Age,
                Email = item.Email
            };
            return new ApiSuccessResult<Admin>(model);
        }

        public async Task<IEnumerable<Admin>> GetAll()
        {
            return await _context.Admins
                            .OrderByDescending(p => p.Username)
                            .ToListAsync();
        }

        public async Task<ApiResult<Pagination<AdminModel>>> GetAllPaging(AdminSearchContext ctx)
        {
            var query = from pr in _context.Admins
                        select new { pr };

            if (!string.IsNullOrEmpty(ctx.Keyword))
            {
                query = query.Where(x => x.pr.FullName.Contains(ctx.Keyword)
                || x.pr.Username.Contains(ctx.Keyword));
            }

            var totalRecords = await query.CountAsync();

            var items = await query.Skip((ctx.PageIndex - 1) * ctx.PageSize)
                .Take(ctx.PageSize)
                .Select(u => new AdminModel()
                {
                    FullName = u.pr.FullName,
                    Id = u.pr.Id,
                    Active = u.pr.Active,
                    Image = u.pr.Image,
                    Username = u.pr.Username,
                    Address = u.pr.Address,
                    Age = u.pr.Age,
                    CreateDate = u.pr.CreateDate,
                    Email = u.pr.Email,
                    Password = u.pr.Password,
                    Position = u.pr.Position,
                    Role = u.pr.Role,
                    Sex = u.pr.Sex
                })
                .ToListAsync();

            var pagination = new Pagination<AdminModel>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageIndex = ctx.PageIndex,
                PageSize = ctx.PageSize,
            };

            return new ApiSuccessResult<Pagination<AdminModel>>(pagination);
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
                            .FirstOrDefaultAsync(p => p.Id == id);

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

        public Task<Admin> GetCheckActive(string name, bool showHidden = true)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            var query = from p in _context.Admins.AsQueryable() where p.Username == name select p;
            if (showHidden)
            {
                query = from p in query where p.Active select p;
            }
            return query.FirstOrDefaultAsync();
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
                Role = model.Role,
                Id = model.Id,
                Position = model.Position,
                CreateDate = model.CreateDate,
                Address = model.Address,
                Age = model.Age,
                FullName = model.FullName,
                Sex = model.Sex,
                Image = model.Image,
                Email = model.Email
            };

            await _context.Admins.AddAsync(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.Id,
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
            item.Position = model.Position;
            item.CreateDate = model.CreateDate;
            item.Address = model.Address;
            item.Age = model.Age;
            item.FullName = model.FullName;
            item.Sex = model.Sex;
            item.Image = model.Image;
            item.Email = model.Email;

            _context.Admins.Update(item);

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

            var item = await _context.Admins.FindAsync(id);

            _context.Admins.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        public async Task<bool> GetAdmin(Admin entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            var res = await _dapper.CheckName<Admin>(entity.Username, nameof(Admin));
            return res > 0;
        }

        #endregion Method
    }
}