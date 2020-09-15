using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Forma1Teams.Services
{
	public class ValidatorService
	{
        public class FoundationValidator : ValidationAttribute
        {
            public override string FormatErrorMessage(string name)
            {
                return "Year value must be between 1946 and " + DateTime.Now.Year;
            }

            protected override ValidationResult IsValid(object objValue, ValidationContext validationContext)
            {
                if ((int)objValue < 1946 || DateTime.Now.Year < (int)objValue)
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
                return ValidationResult.Success;
            }
        }

        public class WorldChampionNumberValidator : ValidationAttribute
        {
            //2020-ban a Ferrarinak volt a legtöbb győzelme (16), a maximális érték pedig évente növekedhet egyel, ha minden évben a Ferrari nyer
            //A +1 pedig azért kell ha valami csoda folytán esetleg 2020-ban is a Ferrari nyerne.
            private int MaximumValue = 16 + 1 + DateTime.Now.Year - 2020;
            public override string FormatErrorMessage(string name)
            {
                return "Value must be between 0 and " + MaximumValue;
            }

            protected override ValidationResult IsValid(object objValue, ValidationContext validationContext)
            {
                if ((int)objValue < 0 || MaximumValue < (int)objValue)
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
                return ValidationResult.Success;
            }
        }
    }
}
