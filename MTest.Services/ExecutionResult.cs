namespace MTest.Services
{
    public class ExecutionResult<T>
    {
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public T Result{ get; set; }
    }
}
