using System.Threading.Tasks;
using System.Collections.Generic;
using Database;
using Microsoft.AspNetCore.Http;

namespace Services
{
    public interface ISeedService
    {
        public Task<Dictionary<ICollection<Account>, ICollection<string>>> SeedRecords(IFormFile file, string path);

        public string UpdatedCsvFile(ICollection<Account> accounts, string path);
    }
}
