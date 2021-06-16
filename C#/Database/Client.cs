using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database
{
    public partial class Client
    {
        [Ignore]
        public int Id { get; set; }
        public byte? Current_Delinquency_status { get; set; }
        public int Application_Date { get; set; }
        public int Application_Score { get; set; }
        public int Gross_Annual_Income { get; set; }
        public char? Home_Telephone_Number { get; set; }
        public char Marital_Status { get; set; }
        public char Occupation_Code { get; set; }
        public char Residential_Status { get; set; }
        public int Time_at_Address { get; set; }
        public int Time_in_Employment { get; set; }
        public int Time_with_Bank { get; set; }
        public string GB_Flag { get; set; }
        public int Age_of_Applicant { get; set; }
        public int Application_Month { get; set; }
        
        [ForeignKey("Account")]
        [Column("Account_Id")]
        [Ignore]
        public int Account_Id { get; set; }

        [Ignore]
        public virtual Account Account { get; set; }
    }
}
