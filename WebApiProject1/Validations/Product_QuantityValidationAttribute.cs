using System.ComponentModel.DataAnnotations;
using WebApiProject1.Models;

namespace WebApiProject1.Validations
{
    public class Product_QuantityValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var od = validationContext.ObjectInstance as OrderDTO;

            if (od==null || od.Quantity <= 0)
            {
                return new ValidationResult("Quantity must be above 1");
            }
            return ValidationResult.Success;
        }
    }
}
