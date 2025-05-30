using KafkaFlow;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Notifications.Business.Abstraction;
using Notifications.Shared;

namespace Notifications.Business.Kafka.MessageHandlers
{
    public class NotificationSenderHandler : IMessageHandler<string>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<NotificationSenderHandler> _logger;
        
        public NotificationSenderHandler(IServiceProvider serviceProvider, ILogger<NotificationSenderHandler> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        } 

        public async Task Handle(IMessageContext context, string message)
        {
            using var scope = _serviceProvider.CreateScope();
            var business = scope.ServiceProvider.GetRequiredService<IBusiness>();
            _logger.LogInformation($"Processsing message : {message}");
            
            await business.SendMessage(message);
        }
    }
}
