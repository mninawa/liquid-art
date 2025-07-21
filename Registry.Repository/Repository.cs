using Registry.Repository.Abstraction;
using Registry.Repository.Model;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Registry.Repository
{
    
    public class Repository : IRepository
    {
        
        private readonly ILogger<Repository> logger;
        private readonly IMongoCollection<Facility> facility;
        private readonly IMongoCollection<BusOperator> busOperators;
        private readonly IMongoCollection<City> cityCollection;
        private readonly IMongoCollection<Coupon> couponCollection;
        private readonly IMongoCollection<Client> clients;
        private readonly IMongoCollection<Device> devices;
        private readonly IMongoCollection<OutboxMessage> outboxMessages;
        public Repository(IMongoConnectionSetting mongoConnectionSetting, ILogger<Repository> logger)
        {
            var mongoClient = new MongoClient(mongoConnectionSetting.ConnectionString);
            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(mongoConnectionSetting.DatabaseName);
            clients = mongoDatabase.GetCollection<Client>(mongoConnectionSetting.ClientCollectionName);
            devices = mongoDatabase.GetCollection<Device>(mongoConnectionSetting.DeviceCollectionName);
            outboxMessages = mongoDatabase.GetCollection<OutboxMessage>(mongoConnectionSetting.OutboxCollectionName);
            
            this.logger = logger;
            this.cityCollection = mongoDatabase.GetCollection<City>(mongoConnectionSetting.CityCollectionName);
            this.couponCollection = mongoDatabase.GetCollection<Coupon>(mongoConnectionSetting.CouponCollectionName);
            this.busOperators = mongoDatabase.GetCollection<BusOperator>(mongoConnectionSetting.BusOperaterCollectionName);
            this.facility = mongoDatabase.GetCollection<Facility>(mongoConnectionSetting.FacilityCollectionName);
        }

        // Clients
        public async Task CreateClient(Client client, CancellationToken cancellationToken = default)
        {
            await clients.InsertOneAsync(client, cancellationToken);
        }

        public async Task<Client?> GetClientById(string id, CancellationToken cancellationToken = default)
        {
            return await clients.FindSync(o => o.Id == id).FirstOrDefaultAsync();;
        }

        public async Task<List<Client>> GetAllClients(int numberOfRecords,CancellationToken cancellationToken = default)
        {
            return await clients.Find(s => true).Limit(numberOfRecords).ToListAsync(cancellationToken: cancellationToken);
        }

        // Devices
        public async Task CreateDevice(Device device, CancellationToken cancellationToken = default)
        {
            await devices.InsertOneAsync(device, cancellationToken);
        }

        public async Task<Device?> GetDeviceById(string id, CancellationToken cancellationToken = default)
        {
       
            return await devices.FindSync(o => o.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Device>> GetAllDevices(int numberOfRecords,CancellationToken cancellationToken = default)
        {
            return await devices.Find(s => true).Limit(numberOfRecords).ToListAsync(cancellationToken: cancellationToken);
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
            return await outboxMessages.FindSync(m => m.Processed == false).ToListAsync(cancellationToken: cancellationToken);
        }
        public async Task<List<City>> GetAllCities(int numberOfRecords, CancellationToken cancellationToken = default)
        {
            return await cityCollection.Find(s => true).Limit(numberOfRecords).ToListAsync(cancellationToken: cancellationToken);
        }
        public async Task CreateCity(City city, CancellationToken cancellationToken = default)
        {
            await cityCollection.InsertOneAsync(city, cancellationToken);
        }

        public async Task CreateManyCity(List<City> city, CancellationToken cancellationToken = default)
        {
            await cityCollection.InsertManyAsync(city, cancellationToken: cancellationToken);
        }

        public async Task<List<Coupon>> GetAllCoupons(int numberOfRecords, CancellationToken cancellationToken = default)
        {
            return await couponCollection.Find(s => true).Limit(numberOfRecords).ToListAsync(cancellationToken: cancellationToken);
        }
        public async Task<List<BusOperator>> GetAllOperators(int numberOfRecords, CancellationToken cancellationToken = default)
        {
            return await busOperators.Find(s => true).Limit(numberOfRecords).ToListAsync(cancellationToken: cancellationToken);
        }
        public async Task<List<Facility>> GetAllFacilities(int numberOfRecords, CancellationToken cancellationToken = default)
        {
            return await facility.Find(s => true).Limit(numberOfRecords).ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
