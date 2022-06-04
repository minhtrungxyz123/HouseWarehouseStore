namespace HouseWarehouseStore.Models
{
    public class MemberModel
    {
        public string? MemberId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public DateTime CreateDate { get; set; }
        public string HomePage { get; set; }
        public bool Active { get; set; }
        public string Role { get; set; }
        public bool? ConfirmEmail { get; set; }
        public string Token { get; set; }
        public bool? LockAccount { get; set; }
    }
}