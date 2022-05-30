namespace HouseWarehouseStore.Models
{
    public class AlbumModel
    {
        public int? AlbumId { get; set; }
        public string Name { get; set; }
        public string ListImage { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public int Sort { get; set; }
        public bool Home { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDate { get; set; }
    }
}