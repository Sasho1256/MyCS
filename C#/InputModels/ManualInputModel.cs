using System.ComponentModel.DataAnnotations;

namespace MyCS.InputModels
{
    public class ManualInputModel 
    {
        #region financial info
        [Required]
        [Display(Name = "Number of Dependants")]
        [Range(0, int.MaxValue, ErrorMessage = "Enter a positive number for Number_of_Dependants.")]
        public int Number_of_Dependants { get; set; }
        #endregion
        #region personal info
        [Required]
        [Display(Name = "Age")]
        [Range(18, 100, ErrorMessage = "Enter a number between 18 and 100.")]
        public int Age_of_Applicant { get; set; }

        [Required]
        [Display(Name = "Gross Annual Income")]
        [Range(0, int.MaxValue, ErrorMessage = "Enter a positive number for Gross_Annual_Income.")]
        public int Gross_Annual_Income { get; set; }

        [Required]
        [Display(Name = "Marital Status")]
        [RegularExpression("D|M|S|W|Z", ErrorMessage = "Choose a valid option (D, M, S, W, Z) for Marital_Status.")]
        public char Marital_Status { get; set; }

        [Required]
        [Display(Name = "Occupation Code")]
        [RegularExpression("O|P|B|M", ErrorMessage = "Choose a valid option (O, P, B, M) for Occupation_Code.")]
        public char Occupation_Code { get; set; }

        [Required]
        [Display(Name = "Residential Status")]
        [RegularExpression("H|L|O|T", ErrorMessage = "Choose a valid option (H, L, O, T) for Residential_Status.")]
        public char Residential_Status { get; set; }
        #endregion
        #region loan info
        [Required]
        [Display(Name = "Loan Amount")]
        [Range(0, int.MaxValue, ErrorMessage = "Enter a positive number for Loan_Amount.")]
        public int Loan_Amount { get; set; }
        #endregion
    }
}
