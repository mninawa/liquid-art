using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Notifications.Repository.Abstraction;
using Notifications.Repository.Model;

namespace Notifications.Repository
{
    public class Repository : IRepository
    {
        
        private readonly ILogger<Repository> logger;
        private readonly IMongoCollection<ClientCache> clientCache;
        private readonly IMongoCollection<Notification> notificationCollection;
        private readonly IMongoCollection<OutboxMessage> outboxMessages;
        private readonly IMongoCollection<Template> templates;
        
        public Repository(IMongoConnectionSetting mongoConnectionSetting, ILogger<Repository> logger)
        {
            var mongoClient = new MongoClient(mongoConnectionSetting.ConnectionString);
            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(mongoConnectionSetting.DatabaseName);
            notificationCollection = mongoDatabase.GetCollection<Notification>(mongoConnectionSetting.NotificationCollectionName);
            clientCache = mongoDatabase.GetCollection<ClientCache>(mongoConnectionSetting.CacheCollectionName);
            outboxMessages = mongoDatabase.GetCollection<OutboxMessage>(mongoConnectionSetting.OutboxCollectionName);
            templates = mongoDatabase.GetCollection<Template>(mongoConnectionSetting.TemplateCollectionName);
            
            this.logger = logger;
        }
        

        public async Task CreateNotification(Notification notification, CancellationToken cancellationToken = default)
        {
            await notificationCollection.InsertOneAsync(notification, cancellationToken);
        }

        public async Task<Notification?> GetNotificationById(string id, CancellationToken cancellationToken = default)
        {
            return await notificationCollection.FindSync(o => o.Id == id).FirstOrDefaultAsync();
        }

     
        public async Task<List<Notification>> GetAllNotifications(int numberOfRecords,CancellationToken cancellationToken = default)
        {
            return await notificationCollection.Find(s => true).Limit(numberOfRecords).ToListAsync(cancellationToken);
        }

        public async Task<bool> DeleteNotification(string id, CancellationToken cancellationToken = default)
        {
            var notification = await notificationCollection.DeleteOneAsync(s => s.Id == id);
            if (notification == null) return false;
            
            return true;
        }

        public async Task CreateClientCache(string clientId, CancellationToken cancellationToken = default)
        {
            var lCache = new ClientCache
            {
                Id = clientId
            };
            await clientCache.InsertOneAsync(lCache, cancellationToken);
        }

        public async Task<bool> CheckClientExistence(string customerId, CancellationToken cancellationToken = default)
        {
            var cust = await clientCache.FindSync(c => c.Id == customerId).FirstOrDefaultAsync();
            
            if (cust == null) return false;
            
            return true;
        }

        public async Task CreateOutboxMessage(OutboxMessage outboxMessage, CancellationToken cancellationToken = default)
        {
            await outboxMessages.InsertOneAsync(outboxMessage);
        }
        
        public async Task UpdateOutboxMessage(OutboxMessage outboxMessage, CancellationToken cancellationToken = default)
        {
            var filter = Builders<OutboxMessage>.Filter
                .Eq(f => f.Id, outboxMessage.Id);
            var update = Builders<OutboxMessage>.Update
                .Set(x => x.Processed, true);
            await outboxMessages.UpdateOneAsync(filter, update);
        }

        public async Task<List<OutboxMessage>> GetPendingOutboxMessages(CancellationToken cancellationToken = default)
        {
            return await outboxMessages.FindSync(m => m.Processed == false).ToListAsync();
        }
        public async Task<Template?> GetTemplateVersionById(string id, CancellationToken cancellationToken = default)
        {
            return await templates.FindSync(o => o.Version == id).FirstOrDefaultAsync();
        }
    }
}
