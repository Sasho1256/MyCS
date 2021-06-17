using MyCS.InputModels;
using System.Threading.Tasks;

namespace Services
{
    public interface ISeedService
    {
        public Task SeedRecords(CsvFile file);
    }
}
