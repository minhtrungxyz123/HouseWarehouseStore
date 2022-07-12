using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;
using Microsoft.EntityFrameworkCore;

namespace Master.Service
{
    public class MemberService : IMemberService
    {
        #region Fields

        private readonly HouseWarehouseStoreDbContext _context;

        public MemberService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region List

        public async Task<ApiResult<Member>> GetByIdAsyn(string id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Members
                            .OrderByDescending(p => p.Email)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            var model = new Member()
            {
                Id = item.Id,
                Email = item.Email,
                Active = item.Active,
                Address = item.Address,
                ConfirmEmail = item.ConfirmEmail,
                CreateDate = item.CreateDate,
                Fullname = item.Fullname,
                HomePage = item.HomePage,
                LockAccount = item.LockAccount,
                Mobile = item.Mobile,
                Password = item.Password,
                Role = item.Role,
                Token = item.Token
            };
            return new ApiSuccessResult<Member>(model);
        }

        public async Task<IEnumerable<Member>> GetAll()
        {
            var getAll = await _context.Members
                            .OrderByDescending(p => p.Email)
                            .ToListAsync();
            return getAll;
        }

        public async Task<ApiResult<Pagination<Member>>> GetAllPaging(MemberSearchContext ctx)
        {
            var query = _context.Members.AsQueryable();
            if (!string.IsNullOrEmpty(ctx.Keyword))
            {
                query = query.Where(x => x.Email.Contains(ctx.Keyword)
                || x.Fullname.Contains(ctx.Keyword)
                || x.Address.Contains(ctx.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query.Skip((ctx.PageIndex - 1) * ctx.PageSize)
                .Take(ctx.PageSize)
                .Select(x => new Member()
                {
                    Id = x.Id,
                    Address = x.Address,
                    Email = x.Email,
                    Role = x.Role,
                    Password = x.Password,
                    Fullname = x.Fullname,
                    Token = x.Token,
                    Mobile = x.Mobile,
                    Active = x.Active,
                    ConfirmEmail = x.ConfirmEmail,
                    CreateDate = x.CreateDate,
                    HomePage = x.HomePage,
                    LockAccount = x.LockAccount
                }).ToListAsync();

            var pagedResult = new Pagination<Member>()
            {
                TotalRecords = totalRow,
                PageIndex = ctx.PageIndex,
                PageSize = ctx.PageSize,
                Items = data
            };
            return new ApiSuccessResult<Pagination<Member>>(pagedResult);
        }

        public async Task<Member?> GetById(string id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Members
                            .OrderByDescending(p => p.Email)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            return item;
        }

        public IList<Member> GetActive(bool showHidden = true)
        {
            var query = from p in _context.Members.AsQueryable() select p;
            if (showHidden)
            {
                query = from p in query where p.Active select p;
            }
            query = from p in query orderby p.Email select p;
            return query.ToList();
        }

        #endregion List

        #region Method

        public async Task<RepositoryResponse> Create(MemberModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            Member item = new Member()
            {
                Email = model.Email,
                Id = Guid.NewGuid().ToString(),
                Active = model.Active,
                LockAccount = model.LockAccount,
                HomePage = model.HomePage,
                CreateDate = model.CreateDate,
                ConfirmEmail = model.ConfirmEmail,
                Mobile = model.Mobile,
                Address = model.Address,
                Fullname = model.Fullname,
                Password = model.Password,
                Role = model.Role,
                Token = model.Token
            };

            await _context.Members.AddAsync(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.Id
            };
        }

        public async Task<RepositoryResponse> Update(string id, MemberModel model)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var item = await _context.Members.FindAsync(id);
            item.Email = model.Email;
            item.Active = model.Active;
            item.LockAccount = model.LockAccount;
            item.HomePage = model.HomePage;
            item.CreateDate = model.CreateDate;
            item.ConfirmEmail = model.ConfirmEmail;
            item.Mobile = model.Mobile;
            item.Address = model.Address;
            item.Fullname = model.Fullname;
            item.Password = model.Password;
            item.Role = model.Role;
            item.Token = model.Token;

            _context.Members.Update(item);

            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = id,
            };
        }

        public async Task<int> Delete(string id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Members.FindAsync(id);

            _context.Members.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        #endregion Method
    }
}