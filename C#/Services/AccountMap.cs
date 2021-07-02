using CsvHelper.Configuration;
using Database;
using System;
using System.Globalization;

namespace Services
{
    public sealed class AccountMap : ClassMap<Account>
    {
        public AccountMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(x => x.Account_Number).Validate(x => CreateExceptionMessage("Account_Number", x.Field.Length != 11, "Account Number should be 11 symbols long. "));
            //client
            Map(m => m.Client.Application_Date).Name("Application_Date").Convert(x => ConvertStringToDateTime(x.Row[4]));
            Map(m => m.Client.Current_Delinquency_status).Name("Current_Delinquency_status");
            Map(c => c.Client.Application_Score).Name("Application_Score");
            Map(c => c.Client.Gross_Annual_Income).Name("Gross_Annual_Income");
            Map(c => c.Client.Home_Telephone_Number).Name("Home_Telephone_Number");
            Map(c => c.Client.Marital_Status).Name("Marital_Status");
            Map(c => c.Client.Occupation_Code).Name("Occupation_Code");
            Map(c => c.Client.Residential_Status).Name("Residential_Status");
            Map(c => c.Client.Time_at_Address).Name("Time_at_Address");
            Map(c => c.Client.Time_in_Employment).Name("Time_in_Employment");
            Map(c => c.Client.Time_with_Bank).Name("Time_with_Bank");
            Map(c => c.Client.GB_Flag).Name("GB_Flag");
            Map(c => c.Client.Age_of_Applicant).Name("Age_of_Applicant");
            Map(c => c.Client.Application_Month).Name("Application_Month").Convert(x => ConvertStringToDateTime(x.Row[4]).Month);
            //loan
            Map(c => c.Loan.Loan_Amount).Name("Loan_Amount");
            Map(c => c.Loan.Loan_Payment_Frequency).Name("Loan_Payment_Frequency");
            Map(c => c.Loan.Loan_Payment_Method).Name("Loan_Payment_Method");
            Map(c => c.Loan.Loan_To_Income).Name("loan_to_income");
        }

        private DateTime ConvertStringToDateTime(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new Exception($"Column Application_Date cannot be null or empty.");
            }
            string year = input.Substring(0, 4) + "/";
            string month = input.Substring(4, 2) + "/";
            string day = input.Substring(6, 2);
            DateTime a;
            try
            {
                a = DateTime.Parse(year + month + day);
            }
            catch (Exception e)
            {
                throw new Exception($"Exception occured at column Application_Date in row ");
            }
            
            return a;
        }

        private bool CreateExceptionMessage(string columnName, bool trigger, string additionalInfo = "")
        {
            if (trigger)
            {
                throw new Exception($"{additionalInfo}" + $"Exception occured at column {columnName} in row ");
            }
            return true;
        }
    }
}
