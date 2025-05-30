using Microsoft.AspNetCore.Http;
using Registry.ClientHttp.Abstraction;
using Registry.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Registry.ClientHttp
{
    public class ClientHttp : IClientHttp
    {
        private readonly HttpClient _httpClient;

        public ClientHttp(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ClientReadDto?> ReadClient(string id, CancellationToken cancellationToken = default)
        {
            var queryString = QueryString.Create(new Dictionary<string, string?>() {
                { "id", id.ToString(CultureInfo.InvariantCulture) }
            });

            var response = await _httpClient.GetAsync($"/Registry/ReadClient{queryString}", cancellationToken);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            return await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<ClientReadDto?>(cancellationToken: cancellationToken);
        }

        public async Task<DeviceReadDto?> ReadDevice(string id, CancellationToken cancellationToken = default)
        {
            var queryString = QueryString.Create(new Dictionary<string, string?>() {
                { "id", id.ToString(CultureInfo.InvariantCulture) }
            });
            var response = await _httpClient.GetAsync($"/Registry/ReadDevice{queryString}", cancellationToken);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            return await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<DeviceReadDto?>(cancellationToken: cancellationToken);
        }
    }
}
