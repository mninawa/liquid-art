using KafkaFlow;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Business.Abstraction;

namespace Notifications.Business.Kafka.MessageHandlers
{
    public class ClientCreatedHandler : IMessageHandler<string>
    {
        private readonly IServiceProvider _serviceProvider;

        public ClientCreatedHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Handle(IMessageContext context, string message)
        {
            using var scope = _serviceProvider.CreateScope();
            var business = scope.ServiceProvider.GetRequiredService<IBusiness>();
            await business.CreateClientCache(message);
        }
    }
}
