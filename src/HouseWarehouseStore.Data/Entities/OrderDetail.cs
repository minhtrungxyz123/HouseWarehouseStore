using System;
using System.Collections.Generic;

namespace HouseWarehouseStore.Data.Entities
{
    public partial class OrderDetail
    {
        public string ProductId { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string OrderId { get; set; } = null!;
        public string Color { get; set; } = null!;
        public string Size { get; set; } = null!;
    }
}
