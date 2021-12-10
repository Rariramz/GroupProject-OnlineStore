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
            User user = await _userManager.GetUserAsync(User);

            List<Order> orders = _context.Orders.Where(ord => ord.UserID.Equals(user.Id)).ToList();
            List<OrderData> orderDatas = new List<OrderData>();

            foreach (Order order in orders)
            {
                OrderData orderData = new OrderData()
                {
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

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.ID)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.ID }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.ID == id);
        }
    }
}
