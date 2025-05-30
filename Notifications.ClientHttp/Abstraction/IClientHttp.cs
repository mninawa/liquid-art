using Notifications.Shared;

namespace Notifications.ClientHttp.Abstraction
{
    public interface IClientHttp
    {
        Task<NotificationsReadDto?> ReadNotification(int id, CancellationToken cancellationToken = default);
        Task SendWhatsApp(NotificationReadDto request, CancellationToken cancellationToken = default);
    }
}
