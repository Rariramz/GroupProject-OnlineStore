namespace Store.Models
{
    public class ItemResult
    {
        public bool Success { get; set; }
        public List<int> ErrorCodes { get; set; } = new List<int> { };
    }

    public class ItemResultConstants
    {
        public const int ERROR_NAME_EMPTY = 440;
        public const int ERROR_NAME_TOO_LONG = 441;
        public const int ERROR_NAME_VALIDATION_FAIL = 442;
        public const int ERROR_DESCRIPTION_EMPTY = 443;
        public const int ERROR_DESCRIPTION_TOO_LONG = 444;
        public const int ERROR_DESCRIPTION_VALIDATION_FAIL = 445;
        public const int ERROR_IMAGE = 446;
        public const int ERROR_CATEGORY_NOT_EXISTS = 447;
        public const int ERROR_PRICE_VALUE = 448;
    }
}
