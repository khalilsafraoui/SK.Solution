using System.ComponentModel.DataAnnotations;


namespace SK.CRM.Application.Validators
{
    public class OptionalPhoneAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string phoneNumber && !string.IsNullOrWhiteSpace(phoneNumber))
            {
                var phoneAttribute = new PhoneAttribute();
                if (!phoneAttribute.IsValid(phoneNumber))
                {
                    return new ValidationResult("Invalid phone number format.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
