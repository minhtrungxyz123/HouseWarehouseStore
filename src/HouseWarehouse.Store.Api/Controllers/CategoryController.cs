using HouseWarehouseStore.Service;
using Microsoft.AspNetCore.Mvc;

namespace HouseWarehouse.Store.Api.Controllers
{
    [Route("product-category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll(bool showHidden = true)
        {
            var comments = await _categoryService.GetAll(showHidden);

            return Ok(comments);
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAllPaging([FromQuery] CategorySearchContext ctx)
        {
            var products = await _categoryService.GetAllPaging(ctx);
            return Ok(products);
        }
    }
}