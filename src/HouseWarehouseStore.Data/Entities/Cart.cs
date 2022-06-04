using System;
using System.Collections.Generic;

namespace HouseWarehouseStore.Data.Entities
{
    public partial class Cart
    {
        public string RecordId { get; set; } = null!;
        public string CartId { get; set; } = null!;
        public string ProductId { get; set; } = null!;
        public decimal Price { get; set; }
        public int Count { get; set; }
        public DateTime DateCreated { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
    }
}
