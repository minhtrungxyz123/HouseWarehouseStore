using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseWarehouseStore.Models
{
    public class CollectionModel
    {
        public string? CollectionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public string Body { get; set; }
        public int Quantity { get; set; }
        public string Factory { get; set; }
        public decimal Price { get; set; }
        public int Sort { get; set; }
        public bool Hot { get; set; }
        public bool Home { get; set; }
        public bool Active { get; set; }
        public string TitleMeta { get; set; }
        public string Content { get; set; }
        public bool StatusProduct { get; set; }
        public string BarCode { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }

        public IFormFile? filesadd { get; set; }
    }
}