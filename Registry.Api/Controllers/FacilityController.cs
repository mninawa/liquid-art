using Microsoft.AspNetCore.Mvc;
using Registry.Business.Abstraction;
using Registry.Shared;

namespace Registry.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FacilityController : ControllerBase
    {
        private readonly IBusiness _business;
        private readonly ILogger<FacilityController> _logger;

        public FacilityController(IBusiness business, ILogger<FacilityController> logger)
        {
            _business = business;
            _logger = logger;
        }

        [HttpGet(Name = "GetAll")]
        public async Task<ActionResult<List<FacilityDto>>> GetAllFacilities(CancellationToken cancellationToken = default)
        {
            var numberOfRecord = 100;
            var entity = await _business.GetAllFacilities(numberOfRecord, cancellationToken);
            return new JsonResult(entity);
        }
    }
}
