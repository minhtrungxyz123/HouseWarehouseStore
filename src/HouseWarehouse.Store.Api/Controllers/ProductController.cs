using HouseWarehouseStore.Service;
using Microsoft.AspNetCore.Mvc;

namespace HouseWarehouse.Store.Api.Controllers
{
    [Route("product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _productService.GetAll();

            return Ok(comments);
        }
    }
}