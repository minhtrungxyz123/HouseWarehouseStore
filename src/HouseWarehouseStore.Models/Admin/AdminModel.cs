namespace HouseWarehouseStore.Models
{
    public class AdminModel
    {
        public string? AdminId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public string Role { get; set; }
    }
}