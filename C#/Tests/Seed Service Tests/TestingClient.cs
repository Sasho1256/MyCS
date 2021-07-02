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
    public class TestingClient
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
        public async Task ShouldThrowExceptionsIfClientIsIncorrect()
        {
            using (var stream = File.OpenRead("../../../Data/client_error.csv"))
            {
                var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "text/csv"
                };
                var seedService = new SeedService(dbContext, creditScoreService, validationService);
                var exceptions = await seedService.SeedRecords(file, Directory.GetCurrentDirectory());
                var message = exceptions.First().Value.ToList();

                var exceptionsM = new List<string>()
            {
                "Current_Delinquency_status must be between 0 and 10 or null.",
                "Application Score must be a positive number.",
                "Gross annual income must be between 0 and 1 million.",
                "Home_Telephone_Number should be Y, N or empty space (null).",
                "Marital_Status should be D, M, S, W or Z.",
                "Occupation_Code should be O, P, B or M.",
                "Residential_Status should be H, L, O or T.",
                "Time_at_Address should be between 0 and 36500 days.",
                "Time_in_Employment should be between 0 and 29200.",
                "Time_with_Bank should be between 0 and 29200.",
                "GB_Flag should be Good, Bad, Indeterminate, NTU or Rejects.",
                "Age_of_Applicant should be between 18 and 90."
            };

                bool isSuperset = new HashSet<string>(exceptionsM).IsSupersetOf(message);
                Assert.True(isSuperset);
            }
        }
    }
}
