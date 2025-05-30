using Microsoft.AspNetCore.Mvc;
using Registry.Business.Abstraction;
using Registry.Shared;

namespace Registry.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RegistryController : ControllerBase
    {
        private readonly IBusiness _business;
        private readonly ILogger<RegistryController> _logger;

        public RegistryController(IBusiness business, ILogger<RegistryController> logger)
        {
            _business = business;
            _logger = logger;
        }

        [HttpPost(Name = "CreateClient")]
        public async Task<ActionResult> CreateClient(ClientInsertDto clientInsertDto, CancellationToken cancellationToken = default)
        {
            await _business.CreateClient(clientInsertDto, cancellationToken);

            return Ok("Client created successfully!");
        }

        [HttpGet(Name = "ReadClient")]
        public async Task<ActionResult<ClientReadDto?>> ReadClient(string id, CancellationToken cancellationToken = default)
        {
            var customer = await _business.GetClientById(id, cancellationToken);
            if (customer == null)
            {
                var error = $"Client with ID {id} not found.";
                _logger.LogWarning(error);
                return NotFound(new { error });
            }
            return new JsonResult(customer);
        }

        [HttpGet(Name = "GetAllClient")]
        public async Task<ActionResult<List<ClientReadDto>>> GetAllClient(CancellationToken cancellationToken = default)
        {
           var numberOfRecord = 100;
            var customers = await _business.GetAllClients(numberOfRecord,cancellationToken);
            return new JsonResult(customers);
        }

        [HttpPost(Name = "CreateDevice")]
        public async Task<ActionResult> CreateDevice(DeviceInsertDto deviceInsertDto, CancellationToken cancellationToken = default)
        {
            await _business.CreateDevice(deviceInsertDto, cancellationToken);
            return Ok("device created successfully!");
        }

        [HttpGet(Name = "ReadDevice")]
        public async Task<ActionResult<DeviceReadDto?>> ReadDevice(string id, CancellationToken cancellationToken = default)
        {
            var supplier = await _business.GetDeviceById(id, cancellationToken);
            if (supplier == null)
            {
                var error = $"Device with ID {id} not found.";
                _logger.LogWarning(error);
                return NotFound(new { error });
            }
            return new JsonResult(supplier);
        }

        [HttpGet(Name = "GetAllDevices")]
        public async Task<ActionResult<List<DeviceReadDto>>> GetAllDevices(CancellationToken cancellationToken = default)
        {
            int numberOfRecord = 100;
            var suppliers = await _business.GetAllDevices(numberOfRecord,cancellationToken);
            return new JsonResult(suppliers);
        }
    }
}
