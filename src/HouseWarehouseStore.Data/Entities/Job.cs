using System;
using System.Collections.Generic;

namespace HouseWarehouseStore.Data.Entities
{
    public partial class Job
    {
        public long Id { get; set; }
        public long? StateId { get; set; }
        public string? StateName { get; set; }
        public string InvocationData { get; set; } = null!;
        public string Arguments { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpireAt { get; set; }
    }
}
