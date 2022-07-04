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
            modelBuilder.ApplyConfiguration(new AdminConfiguration());
            modelBuilder.ApplyConfiguration(new AggregatedCounterConfiguration());
            modelBuilder.ApplyConfiguration(new AlbumConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleConfiguration());
            modelBuilder.ApplyConfiguration(new BannerConfiguration());
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new CollectionConfiguration());
            modelBuilder.ApplyConfiguration(new ColorConfiguration());
            modelBuilder.ApplyConfiguration(new ConfigSiteConfiguration());
            modelBuilder.ApplyConfiguration(new ContactConfiguration());
            modelBuilder.ApplyConfiguration(new CounterConfiguration());
            modelBuilder.ApplyConfiguration(new HashConfiguration());
            modelBuilder.ApplyConfiguration(new JobConfiguration());
            modelBuilder.ApplyConfiguration(new JobParameterConfiguration());
            modelBuilder.ApplyConfiguration(new JobQueueConfiguration());
            modelBuilder.ApplyConfiguration(new ListConfiguration());
            modelBuilder.ApplyConfiguration(new MemberConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCategoryConguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductLikeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductSizeColorConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewProductConfiguration());
            modelBuilder.ApplyConfiguration(new SchemaConfiguration());
            modelBuilder.ApplyConfiguration(new ServerConfiguration());
            modelBuilder.ApplyConfiguration(new SetConfiguration());
            modelBuilder.ApplyConfiguration(new SizeConfiguration());
            modelBuilder.ApplyConfiguration(new StateConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new UserVoucherConfiguration());
            modelBuilder.ApplyConfiguration(new VideoConfiguration());
            modelBuilder.ApplyConfiguration(new VoucherConfiguration());
            modelBuilder.ApplyConfiguration(new FilesConfiguration());
            modelBuilder.ApplyConfiguration(new TagProductConfiguration());
            modelBuilder.ApplyConfiguration(new UploadConfiguration());
            //modelBuilder.ApplyConfiguration(new NotificationConfiguration());
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
        public virtual DbSet<Entities.TagProduct> TagProducts { get; set; }
        public virtual DbSet<Entities.File> Files { get; set; }
        //public  virtual DbSet<Notification> Notifications { get; set; }
    }
}