using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace RestaurantSystem.API.Attributes
{
    public class PhoneNumberValidationAttribute : ValidationAttribute
    {
        private readonly string _pattern = @"^[\+]?[1-9][\d]{0,15}$";

        public override bool IsValid(object? value)
        {
            if (value == null) return true;

            var phoneNumber = value.ToString()?.Replace("-", "").Replace(" ", "");
            return !string.IsNullOrEmpty(phoneNumber) && Regex.IsMatch(phoneNumber, _pattern);
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must be a valid phone number (digits only, optionally starting with +)";
        }
    }

    public class PositiveDecimalAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return true;

            if (value is decimal decimalValue)
            {
                return decimalValue > 0;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must be a positive decimal value";
        }
    }

    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return true;

            if (value is DateTime dateValue)
            {
                return dateValue > DateTime.Now;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must be a future date";
        }
    }
}
