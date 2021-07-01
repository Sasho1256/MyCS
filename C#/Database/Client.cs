using CsvHelper.Configuration.Attributes;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class Client
    {
        [Ignore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Range(0, 1, ErrorMessage = "Current_Delinquency_status must be a 0, 1 or null.")]
        public int? Current_Delinquency_status { get; set; }

        [DataType(DataType.Date)]
        public DateTime Application_Date { get; set; }

        [Range(0, 2_000, ErrorMessage = "Application Score must be a positive number.")]
        public int Application_Score { get; set; }

        [Range(0, 1_000_000, ErrorMessage = "Gross annual income must be between 0 and 1 million.")]
        public int Gross_Annual_Income { get; set; }

        [RegularExpression(@"Y|N|(\s)", ErrorMessage = "Home_Telephone_Number should be Y, N or empty space (null).")]
        public char? Home_Telephone_Number { get; set; }

        [RegularExpression("D|M|S|W|Z", ErrorMessage = "Marital_Status should be D, M, S, W or Z.")]
        public char Marital_Status { get; set; }

        [RegularExpression("O|P|B|M", ErrorMessage = "Occupation_Code should be O, P, B or M.")]
        public char Occupation_Code { get; set; }

        [RegularExpression("H|L|O|T", ErrorMessage = "Residential_Status should be H, L, O or T.")]
        public char Residential_Status { get; set; }

        [Range(0, 36500, ErrorMessage = "Time_at_Address should be between 0 and 36500 days.")]
        public int Time_at_Address { get; set; }

        [Range(0, 29200, ErrorMessage = "Time_in_Employment should be between 0 and 29200.")]
        public int Time_in_Employment { get; set; }

        [Range(0, 29200, ErrorMessage = "Time_with_Bank should be between 0 and 29200.")]
        public int Time_with_Bank { get; set; }

        [RegularExpression("Good|Bad|Indeterminate|NTU|Rejects", ErrorMessage = "GB_Flag should be Good, Bad, Indeterminate, NTU or Rejects.")]
        public string GB_Flag { get; set; }

        [Range(18, 100, ErrorMessage = "Age_of_Applicant should be between 18 and 90.")]
        public int Age_of_Applicant { get; set; }

        [Range(1,12, ErrorMessage = "Application_Month should be between 1 and 12.")]
        public int Application_Month { get; set; }
    }

}
