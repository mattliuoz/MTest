using MTest.Models;
using System;

namespace MTest.Validators
{
    public interface IValidator<T>
    {
        ValidationResult Validate(T requestToValidate);
    }

    public class AddCustomerRequestValidator : BaseValidator, IValidator<AddCustomerRequest>
    {
        public ValidationResult Validate(AddCustomerRequest requestToValidate)
        {
            var validationResult = new ValidationResult();
            validationResult.IsValid = true;

            if (string.IsNullOrWhiteSpace(requestToValidate.FirstName))
            {
                validationResult.IsValid = false;
                validationResult.ErrorMessage = "First name cannot be null or empty.";
                return validationResult;
            }

            if (string.IsNullOrWhiteSpace(requestToValidate.LastName))
            {
                validationResult.IsValid = false;
                validationResult.ErrorMessage = "Last name cannot be null or empty.";
                return validationResult;
            }

            if (!IsValidEmail(requestToValidate.Email))
            {
                validationResult.IsValid = false;
                validationResult.ErrorMessage = "Email format is invalid.";
                return validationResult;
            }

            //IsValidEmail

            //Assumption: there's no mention of if DOB should be required, but based on the logic of how CustCode was generated, 
            // I just assume that DOB is required.
            if (requestToValidate.DOB == DateTime.MinValue)
            {
                validationResult.IsValid = false;
                validationResult.ErrorMessage = "Date of birth cannot be null.";
                return validationResult;
            }

            if (requestToValidate.DOB > DateTime.Now)
            {
                validationResult.IsValid = false;
                validationResult.ErrorMessage = "Date of birth cannot be future.";
                return validationResult;
            }

            return validationResult;
        }
        
    }
}
