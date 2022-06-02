using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseWarehouseStore.Data.Entities
{
    public class Files
    {
        public string Id { get; set; }
        public string FileName { get; set; }

        public string Path { get; set; }

        public string Extension { get; set; }

        public string MimeType { get; set; }

        public decimal Size { get; set; }

        public  int? CollectionId { get; set; }
    }
}