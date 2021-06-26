using CsvHelper.Configuration.Attributes;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database
{
    using System.ComponentModel.DataAnnotations;

    public class Client
    {
        [Ignore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Range(0, byte.MaxValue)]
        public byte? Current_Delinquency_status { get; set; }

        [DataType(DataType.Date)]
        public DateTime Application_Date { get; set; }

        [Range(0, int.MaxValue)]
        public int Application_Score { get; set; }

        [Range(0, int.MaxValue)]
        public int Gross_Annual_Income { get; set; }

        [RegularExpression(@"Y|N|(\s)")]
        public char? Home_Telephone_Number { get; set; }

        [RegularExpression("D|M|S|W|Z")]
        public char Marital_Status { get; set; }

        [RegularExpression("O|P|B|M")]
        public char Occupation_Code { get; set; }

        [RegularExpression("H|L|O|T")]
        public char Residential_Status { get; set; }

        [Range(0, int.MaxValue)]
        public int Time_at_Address { get; set; }

        [Range(0, int.MaxValue)]
        public int Time_in_Employment { get; set; }

        [Range(0, int.MaxValue)]
        public int Time_with_Bank { get; set; }

        public string GB_Flag { get; set; }

        [Range(18, 120)]
        public int Age_of_Applicant { get; set; }

        [Range(1,12)]
        public int Application_Month { get; set; }
    }

}
