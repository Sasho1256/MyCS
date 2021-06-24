﻿using CsvHelper.Configuration;
using Database;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Services
{
    using CsvHelper;

    public sealed class AccountMap : ClassMap<Account>
    {
        public AccountMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(x => x.Account_Number).Validate(x => x.Field.Length == 11);
            //client
            Map(m => m.Client.Application_Date).Name("Application_Date").Convert(x => ConvertStringToDateTime(x.Row[4]));
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
            if (input != null)
            {
                string year = input.Substring(0, 4) + "/";
                string month = input.Substring(4, 2) + "/";
                string day = input.Substring(6, 2);
                // todo: try-catch and return exception if invalid data
                DateTime a;
                var date = DateTime.TryParse(year + month + day, out a);
                return date ? a : new DateTime(2000, 1, 1);
            }
            return new DateTime(2000, 1, 1);
        }
    }
}
