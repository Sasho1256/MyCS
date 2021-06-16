namespace Database
{
    public class Loan
    {
        public int Id { get; set; }

        public int Loan_Amount { get; set; }

        public int Loan_Payment_Frequency { get; set; }

        public int Loan_Payment_Method { get; set; }

        public int Loan_To_Income { get; set; }

        public int AccountId { get; set; }

        public Account Account { get; set; }
    }
}
