namespace HouseWarehouseStore.Models
{
    public class FilesModel
    {
        public int? Id { get; set; }
        public string FileName { get; set; }

        public string Path { get; set; }

        public string Extension { get; set; }

        public string MimeType { get; set; }

        public decimal Size { get; set; }

        public int? CollectionId { get; set; }
    }
}