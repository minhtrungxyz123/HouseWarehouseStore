using System;
using System.Collections.Generic;

namespace HouseWarehouseStore.Data.Entities
{
    public partial class Size
    {
        public string SizeId { get; set; } = null!;
        public string? SizeProduct { get; set; }
    }
}
