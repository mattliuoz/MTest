namespace MTest.Validators
{
    public class GetCustomerRequestValidator : IValidator<int>
    {
        public ValidationResult Validate(int requestToValidate)
        {
            var validationResult = new ValidationResult();
            validationResult.IsValid = true;

            if (requestToValidate<=0)
            {
                validationResult.IsValid = false;
                validationResult.ErrorMessage = "Customer Id is invalid.";
                return validationResult;
            }

            return validationResult;
        }
    }
}
