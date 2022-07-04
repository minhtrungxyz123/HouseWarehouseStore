using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using Master.Api.SignalRHubs;
using Master.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Master.Api.Controllers
{
    [Route("size")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        #region Fields

        private readonly ISizeService _sizeService;
        private readonly IHubContext<ConnectRealTimeHub> _hubContext;

        public SizeController(ISizeService sizeService, IHubContext<ConnectRealTimeHub> hubContext)
        {
            _sizeService = sizeService;
            _hubContext = hubContext;
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

        //[HttpPost("create")]
        //public async Task<IActionResult> Post()
        //{
        //    var create = new SizeModel();
        //    for (int i = 0; i < 100000; i++)
        //    {
        //        create.SizeId = Guid.NewGuid().ToString();
        //        create.SizeProduct = "Kho tạm " + i;
        //        await _sizeService.Create(create);
        //    }

        //    return Ok(create);
        //}

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] SizeModel model)
        {
            var result = await _sizeService.Create(model);

            if (result.Result > 0)
            {
                await _hubContext.Clients.All.SendAsync("MasterCreateToCLient", model);
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
                await _hubContext.Clients.All.SendAsync("MasterEditToCLient", model, id);
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
            await _hubContext.Clients.All.SendAsync("MasterDeleteToCLient", id);
            return Ok(result);
        }

        #endregion Method
    }
}