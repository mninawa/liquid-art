using Microsoft.Extensions.Configuration;
using Registry.Repository.Model;

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
        Task<List<City>> GetAllCities(int numberOfRecords,CancellationToken cancellationToken = default);
        Task CreateCity(City city, CancellationToken cancellationToken = default);
        Task CreateManyCity(List<City> city, CancellationToken cancellationToken = default);
        Task<List<Coupon>> GetAllCoupons(int numberOfRecords,CancellationToken cancellationToken = default);
        Task<List<BusOperator>> GetAllOperators(int numberOfRecords,CancellationToken cancellationToken = default);
        Task<List<Facility>> GetAllFacilities(int numberOfRecords,CancellationToken cancellationToken = default);
    }
   
    public interface IMongoConnectionSetting
    {
        public string? ConnectionString { get; set; }
        public string? DatabaseName { get; set; }
        public string? ClientCollectionName { get; set; }
        public string? FacilityCollectionName { get; set; }
        public string? BusOperatorCollectionName { get; set; }
        public string? CityCollectionName { get; set; }
        public string? CouponCollectionName { get; set; }
        public string? DeviceCollectionName { get; set; }
        public string? OutboxCollectionName { get; set; }
    }
    public class MongoConnectionSetting(IConfiguration configuration) : IMongoConnectionSetting
    {
        public string? ConnectionString { get; set; } = configuration.GetValue<string>("MongoConnectionSetting:connectionString");
        public string? DatabaseName { get; set; } = configuration.GetValue<string>("MongoConnectionSetting:DatabaseName");
        public string? FacilityCollectionName { get; set; } = configuration.GetValue<string>("MongoConnectionSetting:FacilityCollectionName");
        public string? BusOperatorCollectionName { get; set; } = configuration.GetValue<string>("MongoConnectionSetting:BusOperatorCollectionName");
        public string? CouponCollectionName { get; set; } = configuration.GetValue<string>("MongoConnectionSetting:CouponCollectionName");
        public string? CityCollectionName { get; set; } = configuration.GetValue<string>("MongoConnectionSetting:CityCollectionName");
        public string? ClientCollectionName { get; set; } = configuration.GetValue<string>("MongoConnectionSetting:ClientCollectionName");
        public string? DeviceCollectionName { get; set; } = configuration.GetValue<string>("MongoConnectionSetting:DeviceCollectionName");
        public string? OutboxCollectionName { get; set; } = configuration.GetValue<string>("MongoConnectionSetting:OutboxCollectionName");
    }
}

 
