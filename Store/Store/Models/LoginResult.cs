namespace Store.Models
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public List<int> ErrorCodes { get; set; } = new List<int> { };
    }

    public static class LoginResultConstants
    {
        public const int ERROR_EMAIL_EMPTY = 440;
        public const int ERROR_EMAIL_VALIDATION_FAIL = 441;
        public const int ERROR_EMAIL_NOT_FOUND = 442;
        public const int ERROR_EMAIL_NOT_CONFIRMED = 443;
        public const int ERROR_PASSWORD_EMPTY = 444;
        public const int ERROR_PASSWORD_WRONG = 445;
    }
}
