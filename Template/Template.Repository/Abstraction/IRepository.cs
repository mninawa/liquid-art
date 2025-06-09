using Template.Repository.Model;

namespace Template.Repository.Abstraction
{
  
    public interface IRepository
    {
        Task CreateTemplate(Templates template, CancellationToken cancellationToken = default);
        Task<Templates?> GetTemplateById(string id, CancellationToken cancellationToken = default);
        Task<List<Templates>> GetAllTemplates(int numberOfRecords,CancellationToken cancellationToken = default);
        Task<bool> UpdateTemplate(Templates template, CancellationToken cancellationToken = default);
        Task<bool> Delete(string id, CancellationToken cancellationToken = default);
        Task CreateOutboxMessage(OutboxMessage outboxMessage, CancellationToken cancellationToken = default);
        Task UpdateOutboxMessage(OutboxMessage outboxMessage, CancellationToken cancellationToken = default);
        Task<List<OutboxMessage>> GetPendingOutboxMessages(CancellationToken cancellationToken = default);
    }
    
    public interface IMongoConnectionSetting
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string TemplateCollectionName { get; set; }
        public string DeviceCollectionName { get; set; }
        public string OutboxCollectionName { get; set; }
    }
    public class MongoConnectionSetting : IMongoConnectionSetting
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string TemplateCollectionName { get; set; }
        public string DeviceCollectionName { get; set; }
        public string OutboxCollectionName { get; set; }
    }
}
