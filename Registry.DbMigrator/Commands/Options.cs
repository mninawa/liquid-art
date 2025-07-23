
using CommandLine;
namespace Registry.DbMigrator.Commands
{
    public class Options
    {
        [Value(0, Required = false, HelpText = "Specify the name of the city to seed.")]
        public string CityName { get; set; }

        [Option('n', "number", Required = false, HelpText = "Specify the number of records to seed.")]
        public int NumberOfRecords { get; set; } = 1;
    }
}