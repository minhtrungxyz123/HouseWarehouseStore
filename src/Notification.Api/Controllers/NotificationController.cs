using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using Microsoft.AspNetCore.Mvc;
using Notification.Service;

namespace Notification.Api.Controllers
{
    [Route("notification")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        #region Fields

        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        #endregion Fields

        #region Method

        [Route("delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _notificationService.Delete(id);
            return Ok(result);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _notificationService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var item = await _notificationService.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"Notification with id: {id} is not found"));
            }

            return Ok(item);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] NotificationModel model)
        {
            var result = await _notificationService.Create(model);

            if (result.Result > 0)
            {
                return RedirectToAction(nameof(Get), new { id = result.Id });
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Create Notification failed"));
            }
        }

        #endregion Method
    }
}