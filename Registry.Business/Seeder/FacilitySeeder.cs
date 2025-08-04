using AutoMapper;
using Microsoft.Extensions.Logging;
using Registry.Repository.Abstraction;
using Registry.Repository.Model;
using Registry.Shared;

namespace Registry.Business.Seeder;

public class FacilitySeeder
{
    private readonly ILogger<FacilitySeeder> _logger;
    private readonly IRepository _repository;
    private readonly IMapper _mapper;
    public FacilitySeeder(IRepository repository, ILogger<FacilitySeeder> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }
    
    public async Task SeedAsync()
    {
        
        var path = Path.Combine(Directory.GetCurrentDirectory(), "facilities.json");
        var dto = await path.FromJsonFile<List<FacilityDto>>("RECORDS");
        if (dto.Count == 0) return;
        
        var facilities = _mapper.Map<List<Facility>>(dto);
        await _repository.CreateManyFacilities(facilities);
        _logger.LogInformation("facilities created successfully.");
        _logger.LogInformation($"Successfully processed {facilities.Count} facilities data.");
    }
}