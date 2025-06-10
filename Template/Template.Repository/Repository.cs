using Template.Repository.Abstraction;
using Template.Repository.Model;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Template.Repository
{
    
    public class Repository : IRepository
    {
        
        private readonly ILogger<Repository> logger;
        private readonly IMongoCollection<Templates> templates;
        private readonly IMongoCollection<OutboxMessage> outboxMessages;
        public Repository(IMongoConnectionSetting mongoConnectionSetting, ILogger<Repository> logger)
        {
            var mongoClient = new MongoClient(mongoConnectionSetting.ConnectionString);
            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(mongoConnectionSetting.DatabaseName);
            templates = mongoDatabase.GetCollection<Templates>(mongoConnectionSetting.TemplateCollectionName);
            outboxMessages = mongoDatabase.GetCollection<OutboxMessage>(mongoConnectionSetting.OutboxCollectionName);
            this.logger = logger;
        }
        
        public async Task CreateTemplate(Templates template, CancellationToken cancellationToken = default)
        {
            await templates.InsertOneAsync(template, cancellationToken);
        }

        public async Task<Templates?> GetTemplateById(string id, CancellationToken cancellationToken = default)
        {
            return await templates.FindSync(o => o.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Templates>> GetAllTemplates(int numberOfRecords,CancellationToken cancellationToken = default)
        {
            return await templates.Find(s => true).Limit(numberOfRecords).ToListAsync();
        }
      
        public async Task<bool> Delete(string id, CancellationToken cancellationToken = default)
        {
             // var template = await templates.FindSync(o => o.Id == id,cancellationToken).FirstOrDefaultAsync<object>();
             // if (template == null) return false;
             
           //  template..Remove(order);
            return true;
        }
        
        public async Task<bool> UpdateTemplate(Templates updated, CancellationToken cancellationToken = default)
        {
            
            var filter = Builders<Templates>.Filter
                .Eq(f => f.Id, updated.Id);
            var update = Builders<Templates>.Update.Set(x => x.IsPush, updated.IsPush);
            
            await templates.UpdateOneAsync(filter, update);
            return true;
        }

        // Outbox Messages
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
    }
}
