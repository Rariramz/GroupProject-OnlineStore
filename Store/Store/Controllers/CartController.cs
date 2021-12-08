using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.Data;
using Store.Entities;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<User> _userManager;
        public CartController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Add(int id)
        {
            User user = await _userManager.GetUserAsync(User);
            var item = _context.Items.Find(id);
            if (item == null)
            {
                return BadRequest();
            }
            List<UserItem> itemRelations = _context.UserItems.Where(userItem => userItem.UserID == user.Id).ToList();

            var userItem = itemRelations.SingleOrDefault(userItem => userItem.ItemID == id);
            if (userItem == null)
            {
                userItem = new UserItem {
                    UserID = user.Id,
                    ItemID = id,
                    Count = 1,
                    User = user,
                    Item = item};

                _context.UserItems.Add(userItem);
            }
            else
            {
                userItem.Count += 1;

            }
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Add), null, userItem);
        }

        /// <summary>
        /// Удалить useritem
        /// </summary>
        /// <param name="id">id удаляемого useritem</param>      
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var userItem = _context.UserItems.Find(id);
            if (userItem != null)
            {
                _context.UserItems.Remove(userItem);
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction(nameof(Remove), null, userItem);

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetShoppingDetails()
        {

            User user = await _userManager.GetUserAsync(User);
            List<UserItem> itemRelations = _context.UserItems.Where(userItem => userItem.UserID == user.Id).ToList();
            return Json(itemRelations);
        }

        [HttpGet]
        public async Task<IActionResult> GetTotal()
        {
            User user = await _userManager.GetUserAsync(User);
            
            decimal? total = decimal.Zero;
            total = (decimal?)(from cartItems in _context.UserItems
                               where cartItems.UserID == user.Id
                               select cartItems.Count *
                               cartItems.Item.Price).Sum();
            return Json(total ?? decimal.Zero);
        }


    }
}
