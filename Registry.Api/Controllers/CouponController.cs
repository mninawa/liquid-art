using Microsoft.AspNetCore.Mvc;
using Registry.Business.Abstraction;
using Registry.Shared;

namespace Registry.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CouponController : ControllerBase
    {
        private readonly IBusiness _business;
        private readonly ILogger<CouponController> _logger;

        public CouponController(IBusiness business, ILogger<CouponController> logger)
        {
            _business = business;
            _logger = logger;
        }

        [HttpGet(Name = "GetAll")]
        public async Task<ActionResult<List<CouponDto>>> GetAllClient(CancellationToken cancellationToken = default)
        {
            var numberOfRecord = 100;
            var cities = await _business.GetAllCoupons(numberOfRecord, cancellationToken);
            return new JsonResult(cities);
        }
    }
}
