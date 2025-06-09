using Registry.Shared;

namespace Registry.ClientHttp.Abstraction
{
    public interface IClientHttp
    {
        Task<ClientReadDto?> ReadClient(string id, CancellationToken cancellationToken = default);
        Task<DeviceReadDto?> ReadDevice(string id, CancellationToken cancellationToken = default);
    }
}
