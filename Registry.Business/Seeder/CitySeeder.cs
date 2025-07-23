using AutoMapper;
using Microsoft.Extensions.Logging;
using Registry.Business.Abstraction;
using Registry.Repository.Abstraction;
using Registry.Repository.Model;
using Registry.Shared;
using Volo.Abp.DependencyInjection;

namespace Registry.Business.Seeder;

public interface ICitySeeder
{
    Task SeedAsync();
}

public class CitySeeder: ITransientDependency
{

    private readonly ILogger<CitySeeder> _logger;
    private readonly IRepository _repository;
    private readonly IMapper _mapper;
    public CitySeeder(IRepository repository, ILogger<CitySeeder> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }
    
    public async Task SeedAsync()
    {
        
        var path = Path.Combine(Directory.GetCurrentDirectory(), "cities.json");
        var citiesDto = await path.FromJsonFile<List<CityDto>>("RECORDS");
        if (citiesDto.Count == 0) return;
        
        var cities = _mapper.Map<List<City>>(citiesDto);
        await _repository.CreateManyCity(cities);
        _logger.LogInformation("city created successfully.");
        _logger.LogInformation($"Successfully processed {cities.Count} cities data.");
    }
}