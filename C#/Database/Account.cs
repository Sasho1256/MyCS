using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    using System.ComponentModel.DataAnnotations;

    public class Account
    {
        [Ignore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(11, ErrorMessage = "Account number should be 11 symbols long.")]
        public string Account_Number { get; set; }

        [RegularExpression("FL|VL", ErrorMessage = "Account_Type should be FL (Fixed Loan) or VL(Variable Loan).")]
        [Default("FL")]
        public string Account_Type { get; set; }

        [RegularExpression("Accept|Decline", ErrorMessage = "Final_Decision should be Accept or Decline.")]
        [Default("Decline")]
        public string Final_Decision { get; set; }

        [RegularExpression(@"Y|N|(\s)", ErrorMessage = "Cheque_Card_Flag should be Y, N or empty space (null).")]
        public char? Cheque_Card_Flag { get; set; }

        [RegularExpression(@"Y|N|(\s)", ErrorMessage = "Existing_Customer_Flag should be Y, N or empty space (null).")]
        public char? Existing_Customer_Flag { get; set; }

        [RegularExpression(@"Y|N|(\s)", ErrorMessage = "Insurance_Required should be Y, N or empty space (null).")]
        public char? Insurance_Required { get; set; }

        [Range(0, 20, ErrorMessage = "Number_of_Dependants should be a number between 0 and 20.")]
        public int Number_of_Dependants { get; set; }

        [Range(0, 1500, ErrorMessage = "Number_of_Payments should be a number between 0 and 1500.")]
        public int Number_of_Payments { get; set; }

        [RegularExpression("AD|DM|OT|RR", ErrorMessage = "Promotion_Type should be AD, DM, OT or RR.")]
        public string Promotion_Type { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Weight_Factor should be a positive number or 0.")]
        public double Weight_Factor { get; set; }

        [Range(0, 2000, ErrorMessage = "Bureau_Score should be between 0 and 2000.")]
        public int Bureau_Score { get; set; }

        [Range(1, 5, ErrorMessage = "SP_ER_Reference should be between 1 and 5.")]
        public int SP_ER_Reference { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "SP_Number_Of_Searches_L6M should be between 0 and 5000.")]
        public int SP_Number_Of_Searches_L6M { get; set; }

        [Range(0, 100, ErrorMessage = "SP_Number_of_CCJs should be between 0 and 100.")]
        public int SP_Number_of_CCJs { get; set; }

        [RegularExpression("Development|Validation", ErrorMessage = "Split should be Development or Validation.")]
        [Default("Development")]
        public string split { get; set; }

        [Default(0)]
        public int? Credit_Score { get; set; }

        [Default(false)]
        public bool? Eligibility { get; set; }

        public string Reasons { get; set; }

        [ForeignKey("Client")]
        [Column("Client_Id")]
        [Ignore]
        public int Client_Id { get; set; }

        [Ignore]
        public virtual Client Client { get; set; }

        [Ignore]
        public int LoanId { get; set; }

        [Ignore]
        public virtual Loan Loan { get; set; }
    }
}
