using CricketCreationsRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CricketCreations.Models
{
    public class IsUniquePageHeading : ValidationAttribute
    {

        //public IsUniquePageHeading(IPageRepository pageRepository)
        //{
        //    _pageRepository = pageRepository;
        //}
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var _pageRepository = (IPageRepository)validationContext.GetService(typeof(IPageRepository));

            if (_pageRepository.IsUniquePageHeading(value.ToString()))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Page heading must be unique.");
        }
    }
}
