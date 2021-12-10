namespace Store.Models
{
    public class OrderResult
    {
        public bool Success {  get; set; }
        public List<int> ErrorCodes {  get; set; } = new List<int>();
        public OrderData? OrderData { get; set; }
    }

    public static class OrderResultConstants
    {
        public const int ERROR_CART_EMPTY = 440;
        public const int ERROR_USER_NO_SUCH_ADDRESS = 441;
    }

}
