using System;
using System.Collections.Generic;

namespace HouseWarehouseStore.Data.Entities
{
    public partial class ProductSizeColor
    {
        public string Id { get; set; } = null!;
        public string ProductId { get; set; } = null!;
        public string ColorId { get; set; } = null!;
        public string SizeId { get; set; } = null!;
        public string? ProductsProductId { get; set; }
    }
}
