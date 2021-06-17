using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database
{
    public class Loan
    {
        [Ignore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Loan_Amount { get; set; }

        public char? Loan_Payment_Frequency { get; set; }

        public char? Loan_Payment_Method { get; set; }

        public double Loan_To_Income { get; set; }
    }
}
