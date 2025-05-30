using Registry.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registry.ClientHttp.Abstraction
{
    public interface IClientHttp
    {
        Task<ClientReadDto?> ReadClient(string id, CancellationToken cancellationToken = default);
        Task<DeviceReadDto?> ReadDevice(string id, CancellationToken cancellationToken = default);
    }
}
