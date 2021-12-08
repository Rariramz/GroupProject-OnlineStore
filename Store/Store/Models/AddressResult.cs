namespace Store.Models
{
    public class AddressResult
    {
        public bool Success { get; set; }
        public List<int> ErrorCodes { get; set; } = new List<int> { };
    }

    public static class AddressResultConstants
    {
        public const int ERROR_USER_INVALID = 440;
        public const int ERROR_ADDRESSSTRING_EMPTY = 441;
        public const int ERROR_ADDRESSSTRING_VALIDATION_FAIL = 442;
        public const int ERROR_ACCESS_DENIED = 443;
    }
}
