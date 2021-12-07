namespace Store.Models
{
    public class UserData
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public int Discount { get; set; } = 0;
        public List<Address> Addresses { get; set; } = new List<Address>();

    }
}
