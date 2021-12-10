namespace Store.Models
{
    public class CategoryResult
    {
        public bool Success { get; set; }
        public List<int> ErrorCodes { get; set; } = new List<int> { };
        public CategoryData? CategoryData { get; set; }
    }

    public static class CategoryResultConstants
    {
        public const int ERROR_PARENT_NOT_EXISTS = 440;
        public const int ERROR_PARENT_INVALID = 441;
        public const int ERROR_CHILD_CONFLICT = 442;
        public const int ERROR_NAME_EMPTY = 443;
        public const int ERROR_NAME_VALIDATION_FAIL = 444;
        public const int ERROR_NAME_TOO_LONG = 445;
        public const int ERROR_DESCRIPTION_EMPTY = 446;
        public const int ERROR_DESCRIPTION_VALIDATION_FAIL = 447;
        public const int ERROR_DESCRIPTION_TOO_LONG = 448;
        public const int ERROR_IMAGE_1 = 449;
        public const int ERROR_IMAGE_2 = 450;
    }
}
