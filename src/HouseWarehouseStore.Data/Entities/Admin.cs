using System;
using System.Collections.Generic;

namespace HouseWarehouseStore.Data.Entities
{
    public partial class Admin
    {
        public string AdminId { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool Active { get; set; }
        public string Role { get; set; } = null!;
    }
}
