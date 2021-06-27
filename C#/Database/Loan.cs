using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database
{
    using System.ComponentModel.DataAnnotations;

    public class Loan
    {
        [Ignore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Range(0, 1_000_000, ErrorMessage = "Loan_Amount should be between 0 and 1000000.")]
        public int Loan_Amount { get; set; }

        [RegularExpression(@"F|M|W|X|(\s)", ErrorMessage = "Loan_Payment_Frequency should be F, M, W, X or Empty.")]
        public char? Loan_Payment_Frequency { get; set; }

        [RegularExpression(@"B|Q|S|X|(\s)", ErrorMessage = "Loan_Payment_Method should be B, Q, S, X or Empty.")]
        public char? Loan_Payment_Method { get; set; }

        public double Loan_To_Income { get; set; }
    }
}
