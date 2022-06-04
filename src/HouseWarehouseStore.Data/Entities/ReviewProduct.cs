using System;
using System.Collections.Generic;

namespace HouseWarehouseStore.Data.Entities
{
    public partial class ReviewProduct
    {
        public string Id { get; set; } = null!;
        public string ProductId { get; set; } = null!;
        public int NumberStart { get; set; }
        public string? UserId { get; set; }
        public string? Content { get; set; }
        public bool StatusReview { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
