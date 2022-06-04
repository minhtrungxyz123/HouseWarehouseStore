using System;
using System.Collections.Generic;

namespace HouseWarehouseStore.Data.Entities
{
    public partial class About
    {
        public string AboutId { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string? Image { get; set; }
        public string? CoverImage { get; set; }
        public string? Body { get; set; }
        public int Sort { get; set; }
        public bool Active { get; set; }
    }
}
