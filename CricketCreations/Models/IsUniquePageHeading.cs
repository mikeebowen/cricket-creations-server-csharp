using System.ComponentModel.DataAnnotations;
using CricketCreationsRepository.Interfaces;

namespace CricketCreations.Models
{
    public class IsUniquePageHeading : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IPageRepository pageRepository = (IPageRepository)validationContext.GetService(typeof(IPageRepository));
            object instance = validationContext.ObjectInstance;
            string id = instance.GetType().GetProperty("Id").GetValue(instance, null).ToString();

            if (int.TryParse(id, out int idInt))
            {
                if (pageRepository.IsUniquePageHeading(value.ToString(), idInt))
                {
                    return ValidationResult.Success;
                }
            }
            else
            {
                if (pageRepository.IsUniquePageHeading(value.ToString()))
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult("Page heading must be unique.");
        }
    }
}
