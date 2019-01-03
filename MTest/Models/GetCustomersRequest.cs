namespace MTest.Models
{
    public class GetCustomersRequest
    {
        public string OrderBy { get; set; }
        public string OrderByDirection{ get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Page { get; set; }
        public int RecordsPerPage { get; set; }
    }
}
