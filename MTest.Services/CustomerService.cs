using MTest.Data.Entities;
using MTest.Infra;
using MTest.Data.Repositories;
using MTest.Services.Contracts;
using System.Threading.Tasks;

namespace MTest.Services
{
    public interface ICustomerService
    {
        Task<ExecutionResult<CustomerEntity>> AddCustomerAsync(CustomerEntity customer);
        Task<ExecutionResult<CustomerEntity>> GetCustomerAsync(int id);
        Task<ExecutionResult<PagedResult<CustomerEntity>>> GetCustomersAsync(GetCustomersFilter filter);

    }

    public class CustomerService : ICustomerService
    {
        ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ExecutionResult<CustomerEntity>> AddCustomerAsync(CustomerEntity customer)
        {
            //Assumption:Customer Email has to be Unique
            var result = new ExecutionResult<CustomerEntity>();

            var count = await _customerRepository.CountAsync("Where Email = @Email", new { Email = customer.Email });
            if (count > 0)
            {
                result.IsSuccessful = false;
                result.ErrorMessage = "Email already in use.";
                return result;
            }

            var id = await _customerRepository.InsertAsync(customer);

            if (!id.HasValue)
            {
                result.IsSuccessful = false;
                result.ErrorMessage = "Add customer failed.";
                return result;
            }
            customer.Id = id.Value;


            result.IsSuccessful = true;
            result.Result = customer;

            return result;
        }

        public async Task<ExecutionResult<CustomerEntity>> GetCustomerAsync(int id)
        {
            var result = new ExecutionResult<CustomerEntity>();
            var customer = await _customerRepository.GetAsync(id);

            if (customer == null)
            {
                result.IsSuccessful = false;
                result.ErrorMessage = "Get customer failed.";
                return result;
            }

            result.IsSuccessful = true;
            result.Result = customer;

            return result;
        }
      
        //This function supports pagination, and it will get first 5 records of first page by default.
        public async Task<ExecutionResult<PagedResult<CustomerEntity>>> GetCustomersAsync(GetCustomersFilter filter)
        {
            var result = new ExecutionResult<PagedResult<CustomerEntity>>();
            var query = "";
            var firstName = "";
            var lastName = "";
            var email = "";

            var page = 1;
            var recordsPerpage = 5;
            var orderBy = "dob, lastName";
            var orderByDirection = "asc";

            if (!string.IsNullOrWhiteSpace(filter.FirstName))
            {
                query = " FirstName=@FirstName";
                firstName = filter.FirstName;
            }

            if (!string.IsNullOrWhiteSpace(filter.LastName))
            {
                query += " LastName=@LastName";
                lastName = filter.LastName;
            }

            if (!string.IsNullOrWhiteSpace(filter.Email))
            {
                query += " Email=@Email";
                email = filter.Email;
            }

            if (filter.Page>0)
            {
                page = filter.Page;
            }

            if (filter.RecordsPerPage>0)
            {
                recordsPerpage = filter.RecordsPerPage;
            }

            if (!string.IsNullOrWhiteSpace(filter.OrderBy))
            {
                orderBy = filter.OrderBy;
            }

            if (!string.IsNullOrWhiteSpace(filter.OrderByDirection))
            {
                orderByDirection = filter.OrderByDirection;
            }

            if (!string.IsNullOrWhiteSpace(query))
            {
                query = $"where {query}";
            }

            var parameters = new { firstName, lastName, email };

            var customers = await _customerRepository.GetListAsync(page, recordsPerpage, $"{orderBy} {orderByDirection}", query, parameters);
            if (customers == null)
            {
                result.IsSuccessful = false;
                result.ErrorMessage = "Get customers failed.";
                return result;
            }
            var totalCount = await _customerRepository.CountAsync();

            result.IsSuccessful = true;
            var collectionResult = new PagedResult<CustomerEntity>();
            collectionResult.TotalCount = totalCount;
            collectionResult.Page = page;
            collectionResult.RecordsPerPage = recordsPerpage;
            collectionResult.Items = customers;
            result.Result = collectionResult;

            return result;
        }
    }
}
