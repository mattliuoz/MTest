using MTest.Models;
using System;

namespace MTest.Validators
{
    public class GetCustomersRequestValidator : BaseValidator, IValidator<GetCustomersRequest>
    {
        public ValidationResult Validate(GetCustomersRequest requestToValidate)
        {
            var validationResult = new ValidationResult();
            validationResult.IsValid = true;

            if (!string.IsNullOrWhiteSpace(requestToValidate.Email))
            {
                if (!IsValidEmail(requestToValidate.Email))
                {
                    validationResult.IsValid = false;
                    validationResult.ErrorMessage = "Email format is invalid.";
                    return validationResult;
                }
            }
            if (!string.IsNullOrWhiteSpace(requestToValidate.OrderByDirection)
                && !requestToValidate.OrderByDirection.Equals("asc", StringComparison.OrdinalIgnoreCase)
                && !requestToValidate.OrderByDirection.Equals("desc", StringComparison.OrdinalIgnoreCase))
            {
                validationResult.IsValid = false;
                validationResult.ErrorMessage = "OrderByDirection can only be asc or desc.";
                return validationResult;
            }
            if (requestToValidate.Page < 0)
            {
                validationResult.IsValid = false;
                validationResult.ErrorMessage = "Page is invalid.";
                return validationResult;
            }

            if (requestToValidate.RecordsPerPage < 0)
            {
                validationResult.IsValid = false;
                validationResult.ErrorMessage = "RecordsPerPage is invalid.";
                return validationResult;
            }

            return validationResult;
        }
    }
}
