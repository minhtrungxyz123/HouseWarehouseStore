using System;
using System.Collections.Generic;

namespace HouseWarehouseStore.Data.Entities
{
    public partial class Tag
    {
        public string TagId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Soft { get; set; }
        public bool Active { get; set; }
    }
}
