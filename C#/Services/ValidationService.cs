using Database;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyCS.InputModels;

namespace Services
{
    public class ValidationService : IValidationService
    {
        public List<ValidationResult> ValidateBeforeDatabase(List<Account> records)
        {
            var errors = new List<ValidationResult>();
            foreach (var e in records)
            {
                var vcAccount = new ValidationContext(e, null, null);
                var vcClient = new ValidationContext(e.Client, null, null);
                var vcLoan = new ValidationContext(e.Loan, null, null);
                Validator.TryValidateObject(e, vcAccount, errors, true);
                Validator.TryValidateObject(e.Client, vcClient, errors, true);
                Validator.TryValidateObject(e.Loan, vcLoan, errors, true);
            }

            return errors;
        }
        public List<ValidationResult> ValidateInputModel(List<ManualInputModel> records)
        {
            var errors = new List<ValidationResult>();
            foreach (var e in records)
            {
                var vcAccount = new ValidationContext(e, null, null);
                Validator.TryValidateObject(e, vcAccount, errors, true);
            }

            return errors;
        }
    }
}
