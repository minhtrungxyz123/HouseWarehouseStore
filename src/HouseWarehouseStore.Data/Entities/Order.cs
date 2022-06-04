using System;
using System.Collections.Generic;

namespace HouseWarehouseStore.Data.Entities
{
    public partial class Order
    {
        public string? MaDonHang { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Payment { get; set; }
        public int TypePay { get; set; }
        public int Transport { get; set; }
        public DateTime TransportDate { get; set; }
        public int Status { get; set; }
        public string? OrderMemberId { get; set; }
        public bool Viewed { get; set; }
        public int ThanhToanTruoc { get; set; }
        public string Id { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? Body { get; set; }
        public string Email { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string? Gender { get; set; }
        public string Mobile { get; set; } = null!;
    }
}
