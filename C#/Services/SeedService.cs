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
    using System.ComponentModel.DataAnnotations;
    using System.Net.Http;
    using System.Threading;
    using CsvHelper.Configuration;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.EntityFrameworkCore;

    public class SeedService : ISeedService
    {
        private readonly MyCsDbContext context;
        private ICreditScoreService scoreService;
        private readonly IValidationService validationService;

        public SeedService(MyCsDbContext context, ICreditScoreService scoreService, IValidationService validationService)
        {
            this.context = context;
            this.scoreService = scoreService;
            this.validationService = validationService;
        }

        public async Task<Dictionary<ICollection<Account>, ICollection<string>>> SeedRecords(IFormFile file, string path)
        {
            await using var dirStr = new FileStream(path + $"{file.FileName}", FileMode.Create);
            await file.CopyToAsync(dirStr);
            dirStr.Close();

            using var reader = new StreamReader(path + $"{file.FileName}");
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) 
            {
                HeaderValidated = null,
                MissingFieldFound = null
            });

            csv.Context.RegisterClassMap<AccountMap>();
            var records = new List<Account>();
            var exceptions = new List<string>();

            try
            {
                records = csv.GetRecords<Account>().ToList();
            }
            catch (CsvHelperException e)
            {
                exceptions.Add(e.InnerException != null ? e.InnerException.Message + $"{csv.Parser.Row}" : e.Message);
            }

            var errors = validationService.ValidateBeforeDatabase(records);
            exceptions.AddRange(errors.Select(error => error.ErrorMessage));

            foreach (var item in records)
            {
                scoreService.CalculateScore(item);
            }

            if (errors.Count == 0)
            {
                try
                {
                    await this.context.Accounts.AddRangeAsync(records);
                    await this.context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    exceptions.Add(e.InnerException != null ? e.InnerException.Message : e.Message);
                }
            }

            var dic = new Dictionary<ICollection<Account>, ICollection<string>>();
            dic.Add(records, exceptions);
            return dic;
        }

        public string UpdatedCSVFile(ICollection<Account> accounts, string path)
        {
            var file = File.Create(path); 
            using (var writer = new StreamWriter(path))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(accounts);
                writer.Close();
            }
            file.Close();
            //FormFile file1 = new FormFile(file, 0, file.Length, "name", file.Name);
            return path;
        }
    }
}
