﻿using System;
using System.ComponentModel.DataAnnotations;

namespace InputModels
{
    public class ManualInputModel
    {
        #region financial info
        [Required]
        [StringLength(11, ErrorMessage = "Account number cannot be more or less than 11 symbols.")]
        [Display(Name = "Account Number")]
        public string Account_Number { get; set; }

        [Required]
        [Display(Name = "Account Type")]
        //dropdown FL - Fixed Loan/VL - Variable Loan 
        public string Account_Type { get; set; }

        [Required]
        [Display(Name = "Do you have a cheque card?")]
        //checkbox - has cheque card
        public bool Cheque_Card_Flag { get; set; }

        [Required]
        [Display(Name = "Do you have the required insurance?")]
        //checkbox - if insurance is required
        public bool Insurance_Required { get; set; }

        [Required]
        [Display(Name = "Number of Dependants")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a positive number.")]
        public int Number_of_Dependants { get; set; }

        [Required]
        [Display(Name = "Number of Payments")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a positive number.")]
        public int Number_of_Payments { get; set; }

        [Required]
        [Display(Name = "Promotion Type")]
        //dropdown FL - Fixed Loan/VL - Variable Loan 
        //AD, DM, OT, RR
        public string Promotion_Type { get; set; }

        //Bureau score variables
        [Required]
        [Display(Name = "Bureau Score")]
        [Range(0, int.MaxValue, ErrorMessage = "Enter a positive number.")]
        public int Bureau_Score { get; set; }

        [Required]
        [Display(Name = "Electoral Role")]
        [Range(1, 4, ErrorMessage = "Enter a number between 1 and 4.")]
        public int SP_ER_Reference { get; set; }

        [Required]
        [Display(Name = "Number of searches")]
        [Range(0, int.MaxValue, ErrorMessage = "Enter a positive number.")]
        public int SP_Number_Of_Searches_L6M { get; set; }

        [Required]
        [Display(Name = "Number of CCJs")]
        [Range(0, int.MaxValue, ErrorMessage = "Enter a positive number.")]
        public int SP_Number_of_CCJs { get; set; }
        #endregion
        #region personal info
        [Required]
        [Display(Name = "Age")]
        [Range(18, 120, ErrorMessage = "Enter a number between 18 and 120.")]
        public int Age_of_Applicant { get; set; }

        [Required]
        [Display(Name = "Current Delinquency status")]
        public byte? Current_Delinquency_status { get; set; }

        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "Enter a valid date.")]
        [Display(Name = "Application Date")]
        public DateTime Application_Date { get; set; }

        [Required]
        [Display(Name = "Gross Annual Income")]
        [Range(0, int.MaxValue, ErrorMessage = "Enter a positive number.")]
        public int Gross_Annual_Income { get; set; }

        [Required]
        [Display(Name = "Do you have a home phone?")]
        //checkbox - has home phone
        public char? Home_Telephone_Number { get; set; }

        [Required]
        [Display(Name = "Marital Status")]
        //dropdown - D/M/S/W/Z
        public char Marital_Status { get; set; }

        [Required]
        [Display(Name = "Occupation Code")]
        //dropdown - O/P/B/M
        public char Occupation_Code { get; set; }

        [Required]
        [Display(Name = "Residential Status")]
        //dropdown - H/L/O/T
        public char Residential_Status { get; set; }

        [Required]
        [Display(Name = "Time at address")]
        [Range(0, int.MaxValue, ErrorMessage = "Enter a positive number.")]
        public int Time_at_Address { get; set; }

        [Required]
        [Display(Name = "Time in employment")]
        [Range(0, int.MaxValue, ErrorMessage = "Enter a positive number.")]
        public int Time_in_Employment { get; set; }

        [Required]
        [Display(Name = "Time with bank")]
        [Range(0, int.MaxValue, ErrorMessage = "Enter a positive number.")]
        public int Time_with_Bank { get; set; }
        #endregion
        #region loan info
        [Required]
        [Display(Name = "Loan Amount")]
        [Range(0, int.MaxValue, ErrorMessage = "Enter a positive number.")]
        public int Loan_Amount { get; set; }

        // todo: check default values
        [Required]
        [Display(Name = "How often do you pay your loan?")]
        // dropdown - na/f/m/w/x
        public char? Loan_Payment_Frequency { get; set; }

        [Required]
        [Display(Name = "How do you pay your loan?")]
        // dropdown - na/b/q/s/x
        public char? Loan_Payment_Method { get; set; }

        // ??
        public double Loan_To_Income { get; set; }
        #endregion
    }
}