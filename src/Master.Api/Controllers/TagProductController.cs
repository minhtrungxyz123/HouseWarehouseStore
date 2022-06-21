using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using Master.Service;
using Microsoft.AspNetCore.Mvc;

namespace Master.Api.Controllers
{
    [Route("tag-product")]
    [ApiController]
    public class TagProductController : ControllerBase
    {
        #region Fields

        private readonly ITagProductService _tagProductService;

        public TagProductController(ITagProductService tagProductService)
        {
            _tagProductService = tagProductService;
        }

        #endregion Fields

        #region List

        [Route("get-by-id")]
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _tagProductService.GetByIdAsyn(id);
            return Ok(user);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _tagProductService.GetAll());
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAllPaging([FromQuery] TagProductSearchContext ctx)
        {
            var products = await _tagProductService.GetAllPaging(ctx);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var item = await _tagProductService.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"TagProduct with id: {id} is not found"));
            }

            return Ok(item);
        }

        #endregion List

        #region Method

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] TagProductModel model)
        {
            var result = await _tagProductService.Create(model);

            if (result.Result > 0)
            {
                return RedirectToAction(nameof(Get), new { id = result.Id });
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Create TagProduct failed"));
            }
        }

        [Route("delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _tagProductService.Delete(id);
            return Ok(result);
        }

        #endregion Method
    }
}