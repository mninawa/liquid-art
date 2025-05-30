using AutoMapper;
using Microsoft.Extensions.Logging;
using Registry.Business.Abstraction;
using Registry.Repository.Abstraction;
using Registry.Repository.Model;
using Registry.Shared;

namespace Registry.Business
{
    public class Business : IBusiness
    {
        private readonly IRepository _repository;
        private readonly ILogger<Business> _logger;
        private readonly IMapper _mapper;

        public Business(IRepository repository, ILogger<Business> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task CreateClient(ClientInsertDto clientInsertDto,CancellationToken cancellationToken = default)
        {
            var client = _mapper.Map<Client>(clientInsertDto);
            await _repository.CreateClient(client, cancellationToken);
            var outboxMessage =
                await GetRegistryCreatedOutboxMessage(client.Id, "client-created", cancellationToken);
            await _repository.CreateOutboxMessage(outboxMessage, cancellationToken);
            _logger.LogInformation("client created successfully.");

        }

        public async Task<ClientReadDto?> GetClientById(string id, CancellationToken cancellationToken = default)
        {
            var client = await _repository.GetClientById(id, cancellationToken);
            if (client == null) 
                return null;
            
            var customerReadDto = _mapper.Map<ClientReadDto>(client);
            return customerReadDto;
        }

        public async Task<List<ClientReadDto>> GetAllClients(int numberOfRecord,CancellationToken cancellationToken = default)
        {
            var customers = await _repository.GetAllClients(numberOfRecord,cancellationToken);
            if (customers == null || customers.Any() == false) return new List<ClientReadDto>();

            var customersReadDto = _mapper.Map<List<ClientReadDto>>(customers);
            return customersReadDto;
        }

        public async Task CreateDevice(DeviceInsertDto deviceInsertDto, CancellationToken cancellationToken = default)
        {
            var device = _mapper.Map<Device>(deviceInsertDto);

            // I open a transaction here because I need consistency between the client and the outbox message
            
                await _repository.CreateDevice(device, cancellationToken);
                // TransactionalOutbox pattern implementation for Kafka
                var outboxMessage = await GetRegistryCreatedOutboxMessage(device.Id, "device-created", cancellationToken);
                await _repository.CreateOutboxMessage(outboxMessage, cancellationToken);

                _logger.LogInformation("Device created successfully.");
            
        }

        public async Task<DeviceReadDto?> GetDeviceById(string id, CancellationToken cancellationToken = default)
        {
            var device = await _repository.GetDeviceById(id, cancellationToken);
            if (device == null) return null;

            var deviceReadDto = _mapper.Map<DeviceReadDto>(device);
            return deviceReadDto;
        }

        public async Task<List<DeviceReadDto>> GetAllDevices(int numberOfRecord,CancellationToken cancellationToken = default)
        {
            var devices = await _repository.GetAllDevices( numberOfRecord,cancellationToken);
            if (devices == null || devices.Any() == false) return new List<DeviceReadDto>();

            var devicesReadDto = _mapper.Map<List<DeviceReadDto>>(devices);
            return devicesReadDto;
        }

        private async Task<OutboxMessage> GetRegistryCreatedOutboxMessage(string registryId, string topic, CancellationToken cancellationToken)
        {
            var outboxMessage = new OutboxMessage
            {
                Payload = Convert.ToString(registryId),
                Topic = topic,
                CreatedAt = DateTime.Now,
                Processed = false
            };

            return outboxMessage;
        }
    }
}
