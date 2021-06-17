using CsvHelper;
using Database;
using MyCS.InputModels;
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

        public async Task SeedRecords(CsvFile file)
        {
            using var dirStr = new FileStream($".\\wwwroot\\UploadedFiles\\{file.File.FileName}", FileMode.Create);

            file.File.CopyTo(dirStr);

            dirStr.Close();

            using var reader = new StreamReader($".\\wwwroot\\UploadedFiles\\{file.File.FileName}");
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<AccountMap>();
            var records = csv.GetRecords<Account>().ToList();
            await this.context.Accounts.AddRangeAsync(records);
            await this.context.SaveChangesAsync();
        }
    }
}
