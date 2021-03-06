using CsvHelper;
using Database;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;

namespace Services
{
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
            foreach (var item in records)
            {
                if (item.Account_Number == null)
                {
                    item.Account_Number = GenerateAccountNumber();
                }

                scoreService.CalculateScore(item);
            }
            var errors = validationService.ValidateBeforeDatabase(records);
            exceptions.AddRange(errors.Select(error => error.ErrorMessage));

            

            var allAccounts = this.context.Accounts.Select(x => x.Account_Number).ToList();
            bool isSuperset = new HashSet<string>(allAccounts).IsSupersetOf(records.Select(x => x.Account_Number));

            if (errors.Count == 0 && !isSuperset)
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

        public string UpdatedCsvFile(ICollection<Account> accounts, string path)
        {
            var file = File.CreateText(path);
            var outputRecords = accounts.Select(x => new
            {
                x.Account_Number,
                x.Credit_Score,
                x.Eligibility
            }).ToList();
            using var csv = new CsvWriter(file, CultureInfo.InvariantCulture);
            csv.WriteRecords(outputRecords);
            return path;
        }

        private string GenerateAccountNumber()
        {
            List<string> acc = this.context.Accounts.Select(x => x.Account_Number).ToList();
            List<string> nums = "1 2 3 4 5 6 7 8 9 0".Split(" ").ToList();
            Random rand = new Random();
            string accNum = "";

            do
            {
                accNum = "";
                for (int i = 1; i <= 11; i++)
                {
                    accNum += $"{nums[rand.Next(nums.Count - 1)]}";
                }
            } while (acc.Exists(x => x == accNum));

            return accNum;
        }
    }
}
