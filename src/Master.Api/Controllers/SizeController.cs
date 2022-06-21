using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using Master.Service;
using Microsoft.AspNetCore.Mvc;

namespace Master.Api.Controllers
{
    [Route("size")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        #region Fields

        private readonly ISizeService _sizeService;

        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        #endregion Fields

        #region List

        [Route("get-available")]
        [HttpGet]
        public async Task<IActionResult> GetAvailableList()
        {
            var user = _sizeService.GetActive();
            return Ok(user);
        }

        [Route("get-by-id")]
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _sizeService.GetByIdAsyn(id);
            return Ok(user);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _sizeService.GetAll());
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAllPaging([FromQuery] SizeSearchContext ctx)
        {
            var products = await _sizeService.GetAllPaging(ctx);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var item = await _sizeService.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"Size with id: {id} is not found"));
            }

            return Ok(item);
        }

        #endregion List

        #region Method

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] SizeModel model)
        {
            var result = await _sizeService.Create(model);

            if (result.Result > 0)
            {
                return RedirectToAction(nameof(Get), new { id = result.Id });
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Create Size failed"));
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put([FromBody] SizeModel model, string id)
        {
            var item = await _sizeService.GetById(id);
            if (item == null)
                return NotFound(new ApiNotFoundResponse($"Size with id: {id} is not found"));

            var result = await _sizeService.Update(id, model);

            if (result.Result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Update Size failed"));
            }
        }

        [Route("delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _sizeService.Delete(id);
            return Ok(result);
        }

        #endregion Method
    }
}