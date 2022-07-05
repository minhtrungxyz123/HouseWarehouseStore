using System.ComponentModel.DataAnnotations;

namespace HouseWarehouseStore.Data.Entities
{
    public class Notification
    {
        [Key]
        public string NotiId { get; set; }
        public string? UserId { get; set; }
        public string? NotiHeader { get; set; }
        public string? NotiBody { get; set; }
        public bool? IsRead { get; set; }
        public string? Url { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}