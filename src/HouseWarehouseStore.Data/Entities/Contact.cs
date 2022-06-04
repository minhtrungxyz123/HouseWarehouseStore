using System;
using System.Collections.Generic;

namespace HouseWarehouseStore.Data.Entities
{
    public partial class Contact
    {
        public string ContactId { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string Address { get; set; } = null!;
        public long Mobile { get; set; }
        public string Email { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Body { get; set; } = null!;
        public DateTime CreateDate { get; set; }
    }
}
