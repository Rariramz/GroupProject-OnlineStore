using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Data;
using Store.Entities;
using Store.Models;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;

namespace Store.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "admin, user")]
    public class AccountController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;


        public AccountController(ApplicationDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        public async Task<IActionResult> Info()
        {
            User user = await _userManager.GetUserAsync(User);

            UserData userData = new UserData()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Discount = user.Discount
            };

            List<UserAddress> addressesRelations = _context.UserAddresses.Where(address => address.UserID == user.Id).ToList();
            List<Address> addresses = new List<Address>();

            foreach (UserAddress relation in addressesRelations)
            {
                addresses.Add(relation.Address);
            }

            return Json(userData);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginModel loginModel)
        {
            string email = loginModel.Email;
            string password = loginModel.Password;
            LoginResult loginResult = new LoginResult() { Success = true };

            if (string.IsNullOrEmpty(email))
            {
                loginResult.Success = false;
                loginResult.ErrorCodes.Add(440); // Empty email
            }

            if (string.IsNullOrEmpty(password))
            {
                loginResult.Success = false;
                loginResult.ErrorCodes.Add(444); // Empty password
            }

            try
            {
                string address = new MailAddress(email).Address;
            }
            catch
            {
                loginResult.Success = false;
                loginResult.ErrorCodes.Add(441); // Bad email
            }

            if (!loginResult.Success)
            {
                return Json(loginResult);
            }

            User user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                loginResult.Success = false;
                loginResult.ErrorCodes.Add(442); // No such user
            }
            else if(!user.EmailConfirmed)
            {
                loginResult.Success = false;
                loginResult.ErrorCodes.Add(443); // Email not confirmed
            }
            else if(!await _userManager.CheckPasswordAsync(user, password))
            {
                loginResult.Success = false;
                loginResult.ErrorCodes.Add(445); // Email not confirmed
            }
            else
            {
                loginResult.Success = true;
                await _signInManager.SignInAsync(user, true);
            }

            return Json(loginResult);
        }
    }
}
