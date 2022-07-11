using HouseWarehouseStore.Data.Repositories;

namespace HouseWarehouseStore.Data.Entities
{
    public partial class Member : BaseEntity
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string? Address { get; set; }
        public string? Mobile { get; set; }
        public DateTime CreateDate { get; set; }
        public string? HomePage { get; set; }
        public bool Active { get; set; }
        public string Role { get; set; } = null!;
        public bool? ConfirmEmail { get; set; }
        public string? Token { get; set; }
        public bool? LockAccount { get; set; }
    }
}