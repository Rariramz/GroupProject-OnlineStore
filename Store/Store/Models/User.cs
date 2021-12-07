using Microsoft.AspNetCore.Identity;

namespace Store.Models
{
    public class User : IdentityUser
    {
        public string? EmailConfirmationCode { get; set; }
        public int Discount { get; set; }
    }
}