using Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IValidationService
    {
        public List<ValidationResult> ValidateBeforeDatabase(List<Account> records);
    }
}
