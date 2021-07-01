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
    public class SeedServiceTest
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
    }
}
