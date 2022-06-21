using Microsoft.AspNetCore.Mvc.Rendering;

namespace HouseWarehouseStore.Models
{
    public class TagProductModel
    {
        public string TagId { get; set; }
        public string ProductId { get; set; }

        public string? TagName { get; set; }
        public string? ProductName { get; set; }
        public IList<SelectListItem>? AvailableTag { get; set; }
        public IList<SelectListItem>? AvailableProduct { get; set; }
    }
}