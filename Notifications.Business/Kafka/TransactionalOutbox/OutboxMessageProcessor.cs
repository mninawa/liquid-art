using KafkaFlow.Producers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Notifications.Repository.Abstraction;

namespace Notifications.Business.Kafka.TransactionalOutbox
{
    public class OutboxMessageProcessor : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<OutboxMessageProcessor> _logger;

        public OutboxMessageProcessor(IServiceProvider serviceProvider, ILogger<OutboxMessageProcessor> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (cancellationToken.IsCancellationRequested == false)
            {
                using var scope = _serviceProvider.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<IRepository>();
                var producerAccessor = scope.ServiceProvider.GetRequiredService<IProducerAccessor>();
                var kafkaProducer = producerAccessor.GetProducer("notifications");
                try
                {
                    var pendingMessages = await repository.GetPendingOutboxMessages(cancellationToken);

                    foreach (var message in pendingMessages)
                    {
                        try
                        {
                            await kafkaProducer.ProduceAsync(message.Topic, message.Id.ToString(), message.Payload);
                            message.Processed = true;
                            await repository.UpdateOutboxMessage(message);
                            _logger.LogInformation($"Processed outbox message with ID: {message.Id}");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, $"Error processing message with ID: {message.Id}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during outbox message processing");
                }
                await Task.Delay(10000, cancellationToken);
            }
        }
    }
}
