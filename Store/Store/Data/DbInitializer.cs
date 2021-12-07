using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Store.Entities;

namespace Store.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DbInitializer(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            // создать БД, если она еще не создана
            _context.Database.EnsureCreated();
            // проверка наличия ролей
            if (!_context.Roles.Any())
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

                await _roleManager.CreateAsync(roleAdmin);
                await _roleManager.CreateAsync(roleUser);
                await _context.SaveChangesAsync();
            }



            // проверка наличия пользователей
            if (!_context.Users.Any())
            {
                // создать пользователя user@mail.ru
                var user = new User
                {
                    UserName = "user@mail.ru",
                    Email = "user@mail.ru",
                };
                var admin = new User
                {
                    UserName = "admin@mail.ru",
                    Email = "admin@mail.ru",
                };

                user.EmailConfirmed = true;
                admin.EmailConfirmed = true;

                await _userManager.CreateAsync(user, "123");
                await _userManager.AddToRoleAsync(user, "user");
                await _userManager.CreateAsync(admin, "123");
                await _userManager.AddToRoleAsync(admin, "admin");

                await _context.SaveChangesAsync();
            }

            if (!_context.Categories.Any())
            {
                Category rootCategory = new Category
                {
                    Name = "Свечи обычные",
                    Description = "Свечи обычные описание",
                    ParentID = null 
                };


                _context.Categories.Add(rootCategory);

                await _context.SaveChangesAsync();

                _context.Items.AddRange(
                    new List<Item>
                    {
                        new Item
                        {
                            Name = "Свеча длинная",
                            Description = "Очень красивая длинная свеча, пригодится для долгих праздников",
                            Image = "long_candle.jpg",
                            CategoryID = rootCategory.ID
                        },
                        new Item
                        {
                            Name = "Свеча толстая",
                            Description = "Очень красивая толстая свеча",
                            Image = "fat_candle.jpg",
                            CategoryID = rootCategory.ID
                        },
                        new Item
                        {
                            Name = "Свеча фигурная",
                            Description = "Свеча в виде фигуры",
                            Image = "figure_candle.jpg",
                            CategoryID = rootCategory.ID
                        },
                    });
                await _context.SaveChangesAsync();
            }

        }
    }
}