using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Store.Entities;
using Store.Tools;

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
                Image imageMain = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\Все_свечи.jpg") };
                Image insideImageMain = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\все_свечи_широкая.jpg") };

                _context.Images.Add(imageMain);
                _context.Images.Add(insideImageMain);
                _context.SaveChanges();

                Category rootCategory = new Category
                {
                    Name = "Свечи обычные",
                    Description = "Свечи обычные описание",
                    ImageID = imageMain.ID,
                    InsideImageID = insideImageMain.ID,
                    ParentID = null 
                };

                _context.Categories.Add(rootCategory);


                await _context.SaveChangesAsync();

                Category subCategory = new Category
                {
                    Name = "Свечи обычные но не совсем",
                    Description = "Свечи не совсем обычные описание",
                    ImageID = imageMain.ID,
                    InsideImageID = insideImageMain.ID,
                    ParentID = rootCategory.ID
                };

                _context.Categories.Add(subCategory);
                await _context.SaveChangesAsync();

                Image longCandle = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\свеча_длинная.jpg") };
                Image fatCandle = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\свеча_толстая.jpg") };
                Image coolCandle = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\свеча_в_виде_фигуры.jpg") };

                _context.Images.Add(longCandle);
                _context.Images.Add(fatCandle);
                _context.Images.Add(coolCandle);
                _context.SaveChanges();

                _context.Items.AddRange(
                    new List<Item>
                    {
                        new Item
                        {
                            Name = "Свеча длинная",
                            Description = "Очень красивая длинная свеча, пригодится для долгих праздников",
                            ImageID = longCandle.ID,
                            CategoryID = subCategory.ID
                        },
                        new Item
                        {
                            Name = "Свеча толстая",
                            Description = "Очень красивая толстая свеча",
                            ImageID = fatCandle.ID,
                            CategoryID = subCategory.ID
                        },
                        new Item
                        {
                            Name = "Свеча фигурная",
                            Description = "Свеча в виде фигуры",
                            ImageID = coolCandle.ID,
                            CategoryID = subCategory.ID
                        },
                    });
                await _context.SaveChangesAsync();
            }

        }
    }
}