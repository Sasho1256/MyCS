namespace Services
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Database;
    using MyCS.InputModels;

    public class CreditScoreService : ICreditScoreService
    {
        private readonly MyCsDbContext dbContext;
        private readonly IMapper mapper;

        public CreditScoreService(MyCsDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task CreateRecordFromManualInput(ManualInputModel input)
        {
            var result = mapper.Map<Account>(input);
            result.Client = mapper.Map<Client>(input);
            result.Loan = mapper.Map<Loan>(input);
            // todo: calculate credit score and decide eligibility
            // todo: save to database
        }
    }
}
