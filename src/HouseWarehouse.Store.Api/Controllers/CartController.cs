using HouseWarehouseStore.Service;
using Microsoft.AspNetCore.Mvc;

namespace HouseWarehouse.Store.Api.Controllers
{
    [Route("cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAllPaging([FromQuery] CartSearchContext ctx)
        {
            var products = await _cartService.GetAllPaging(ctx);
            return Ok(products);
        }
    }
}