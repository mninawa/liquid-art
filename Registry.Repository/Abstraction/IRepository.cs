using Registry.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registry.Repository.Abstraction
{
  
    public interface IRepository
    {
        Task CreateClient(Client client, CancellationToken cancellationToken = default);
        Task<Client?> GetClientById(string id, CancellationToken cancellationToken = default);
        Task<List<Client>> GetAllClients(int numberOfRecords,CancellationToken cancellationToken = default);
        Task CreateDevice(Device Device, CancellationToken cancellationToken = default);
        Task<Device?> GetDeviceById(string id, CancellationToken cancellationToken = default);
        Task<List<Device>> GetAllDevices(int numberOfRecords,CancellationToken cancellationToken = default);
        Task CreateOutboxMessage(OutboxMessage outboxMessage, CancellationToken cancellationToken = default);
        Task UpdateOutboxMessage(OutboxMessage outboxMessage, CancellationToken cancellationToken = default);
        Task<List<OutboxMessage>> GetPendingOutboxMessages(CancellationToken cancellationToken = default);
    }
    
    public interface IMongoConnectionSetting
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ClientCollectionName { get; set; }
        public string DeviceCollectionName { get; set; }
        public string OutboxCollectionName { get; set; }
    }
    public class MongoConnectionSetting : IMongoConnectionSetting
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ClientCollectionName { get; set; }
        public string DeviceCollectionName { get; set; }
        public string OutboxCollectionName { get; set; }
    }
}
