using Microsoft.AspNetCore.Mvc;
using Registry.Business.Abstraction;
using Registry.Shared;

namespace Registry.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OperatorsController : ControllerBase
    {
        private readonly IBusiness _business;
        private readonly ILogger<OperatorsController> _logger;

        public OperatorsController(IBusiness business, ILogger<OperatorsController> logger)
        {
            _business = business;
            _logger = logger;
        }
        
        [HttpGet(Name = "GetAll")]
        public async Task<ActionResult<List<BusOperatorDto>>> GetAll(CancellationToken cancellationToken = default)
        {
            var numberOfRecord = 100;
            var cities = await _business.GetAllOperators(numberOfRecord, cancellationToken);
            return new JsonResult(cities);
        }
    }
}
