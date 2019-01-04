using Moq;
using MTest.Data.Entities;
using MTest.Data.Repositories;
using MTest.Services;
using MTest.Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Services
{
    public class CustomerServiceTests
    {

        [Fact]
        public async Task AddCustomer_should_return_error_when_email_exists()
        {
            var repository = new Mock<ICustomerRepository>();
            repository.Setup(r => r.CountAsync(It.IsAny<string>(), It.IsAny<object>())).Returns(Task.FromResult(1));
            var sut = new CustomerService(repository.Object);
            var customer = new CustomerEntity();

            var result = await sut.AddCustomerAsync(customer);

            Assert.False(result.IsSuccessful);
            Assert.Equal("Email already in use.", result.ErrorMessage);
        }

        [Fact]
        public async Task AddCustomer_should_return_error_when_add_customer_failed()
        {
            var repository = new Mock<ICustomerRepository>();
            repository.Setup(r => r.CountAsync(It.IsAny<string>(), It.IsAny<object>())).Returns(Task.FromResult(0));
            repository.Setup(r => r.InsertAsync(It.IsAny<CustomerEntity>())).Returns(Task.FromResult((int?)null));
            var sut = new CustomerService(repository.Object);
            var customer = new CustomerEntity();

            var result = await sut.AddCustomerAsync(customer);

            Assert.False(result.IsSuccessful);
            Assert.Equal("Add customer failed.", result.ErrorMessage);
        }

        [Fact]
        public async Task AddCustomer_should_return_correct_id()
        {
            var repository = new Mock<ICustomerRepository>();
            repository.Setup(r => r.CountAsync(It.IsAny<string>(), It.IsAny<object>())).Returns(Task.FromResult(0));
            repository.Setup(r => r.InsertAsync(It.IsAny<CustomerEntity>())).Returns(Task.FromResult((int?)1));
            var sut = new CustomerService(repository.Object);
            var customer = new CustomerEntity();

            var result = await sut.AddCustomerAsync(customer);

            Assert.True(result.IsSuccessful);
            Assert.Equal(1, result.Result.Id);
        }

        [Fact]
        public async Task GetCustomer_should_return_error_when_get_customer_from_db_failed()
        {
            var repository = new Mock<ICustomerRepository>();
            repository.Setup(r => r.GetAsync(It.IsAny<int>())).Returns(Task.FromResult((CustomerEntity)null));
            var sut = new CustomerService(repository.Object);

            var result = await sut.GetCustomerAsync(1);

            Assert.False(result.IsSuccessful);
            Assert.Equal("Get customer failed.", result.ErrorMessage);
        }

        [Fact]
        public async Task GetCustomer_should_return_correct_customer()
        {
            var repository = new Mock<ICustomerRepository>();
            var customer = new CustomerEntity()
            {
                Id = 1,
                FirstName = "test",
                LastName = "user",
                DOB = new System.DateTime(2011, 01, 01),
                Email = "test@user.com"
            };
            repository.Setup(r => r.GetAsync(1)).Returns(Task.FromResult(customer));
            var sut = new CustomerService(repository.Object);

            var result = await sut.GetCustomerAsync(1);

            Assert.True(result.IsSuccessful);
            Assert.Equal(customer.Id, result.Result.Id);
            Assert.Equal(customer.FirstName, result.Result.FirstName);
            Assert.Equal(customer.LastName, result.Result.LastName);
            Assert.Equal(customer.DOB, result.Result.DOB);
            Assert.Equal(customer.Email, result.Result.Email);
        }

        [Fact]
        public async Task GetCustomers_should_return_error_when_get_customers_from_db_failed()
        {
            var repository = new Mock<ICustomerRepository>();
            repository.Setup(r => r.GetListAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>())).Returns(Task.FromResult((IEnumerable<CustomerEntity>)null));
            var sut = new CustomerService(repository.Object);
            var filter = new GetCustomersFilter();

            var result = await sut.GetCustomersAsync(filter);

            Assert.False(result.IsSuccessful);
            Assert.Equal("Get customers failed.", result.ErrorMessage);
        }

        [Fact]
        public async Task GetCustomers_should_return_correct_customer()
        {
            var repository = new Mock<ICustomerRepository>();

            var customers = BuildCustomers();
            repository.Setup(r => r.GetListAsync(1, 1, "test desc", It.IsAny<string>(), It.IsAny<object>())).Returns(Task.FromResult(BuildCustomers()));
            var sut = new CustomerService(repository.Object);
            var filter = new GetCustomersFilter() { OrderBy = "test", OrderByDirection = "desc", Page = 1, RecordsPerPage = 1 };
            var result = await sut.GetCustomersAsync(filter);

            Assert.True(result.IsSuccessful);
            Assert.Single(result.Result.Items);
            
            Assert.Equal(customers.First().FirstName, result.Result.Items.First().FirstName);
            Assert.Equal(customers.First().LastName, result.Result.Items.First().LastName);
            Assert.Equal(customers.First().DOB, result.Result.Items.First().DOB);
            Assert.Equal(customers.First().Email, result.Result.Items.First().Email);
        }

        [Fact]
        public async Task GetCustomers_should_return_correct_no_customer_when_no_match_found()
        {
            var repository = new Mock<ICustomerRepository>();

            var customers = BuildCustomers();
            repository.Setup(r => r.GetListAsync(1, 1, "test desc", It.IsAny<string>(), It.IsAny<object>())).Returns(Task.FromResult(BuildCustomers()));
            var sut = new CustomerService(repository.Object);
            var filter = new GetCustomersFilter() { OrderBy = "test_no_match", OrderByDirection = "desc", Page = 1, RecordsPerPage = 1 };
            var result = await sut.GetCustomersAsync(filter);

            Assert.True(result.IsSuccessful);
            Assert.Empty(result.Result.Items);
        }

        private IEnumerable<CustomerEntity> BuildCustomers()
        {
            var customer = new CustomerEntity()
            {
                Id = 1,
                FirstName = "test",
                LastName = "user",
                DOB = new System.DateTime(2011, 01, 01),
                Email = "test@user.com"
            };
            var customers = new List<CustomerEntity> { customer };
            return customers;
        }
    }
}
