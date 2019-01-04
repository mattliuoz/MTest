using MTest.Models;
using MTest.Validators;
using Xunit;

namespace Tests.Validators
{
    public class GetCustomersRequestValidatorTests
    {
        private readonly GetCustomersRequestValidator sut;

        public GetCustomersRequestValidatorTests()
        {
            sut = new GetCustomersRequestValidator();
        }

        [Fact]
        public void Validate_should_return_error_when_OrderByDirection_is_a_wrong_value()
        {
            var request = BuildTestRequest();
            request.OrderByDirection = "blah";

            var result = sut.Validate(request);

            Assert.False(result.IsValid);
            Assert.Equal("OrderByDirection can only be asc or desc.", result.ErrorMessage);
        }

        [Fact]
        public void Validate_should_return_error_when_RecordsPerPage_is_less_than_0()
        {
            var request = BuildTestRequest();
            request.RecordsPerPage = -1;

            var result = sut.Validate(request);

            Assert.False(result.IsValid);
            Assert.Equal("RecordsPerPage is invalid.", result.ErrorMessage);
        }

        [Fact]
        public void Validate_should_return_error_when_page_is_less_than_0()
        {
            var request = BuildTestRequest();
            request.Page = -1;

            var result = sut.Validate(request);

            Assert.False(result.IsValid);
            Assert.Equal("Page is invalid.", result.ErrorMessage);
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
        public void Validate_should_pass()
        {
            var request = BuildTestRequest();

            var result = sut.Validate(request);

            Assert.True(result.IsValid);
            Assert.Null(result.ErrorMessage);
        }

        private GetCustomersRequest BuildTestRequest()
        {
            return new GetCustomersRequest()
            {
                FirstName = "test first name",
                LastName = "test last name",
                Email = "test@email.com",
                OrderBy = "id",
                OrderByDirection = "desc",
                Page = 1,
                RecordsPerPage = 2,
            };
        }
    }
}
