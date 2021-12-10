namespace Store.Models
{
    public class OrderResult
    {
        public bool Success {  get; set; }
        public List<int> ErrorCodes {  get; set; } = new List<int>();
    }

    public static class OrderResultConstants
    {
        
    }

}
