using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Entities;
using Store.Models;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<User> _userManager;

        public OrdersController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Orders/5
        [HttpGet]
        public async Task<ActionResult<Order>> GetOrders()
        {
            User user = await _userManager.FindByEmailAsync(User.Identity.Name);

            List<Order> orders = _context.Orders.Where(ord => ord.UserID.Equals(user.Id)).ToList();
            List<OrderData> orderDatas = new List<OrderData>();

            foreach (Order order in orders)
            {
                OrderData orderData = new OrderData()
                {
                    ID = order.ID,
                    Description = order.Description,
                    TotalPrice = order.TotalPrice,
                    InitialDate = order.InitialDate,
                    DeliveryDate = order.DeliveryDate,
                    IsDelivery = order.IsDelivery
                };
                string? address = (from add in _context.Addresses
                                 where add.ID == order.AddressID
                                 select add.AddressString).ToList().FirstOrDefault();
                if (address == null)
                {
                    return NoContent();
                }

                orderData.Address = address;
                orderDatas.Add(orderData);
            }

            return Json(orderDatas);

        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostOrder([FromForm]OrderModel orderModel)
        {
            User requestUser = await _userManager.FindByEmailAsync(User.Identity.Name);
            User targetUser = await _userManager.FindByIdAsync(orderModel.UserID);
            OrderResult orderResult = new OrderResult() { Success = true };

            if (string.IsNullOrEmpty(orderModel.UserID))
            {
                targetUser = await _userManager.FindByIdAsync(requestUser.Id);
            }

            if (targetUser == null)
            {
                orderResult.ErrorCodes.Add(OrderResultConstants.ERROR_USER_INVALID);
            }
            else if (requestUser.Id != targetUser.Id && !await _userManager.IsInRoleAsync(requestUser, "admin"))
            {
                orderResult.ErrorCodes.Add(OrderResultConstants.ERROR_ACCESS_DENIED);
            }

            if (orderResult.ErrorCodes.Count > 0)
            {
                orderResult.Success = false;
                return Json(orderResult);
            }
            Address? address = await _context.Addresses.FirstOrDefaultAsync(addr => addr.ID == orderModel.AddressID);

            if (address == null)
            {
                orderResult.ErrorCodes.Add(OrderResultConstants.ERROR_ORDER_ADDRESS_NOT_EXIST);
            }

            if (orderResult.ErrorCodes.Count > 0)
            {
                orderResult.Success = false;
                return Json(orderResult);
            }



           Order order = new Order()
           {
               Description = orderModel.Description,
               TotalPrice = orderModel.TotalPrice,
               UserID = orderModel.UserID,
               AddressID = orderModel.AddressID,
               InitialDate = orderModel.InitialDate,
               DeliveryDate = orderModel.DeliveryDate,
               IsDelivery = orderModel.IsDelivery
           };

            _context.Add(order);
            return Json(orderResult);

        }
    }
}
