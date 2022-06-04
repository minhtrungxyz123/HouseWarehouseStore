using System;
using System.Collections.Generic;

namespace HouseWarehouseStore.Data.Entities
{
    public partial class ProductLike
    {
        public string ProductLikeId { get; set; } = null!;
        public string ProductId { get; set; } = null!;
        public string MemberId { get; set; } = null!;
        public string? ProductsProductId { get; set; }
    }
}
