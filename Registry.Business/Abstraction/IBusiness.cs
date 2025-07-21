using Registry.Shared;

namespace Registry.Business.Abstraction
{
    public interface IBusiness
    {
        Task CreateClient(ClientInsertDto clientInsertDto, CancellationToken cancellationToken = default);
        Task<ClientReadDto?> GetClientById(string id, CancellationToken cancellationToken = default);
        Task<List<ClientReadDto>> GetAllClients(int numberOfRecord,CancellationToken cancellationToken = default);
        Task CreateDevice(DeviceInsertDto deviceInsertDto, CancellationToken cancellationToken = default);
        Task<DeviceReadDto?> GetDeviceById(string id, CancellationToken cancellationToken = default);
        Task<List<DeviceReadDto>> GetAllDevices(int numberOfRecord, CancellationToken cancellationToken = default);
        
        
        Task CreateCity(CityDto clientInsertDto, CancellationToken cancellationToken = default);
        Task CreateMAnyCity(List<CityDto> clientInsertDto, CancellationToken cancellationToken = default);
        Task<List<CityDto>> GetAllCities(int numberOfRecord,CancellationToken cancellationToken = default);
        Task<List<CouponDto>> GetAllCoupons(int numberOfRecord,CancellationToken cancellationToken = default);
        Task<List<BusOperatorDto>> GetAllOperators(int numberOfRecord,CancellationToken cancellationToken = default);
        Task<List<FacilityDto>> GetAllFacilities(int numberOfRecord,CancellationToken cancellationToken = default);
    }
}
