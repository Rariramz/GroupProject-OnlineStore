namespace Store.Models
{
    public class UserDataModel
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string EmailConfirmationCode { get; set; } = "";
        public int Discount { get; set; } = 0;

        public List<int> UserAddresses { get; set; } = new List<int>();
        public List<int> Orders { get; set; } = new List<int>();
    }
}
