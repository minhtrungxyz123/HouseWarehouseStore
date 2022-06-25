using HouseWarehouseStore.Data.Repositories;

namespace HouseWarehouseStore.Data.Entities
{
    public partial class Admin : BaseEntity
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool Active { get; set; }
        public string Role { get; set; } = null!;
        public string? Image { get; set; }
        public string? FullName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Address { get; set; }
        public string? Sex { get; set; }
        public string? Age { get; set; }
        public string? Position { get; set; }
        public string? Email { get; set; }
    }
}