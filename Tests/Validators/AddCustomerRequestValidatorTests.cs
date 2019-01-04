using MTest.Models;
using MTest.Validators;
using System;
using Xunit;

namespace Tests.Validators
{
    public class AddCustomerRequestValidatorTests
    {
        private readonly AddCustomerRequestValidator sut;

        public AddCustomerRequestValidatorTests()
        {
            sut = new AddCustomerRequestValidator();
        }

        [Fact]
        public void Validate_should_return_error_when_first_name_is_empty()
        {
            var request = BuildTestRequest();
            request.FirstName = string.Empty;

            var result = sut.Validate(request);

            Assert.False(result.IsValid);
            Assert.Equal("First name cannot be null or empty.", result.ErrorMessage);
        }

        [Fact]
        public void Validate_should_return_error_when_last_name_is_empty()
        {
            var request = BuildTestRequest();
            request.LastName = string.Empty;

            var result = sut.Validate(request);

            Assert.False(result.IsValid);
            Assert.Equal("Last name cannot be null or empty.", result.ErrorMessage);
        }

        [Fact]
        public void Validate_should_return_error_when_email_is_empty()
        {
            var request = BuildTestRequest();
            request.Email = string.Empty;

            var result = sut.Validate(request);

            Assert.False(result.IsValid);
            Assert.Equal("Email format is invalid.", result.ErrorMessage);
        }

        [Fact]
        public void Validate_should_return_error_when_email_format_is_invalid()
        {
            var request = BuildTestRequest();
            request.Email = "blah";

            var result = sut.Validate(request);

            Assert.False(result.IsValid);
            Assert.Equal("Email format is invalid.", result.ErrorMessage);
        }

        [Fact]
        public void Validate_should_return_error_when_dob_is_empty()
        {
            var request = BuildTestRequest();
            request.DOB = DateTime.MinValue;

            var result = sut.Validate(request);

            Assert.False(result.IsValid);
            Assert.Equal("Date of birth cannot be null.", result.ErrorMessage);
        }

        [Fact]
        public void Validate_should_pass()
        {
            var request = BuildTestRequest();

            var result = sut.Validate(request);

            Assert.True(result.IsValid);
            Assert.Null(result.ErrorMessage);
        }

        private AddCustomerRequest BuildTestRequest()
        {
            return new AddCustomerRequest()
            {
                FirstName = "test first name",
                LastName = "test last name",
                Email = "test@email.com",
                DOB = new DateTime(2011, 01, 01)
            };
        }
    }
}
