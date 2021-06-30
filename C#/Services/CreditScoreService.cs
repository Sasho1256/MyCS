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
        private readonly IValidationService validationService;

        public CreditScoreService(MyCsDbContext dbContext, IMapper mapper, IValidationService validationService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.validationService = validationService;
        }

        public async Task<Dictionary<Account, List<string>>> CreateRecordFromManualInput(ManualInputModel input)
        {
            var result = mapper.Map<Account>(input);
            result.Client = mapper.Map<Client>(input);
            result.Loan = mapper.Map<Loan>(input);

            result.Account_Number = GenerateAccountNumber();
            CalculateScore(result);

            List<string> exceptions = validationService.ValidateInputModel(new List<ManualInputModel> { input }).Select(x => x.ErrorMessage).ToList();
            var dic = new Dictionary<Account, List<string>>();
            dic.Add(result, exceptions);

            await dbContext.Accounts.AddAsync(result);
            await dbContext.SaveChangesAsync();
            return dic;
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

            if (result.Loan.Loan_Amount >= 0 && result.Loan.Loan_Amount < 10_000)
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
            #region Loan_To_Income

            //if (result.Loan.Loan_To_Income == 0)
            //{
                if (result.Loan.Loan_Amount == 0)
                {
                    result.Loan.Loan_To_Income = -9999998;
                }
                else if (result.Client.Gross_Annual_Income == 0)
                {
                    result.Loan.Loan_To_Income = -9999997;
                }
                else
                {
                    double loanToIncome = (result.Loan.Loan_Amount * 1.0 / result.Client.Gross_Annual_Income * 1.0) * 100.0;
                    result.Loan.Loan_To_Income = Math.Round(loanToIncome, 2);
                }
            //}


            if (result.Loan.Loan_To_Income == -9999998)
            {
                score += 30;
            }
            else if (result.Loan.Loan_To_Income >= 0 && result.Loan.Loan_To_Income < 500)
            {
                score += 20;
            }
            else if (result.Loan.Loan_To_Income >= 500)
            {
                score += 10;
            }

            #endregion
            //==========================

            result.Credit_Score = score;
            if (score >= 200)
            {
                result.Eligibility = true;
            }
            else
            {
                result.Eligibility = false;
            }
        }

        public string GenerateAccountNumber()
        {
            List<string> acc = dbContext.Accounts.Select(x => x.Account_Number).ToList();
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