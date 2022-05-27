namespace HouseWarehouseStore.Data.Entities
{
    public partial class About
    {
        public int AboutId { get; set; }
        public string Subject { get; set; }
        public string Image { get; set; }
        public string CoverImage { get; set; }
        public string Body { get; set; }
        public int Sort { get; set; }
        public bool Active { get; set; }
    }
}