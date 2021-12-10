using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Entities;
using Store.Models;
using Store.Services;

namespace Store.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<User> _userManager;
        private readonly EmailConfirmation _emailConfirmation;

        public OrdersController(ApplicationDbContext context, UserManager<User> userManager, EmailConfirmation emailConfirmation)
        {
            _context = context;
            _userManager = userManager;
            _emailConfirmation = emailConfirmation;
        }

        [HttpGet]
        public async Task<IActionResult> MakeOrder(int addressId)
        {
            User user = await _userManager.FindByEmailAsync(User.Identity.Name);
            OrderResult orderResult = new OrderResult() { Success = true };

            List<UserItem> userItems = _context.UserItems.Where(item => item.UserID == user.Id).ToList();
            if(userItems.Count == 0)
            {
                orderResult.ErrorCodes.Add(OrderResultConstants.ERROR_CART_EMPTY);
            }

            UserAddress? userAddress = await _context.UserAddresses.FirstOrDefaultAsync(relation => relation.AddressID == addressId &&
                                                                                            relation.UserID == user.Id);

            if(userAddress == null)
            {
                orderResult.ErrorCodes.Add(OrderResultConstants.ERROR_USER_NO_SUCH_ADDRESS);
            }

            if(orderResult.ErrorCodes.Count > 0)
            {
                orderResult.Success = false;
                return Json(orderResult);
            }

            Address? address = await _context.Addresses.FirstOrDefaultAsync(add => add.ID == userAddress!.AddressID);

            List<OrderItemData> itemDatas = new List<OrderItemData>();
            decimal totalPrice = 0;
            foreach(UserItem relation in userItems)
            {
                Item? item = await _context.Items.FirstOrDefaultAsync(item => item.ID == relation.ItemID);
                ItemData itemData = new ItemData()
                {
                    ID = item.ID,
                    CategoryID = item.CategoryID,
                    Name = item.Name,
                    Price = (float)item.Price,
                    Description = item.Description
                };
                OrderItemData orderItemData = new OrderItemData()
                {
                    ItemData = itemData,
                    Count = relation.Count
                };

                totalPrice += (decimal)(itemData.Price * orderItemData.Count);
                itemDatas.Add(orderItemData);
            }

            OrderData orderData = new OrderData()
            {
                AddressData = new AddressData()
                {
                    ID = address.ID,
                    AddressString = address.AddressString
                },
                InitialDate = DateTime.Now,
                ItemDatas = itemDatas,
                TotalPrice = totalPrice,
            };

            _emailConfirmation.ConfirmOrder(user.Email, orderData);
            orderResult.Success = true;
            orderResult.OrderData = orderData;

            return Json(orderResult);
        }
    }
}
