using System.ComponentModel.DataAnnotations;
using CricketCreationsRepository.Interfaces;

namespace CricketCreations.Models
{
    public class IsUniquePageHeading : ValidationAttribute
    {
        // public IsUniquePageHeading(IPageRepository pageRepository)
        // {
        //    _pageRepository = pageRepository;
        // }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var pageRepository = (IPageRepository)validationContext.GetService(typeof(IPageRepository));

            if (pageRepository.IsUniquePageHeading(value.ToString()))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Page heading must be unique.");
        }
    }
}
