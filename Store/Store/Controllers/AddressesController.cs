using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Entities;
using Microsoft.AspNetCore.Identity;
using Store.Models;
using System.Text.RegularExpressions;

namespace Store.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AddressesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AddressesController(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        // GET: api/Addresses/GetAddresses
        [HttpGet]
        [Authorize("admin")]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
        {
            return await _context.Addresses.ToListAsync();
        }

        // GET: api/Addresses/GetAddress?id=
        [HttpGet]
        [Authorize("admin")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            var address = await _context.Addresses.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize("admin")]
        public async Task<ActionResult<Address>> PostAddress(Address address)
        {
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddress", new { id = address.ID }, address);
        }

        // DELETE: api/Addresses/DeleteAddress
        [HttpDelete]
        [Authorize("admin")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Addresses/AddressExists?id=
        [HttpGet]
        [Authorize("admin")]
        public async Task<bool> AddressExists(int id)
        {
            return _context.Addresses.Any(e => e.ID == id);
        }

        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> CreateAddressForUser(AddressModel addressModel)
        {
            User requestUser = await _userManager.FindByEmailAsync(User.Identity.Name);
            User targetUser = await _userManager.FindByIdAsync(addressModel.UserID);
            AddressResult addressResult = new AddressResult() { Success = true };

            if (string.IsNullOrEmpty(addressModel.UserID))
            {
                targetUser = await _userManager.FindByIdAsync(requestUser.Id);
            }

            if(targetUser == null )
            {
                addressResult.ErrorCodes.Add(AddressResultConstants.ERROR_USER_INVALID);  
            }
            else if(requestUser.Id != targetUser.Id && !await _userManager.IsInRoleAsync(requestUser, "admin"))
            {
                addressResult.ErrorCodes.Add(AddressResultConstants.ERROR_ACCESS_DENIED);
            }

            if(addressResult.ErrorCodes.Count > 0)
            {
                addressResult.Success = false;
                return Json(addressResult);
            }

            string addresRegex = @"^[a-zA-Zа-яА-Я0-9 -,.]+$";
            if (string.IsNullOrEmpty(addressModel.AddressString))
            {
                addressResult.ErrorCodes.Add(AddressResultConstants.ERROR_ADDRESSSTRING_EMPTY);
            }
            else if(!Regex.IsMatch(addressModel.AddressString, addresRegex))
            {
                addressResult.ErrorCodes.Add(AddressResultConstants.ERROR_ADDRESSSTRING_VALIDATION_FAIL);
            }

            if (addressResult.ErrorCodes.Count > 0)
            {
                addressResult.Success = false;
                return Json(addressResult);
            }

            Address address = new Address() { AddressString = addressModel.AddressString };
            _context.Add(address);
            _context.SaveChanges();

            UserAddress userAddress = new UserAddress() { AddressID = address.ID, UserID = targetUser.Id };
            _context.Add(userAddress);
            _context.SaveChanges();

            return Json(addressResult);
        }
    }
}
