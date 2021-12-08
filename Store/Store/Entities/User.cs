using Microsoft.AspNetCore.Identity;

namespace Store.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string EmailConfirmationCode { get; set; } = "";
        public int Discount { get; set; } = 0;
    }
}