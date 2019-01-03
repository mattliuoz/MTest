using Dapper;
using System;

namespace MTest.Data.Entities
{
    [Table("Customers")]
    public class CustomerEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        
    }
}
