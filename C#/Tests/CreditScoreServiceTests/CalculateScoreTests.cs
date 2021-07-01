using AutoMapper;
using Database;
using Microsoft.EntityFrameworkCore;
using MyCS.InputModels;
using NUnit.Framework;
using Services;
using Services.Mappings;
using System.Linq;
using System.Threading.Tasks;

namespace Tests.CreditScoreServiceTests
{
    public class CalculateScoreTests
    {
        private MyCsDbContext dbContext;
        private IMapper mapper;
        private IValidationService validationService;

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
            this.validationService = new ValidationService();
        }

        [TearDown]
        public void TearDown()
        {
            this.dbContext.Database.EnsureDeleted();
        }

        [Test]
        public async Task ScoreShouldBe220()
        {
            var input = new ManualInputModel
            {
                Gross_Annual_Income = 12_000,
                Loan_Amount = 12000,
                Marital_Status = 'D',
                Number_of_Dependants = 1,
                Occupation_Code = 'O',
                Residential_Status = 'H',
                Age_of_Applicant = 19,
            };

            var scoreService = new CreditScoreService(dbContext, mapper, validationService);
            var dictionary = await scoreService.CreateRecordFromManualInput(input);
            int? crSc = dictionary.First().Key.Credit_Score;

            Assert.AreEqual(220, crSc);
        }

        [Test]
        public async Task ScoreShouldBe210()
        {
            var input = new ManualInputModel
            {
                Gross_Annual_Income = 0,
                Loan_Amount = 1,
                Marital_Status = 'D',
                Number_of_Dependants = 0,
                Occupation_Code = 'O',
                Residential_Status = 'H',
                Age_of_Applicant = 19,
            };

            var scoreService = new CreditScoreService(dbContext, mapper, validationService);
            var dictionary = await scoreService.CreateRecordFromManualInput(input);
            int? crSc = dictionary.First().Key.Credit_Score;
            bool? elg = dictionary.First().Key.Eligibility;

            Assert.AreEqual(210, crSc);
            Assert.AreEqual(true, elg);
        }

        [Test]
        public async Task ScoreShouldBe240()
        {
            var input = new ManualInputModel
            {
                Gross_Annual_Income = 20_000,
                Loan_Amount = 20_000,
                Marital_Status = 'M',
                Number_of_Dependants = 3,
                Occupation_Code = 'P',
                Residential_Status = 'L',
                Age_of_Applicant = 28,
            };

            var scoreService = new CreditScoreService(dbContext, mapper, validationService);
            var dictionary = await scoreService.CreateRecordFromManualInput(input);
            int? crSc = dictionary.First().Key.Credit_Score;
            bool? elg = dictionary.First().Key.Eligibility;

            Assert.AreEqual(240, crSc);
            Assert.AreEqual(true, elg);
        }

        [Test]
        public async Task ScoreShouldBe210Again()
        {
            var input = new ManualInputModel
            {
                Gross_Annual_Income = 30_000,
                Loan_Amount = 30_000,
                Marital_Status = 'S',
                Number_of_Dependants = 5,
                Occupation_Code = 'B',
                Residential_Status = 'O',
                Age_of_Applicant = 38,
            };

            var scoreService = new CreditScoreService(dbContext, mapper, validationService);
            var dictionary = await scoreService.CreateRecordFromManualInput(input);
            int? crSc = dictionary.First().Key.Credit_Score;
            bool? elg = dictionary.First().Key.Eligibility;

            Assert.AreEqual(210, crSc);
            Assert.AreEqual(true, elg);
        }

        [Test]
        public async Task ScoreShouldBe220Again()
        {
            var input = new ManualInputModel
            {
                Gross_Annual_Income = 40_000,
                Loan_Amount = 40_000,
                Marital_Status = 'W',
                Number_of_Dependants = 6,
                Occupation_Code = 'M',
                Residential_Status = 'T',
                Age_of_Applicant = 48,
            };

            var scoreService = new CreditScoreService(dbContext, mapper, validationService);
            var dictionary = await scoreService.CreateRecordFromManualInput(input);
            int? crSc = dictionary.First().Key.Credit_Score;
            bool? elg = dictionary.First().Key.Eligibility;

            Assert.AreEqual(220, crSc);
            Assert.AreEqual(true, elg);
        }

        [Test]
        public async Task ScoreShouldBe200()
        {
            var input = new ManualInputModel
            {
                Gross_Annual_Income = 70_000,
                Loan_Amount = 70_000,
                Marital_Status = 'Z',
                Number_of_Dependants = 6,
                Occupation_Code = 'M',
                Residential_Status = 'T',
                Age_of_Applicant = 55,
            };

            var scoreService = new CreditScoreService(dbContext, mapper, validationService);
            var dictionary = await scoreService.CreateRecordFromManualInput(input);
            int? crSc = dictionary.First().Key.Credit_Score;
            bool? elg = dictionary.First().Key.Eligibility;

            Assert.AreEqual(200, crSc);
            Assert.AreEqual(true, elg);
        }

        [Test]
        public async Task ScoreShouldBe80()
        {
            var input = new ManualInputModel
            {
                Gross_Annual_Income = 1_000,
                Loan_Amount = 70_000,
                Marital_Status = 'Z',
                Number_of_Dependants = 6,
                Occupation_Code = 'O',
                Residential_Status = 'O',
                Age_of_Applicant = 18,
            };

            var scoreService = new CreditScoreService(dbContext, mapper, validationService);
            var dictionary = await scoreService.CreateRecordFromManualInput(input);
            int? crSc = dictionary.First().Key.Credit_Score;
            bool? elg = dictionary.First().Key.Eligibility;

            Assert.AreEqual(80, crSc);
            Assert.AreEqual(false, elg);
        }
    }
}
