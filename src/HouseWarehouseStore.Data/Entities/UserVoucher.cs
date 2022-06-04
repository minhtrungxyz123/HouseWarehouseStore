using System;
using System.Collections.Generic;

namespace HouseWarehouseStore.Data.Entities
{
    public partial class UserVoucher
    {
        public string Id { get; set; } = null!;
        public string? MaDonHang { get; set; }
        public decimal SumHd { get; set; }
        public string Code { get; set; } = null!;
    }
}
