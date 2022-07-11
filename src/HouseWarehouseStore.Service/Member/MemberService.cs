using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Entities;
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
    }
}