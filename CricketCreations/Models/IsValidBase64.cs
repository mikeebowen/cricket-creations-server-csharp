using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CricketCreations.Models
{
    public class IsValidBase64 : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            string base64;
            string val = value.ToString();

            if (val.Contains(','))
            {
                base64 = val.Split(',')[1];
            }
            else
            {
                base64 = val;
            }

            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);

            bool valid = Convert.TryFromBase64String(base64, buffer, out int bytesParsed);

            return valid ? ValidationResult.Success : new ValidationResult("Invalid base64 string");
        }
    }
}
