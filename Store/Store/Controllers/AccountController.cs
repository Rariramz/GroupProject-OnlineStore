using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Data;
using Store.Entities;
using Microsoft.AspNetCore.Identity;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin, user")]
    public class AccountController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<User> _userManager;

        public AccountController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<ActionResult> Index()
        {
            //User user = await _userManager.GetUserAsync(User);

            //UserData userData = new UserData() 
            //{ 
            //    FirstName = user.FirstName, 
            //    LastName = user.LastName,
            //    Email = user.Email,
            //    Discount = user.Discount
            //};

            //List<UserAddress> addressesRelations = _context.UserAddresses.Where(address => address.UserID == user.Id).ToList();
            //List<Address> addresses = new List<Address>();

            //foreach(UserAddress relation in addressesRelations)
            //{
            //    Address address = _context.Addresses.First(address => address.ID == )
            //}

            return View();
        }

    }
}
