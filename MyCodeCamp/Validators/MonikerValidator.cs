using System.ComponentModel.DataAnnotations;
using MyCodeCamp.Data;

namespace MyCodeCamp.Validators
{
    public class MonikerValidator : ValidationAttribute
    {
        
        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            ICampRepository campRepository =
              (ICampRepository)  validationContext.GetService(typeof(ICampRepository));

            if (campRepository.IsMonikerUnique((string) value))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Duplicate moniker");
            
        }
    }
}