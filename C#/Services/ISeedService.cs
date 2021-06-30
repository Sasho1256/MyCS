using System.Threading.Tasks;

namespace Services
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Database;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;

    public interface ISeedService
    {
        public Task<Dictionary<ICollection<Account>, ICollection<string>>> SeedRecords(IFormFile file, string path);

        public string UpdatedCSVFile(ICollection<Account> accounts, string path);
    }
}
