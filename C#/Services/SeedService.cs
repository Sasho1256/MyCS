using CsvHelper;
using Database;
using MyCS.InputModels;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using CsvHelper.Configuration;

    public class SeedService : ISeedService
    {
        private readonly MyCsDbContext context;

        public SeedService(MyCsDbContext context)
        {
            this.context = context;
        }

        public async Task SeedRecords(CsvFile file)
        {
            await using var dirStr = new FileStream($".\\wwwroot\\UploadedFiles\\{file.File.FileName}", FileMode.Create);
            await file.File.CopyToAsync(dirStr);
            dirStr.Close();

            using var reader = new StreamReader($".\\wwwroot\\UploadedFiles\\{file.File.FileName}");
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
            csv.Context.RegisterClassMap<AccountMap>();
            List<Account> records;
            records = csv.GetRecords<Account>().ToList();
            await this.context.Accounts.AddRangeAsync(records);
            await this.context.SaveChangesAsync();
        }
    }
}
