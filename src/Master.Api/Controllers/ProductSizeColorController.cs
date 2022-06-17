using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using Master.Service;
using Microsoft.AspNetCore.Mvc;

namespace Master.Api.Controllers
{
    [Route("product-size-color")]
    [ApiController]
    public class ProductSizeColorController : ControllerBase
    {
        #region Fields

        private readonly IProductSizeColorService _productSizeColorService;

        public ProductSizeColorController(IProductSizeColorService productSizeColorService)
        {
            _productSizeColorService = productSizeColorService;
        }

        #endregion Fields

        #region List

        [Route("get-by-id")]
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _productSizeColorService.GetByIdAsyn(id);
            return Ok(user);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _productSizeColorService.GetAll());
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAllPaging([FromQuery] ProductSizeColorSearchContext ctx)
        {
            var products = await _productSizeColorService.GetAllPaging(ctx);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var item = await _productSizeColorService.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"ProductSizeColor with id: {id} is not found"));
            }

            return Ok(item);
        }

        #endregion List

        #region Method

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] ProductSizeColorModel model)
        {
            var result = await _productSizeColorService.Create(model);

            if (result.Result > 0)
            {
                return RedirectToAction(nameof(Get), new { id = result.Id });
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Create ProductSizeColor failed"));
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put([FromBody] ProductSizeColorModel model, string id)
        {
            var item = await _productSizeColorService.GetById(id);
            if (item == null)
                return NotFound(new ApiNotFoundResponse($"ProductSizeColor with id: {id} is not found"));

            var result = await _productSizeColorService.Update(id, model);

            if (result.Result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Update ProductSizeColor failed"));
            }
        }

        [Route("delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _productSizeColorService.Delete(id);
            return Ok(result);
        }

        #endregion Method
    }
}