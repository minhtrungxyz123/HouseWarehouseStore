using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Master.Service
{
    public class KeycloakService : IKeycloakService
    {
        #region Fields

        private readonly HouseWarehouseStoreDbContext _context;

        public KeycloakService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region List

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

        #endregion List
    }
}