using Database;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyCS.InputModels;

namespace Services
{
    public interface IValidationService
    {
        public List<ValidationResult> ValidateBeforeDatabase(List<Account> records);

        public List<ValidationResult> ValidateInputModel(List<ManualInputModel> records);
    }
}
