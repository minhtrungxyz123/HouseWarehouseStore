using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using Master.Service;
using Microsoft.AspNetCore.Mvc;

namespace Master.Api.Controllers
{
    [Route("tag")]
    [ApiController]
    public class TagController : ControllerBase
    {
        #region Fields

        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        #endregion Fields

        #region List

        [Route("get-by-id")]
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _tagService.GetByIdAsyn(id);
            return Ok(user);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _tagService.GetAll());
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAllPaging([FromQuery] TagSearchContext ctx)
        {
            var products = await _tagService.GetAllPaging(ctx);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var item = await _tagService.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"Tag with id: {id} is not found"));
            }

            return Ok(item);
        }

        [Route("get-available")]
        [HttpGet]
        public async Task<IActionResult> GetAvailableList(bool showHidden = true)
        {
            var user = _tagService.GetActive(showHidden);
            return Ok(user);
        }

        #endregion List

        #region Method

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] TagModel model)
        {

            var result = await _tagService.Create(model);

            if (result.Result > 0)
            {
                return RedirectToAction(nameof(Get), new { id = result.Id });
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Create Tag failed"));
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put([FromBody] TagModel model, string id)
        {
            var item = await _tagService.GetById(id);
            if (item == null)
                return NotFound(new ApiNotFoundResponse($"Tag with id: {id} is not found"));

            var result = await _tagService.Update(id, model);

            if (result.Result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Update Tag failed"));
            }
        }

        [Route("delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _tagService.Delete(id);
            return Ok(result);
        }

        #endregion Method
    }
}