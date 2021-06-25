using MyCS.InputModels;
using System.Threading.Tasks;

namespace Services
{
    using System.Collections.Generic;

    public interface ISeedService
    {
        public Task<ICollection<ExceptionModel>> SeedRecords(CsvFile file);
    }
}
