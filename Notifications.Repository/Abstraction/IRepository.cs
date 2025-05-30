using Notifications.Repository.Model;

namespace Notifications.Repository.Abstraction
{
    public interface IRepository
    {

        Task CreateNotification(Notification notification, CancellationToken cancellationToken = default);
        Task<Notification?> GetNotificationById(string id, CancellationToken cancellationToken = default);
        Task<List<Notification>> GetAllNotifications(int numberOfRecords,CancellationToken cancellationToken = default);
        Task<bool> DeleteNotification(string id, CancellationToken cancellationToken = default);
        Task CreateClientCache(string clientId, CancellationToken cancellationToken = default);
        Task<bool> CheckClientExistence(string clientId, CancellationToken cancellationToken = default);
        Task CreateOutboxMessage(OutboxMessage outboxMessage, CancellationToken cancellationToken = default);
        Task UpdateOutboxMessage(OutboxMessage outboxMessage, CancellationToken cancellationToken = default);
        Task<List<OutboxMessage>> GetPendingOutboxMessages(CancellationToken cancellationToken = default);
        
        Task<Template?> GetTemplateVersionById(string id, CancellationToken cancellationToken = default);
    }
    
    public interface IMongoConnectionSetting
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string NotificationCollectionName { get; set; }
        public string CacheCollectionName { get; set; }
        public string OutboxCollectionName { get; set; }
        public string TemplateCollectionName { get; set; }
    }
    public class MongoConnectionSetting : IMongoConnectionSetting
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string NotificationCollectionName { get; set; }
        public string CacheCollectionName { get; set; }
        public string OutboxCollectionName { get; set; }
        public string TemplateCollectionName { get; set; }
    }
}
