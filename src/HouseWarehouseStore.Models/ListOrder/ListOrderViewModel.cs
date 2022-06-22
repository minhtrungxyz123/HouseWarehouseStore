using HouseWarehouseStore.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace HouseWarehouseStore.Models
{
    public class OrderViewModel
    {
        public Order Order { get; set; }

        public IEnumerable<OrderDetail> OrderDetails { get; set; }
        public IEnumerable<UserVoucher> UserVoucher { get; set; }
    }

    public class OrderDetailProduct
    {
        public Product Products { get; set; }
        public int Quantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal? Price { get; set; }
    }

    public class ListOrderViewModel
    {
        public PaginatedList<Order> Orders { get; set; }

        [StringLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string MaDonhang { get; set; }

        [StringLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string CustomerName { get; set; }

        [EmailAddress(ErrorMessage = "Email không hợp lệ"), StringLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string CustomerEmail { get; set; }

        [StringLength(20, ErrorMessage = "Tối đa 20 ký tự")]
        public string CustomerMobile { get; set; }

        public int Status { get; set; }
        public int Payment { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }

        [Required]
        public int PageSize { get; set; }

        public IEnumerable<UserVoucher> UserVoucher { get; set; }
    }

    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public static PaginatedList<T> CreateAsync(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}