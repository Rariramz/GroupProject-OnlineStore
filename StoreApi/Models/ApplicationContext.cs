using Microsoft.EntityFrameworkCore;

namespace StoreApi.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users {  get; set; }
        public DbSet<Item> Items {  get; set; }
        public DbSet<Address> Addresses {  get; set; }
        public DbSet<Order> Orders {  get; set; }
        public DbSet<UserAddress> UserAddresses {  get; set; }
        public DbSet<UserItem> UserItems {  get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
        {

        }

    }
}
