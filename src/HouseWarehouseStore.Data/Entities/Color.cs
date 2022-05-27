namespace HouseWarehouseStore.Data.Entities
{
    public partial class Color
    {
        public Color()
        {
            ProductSizeColors = new HashSet<ProductSizeColor>();
        }

        public int ColorId { get; set; }
        public string Code { get; set; }
        public string NameColor { get; set; }

        public virtual ICollection<ProductSizeColor> ProductSizeColors { get; set; }
    }
}