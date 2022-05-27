namespace HouseWarehouseStore.Data.Entities
{
    public partial class Tag
    {
        public Tag()
        {
            Products = new HashSet<Product>();
        }

        public int TagId { get; set; }
        public string Name { get; set; }
        public int Soft { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}