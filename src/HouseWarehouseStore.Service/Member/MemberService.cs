using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseWarehouseStore.Service
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

        #region Method

        public Task<Member> GetCheckActive(string name, bool showHidden = true)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            var query = from p in _context.Members.AsQueryable() where p.Email == name select p;
            if (showHidden)
            {
                query = from p in query where p.Active select p;
            }
            return query.FirstOrDefaultAsync();
        }

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

        #endregion
    }
}