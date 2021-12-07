namespace Store.Models
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public List<int> ErrorCodes { get; set; } = new List<int> { };
    }
}
