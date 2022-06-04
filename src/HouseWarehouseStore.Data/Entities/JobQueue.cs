using System;
using System.Collections.Generic;

namespace HouseWarehouseStore.Data.Entities
{
    public partial class JobQueue
    {
        public string Id { get; set; } = null!;
        public long JobId { get; set; }
        public string Queue { get; set; } = null!;
        public DateTime? FetchedAt { get; set; }
    }
}
