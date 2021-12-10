namespace Store.Models
{
    public class OrderResult
    {
        public bool Success {  get; set; }
        public List<int> ErrorCodes {  get; set; } = new List<int>();
    }

    public static class OrderResultConstants
    {
        public const int ERROR_USER_INVALID = 440;
        public const int ERROR_ORDER_ADDRESS_NOT_EXIST = 441;
        public const int ERROR_ACCESS_DENIED = 442;
        public const int ERROR_ORDER_ITEM_NOT_FOUND = 443;
    }

}
