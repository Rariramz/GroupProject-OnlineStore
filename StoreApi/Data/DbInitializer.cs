using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StoreApi.Models;

namespace WEB_953501_MALETS.Data
{
    public class DbInitializer
    {
        public static async Task Seed(ApplicationContext context)

        {
            // создать БД, если она еще не создана
            context.Database.EnsureCreated();
            // проверка наличия ролей
            
            // проверка наличия пользователей
            if (!context.Users.Any())
            {
                // создать пользователя user@mail.ru
                var user = new User
                {
                    Name = "user",
                    Surname = "user",
                    Email = "user@mail.ru",
                    IsAdmin = false
                };
                var admin = new User
                {
                    Name = "admin",
                    Surname = "admin",
                    Email = "admin@mail.ru",
                    IsAdmin = true,
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