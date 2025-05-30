using AutoMapper;
using Microsoft.Extensions.Logging;
using Notifications.Repository.Abstraction;
using Notifications.Repository.Model;
using Notifications.Shared;
using Registry.Shared;
using Newtonsoft.Json;
using Notifications.Business.Abstraction;
using JsonSerializer = System.Text.Json.JsonSerializer;
using RestSharp;
namespace Notifications.Business
{
    public class Business : IBusiness
    {
        private readonly IRepository _repository;
        private readonly ILogger<Business> _logger;
        private readonly IMapper _mapper;
        private readonly Registry.ClientHttp.Abstraction.IClientHttp _registryClientHttp;
        public Business(IRepository repository, ILogger<Business> logger, IMapper mapper,
                        Registry.ClientHttp.Abstraction.IClientHttp registryClientHttp)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _registryClientHttp = registryClientHttp;
        }

        public async Task CreateNotification(NotificationInsertDto notificationInsertDto, CancellationToken cancellationToken = default)
        {
            foreach (var note in notificationInsertDto.NotificationsDetail)
            {
                
                ClientReadDto? clientInfo = await _registryClientHttp.ReadClient(note.ClientId, cancellationToken);
                if (clientInfo == null)
                {
                    var error =
                        $"Notification creation failed: client with ID {note.ClientId} not found.";
                    _logger.LogError(error);
                    throw new Exception(error);
                }

                // Checking cache
                var clientExists =
                    await _repository.CheckClientExistence(note.ClientId, cancellationToken);
                if (clientExists == false)
                {
                    var error =
                        $"Notification creation failed: client with ID {note.ClientId} not found.";
                    _logger.LogError(error);
                    throw new Exception(error);
                }

                if (notificationInsertDto.NotificationsDetail == null || notificationInsertDto.NotificationsDetail.Any() == false)
                {
                    var error = "Notification creation failed: no details are provided.";
                    _logger.LogError(error);
                    throw new Exception(error);
                }

                var templete = await _repository.GetTemplateVersionById(note.TemplateId);
                if (templete == null)
                {
                    var error =
                        $"Notification creation failed: template with version {note.TemplateId} not found.";
                    _logger.LogError(error);
                    throw new Exception(error);
                }

                var notification = new Notification
                {
                    Balance = note.Balance,
                    Amount = note.Amount,
                    Mobile = clientInfo.Phone,
                    ClientId = clientInfo.Id,
                    EventDate = DateTime.Now,
                    messageId = notificationInsertDto.Id
                };

                Dictionary<string, string> replacements = new Dictionary<string, string>(){
                    {"{amount}", note.Amount},
                    {"{merchant}", note.Merchant},
                    {"{account}", note.Account},
                    {"{balance}",note.Balance},
                    {"{date}", note.Date},
                };

                notification.Content = templete.Content.Parse(replacements);
                
                await _repository.CreateNotification(notification, cancellationToken);
                var outboxMessage = await GetNotificationCreatedOutboxMessage(notification, cancellationToken);
                await _repository.CreateOutboxMessage(outboxMessage, cancellationToken);
            }

        }

        public async Task<NotificationsReadDto?> GetNotificationById(string id, CancellationToken cancellationToken = default)
        {
            var notes = await _repository.GetNotificationById(id, cancellationToken);
            if (notes == null) return null;

            var dto = _mapper.Map<NotificationsReadDto>(notes);
            return dto;
        }

        public async Task<List<NotificationsReadDto>> GetAllNotifications(CancellationToken cancellationToken = default)
        {
            var notifications = await _repository.GetAllNotifications(100,cancellationToken);
            if (notifications == null || notifications.Any() == false) return new List<NotificationsReadDto>();

            var notificationsReadDto = _mapper.Map<List<NotificationsReadDto>>(notifications);
            return notificationsReadDto;
        }

        public async Task<bool> UpdateNotificationStatus(string id, string status, CancellationToken cancellationToken = default)
        {
            var notification = await _repository.GetNotificationById(id, cancellationToken);
            if (notification == null)
            {
                _logger.LogError($"Notification with ID {id} not found.");
                return false;
            }

            if (notification.Status == "Completed")
            {
                _logger.LogError($"Notification with ID {id} is already completed. You can't change the status of a completed notification!");
                return false;
            }

            if (notification.Status == "Failed")
            {
                throw new InvalidOperationException("You can't change the status of a failed notification!");
            }

            var possibleStatuses = new List<string> { "Pending", "Completed", "Failed" };
            if (string.IsNullOrEmpty(status) == true || possibleStatuses.Contains(status) == false)
            {
                _logger.LogError($"Invalid status {status}! Possible statuses are: {string.Join(", ", possibleStatuses)}");
                return false;
            }
            notification.Status = status;
            return true;
        }

        public async Task<bool> DeleteNotification(string id, CancellationToken cancellationToken = default)
        {
            var notification = await _repository.GetNotificationById(id, cancellationToken);

            if (notification != null && notification.Status == "Pending")
            {
                var error = $"Notification with ID {id} cannot be deleted because it is in Pending status.";
                _logger.LogError(error);
                throw new Exception(error);
            }

            var result = await _repository.DeleteNotification(id, cancellationToken);
            if (result == false)
            {
                _logger.LogError($"Notification with ID {id} not found.");
                return false;
            }
            
            return true;
        }

        public async Task CreateClientCache(string customerId, CancellationToken cancellationToken = default)
        {
            var customerExists = await _repository.CheckClientExistence(customerId, cancellationToken);
            if (customerExists == false)
            {
                await _repository.CreateClientCache(customerId, cancellationToken);
            }
        }

        private async Task<OutboxMessage> GetNotificationCreatedOutboxMessage(Notification notification, CancellationToken cancellationToken)
        {
            var outboxMessage = new OutboxMessage
            {
                Payload = JsonConvert.SerializeObject(notification),
                Topic = "send-notification",
                CreatedAt = DateTime.Now,
                Processed = false
            };

            return outboxMessage;
        }

        public async Task SendMessage(string message, CancellationToken cancellationToken = default)
        {
            var dto = new NotificationReadDto();
            var notification = JsonSerializer.Deserialize<Notification>(message);

            var client = new RestClient("https://api.wassenger.com/v1/messages");
            // Set up the request with the POST method
            var request = new RestRequest();
            request.Method = Method.Post;
            // Add headers for content type and authentication
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Token", "d289429aa78c4e233559323640d33b66220d3981faaa8756e30aa6c813316bcd8b35c6b4832cc971"); // Replace with your API key

            // Define the request body for sending a text message
            var requestBody = new
            {
                phone =notification.Mobile,
                message =notification.Content
            };
            // Add the request body as JSON
            request.AddJsonBody(requestBody);

            var response = client.Execute(request);

            Console.WriteLine(response.Content);

        }
    }
}