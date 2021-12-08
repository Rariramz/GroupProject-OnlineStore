namespace Store.Models
{
    public class CategoryResult
    {
        public bool Success { get; set; }
        public List<int> ErrorCodes { get; set; } = new List<int> { };
    }

    public static class CategoryResultConstants
    {
        public const int ERROR_ROOT_ALREADY_EXISTS = 440;
        public const int ERROR_PARENT_INVALID = 441;
        public const int ERROR_CHILD_CONFLICT = 442;
    }
}
