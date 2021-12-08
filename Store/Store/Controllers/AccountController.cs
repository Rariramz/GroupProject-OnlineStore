using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Data;
using Store.Entities;
using Store.Models;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using Store.Services;
using System.Text.RegularExpressions;

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
        private EmailConfirmation _emailConfirmation;



        public AccountController(ApplicationDbContext context, UserManager<User> userManager, SignInManager<User> signInManager, EmailConfirmation emailConfirmation)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailConfirmation = emailConfirmation;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Info()
        {
            User user = await _userManager.GetUserAsync(User);
            if(user == null)
            {
                return Json(new UserData() { Success = false });
            }

            UserData userData = new UserData()
            {
                Success = true,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Discount = user.Discount
            };

            List<UserAddress> addressesRelations = _context.UserAddresses.Where(address => address.UserID == user.Id).ToList();
            List<AddressData> addresses = new List<AddressData>();

            foreach (UserAddress relation in addressesRelations)
            {
                Address address = _context.Addresses.FirstOrDefault(address => address.ID == relation.AddressID);
                addresses.Add(new AddressData() { AddressString = address.AddressString});
            }

            userData.Addresses = addresses;
            return Json(userData);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm]LoginModel loginModel)
        {
            string email = loginModel.Email;
            string password = loginModel.Password;
            LoginResult loginResult = new LoginResult() { Success = true };

            if (string.IsNullOrEmpty(email))
            {
                loginResult.Success = false;
                loginResult.ErrorCodes.Add(LoginResultConstants.ERROR_EMAIL_EMPTY); // Empty email
            }

            if (string.IsNullOrEmpty(password))
            {
                loginResult.Success = false;
                loginResult.ErrorCodes.Add(LoginResultConstants.ERROR_PASSWORD_EMPTY); // Empty password
            }

            try
            {
                string address = new MailAddress(email).Address;
            }
            catch
            {
                loginResult.Success = false;
                loginResult.ErrorCodes.Add(LoginResultConstants.ERROR_EMAIL_VALIDATION_FAIL); // Bad email
            }

            if (!loginResult.Success)
            {
                return Json(loginResult);
            }

            User user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                loginResult.Success = false;
                loginResult.ErrorCodes.Add(LoginResultConstants.ERROR_EMAIL_NOT_FOUND); // No such user
            }
            else if(!user.EmailConfirmed)
            {
                loginResult.Success = false;
                loginResult.ErrorCodes.Add(443); // Email not confirmed
            }
            else if(!await _userManager.CheckPasswordAsync(user, password))
            {
                loginResult.Success = false;
                loginResult.ErrorCodes.Add(LoginResultConstants.ERROR_PASSWORD_WRONG); // Wrong password
            }
            else
            {
                loginResult.Success = true;
                await _signInManager.SignInAsync(user, true);
            }

            return Json(loginResult);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromForm]RegistrationModel registrationModel)
        {
            string nameRussianRegex = @"^[а-яА-Я ,.'-]+$";
            string nameEnglishRegex = @"^[a-zA-Z ,.'-]+$";
            RegistrationResult registrationResult = new RegistrationResult() { Success = true };
            if (string.IsNullOrEmpty(registrationModel.FirstName))
            {
                registrationResult.ErrorCodes.Add(RegistrationResultConstants.ERROR_FIRSTNAME_EMPTY);
            }
            else if(!Regex.IsMatch(registrationModel.FirstName, nameRussianRegex) &&
                    !Regex.IsMatch(registrationModel.FirstName, nameEnglishRegex))
            {
                registrationResult.ErrorCodes.Add(RegistrationResultConstants.ERROR_FIRSTNAME_VALIDATION_FAIL);
            }
            else if(registrationModel.FirstName.Length > 20)
            {
                registrationResult.ErrorCodes.Add(RegistrationResultConstants.ERROR_FIRSTNAME_TOO_LONG);
            }

            if (string.IsNullOrEmpty(registrationModel.LastName))
            {
                registrationResult.ErrorCodes.Add(RegistrationResultConstants.ERROR_LASTNAME_EMPTY);
            }
            else if (!Regex.IsMatch(registrationModel.LastName, nameRussianRegex) &&
                    !Regex.IsMatch(registrationModel.LastName, nameEnglishRegex))
            {
                registrationResult.ErrorCodes.Add(RegistrationResultConstants.ERROR_LASTNAME_VALIDATION_FAIL);
            }
            else if (registrationModel.LastName.Length > 20)
            {
                registrationResult.ErrorCodes.Add(RegistrationResultConstants.ERROR_LASTNAME_TOO_LONG);
            }

            if (string.IsNullOrEmpty(registrationModel.Email))
            {
                registrationResult.ErrorCodes.Add(RegistrationResultConstants.ERROR_EMAIL_EMPTY);
            }
            else if(!MailAddress.TryCreate(registrationModel.Email, out var _))
            {
                registrationResult.ErrorCodes.Add(RegistrationResultConstants.ERROR_EMAIL_VALIDATION_FAIL);
            }

            bool passwordValidated = true;

            if (string.IsNullOrEmpty(registrationModel.Password))
            {
                registrationResult.ErrorCodes.Add(RegistrationResultConstants.ERROR_PASSWORD_FIELD1_EMPTY);
                passwordValidated = false;
            }
            if (string.IsNullOrEmpty(registrationModel.PasswordConfirm))
            {
                registrationResult.ErrorCodes.Add(RegistrationResultConstants.ERROR_PASSWORD_FIELD2_EMPTY);
                passwordValidated = false;
            }
            if (passwordValidated)
            {
                if(registrationModel.Password != registrationModel.PasswordConfirm)
                {
                    registrationResult.ErrorCodes.Add(RegistrationResultConstants.ERROR_PASSWORD_MATCH_FAIL);
                }
                else if(registrationModel.Password.Length < 8)
                {
                    registrationResult.ErrorCodes.Add(RegistrationResultConstants.ERROR_PASSWORD_TOO_WEAK);
                }
            }

            if(registrationResult.ErrorCodes.Count > 0)
            {
                registrationResult.Success = false;
                return Json(registrationResult);
            }

            User matchingUser = await _userManager.FindByEmailAsync(registrationModel.Email);
            if(matchingUser != null)
            {
                registrationResult.ErrorCodes.Add(RegistrationResultConstants.ERROR_EMAIL_ALREADY_EXISTS);
                registrationResult.Success = false;
                return Json(registrationResult);
            }

            User user = new User()
            {
                FirstName = registrationModel.FirstName,
                LastName = registrationModel.LastName,
                Email = registrationModel.Email,
                UserName = registrationModel.Email,
                EmailConfirmationCode = _emailConfirmation.BeginConfirmation(registrationModel.Email)
            };
            await _userManager.CreateAsync(user, registrationModel.Password);            
            await _context.SaveChangesAsync();

            User createdUser = await _userManager.FindByEmailAsync(registrationModel.Email);
            await _userManager.AddToRoleAsync(createdUser, "user");
            await _context.SaveChangesAsync();

            await _signInManager.SignInAsync(createdUser, true);

            return Json(registrationResult);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Confirm(string email, string code)
        {
            User user = await _userManager.FindByEmailAsync(email);
            if(user != null)
            {
                if(user.EmailConfirmationCode == code)
                {
                    user.EmailConfirmed = true;
                    _context.SaveChanges();
                    return Redirect("/"); //redirect here to success confirmation page
                }
                else
                {
                    return Redirect("/");
                }
            }
            else
            {
                return Redirect("/");
            }
        }
    }
}
