using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using Master.Service;
using Microsoft.AspNetCore.Mvc;

namespace Master.Api.Controllers
{
    [Route("configSite")]
    [ApiController]
    public class ConfigSiteController : ControllerBase
    {
        #region Fields

        private readonly IConfigSiteService _configSiteService;

        public ConfigSiteController(IConfigSiteService configSiteService)
        {
            _configSiteService = configSiteService;
        }

        #endregion Fields

        #region List

        [Route("get-by-id")]
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _configSiteService.GetByIdAsyn(id);
            return Ok(user);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _configSiteService.GetAll());
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAllPaging([FromQuery] ConfigSiteSearchContext ctx)
        {
            var products = await _configSiteService.GetAllPaging(ctx);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var item = await _configSiteService.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"ConfigSite with id: {id} is not found"));
            }

            return Ok(item);
        }

        #endregion List

        #region Method

        [HttpPost("create")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post([FromForm] ConfigSiteModel model)
        {
            var result = await _configSiteService.Create(model);

            if (result.Result > 0)
            {
                return RedirectToAction(nameof(Get), new { id = result.Id });
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Create ConfigSite failed"));
            }
        }

        [HttpPut("update/{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Put([FromForm] ConfigSiteModel model, string id)
        {
            var item = await _configSiteService.GetById(id);
            if (item == null)
                return NotFound(new ApiNotFoundResponse($"ConfigSite with id: {id} is not found"));

            var result = await _configSiteService.Update(id, model);

            if (result.Result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Update ConfigSite failed"));
            }
        }

        [Route("delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _configSiteService.Delete(id);
            return Ok(result);
        }

        #endregion Method
    }
}