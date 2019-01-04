using MTest.Validators;
using Xunit;

namespace Tests.Validators
{
    public class GetCustomerRequestValidatorTests
    {
        private readonly GetCustomerRequestValidator sut;

        public GetCustomerRequestValidatorTests()
        {
            sut = new GetCustomerRequestValidator();
        }

        [Fact]
        public void Validate_should_return_error_when_id_is_0()
        {
            var id = 0;
            
            var result = sut.Validate(id);

            Assert.False(result.IsValid);
            Assert.Equal("Customer Id is invalid.", result.ErrorMessage);
        }

        [Fact]
        public void Validate_should_return_error_when_id_less_than_0()
        {
            var id = -1;

            var result = sut.Validate(id);

            Assert.False(result.IsValid);
            Assert.Equal("Customer Id is invalid.", result.ErrorMessage);
        }

        [Fact]
        public void Validate_should_pass()
        {
            var id = 1;

            var result = sut.Validate(id);

            Assert.True(result.IsValid);
            Assert.Null(result.ErrorMessage);
        }

    }
}
