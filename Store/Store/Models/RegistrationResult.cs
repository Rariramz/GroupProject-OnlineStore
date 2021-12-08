namespace Store.Models
{
    public class RegistrationResult
    {
        public bool Success { get; set; }
        public List<int> ErrorCodes { get; set; } = new List<int> { };
    }
    public static class RegistrationResultConstants
    {
        public const int ERROR_FIRSTNAME_EMPTY = 440;
        public const int ERROR_FIRSTNAME_TOO_LONG = 441;
        public const int ERROR_FIRSTNAME_VALIDATION_FAIL = 442;
        public const int ERROR_LASTNAME_EMPTY = 443;
        public const int ERROR_LASTNAME_TOO_LONG = 444;
        public const int ERROR_LASTNAME_VALIDATION_FAIL = 445;
        public const int ERROR_EMAIL_EMPTY = 446;
        public const int ERROR_EMAIL_ALREADY_EXISTS = 447;
        public const int ERROR_EMAIL_VALIDATION_FAIL = 448;
        public const int ERROR_PASSWORD_TOO_WEAK = 449;
        public const int ERROR_PASSWORD_FIELD1_EMPTY = 450;
        public const int ERROR_PASSWORD_FIELD2_EMPTY = 451;
        public const int ERROR_PASSWORD_MATCH_FAIL = 452;
       
    }
}
