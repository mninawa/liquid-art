using Microsoft.AspNetCore.Mvc;
using Registry.Business.Abstraction;
using Registry.Shared;

namespace Registry.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CityController : ControllerBase
    {
        private readonly IBusiness _business;
        private readonly ILogger<CityController> _logger;

        public CityController(IBusiness business, ILogger<CityController> logger)
        {
            _business = business;
            _logger = logger;
        }

        [HttpPost(Name = "create")]
        public async Task<ActionResult> CreateClient(CityDto cityDto, CancellationToken cancellationToken = default)
        {
            await _business.CreateCity(cityDto, cancellationToken);
            return Ok("city created successfully!");
        }

        [HttpGet(Name = "GetAll")]
        public async Task<ActionResult<List<CityDto>>> GetAllClient(CancellationToken cancellationToken = default)
        {
            var numberOfRecord = 100;
            var cities = await _business.GetAllCities(numberOfRecord, cancellationToken);
            return new JsonResult(cities);
        }
    }
}
