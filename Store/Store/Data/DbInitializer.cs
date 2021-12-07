using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Store.Entities;

namespace Store.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext context;
        public DbInitializer(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task Seed()

        {
            // создать БД, если она еще не создана
            context.Database.EnsureCreated();
            // проверка наличия ролей
            if (!context.Roles.Any())
            {
                var roleAdmin = new IdentityRole
                {
                    Name = "admin",
                    NormalizedName = "admin"
                };

                var roleUser = new IdentityRole
                {
                    Name = "user",
                    NormalizedName = "user"
                };
                // создать роль admin

                context.Roles.Add(roleAdmin);
                context.Roles.Add(roleUser);
                await context.SaveChangesAsync();
            }

            // проверка наличия пользователей
            if (!context.Users.Any())
            {
                // создать пользователя user@mail.ru
                var user = new User
                {
                    UserName = "user",
                    Email = "user@mail.ru",
                };
                var admin = new User
                {
                    UserName = "admin",
                    Email = "admin@mail.ru",
                };

                context.Users.Add(user);
                context.Users.Add(admin);

                await context.SaveChangesAsync();
            }

            if (!context.Items.Any())
            {
                context.Items.AddRange(
                    new List<Item>
                    {
                        new Item
                        {
                            Name = "Свеча длинная",
                            Description = "Очень красивая длинная свеча, пригодится для долгих праздников",
                            Image = "long_candle.jpg",
                        },
                        new Item
                        {
                            Name = "Свеча толстая",
                            Description = "Очень красивая толстая свеча",
                            Image = "fat_candle.jpg",
                        },
                        new Item
                        {
                            Name = "Свеча фигурная",
                            Description = "Свеча в виде фигуры",
                            Image = "figure_candle.jpg",
                        },
                    });
                await context.SaveChangesAsync();
            }

        }
    }
}