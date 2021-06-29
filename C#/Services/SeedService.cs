﻿using CsvHelper;
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

        public SeedService(MyCsDbContext context, ICreditScoreService scoreService)
        {
            this.context = context;
            this.scoreService = scoreService;
        }

        public async Task<Dictionary<ICollection<Account>, ICollection<string>>> SeedRecords(IFormFile file)
        {
            await using var dirStr = new FileStream($".\\wwwroot\\UploadedFiles\\{file.FileName}", FileMode.Create);
            await file.CopyToAsync(dirStr);
            dirStr.Close();

            using var reader = new StreamReader($".\\wwwroot\\UploadedFiles\\{file.FileName}");
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

            var errors = this.ValidateBeforeDatabase(records);
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

        private List<ValidationResult> ValidateBeforeDatabase(List<Account> records)
        {
            var errors = new List<ValidationResult>();
            foreach (var e in records)
            {
                var vcAccount = new ValidationContext(e, null, null);
                var vcClient = new ValidationContext(e.Client, null, null);
                var vcLoan = new ValidationContext(e.Loan, null, null);
                Validator.TryValidateObject(e, vcAccount, errors, true);
                Validator.TryValidateObject(e.Client, vcClient, errors, true);
                Validator.TryValidateObject(e.Loan, vcLoan, errors, true);
            }

            return errors;
        }
    }
}
