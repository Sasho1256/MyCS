using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Services;
using Services.Mappings;

namespace Tests.Seed_Service_Tests
{
    public class TestingLoan
    {
        private MyCsDbContext dbContext;
        private IValidationService validationService;
        private IMapper mapper;
        private ICreditScoreService creditScoreService;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<MyCsDbContext>().UseInMemoryDatabase("MyCS").Options;
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            });
            this.mapper = config.CreateMapper();
            this.dbContext = new MyCsDbContext(options);
            this.creditScoreService = new CreditScoreService(dbContext, mapper, validationService);
            this.validationService = new ValidationService();
        }

        [TearDown]
        public void TearDown()
        {
            this.dbContext.Database.EnsureDeleted();
        }

        [Test]
        public async Task ShouldThrowExceptionsIfLoanIsIncorrect()
        {
            using (var stream = File.OpenRead("../../../Data/loan_error.csv"))
            {
                var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "text/csv"
                };
                var seedService = new SeedService(dbContext, creditScoreService, validationService);
                var exceptions = await seedService.SeedRecords(file, Directory.GetCurrentDirectory());
                var message = exceptions.First().Value.ToList();
                //Assert.AreEqual(exceptions.Count, 1);
                //Assert.AreEqual(message, "Final_Decision should be Accept or Decline.");

                var exceptionsM = new List<string>()
                {
                    "Loan_Amount should be between 0 and 1000000.",
                    "Loan_Payment_Frequency should be F, M, W, X or Empty.",
                    "Loan_Payment_Method should be B, Q, S, X or Empty."
                };

                bool isSuperset = new HashSet<string>(exceptionsM).IsSupersetOf(message);
                Assert.True(isSuperset);
            }
        }
    }
}
