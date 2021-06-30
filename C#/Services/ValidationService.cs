using Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
