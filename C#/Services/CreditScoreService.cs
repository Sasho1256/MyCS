﻿namespace Services
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

            #region Gross_Annual_Income

            if (result.Client.Gross_Annual_Income >= 0 && result.Client.Gross_Annual_Income < 10_000)
            {
                score += 10;
            }
            else if (result.Client.Gross_Annual_Income >= 10_000 && result.Client.Gross_Annual_Income < 20_000)
            {
                score += 20;
            }
            else if (result.Client.Gross_Annual_Income >= 20_000 && result.Client.Gross_Annual_Income < 30_000)
            {
                score += 30;
            }
            else if (result.Client.Gross_Annual_Income >= 30_000 && result.Client.Gross_Annual_Income < 40_000)
            {
                score += 40;
            }
            else if (result.Client.Gross_Annual_Income >= 40_000 && result.Client.Gross_Annual_Income < 70_000)
            {
                score += 50;
            }
            else if (result.Client.Gross_Annual_Income >= 70_000)
            {
                score += 60;
            }
            #endregion
            //==========================
            #region Loan_Amount

            if (result.Loan.Loan_Amount >= 500 && result.Loan.Loan_Amount < 10_000)
            {
                score += 60;
            }
            else if (result.Loan.Loan_Amount >= 10_000 && result.Loan.Loan_Amount < 20_000)
            {
                score += 50;
            }
            else if (result.Loan.Loan_Amount >= 20_000 && result.Loan.Loan_Amount < 30_000)
            {
                score += 40;
            }
            else if (result.Loan.Loan_Amount >= 30_000 && result.Loan.Loan_Amount < 40_000)
            {
                score += 30;
            }
            else if (result.Loan.Loan_Amount >= 40_000 && result.Loan.Loan_Amount < 70_000)
            {
                score += 20;
            }
            else if (result.Loan.Loan_Amount >= 70_000)
            {
                score += 10;
            }
            #endregion
            //==========================
            #region Marital_Status

            if (result.Client.Marital_Status == 'D')
            {
                score += 40;
            }
            else if (result.Client.Marital_Status == 'M')
            {
                score += 50;
            }
            else if (result.Client.Marital_Status == 'S')
            {
                score += 30;
            }
            else if (result.Client.Marital_Status == 'W')
            {
                score += 20;
            }
            else if (result.Client.Marital_Status == 'Z')
            {
                score += 10;
            }
            #endregion
            //==========================
            #region Number_of_Dependants

            if (result.Number_of_Dependants == 0)
            {
                score += 40;
            }
            else if (result.Number_of_Dependants == 1 || result.Number_of_Dependants == 2)
            {
                score += 30;
            }
            else if (result.Number_of_Dependants == 3 || result.Number_of_Dependants == 4)
            {
                score += 20;
            }
            else if (result.Number_of_Dependants >= 5)
            {
                score += 10;
            }
            #endregion
            //==========================
            #region Occupation_Code

            if (result.Client.Occupation_Code == 'O')
            {
                score += 10;
            }
            else if (result.Client.Occupation_Code == 'P')
            {
                score += 30;
            }
            else if (result.Client.Occupation_Code == 'B')
            {
                score += 20;
            }
            else if (result.Client.Occupation_Code == 'M')
            {
                score += 40;
            }
            #endregion
            //==========================
            #region Residential_Status

            if (result.Client.Residential_Status == 'H')
            {
                score += 40;
            }
            else if (result.Client.Residential_Status == 'L')
            {
                score += 30;
            }
            else if (result.Client.Residential_Status == 'O')
            {
                score += 10;
            }
            else if (result.Client.Residential_Status == 'T')
            {
                score += 20;
            }
            #endregion
            //==========================
            #region Age_of_Applicant

            if (result.Client.Age_of_Applicant >= 18 && result.Client.Age_of_Applicant < 28)
            {
                score += 10;
            }
            else if (result.Client.Age_of_Applicant >= 28 && result.Client.Age_of_Applicant < 38)
            {
                score += 20;
            }
            else if (result.Client.Age_of_Applicant >= 38 && result.Client.Age_of_Applicant < 48)
            {
                score += 50;
            }
            else if (result.Client.Age_of_Applicant >= 48 && result.Client.Age_of_Applicant < 55)
            {
                score += 40;
            }
            else if (result.Client.Age_of_Applicant >= 55)
            {
                score += 30;
            }
            #endregion
            //==========================

            result.Credit_Score = score;
            if (score > 5)                 //TODO: ADD REAL MIDDLEPOINT
            {
                result.Eligibility = true;
            }
        }
    }
}
