using System;
using System.Collections.Generic;

namespace HouseWarehouseStore.Data.Entities
{
    public partial class Album
    {
        public string AlbumId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string ListImage { get; set; } = null!;
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Body { get; set; }
        public int Sort { get; set; }
        public bool Home { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
