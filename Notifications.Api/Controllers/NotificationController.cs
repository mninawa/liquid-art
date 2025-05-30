using Notifications.Business.Abstraction;
using Notifications.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Notifications.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class NotificationController : ControllerBase
    {
        private readonly IBusiness _business;
        private readonly ILogger<NotificationController> _logger;

        public NotificationController(IBusiness business, ILogger<NotificationController> logger)
        {
            _business = business;
            _logger = logger;
        }

        [HttpPost(Name = "CreateNotification")]
        public async Task<ActionResult> CreateNotification(NotificationInsertDto notificationInsertDto, CancellationToken cancellationToken = default)
        {
            await _business.CreateNotification(notificationInsertDto, cancellationToken);
            return Ok("Notification created successfully!");
        }

        [HttpGet(Name = "ReadNotification")]
        public async Task<ActionResult<NotificationsReadDto?>> ReadNotification(string id, CancellationToken cancellationToken = default)
        {
            var notification = await _business.GetNotificationById(id, cancellationToken);

            if (notification == null)
            {
                var error = $"Notification with ID {id} not found.";
                _logger.LogWarning(error);
                return NotFound(new { error });
            }
            return new JsonResult(notification);
        }

        [HttpGet(Name = "GetAllNotifications")]
        public async Task<ActionResult<List<NotificationsReadDto >>> GetAllNotifications(CancellationToken cancellationToken = default)
        {
            var notifications = await _business.GetAllNotifications(cancellationToken);

            return new JsonResult(notifications);
        }

        [HttpDelete(Name = "DeleteNotification")]
        public async Task<ActionResult> DeleteNotification(string id, CancellationToken cancellationToken = default)
        {
            var deleted = await _business.DeleteNotification(id, cancellationToken);
            if (deleted == false)
            {
                var error = $"Notification with ID {id} not found. No deletion was made.";
                _logger.LogWarning(error);
                return NotFound(new { error });
            }
            return Ok("Notification deleted successfully!");
        }
    }
}
