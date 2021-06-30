namespace Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Database;
    using Microsoft.EntityFrameworkCore;
    using MyCS.InputModels;
    using NUnit.Framework;
    using Services;
    using Services.Mappings;

    public class ManualFormTest
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
        public async Task ShouldThrowExceptionWhenInputModelPropertiesAreNull()
        {
            var input = new ManualInputModel();
            
            var scoreService = new CreditScoreService(dbContext, mapper, validationService);
            var dictionary = await scoreService.CreateRecordFromManualInput(input);

            Assert.True(dictionary.First().Value.Count != 0);
        }

        [Test]
        public async Task ShouldThrowCorrectExceptionMessagesWhenDataIsWrong()
        {
            var input = new ManualInputModel
            {
                Number_of_Dependants = -1,
                Age_of_Applicant = -1,
                Gross_Annual_Income = -1,
                Marital_Status = ' ',
                Occupation_Code = ' ',
                Residential_Status = ' ',
                Loan_Amount = -1
            };

            var scoreService = new CreditScoreService(dbContext, mapper, validationService);
            var dictionary = await scoreService.CreateRecordFromManualInput(input);
            var exceptionMessages = dictionary.First().Value.ToList();
            var exceptions = new List<string>()
            {
                "Enter a positive number for Number_of_Dependants.",
                "Enter a number between 18 and 100.",
                "Enter a positive number for Gross_Annual_Income.",
                "Choose a valid option (D, M, S, W, Z) for Marital_Status.",
                "Choose a valid option (O, P, B, M) for Occupation_Code.",
                "Choose a valid option (H, L, O, T) for Residential_Status.",
                "Enter a positive number for Loan_Amount."
            };

            bool isSuperset = new HashSet<string>(exceptions).IsSupersetOf(exceptionMessages);
            Assert.True(isSuperset);
        }
    }
}
