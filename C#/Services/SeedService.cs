using CsvHelper;
using Database;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class SeedService : ISeedService
    {
        private readonly MyCsDbContext context;

        public SeedService(MyCsDbContext context)
        {
            this.context = context;
        }

        public async Task SeedRecords()
        {
            using var reader = new StreamReader("..\\Services\\Data\\data.csv");
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<AccountMap>();
            var records = csv.GetRecords<Account>().ToList();
            //this.context.Accounts.AddRange(records);
            //this.context.SaveChanges();
            await this.context.Accounts.AddRangeAsync(records);
            await this.context.SaveChangesAsync();
        }
    }
}
