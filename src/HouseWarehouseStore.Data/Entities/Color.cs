using System;
using System.Collections.Generic;

namespace HouseWarehouseStore.Data.Entities
{
    public partial class Color
    {
        public string ColorId { get; set; } = null!;
        public string? Code { get; set; }
        public string? NameColor { get; set; }
    }
}
