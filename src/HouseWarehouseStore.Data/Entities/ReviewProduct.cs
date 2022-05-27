namespace HouseWarehouseStore.Data.Entities
{
    public partial class ReviewProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int NumberStart { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public bool StatusReview { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Product Product { get; set; }
    }
}