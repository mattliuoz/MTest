using MTest.Models;
using Xunit;

namespace Tests.Models
{
    public class CustomerTests
    {
        [Fact]
        public void Customer_should_return_correct_custCode()
        {

            var sut = new Customer() { FirstName = "fIrsTName", LastName = "lastName", DOB = new System.DateTime(2019, 01, 01), Email = "test@test.com", Id = 1 };
           
            Assert.Equal("firstnamelastname20190101", sut.CustCode);
        }

    }
}
