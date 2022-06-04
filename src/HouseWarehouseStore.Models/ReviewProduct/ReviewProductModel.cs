namespace HouseWarehouseStore.Models
{
    public class ReviewProductModel
    {
        public string? Id { get; set; }
        public string ProductId { get; set; }
        public int NumberStart { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public bool StatusReview { get; set; }
        public DateTime DateCreated { get; set; }
    }
}