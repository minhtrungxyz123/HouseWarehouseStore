using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using Master.Service;
using Microsoft.AspNetCore.Mvc;

namespace Master.Api.Controllers
{
    [Route("product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Fields

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        #endregion Fields

        #region List

        [Route("get-available")]
        [HttpGet]
        public async Task<IActionResult> GetAvailableList(bool showHidden = true)
        {
            var user = _productService.GetActive(showHidden);
            return Ok(user);
        }

        [Route("get-by-id")]
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _productService.GetByIdAsyn(id);
            return Ok(user);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _productService.GetAll());
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAllPaging([FromQuery] ProductSearchContext ctx)
        {
            var products = await _productService.GetAllPaging(ctx);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var item = await _productService.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"Product with id: {id} is not found"));
            }

            return Ok(item);
        }

        #endregion List

        #region Method

        [HttpPost("create")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post([FromForm] ProductModel model)
        {
            var result = await _productService.Create(model);

            if (result.Result > 0)
            {
                return RedirectToAction(nameof(Get), new { id = result.Id });
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Create Product failed"));
            }
        }

        [HttpPut("update/{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Put([FromForm] ProductModel model, string id)
        {
            var item = await _productService.GetById(id);
            if (item == null)
                return NotFound(new ApiNotFoundResponse($"Product with id: {id} is not found"));

            var result = await _productService.Update(id, model);

            if (result.Result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Update Product failed"));
            }
        }

        [Route("delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _productService.Delete(id);
            return Ok(result);
        }

        #endregion Method

        #region Utilities

        [Route("get-status-product")]
        [HttpGet]
        public async Task<IActionResult> GetStatusProduct(bool showHidden = true)
        {
            var user = _productService.GetStatusProduct(showHidden);
            return Ok(user);
        }

        [Route("get-active")]
        [HttpGet]
        public async Task<IActionResult> GetActive(bool showHidden = true)
        {
            var user = _productService.GetActive(showHidden);
            return Ok(user);
        }

        [Route("get-home")]
        [HttpGet]
        public async Task<IActionResult> GetHome(bool showHidden = true)
        {
            var user = _productService.GetHome(showHidden);
            return Ok(user);
        }

        [Route("get-hot")]
        [HttpGet]
        public async Task<IActionResult> GetHot(bool showHidden = true)
        {
            var user = _productService.GetHot(showHidden);
            return Ok(user);
        }

        #endregion Utilities
    }
}