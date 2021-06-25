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
    using Microsoft.AspNetCore.Http;

    public class SeedService : ISeedService
    {
        private readonly MyCsDbContext context;

        public SeedService(MyCsDbContext context)
        {
            this.context = context;
        }

        public async Task<ICollection<ExceptionModel>> SeedRecords(IFormFile file)
        {
            await using var dirStr = new FileStream($".\\wwwroot\\UploadedFiles\\{file.FileName}", FileMode.Create);
            await file.CopyToAsync(dirStr);
            dirStr.Close();

            using var reader = new StreamReader($".\\wwwroot\\UploadedFiles\\{file.FileName}");
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
            csv.Context.RegisterClassMap<AccountMap>();
            var records = new List<Account>();
            var badRecords = new List<ExceptionModel>();
            try
            {
                records = csv.GetRecords<Account>().ToList();
            }
            catch (CsvHelperException e)
            {
                badRecords.Add(new ExceptionModel
                {
                    Row = csv.Parser.RawRow,
                    RawRecord = csv.Context.Parser.RawRecord,
                    ValidationMessage = e.Message
                });
            }
            await this.context.Accounts.AddRangeAsync(records);
            await this.context.SaveChangesAsync();
            return badRecords;
        }
    }
}
