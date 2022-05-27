using HouseWarehouseStore.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Warehouse.Data.EF
{
    public class HouseWarehouseStoreDbContextFactory : IDesignTimeDbContextFactory<HouseWarehouseStoreDbContext>
    {
        public HouseWarehouseStoreDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("HouseWarehouseStoreDatabase");

            var optionsBuilder = new DbContextOptionsBuilder<HouseWarehouseStoreDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new HouseWarehouseStoreDbContext(optionsBuilder.Options);
        }
    }
}