using Microsoft.Extensions.Configuration;
using MTest.Data.Entities;
using MTest.Data.Providers;
using Dapper;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MTest.Data.Repositories
{
    //Assumption: There are few classes I will probably move to a separate project/assembly 
    //BUt consider the size and scope of this application/service, I think its fine to keep it this way
    //Having the repository layer is very useful to isolate Data access from other logics and also allows
    //Unit tests to easily mock out data access layer
    public interface ICustomerRepository
    {
        Task<int?> InsertAsync(CustomerEntity customer);
        Task<int> CountAsync(string conditions = "", object parameters = null);
        Task<CustomerEntity> GetAsync(int id);
        Task<IEnumerable<CustomerEntity>> GetListAsync(int pageNumber, int rowsPerPage, string orderBy, string conditions = "", object parameters = null);
    }

    public class CustomerRepository : ICustomerRepository
    {
        IConfiguration _configuration;

        public CustomerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int?> InsertAsync(CustomerEntity customer)
        {
            using (var connection = ConnectionProvider.GetConnection(_configuration))
            {
                var id = await connection.InsertAsync(customer);
                return id;
            }
        }

        public async Task<int> CountAsync(string conditions = "", object parameters = null)
        {
            using (var connection = ConnectionProvider.GetConnection(_configuration))
            {
                var count = await connection.RecordCountAsync<CustomerEntity>(conditions, parameters);
                return count;
            }
        }

        public async Task<CustomerEntity> GetAsync(int id)
        {
            using (var connection = ConnectionProvider.GetConnection(_configuration))
            {
                var customer = await connection.GetAsync<CustomerEntity>(id);
                return customer;
            }
        }

        public async Task<IEnumerable<CustomerEntity>> GetListAsync(int pageNumber, int rowsPerPage, string orderBy, string conditions = "", object parameters = null)
        {
            using (var connection = ConnectionProvider.GetConnection(_configuration))
            {
                var customers = await connection.GetListPagedAsync<CustomerEntity>(pageNumber, rowsPerPage, conditions, orderBy, parameters);
                return customers;
            }
        }

    }
}
