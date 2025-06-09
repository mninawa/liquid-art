using System.Globalization;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using Template.ClientHttp.Abstraction;
using Template.Shared;

namespace Template.ClientHttp
{
    public class ClientHttp : IClientHttp
    {
        private readonly HttpClient _httpClient;

        public ClientHttp(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
    }
}
