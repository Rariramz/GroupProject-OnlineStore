using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Entities;
using System.Data.SqlClient;
using Store.Models;

namespace Store.Controllers
{
    [Route("api/[controller]/[action]")]
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
        public async Task<IActionResult> AddItemForUser([FromForm] CartItemModel cartItemModel)
        {
            User requestUser = await _userManager.FindByEmailAsync(User.Identity.Name);
            User targetUser = await _userManager.FindByIdAsync(cartItemModel.UserID);
            CartItemResult cartItemResult = new CartItemResult() { Success = true };

            if (string.IsNullOrEmpty(cartItemModel.UserID))
            {
                targetUser = await _userManager.FindByIdAsync(requestUser.Id);
            }

            if (targetUser == null)
            {
                cartItemResult.ErrorCodes.Add(UserItemResultConstants.ERROR_USER_INVALID);
            }
            else if (requestUser.Id != targetUser.Id && !await _userManager.IsInRoleAsync(requestUser, "admin"))
            {
                cartItemResult.ErrorCodes.Add(UserItemResultConstants.ERROR_ACCESS_DENIED);
            }

            if (cartItemResult.ErrorCodes.Count > 0)
            {
                cartItemResult.Success = false;
                return Json(cartItemResult);
            }

            if (cartItemModel.Count < 1)
            {
                cartItemResult.ErrorCodes.Add(UserItemResultConstants.ERROR_COUNT_LESS_ONE);
            }

            if (cartItemModel.Count == null)
            {
                cartItemResult.ErrorCodes.Add(UserItemResultConstants.ERROR_COUNT_IS_NULL);
            }

            Item? item = await _context.Items.FirstOrDefaultAsync(item => item.ID == cartItemModel.ItemID);
            if(item == null)
            {
                cartItemResult.ErrorCodes.Add(UserItemResultConstants.ERROR_ITEM_NOT_EXIST);
            }

            if (cartItemResult.ErrorCodes.Count > 0)
            {
                cartItemResult.Success = false;
                return Json(cartItemResult);
            }



            UserItem? userItem = await _context.UserItems.FirstOrDefaultAsync(item => item.ItemID == cartItemModel.ItemID && item.UserID == targetUser.Id);
            if(userItem != null)
            {
                userItem.Count++;
                _context.Entry(userItem).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            else
            {
                userItem = new UserItem()
                {
                    UserID = targetUser!.Id,
                    ItemID = cartItemModel.ItemID,
                    Count = cartItemModel.Count!.Value
                };
                _context.Add(userItem);
                await _context.SaveChangesAsync();
            }        

            return Json(cartItemResult);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeItemCount([FromForm] CartItemModel cartItemModel)
        {
            User requestUser = await _userManager.FindByEmailAsync(User.Identity.Name);
            User targetUser = await _userManager.FindByIdAsync(cartItemModel.UserID);

            CartItemResult userItemResult = new CartItemResult() { Success = true };

            if (string.IsNullOrEmpty(cartItemModel.UserID))
            {
                targetUser = await _userManager.FindByIdAsync(requestUser.Id);
            }

            if (targetUser == null)
            {
                userItemResult.ErrorCodes.Add(CartItemResultConstants.ERROR_USER_INVALID);
            }
            else if (requestUser.Id != targetUser.Id && !await _userManager.IsInRoleAsync(requestUser, "admin"))
            {
                userItemResult.ErrorCodes.Add(CartItemResultConstants.ERROR_ACCESS_DENIED);
            }

            if (userItemResult.ErrorCodes.Count > 0)
            {
                userItemResult.Success = false;
                return Json(userItemResult);
            }

            UserItem? item = _context.UserItems.FirstOrDefault(item => item.ItemID == cartItemModel.ItemID
                                                                && item.UserID == targetUser.Id);

            if (item == null)
            {
                userItemResult.ErrorCodes.Add(UserItemResultConstants.ERROR_CART_ITEM_NOT_FOUND);
            }

            if (cartItemModel.Count == null)
            {
                userItemResult.ErrorCodes.Add(UserItemResultConstants.ERROR_COUNT_IS_NULL);
            }

            if (userItemResult.ErrorCodes.Count > 0)
            {
                userItemResult.Success = false;
                return Json(userItemResult);
            }  

            item!.Count += cartItemModel.Count!.Value;
            if(item.Count < 1)
            {
                userItemResult.ErrorCodes.Add(UserItemResultConstants.ERROR_COUNT_LESS_ONE);
                userItemResult.Success = false;
                return Json(userItemResult);
            }
            _context.Entry(item).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Json(userItemResult);
        }    
  
        [HttpPost]
        public async Task<IActionResult> Remove([FromForm] CartItemModel cartItemModel)
        {
            User requestUser = await _userManager.FindByEmailAsync(User.Identity.Name);
            User targetUser = await _userManager.FindByIdAsync(cartItemModel.UserID);
            CartItemResult cartItemResult = new CartItemResult { Success = true };

            

            if (string.IsNullOrEmpty(cartItemModel.UserID))
            {
                targetUser = await _userManager.FindByIdAsync(requestUser.Id);
            }

            if (targetUser == null)
            {
                cartItemResult.ErrorCodes.Add(UserItemResultConstants.ERROR_USER_INVALID);
            }
            else if (requestUser.Id != targetUser.Id && !await _userManager.IsInRoleAsync(requestUser, "admin"))
            {
                cartItemResult.ErrorCodes.Add(UserItemResultConstants.ERROR_ACCESS_DENIED);
            }

            if (cartItemResult.ErrorCodes.Count > 0)
            {
                cartItemResult.Success = false;
                return Json(cartItemResult);
            }

            UserItem? userItem = await _context.UserItems.FirstOrDefaultAsync(cart => cart.ItemID == cartItemModel.ItemID &&
                                                                                    cart.UserID == targetUser.Id);

            if (userItem == null)
            {
                cartItemResult.ErrorCodes.Add(UserItemResultConstants.ERROR_CART_ITEM_NOT_FOUND);
            }

            if(cartItemResult.ErrorCodes.Count > 0)
            {
                cartItemResult.Success = false;
                return Json(cartItemResult);
            }

            _context.UserItems.Remove(userItem);
            await _context.SaveChangesAsync();
            return Json(cartItemResult);

        }

        [HttpGet]
        public async Task<IActionResult> GetShoppingDetails()
        {
            User user = await _userManager.FindByEmailAsync(User.Identity.Name);
            List<UserItem> itemRelations = _context.UserItems.Where(userItem => userItem.UserID == user.Id).ToList();
            List<CartItemData> cartItems = new List<CartItemData>();

            foreach(UserItem itemRelation in itemRelations)
            {
                cartItems.Add(new CartItemData() { ItemID = itemRelation.ItemID, Count = itemRelation.Count });
            }

            return Json(cartItems);
        }

        [HttpGet]
        public async Task<IActionResult> GetTotal()
        {
            User user = await _userManager.FindByNameAsync(User.Identity.Name);

            List<UserItem> itemRelations = _context.UserItems.Where(userItem => userItem.UserID == user.Id).ToList();
            decimal price = 0;
            foreach (UserItem itemRelation in itemRelations)
            {
                Item item = await _context.Items.FirstOrDefaultAsync(it => it.ID == itemRelation.ItemID);
                price += item.Price * itemRelation.Count;
            }

            return Json(price);
        }
    }
}
