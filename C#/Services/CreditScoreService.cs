namespace Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        public async Task<Account> CreateRecordFromManualInput(ManualInputModel input)
        {
            var result = mapper.Map<Account>(input);
            result.Client = mapper.Map<Client>(input);
            result.Loan = mapper.Map<Loan>(input);

            CalculateScore(result);

            dbContext.SaveChanges();
            return result;
        }

        public void CalculateScore(Account result)
        {
            int score = 0;

            if (result.Client.Age_of_Applicant >= 18 && result.Client.Age_of_Applicant < 27)
            {
                score += 1;
            }
            else if (result.Client.Age_of_Applicant >= 27 && result.Client.Age_of_Applicant < 37)
            {
                score += 2;
            }
            else if (result.Client.Age_of_Applicant >= 37 && result.Client.Age_of_Applicant < 48)
            {
                score += 3;
            }
            else if (result.Client.Age_of_Applicant >= 48)
            {
                score += 4;
            }

            //================================================================================

            if (result.Client.Residential_Status == 'H')
            {
                score += 4;
            }
            else if (result.Client.Residential_Status == 'O')
            {
                score += 3;
            }
            else if (result.Client.Residential_Status == 'T')
            {
                score += 2;
            }
            else if (result.Client.Residential_Status == 'L')
            {
                score += 1;
            }

            //================================================================================

            if (result.Number_of_Dependants >= 0 && result.Number_of_Dependants <= 2)
            {
                score += 3;
            }
            else if (result.Number_of_Dependants >= 3 && result.Number_of_Dependants <= 5)
            {
                score += 2;
            }
            else if (result.Number_of_Dependants >= 6)
            {
                score += 1;
            }

            result.Credit_Score = score;
            if (score > 5)                 //TODO: ADD REAL MIDDLEPOINT
            {
                result.Eligibility = true;
            }
        }
    }
}
