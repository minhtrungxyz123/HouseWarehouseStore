using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using Master.Service;
using Microsoft.AspNetCore.Mvc;

namespace Master.Api.Controllers
{
    [Route("voucher")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        #region Fields

        private readonly IVoucherService _voucherService;

        public VoucherController(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        #endregion Fields

        #region List

        [Route("get-by-id")]
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _voucherService.GetByIdAsyn(id);
            return Ok(user);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _voucherService.GetAll());
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAllPaging([FromQuery] VoucherSearchContext ctx)
        {
            var products = await _voucherService.GetAllPaging(ctx);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var item = await _voucherService.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"Voucher with id: {id} is not found"));
            }

            return Ok(item);
        }

        [Route("get-active")]
        [HttpGet]
        public async Task<IActionResult> GetActive(bool showHidden = true)
        {
            var user = _voucherService.GetActive(showHidden);
            return Ok(user);
        }

        [Route("get-type")]
        [HttpGet]
        public async Task<IActionResult> GetType(bool showHidden = true)
        {
            var user = _voucherService.GetType(showHidden);
            return Ok(user);
        }

        [Route("get-condition")]
        [HttpGet]
        public async Task<IActionResult> GetCondition(bool showHidden = true)
        {
            var user = _voucherService.GetCondition(showHidden);
            return Ok(user);
        }

        #endregion List

        #region Method

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] VoucherModel model)
        {
            var result = await _voucherService.Create(model);

            if (result.Result > 0)
            {
                return RedirectToAction(nameof(Get), new { id = result.Id });
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Create Voucher failed"));
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put([FromBody] VoucherModel model, string id)
        {
            var item = await _voucherService.GetById(id);
            if (item == null)
                return NotFound(new ApiNotFoundResponse($"Voucher with id: {id} is not found"));

            var result = await _voucherService.Update(id, model);

            if (result.Result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Update Voucher failed"));
            }
        }

        [Route("delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _voucherService.Delete(id);
            return Ok(result);
        }

        #endregion Method
    }
}