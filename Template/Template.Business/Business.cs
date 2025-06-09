using AutoMapper;
using Microsoft.Extensions.Logging;
using Template.Business.Abstraction;
using Template.Repository.Abstraction;
using Template.Repository.Model;
using Template.Shared;

namespace Template.Business
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

        public async Task CreateTemplate(TemplateInsertDto insertDto,CancellationToken cancellationToken = default)
        {
            var template = _mapper.Map<Templates>(insertDto);
            await _repository.CreateTemplate(template, cancellationToken);
            
            var outboxMessage =
                await GetRegistryCreatedOutboxMessage(template.Id, "template-created", cancellationToken);
            await _repository.CreateOutboxMessage(outboxMessage, cancellationToken);
            _logger.LogInformation("client created successfully.");

        }

        public async Task<TemplateInsertDto?> GetTemplateById(string id, CancellationToken cancellationToken = default)
        {
            var client = await _repository.GetTemplateById(id, cancellationToken);
            if (client == null) 
                return null;
            
            var readDto = _mapper.Map<TemplateInsertDto>(client);
            return readDto;
        }

        public async Task<List<TemplateInsertDto>> GetAllTemplates(int numberOfRecord,CancellationToken cancellationToken = default)
        {
            var template = await _repository.GetAllTemplates(numberOfRecord,cancellationToken);
            if (template == null || template.Any() == false) return new List<TemplateInsertDto>();

            var readDto = _mapper.Map<List<TemplateInsertDto>>(template);
            return readDto;
        }

        public async Task<bool> UpdateTemplate(string id, TemplateUpdateDto updateDto,
            CancellationToken cancellationToken = default)
        {
            var template = await _repository.GetTemplateById(id, cancellationToken);
            if (template == null) return false;

            //:TODO add update according to the rules
            // var oldStatus = template.Status;
            // _mapper.Map(updateDto, template);
            //
            // await _repository.UpdateOutboxMessage(template, cancellationToken);

            _logger.LogInformation(
                $"Template with id {id} updated successfully with status {template.Status.ToString()}");


            return true;
        }
        
        public Task<bool> Delete(string id, CancellationToken cancellationToken = default)
        {
            //:TODO add update according to the rules
            throw new NotImplementedException();
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
