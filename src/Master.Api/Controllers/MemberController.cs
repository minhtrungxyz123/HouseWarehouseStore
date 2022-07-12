using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using Master.Service;
using Microsoft.AspNetCore.Mvc;

namespace Master.Api.Controllers
{
    [Route("member")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        #region Fields

        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        #endregion Fields

        #region List

        [Route("get-by-id")]
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _memberService.GetByIdAsyn(id);
            return Ok(user);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _memberService.GetAll());
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAllPaging([FromQuery] MemberSearchContext ctx)
        {
            var products = await _memberService.GetAllPaging(ctx);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var item = await _memberService.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"Member with id: {id} is not found"));
            }

            return Ok(item);
        }

        [Route("get-active")]
        [HttpGet]
        public async Task<IActionResult> GetActive(bool showHidden = true)
        {
            var user = _memberService.GetActive(showHidden);
            return Ok(user);
        }

        #endregion List

        #region Method

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] MemberModel model)
        {
            var result = await _memberService.Create(model);

            if (result.Result > 0)
            {
                return RedirectToAction(nameof(Get), new { id = result.Id });
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Create Member failed"));
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put([FromBody] MemberModel model, string id)
        {
            var item = await _memberService.GetById(id);
            if (item == null)
                return NotFound(new ApiNotFoundResponse($"Member with id: {id} is not found"));

            var result = await _memberService.Update(id, model);

            if (result.Result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Update Member failed"));
            }
        }

        [Route("delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _memberService.Delete(id);
            return Ok(result);
        }

        #endregion Method
    }
}