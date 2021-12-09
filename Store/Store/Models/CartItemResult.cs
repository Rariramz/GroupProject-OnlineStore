namespace Store.Models
{
    public class CartItemResult
    {
        public bool Success { get; set; }
        public List<int> ErrorCodes { get; set; } = new List<int> { };
    }
    public static class UserItemResultConstants
    {
        public const int ERROR_USER_INVALID = 440;
        public const int ERROR_COUNT_LESS_ONE = 441;
        public const int ERROR_ACCESS_DENIED = 442;
        public const int ERROR_CART_ITEM_NOT_FOUND = 443;
    }
}
