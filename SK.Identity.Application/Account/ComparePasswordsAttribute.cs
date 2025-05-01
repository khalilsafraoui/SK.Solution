﻿using System.ComponentModel.DataAnnotations;

namespace SK.Identity.Account
{
    public class ComparePasswordsAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public ComparePasswordsAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
            ErrorMessage = "Passwords do not match.";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var currentValue = value?.ToString();
            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var comparisonValue = property.GetValue(validationContext.ObjectInstance)?.ToString();

            if (currentValue != comparisonValue)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }

}
