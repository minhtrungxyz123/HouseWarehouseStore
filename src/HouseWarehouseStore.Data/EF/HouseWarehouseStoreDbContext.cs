using HouseWarehouseStore.Data.Configuration;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Data.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HouseWarehouseStore.Data.EF
{
    public class HouseWarehouseStoreDbContext : DbContext, IUnitOfWork
    {
        public HouseWarehouseStoreDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Configure using Fluent API
            modelBuilder.ApplyConfiguration(new AboutConfiguration());
        }

        private readonly IMediator _mediator;

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _mediator.DispatchDomainEventsAsync(this);

            var result = await base.SaveChangesAsync(cancellationToken);
            if (result > 0)
                return true;
            else
                return false;
        }

        public virtual DbSet<About> Abouts { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<AggregatedCounter> AggregatedCounters { get; set; }
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ArticleCategory> ArticleCategories { get; set; }
        public virtual DbSet<Banner> Banners { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Collection> Collections { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<ConfigSite> ConfigSites { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Counter> Counters { get; set; }
        public virtual DbSet<Hash> Hashes { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<JobParameter> JobParameters { get; set; }
        public virtual DbSet<JobQueue> JobQueues { get; set; }
        public virtual DbSet<List> Lists { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductLike> ProductLikes { get; set; }
        public virtual DbSet<ProductSizeColor> ProductSizeColors { get; set; }
        public virtual DbSet<ReviewProduct> ReviewProducts { get; set; }
        public virtual DbSet<Schema> Schemas { get; set; }
        public virtual DbSet<Server> Servers { get; set; }
        public virtual DbSet<Set> Sets { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Upload> Uploads { get; set; }
        public virtual DbSet<UserVoucher> UserVouchers { get; set; }
        public virtual DbSet<Video> Videos { get; set; }
        public virtual DbSet<Voucher> Vouchers { get; set; }
    }
}