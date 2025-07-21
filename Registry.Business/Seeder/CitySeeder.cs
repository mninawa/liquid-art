using AutoMapper;
using Microsoft.Extensions.Logging;
using Registry.Business.Abstraction;
using Registry.Repository.Abstraction;
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
        
        var path = Path.Combine(Directory.GetCurrentDirectory(), "city.json");
        var cities = await path.FromJsonFile<List<CityDto>>("RECORDS");
        if (!cities.Any()) return;

        //await _repository.CreateManyCity(cities);
        _logger.LogInformation("city created successfully.");

        Console.WriteLine($"Successfully processed {cities.Count} cities data.");
    }
}