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

        [Range(0, int.MaxValue)]
        public int Loan_Amount { get; set; }

        [RegularExpression(@"F|M|W|X|(\s)")]
        public char? Loan_Payment_Frequency { get; set; }

        [RegularExpression(@"B|Q|S|X|(\s)")]
        public char? Loan_Payment_Method { get; set; }

        public double Loan_To_Income { get; set; }
    }
}
