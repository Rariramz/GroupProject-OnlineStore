using Microsoft.EntityFrameworkCore;

namespace StoreApi.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users {  get; set; }
        public DbSet<Item> Items {  get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
        {

        }

    }
}
