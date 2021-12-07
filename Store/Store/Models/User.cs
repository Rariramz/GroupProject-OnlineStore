using Microsoft.AspNetCore.Identity;

namespace Store.Models
{
    public class User : IdentityUser
    {
        public string? EmailConfirmationCode { get; set; }
        public int Discount { get; set; }

        public ICollection<Order>? Orders { get; set; }
        public ICollection<UserAddress>? UserAddresses { get; set; }
    }
}