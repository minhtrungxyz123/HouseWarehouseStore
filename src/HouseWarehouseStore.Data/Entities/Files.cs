namespace HouseWarehouseStore.Data.Entities
{
    public class Files
    {
        public int Id { get; set; }
        public string FileName { get; set; }

        public string Path { get; set; }

        public string Extension { get; set; }

        public string MimeType { get; set; }

        public decimal Size { get; set; }

        public  string CollectionId { get; set; }
    }
}