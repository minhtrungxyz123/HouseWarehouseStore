using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public async Task<ApiResult<Member>> GetByIdAsyn(int? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Members
                            .OrderByDescending(p => p.Fullname)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.MemberId == id);

            var model = new Member()
            {
                Address = item.Address,
                Active = item.Active,
                MemberId = item.MemberId,
                Password = item.Password,
                Role = item.Role,
                Fullname = item.Fullname,
                ConfirmEmail = item.ConfirmEmail,
                CreateDate = item.CreateDate,
                Email = item.Email,
                HomePage = item.HomePage,
                LockAccount = item.LockAccount,
                Mobile = item.Mobile,
                ProductLikes = item.ProductLikes,
                Token = item.Token
            };
            return new ApiSuccessResult<Member>(model);
        }

        public async Task<IEnumerable<Member>> GetAll()
        {
            var getAll = await _context.Members
                            .OrderByDescending(p => p.Fullname)
                            .ToListAsync();
            return getAll;
        }

        public async Task<ApiResult<Pagination<Member>>> GetAllPaging(MemberSearchContext ctx)
        {
            var query = _context.Members.AsQueryable();
            if (!string.IsNullOrEmpty(ctx.Keyword))
            {
                query = query.Where(x => x.Fullname.Contains(ctx.Keyword)
                || x.Address.Contains(ctx.Keyword)
                || x.Email.Contains(ctx.Keyword)
                || x.Mobile.Contains(ctx.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query.Skip((ctx.PageIndex - 1) * ctx.PageSize)
                .Take(ctx.PageSize)
                .Select(x => new Member()
                {
                    Fullname = x.Fullname,
                    Password = x.Password,
                    MemberId = x.MemberId,
                    Role = x.Role,
                    Active = x.Active,
                    Mobile = x.Mobile,
                    Email = x.Email,
                    Address = x.Address,
                    ConfirmEmail = x.ConfirmEmail,
                    Token = x.Token,
                    ProductLikes = x.ProductLikes,
                    LockAccount = x.LockAccount,
                    CreateDate = x.CreateDate,
                    HomePage = x.HomePage,
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

        public async Task<Member?> GetById(int? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Members
                            .OrderByDescending(p => p.Fullname)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.MemberId == id);

            return item;
        }

        public IList<Member> GetMvcListItems(bool showHidden = true)
        {
            var query = from p in _context.Members.AsQueryable() select p;
            if (showHidden)
            {
                query = from p in query where p.Active select p;
            }
            query = from p in query orderby p.Fullname select p;
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
                Fullname = model.Fullname,
                Active = model.Active,
                Password = model.Password,
                Role = model.Role,
                Mobile = model.Mobile,
                Token = model.Token,
                Address = model.Address,
                ConfirmEmail = model.ConfirmEmail,
                CreateDate = model.CreateDate,
                Email = model.Email,
                HomePage = model.HomePage,
                LockAccount = model.LockAccount
            };

            await _context.Members.AddAsync(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.MemberId
            };
        }

        public async Task<RepositoryResponse> Update(int? id, MemberModel model)
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
            item.Fullname = model.Fullname;
            item.Active = model.Active;
            item.Password = model.Password;
            item.Role = model.Role;
            item.Mobile = model.Mobile;
            item.Token = model.Token;
            item.Address = model.Address;
            item.ConfirmEmail = model.ConfirmEmail;
            item.CreateDate = model.CreateDate;
            item.Email = model.Email;
            item.HomePage = model.HomePage;
            item.LockAccount = model.LockAccount;

            _context.Members.Update(item);

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

            var item = await _context.Members.FindAsync(id);

            _context.Members.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        #endregion Method
    }
}