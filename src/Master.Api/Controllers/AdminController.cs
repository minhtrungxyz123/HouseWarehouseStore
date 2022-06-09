using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using Master.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Master.Api.Controllers
{
    [Route("admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        #region Fields

        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        #endregion Fields

        #region List

        [Route("get-by-id")]
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _adminService.GetByIdAsyn(id);
            return Ok(user);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _adminService.GetAll());
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAllPaging([FromQuery] AdminSearchContext ctx)
        {
            var products = await _adminService.GetAllPaging(ctx);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var item = await _adminService.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"Admin with id: {id} is not found"));
            }

            return Ok(item);
        }

        [Route("get-available")]
        [HttpGet]
        public async Task<IActionResult> GetAvailableList(bool showHidden = true)
        {
            var user = _adminService.GetMvcListItems(showHidden);
            return Ok(user);
        }

        [Route("check-active")]
        [HttpGet]
        public async Task<IActionResult> GetCheckActive(string name ,bool showHidden = true)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            var checkActive = _adminService.GetCheckActive(name, showHidden);

            if (checkActive == null)
            {
                return NotFound(new ApiNotFoundResponse($"Admin with name: {name} is not found"));
            }

            return Ok(checkActive);
        }

        #endregion List

        #region Method

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] AdminModel model)
        {
            var result = await _adminService.Create(model);

            if (result.Result > 0)
            {
                return RedirectToAction(nameof(Get), new { id = result.Id });
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Create admin failed"));
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put([FromBody] AdminModel model, string id)
        {
            var item = await _adminService.GetById(id);
            if (item == null)
                return NotFound(new ApiNotFoundResponse($"Admin with id: {id} is not found"));

            var result = await _adminService.Update(id, model);

            if (result.Result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Update admin failed"));
            }
        }

        [Route("delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _adminService.Delete(id);
            return Ok(result);
        }

        #endregion Method
    }
}
