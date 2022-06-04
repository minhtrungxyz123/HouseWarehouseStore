using System;
using System.Collections.Generic;

namespace HouseWarehouseStore.Data.Entities
{
    public partial class Upload
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Image { get; set; }
    }
}
