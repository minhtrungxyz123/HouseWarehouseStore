using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using Master.Service;
using Microsoft.AspNetCore.Mvc;

namespace Master.Api.Controllers
{
    [Route("album")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        #region Fields

        private readonly IAlbumSevice _albumSevice;

        public AlbumController(IAlbumSevice albumSevice)
        {
            _albumSevice = albumSevice;
        }

        #endregion Fields

        #region List

        [Route("get-by-id")]
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _albumSevice.GetByIdAsyn(id);
            return Ok(user);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _albumSevice.GetAll());
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAllPaging([FromQuery] AlbumSearchContext ctx)
        {
            var products = await _albumSevice.GetAllPaging(ctx);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var item = await _albumSevice.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"Album with id: {id} is not found"));
            }

            return Ok(item);
        }

        #endregion List

        #region Method

        [HttpPost("create")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post([FromForm] AlbumModel model)
        {
            var result = await _albumSevice.Create(model);

            if (result.Result > 0)
            {
                return RedirectToAction(nameof(Get), new { id = result.Id });
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Create Album failed"));
            }
        }

        [HttpPut("update/{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Put([FromForm] AlbumModel model, string id)
        {
            var item = await _albumSevice.GetById(id);
            if (item == null)
                return NotFound(new ApiNotFoundResponse($"Album with id: {id} is not found"));

            var result = await _albumSevice.Update(id, model);

            if (result.Result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Update Album failed"));
            }
        }

        [Route("delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _albumSevice.Delete(id);
            return Ok(result);
        }

        #endregion Method

        #region Utilities

        [Route("get-active")]
        [HttpGet]
        public async Task<IActionResult> GetActive(bool showHidden = true)
        {
            var user = _albumSevice.GetActive(showHidden);
            return Ok(user);
        }

        [Route("get-home")]
        [HttpGet]
        public async Task<IActionResult> GetHome(bool showHidden = true)
        {
            var user = _albumSevice.GetHome(showHidden);
            return Ok(user);
        }

        #endregion Utilities
    }
}