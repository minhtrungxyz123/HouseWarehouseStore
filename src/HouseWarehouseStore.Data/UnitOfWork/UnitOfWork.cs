using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Data.Repositories;

namespace HouseWarehouseStore.Data.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private readonly HouseWarehouseStoreDbContext _context;

        public UnitOfWork(HouseWarehouseStoreDbContext dbContext)
        {
            _context = dbContext;
        }

        private RepositoryEF<Voucher> _voucher;
        private GenericRepository<Order> _orderRepository;
        private GenericRepository<UserVoucher> _usevoucher;
        private GenericRepository<OrderDetail> _orderdetailRepository;

        public RepositoryEF<Voucher> VoucherRepository => _voucher ?? (_voucher = new RepositoryEF<Voucher>(_context));
        public GenericRepository<Order> OrderRepository => _orderRepository ?? (_orderRepository = new GenericRepository<Order>(_context));
        public GenericRepository<UserVoucher> UserVoucherRepository => _usevoucher ?? (_usevoucher = new GenericRepository<UserVoucher>(_context));
        public GenericRepository<OrderDetail> OrderDetailRepository => _orderdetailRepository ?? (_orderdetailRepository = new GenericRepository<OrderDetail>(_context));

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void SaveNotAync()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}