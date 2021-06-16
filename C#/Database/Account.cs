using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public partial class Account
    {
        [Ignore]
        public int Id { get; set; }
        public string Account_Number { get; set; }
        public string Account_Type { get; set; }
        public string Final_Decision { get; set; }
        public char Cheque_Card_Flag { get; set; }
        public char Existing_Customer_Flag { get; set; }
        public char Insurance_Required { get; set; }
        public int Number_of_Dependants { get; set; }
        public int Number_of_Payments { get; set; }
        public string Promotion_Type { get; set; }
        public double Weight_Factor { get; set; }
        public int Bureau_Score { get; set; }
        public int SP_ER_Reference { get; set; }
        public int SP_Number_Of_Searches_L6M { get; set; }
        public int SP_Number_of_CCJs { get; set; }
        public string split { get; set; }

        [ForeignKey("Client")]
        [Column("Client_Id")]
        [Ignore]
        public int Client_Id { get; set; }

        [Ignore]
        public virtual Client Client { get; set; }

        [Ignore]
        public virtual ICollection<Loan> Loans { get; set; }
    }
}
