using Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    using MyCS.InputModels;

    public interface IValidationService
    {
        public List<ValidationResult> ValidateBeforeDatabase(List<Account> records);

        public List<ValidationResult> ValidateInputModel(List<ManualInputModel> records);
    }
}
