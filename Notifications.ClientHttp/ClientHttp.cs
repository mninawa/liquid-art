using Microsoft.AspNetCore.Http;
using Notifications.Shared;
using System.Globalization;
using System.Net.Http.Json;
using Notifications.ClientHttp.Abstraction;

namespace Notifications.ClientHttp
{
    public class ClientHttp : IClientHttp
    {
        private readonly HttpClient _httpClient;

        public ClientHttp(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<NotificationsReadDto?> ReadNotification(int id, CancellationToken cancellationToken = default)
        {
            var queryString = QueryString.Create(new Dictionary<string, string?>() {
                { "id", id.ToString(CultureInfo.InvariantCulture) }
            });

            var response = await _httpClient.GetAsync($"/Notification/ReadNotifications{queryString}", cancellationToken);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            return await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<NotificationsReadDto?>(cancellationToken: cancellationToken);
        }
        
        public async Task SendWhatsApp(NotificationReadDto request, CancellationToken cancellationToken = default)
        {
            
        }
    }
}
