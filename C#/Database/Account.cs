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

    public partial class Account
    {
        [Ignore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(11, ErrorMessage = "Account number should be 11 symbols long.")]
        public string Account_Number { get; set; }

        [RegularExpression("FL|VL")]
        public string Account_Type { get; set; }

        [RegularExpression("Accept|Decline")]
        public string Final_Decision { get; set; }

        [RegularExpression(@"Y|N|(\s)")]
        public char? Cheque_Card_Flag { get; set; }

        [RegularExpression(@"Y|N|(\s)")]
        public char? Existing_Customer_Flag { get; set; }

        [RegularExpression(@"Y|N|(\s)")]
        public char? Insurance_Required { get; set; }

        [Range(0, int.MaxValue)]
        public int Number_of_Dependants { get; set; }

        [Range(0, int.MaxValue)]
        public int Number_of_Payments { get; set; }

        [RegularExpression("AD|DM|OT|RR")]
        public string Promotion_Type { get; set; }

        [Range(0, int.MaxValue)]
        public double Weight_Factor { get; set; }

        [Range(0, int.MaxValue)]
        public int Bureau_Score { get; set; }

        [Range(1, 4)]
        public int SP_ER_Reference { get; set; }

        [Range(0, int.MaxValue)]
        public int SP_Number_Of_Searches_L6M { get; set; }

        [Range(0, int.MaxValue)]
        public int SP_Number_of_CCJs { get; set; }

        public string split { get; set; }

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
