namespace HouseWarehouseStore.Models
{
    public class CartModel
    {
        public int RecordId { get; set; }
        public string CartId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public DateTime DateCreated { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
    }
}