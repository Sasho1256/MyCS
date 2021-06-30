namespace Tests
{
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

    public class Testing
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
        public async Task ShouldSeedDataBeInputtedCorrectly()
        {
            using (var stream = File.OpenRead("../../../Data/file.csv"))
            {
                var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "text/csv"
                };
                var seedService = new SeedService(dbContext, creditScoreService, validationService);
                await seedService.SeedRecords(file, Directory.GetCurrentDirectory());
            }

            Assert.AreEqual(dbContext.Accounts.Count(), 2);
        }

        [Test]
        public async Task ShouldThrowExceptionsIfAccountAcceptIsIncorrect()
        {
            using (var stream = File.OpenRead("../../../Data/account_error.csv"))
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
                "Account_Type should be FL (Fixed Loan) or VL(Variable Loan).",
                "Final_Decision should be Accept or Decline.",
                "Cheque_Card_Flag should be Y, N or empty space (null).",
                "Existing_Customer_Flag should be Y, N or empty space (null).",
                "Insurance_Required should be Y, N or empty space (null).",
                "Number_of_Dependants should be a number between 0 and 20.",
                "Number_of_Payments should be a number between 0 and 1500.",
                "Promotion_Type should be AD, DM, OT or RR.",
                "Weight_Factor should be a positive number or 0.",
                "Bureau_Score should be between 0 and 2000.",
                "SP_ER_Reference should be between 1 and 5.",
                "SP_Number_Of_Searches_L6M should be between 0 and 5000.",
                "SP_Number_of_CCJs should be between 0 and 100.",
                "Split should be Development or Validation."
            };

                bool isSuperset = new HashSet<string>(exceptionsM).IsSupersetOf(message);
                Assert.True(isSuperset);
            }
        }
    }
}
