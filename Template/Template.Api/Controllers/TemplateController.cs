using Microsoft.AspNetCore.Mvc;
using Template.Business.Abstraction;
using Template.Shared;

namespace Template.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TemplateController : ControllerBase
    {
        private readonly IBusiness _business;
        private readonly ILogger<TemplateController> _logger;

        public TemplateController(IBusiness business, ILogger<TemplateController> logger)
        {
            _business = business;
            _logger = logger;
        }

        [HttpPost(Name = "Create")]
        public async Task<ActionResult> CreateTemplates(TemplateInsertDto insert, CancellationToken cancellationToken = default)
        {
            await _business.CreateTemplate(insert, cancellationToken);
            return Ok("template created successfully!");
        }

        [HttpGet(Name = "Get")]
        public async Task<ActionResult<TemplateInsertDto?>> ReadTemplates(string id, CancellationToken cancellationToken = default)
        {
            var customer = await _business.GetTemplateById(id, cancellationToken);
            if (customer == null)
            {
                var error = $"Template with ID {id} not found.";
                _logger.LogWarning(error);
                return NotFound(new { error });
            }
            return new JsonResult(customer);
        }

        [HttpGet(Name = "GetAll")]
        public async Task<ActionResult<List<TemplateInsertDto>>> GetAllTemplates(CancellationToken cancellationToken = default)
        {
           var numberOfRecord = 100;
            var entities = await _business.GetAllTemplates(numberOfRecord,cancellationToken);
            return new JsonResult(entities);
        }
        
      
        [HttpDelete(Name = "Delete")]
        public async Task<ActionResult> Delete(string id, CancellationToken cancellationToken = default)
        {
            var deleted = await _business.Delete(id, cancellationToken);

            if (deleted == false)
            {
                var error = $"Template with ID {id} not found. No deletion was made.";
                _logger.LogWarning(error);
                return NotFound(new { error });
            }

            return Ok("Template deleted successfully!");
        }
        
        [HttpPut(Name = "UpdateTemplate")]
        public async Task<ActionResult> UpdateTemplate(string id, TemplateUpdateDto updateDto, CancellationToken cancellationToken = default)
        {
            var result = await _business.UpdateTemplate(id, updateDto, cancellationToken);
            
            if (result == false)
            {
                var error = $"Template with ID {id} not found.";
                _logger.LogWarning(error);
                return NotFound(new { error });
            }

            return Ok($"Payment updated successfully! Payment with Id {id} is now {updateDto.Status}!");
        }

   
    }
}
