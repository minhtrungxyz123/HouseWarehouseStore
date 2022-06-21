using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HouseWarehouseStore.Models
{
    public class ProductSizeColorModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Chưa nhập thông tin")]
        public string ProductId { get; set; }
        public string ColorId { get; set; }
        public string SizeId { get; set; }

        public string? ColorName { get; set; }
        public string? SizeName { get; set; }
        public string? ProductsProductId { get; set; }
        public IList<SelectListItem>? AvailableProduct { get; set; }
        public IList<SelectListItem>? AvailableColor { get; set; }
        public IList<SelectListItem>? AvailableSize { get; set; }
    }
}