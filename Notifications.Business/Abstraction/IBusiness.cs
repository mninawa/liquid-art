using Notifications.Shared;

namespace Notifications.Business.Abstraction
{
    public interface IBusiness
    {
        Task CreateNotification(NotificationInsertDto notificationInsertDto, CancellationToken cancellationToken = default);
        Task<NotificationsReadDto?> GetNotificationById(string id, CancellationToken cancellationToken = default);
        Task<List<NotificationsReadDto>> GetAllNotifications(CancellationToken cancellationToken = default);
        Task<bool> UpdateNotificationStatus(string id, string status, CancellationToken cancellationToken = default);
        Task<bool> DeleteNotification(string id, CancellationToken cancellationToken = default);
        Task CreateClientCache(string id, CancellationToken cancellationToken = default);
        Task SendMessage(string message, CancellationToken cancellationToken = default);
    }
}
