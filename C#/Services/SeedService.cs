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
    using Microsoft.EntityFrameworkCore;

    public class SeedService : ISeedService
    {
        private readonly MyCsDbContext context;

        public SeedService(MyCsDbContext context)
        {
            this.context = context;
        }

        public async Task<ICollection<string>> SeedRecords(IFormFile file)
        {
            await using var dirStr = new FileStream($".\\wwwroot\\UploadedFiles\\{file.FileName}", FileMode.Create);
            await file.CopyToAsync(dirStr);
            dirStr.Close();

            using var reader = new StreamReader($".\\wwwroot\\UploadedFiles\\{file.FileName}");
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
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

            var errors = this.ValidateBeforeDatabase(records);
            exceptions.AddRange(errors.Select(error => error.ErrorMessage));

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
            return exceptions;
        }

        private List<ValidationResult> ValidateBeforeDatabase(List<Account> records)
        {
            var errors = new List<ValidationResult>();
            foreach (var e in records)
            {
                var vc = new ValidationContext(e, null, null);
                Validator.TryValidateObject(
                    e, vc, errors, true);
            }

            return errors;
        }
    }
}
