namespace HouseWarehouseStore.Data.Entities
{
    public partial class Video
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string VideoLink { get; set; }
        public string Body { get; set; }
        public bool Active { get; set; }
        public bool Home { get; set; }
        public int Soft { get; set; }
        public DateTime DateCreated { get; set; }
    }
}