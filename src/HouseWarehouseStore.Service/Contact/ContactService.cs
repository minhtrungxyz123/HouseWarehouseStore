using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HouseWarehouseStore.Service
{
    public class ContactService : IContactService
    {
        private readonly HouseWarehouseStoreDbContext _context;

        public ContactService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Contact>> GetAll()
        {
            return await _context.Contacts
                             .OrderByDescending(p => p.Fullname)
                             .ToListAsync();
        }
    }
}