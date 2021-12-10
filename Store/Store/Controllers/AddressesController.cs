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
    [Authorize]
    public class AddressesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public AddressesController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult> GetAddresses()
        {
            User user = await _userManager.FindByEmailAsync(User.Identity.Name);
            List<UserAddress> userAddresses =  _context.UserAddresses.Where(address => address.UserID.Equals(user.Id)).ToList();

            List<AddressData> addresses = new List<AddressData>();

            foreach (UserAddress relation in userAddresses)
            {
                Address? address = _context.Addresses.FirstOrDefault(address => address.ID == relation.AddressID);
                addresses.Add(new AddressData()
                {
                    ID = address.ID,
                    AddressString = address.AddressString
                });
            }

            return Json(addresses);

        }

        [HttpGet]
        public async Task<ActionResult> GetAddress([FromForm] AddressModel addressModel)
        {
            User requestUser = await _userManager.FindByEmailAsync(User.Identity.Name);
            User targetUser = await _userManager.FindByIdAsync(addressModel.UserID);
            AddressResult addressResult = new AddressResult() { Success = true };

            if (string.IsNullOrEmpty(addressModel.UserID))
            {
                targetUser = await _userManager.FindByIdAsync(requestUser.Id);
            }

            if (targetUser == null)
            {
                addressResult.ErrorCodes.Add(AddressResultConstants.ERROR_USER_INVALID);
            }
            else if (requestUser.Id != targetUser.Id && !await _userManager.IsInRoleAsync(requestUser, "admin"))
            {
                addressResult.ErrorCodes.Add(AddressResultConstants.ERROR_ACCESS_DENIED);
            }

            if (addressResult.ErrorCodes.Count > 0)
            {
                addressResult.Success = false;
                return Json(addressResult);
            }

            List<UserAddress> userAddresses = _context.UserAddresses.Where(address => address.UserID.Equals(targetUser.Id)).ToList();

            foreach (UserAddress relation in userAddresses)
            {
                Address? address = _context.Addresses.FirstOrDefault(address => address.ID == relation.AddressID);
                if (address.AddressString.Equals(addressModel.AddressString))
                {
                    AddressData addressData = new AddressData()
                    {
                        AddressString = address.AddressString,
                        ID= address.ID
                    };

                    return Json(addressData);
                }
            }

            addressResult.ErrorCodes.Add(AddressResultConstants.ERROR_ADDRESS_NOT_FOUND);
            addressResult.Success = false;
            return Json(addressResult);
        }

        // DELETE: api/Addresses/DeleteAddress
        [HttpPost]
        public async Task<IActionResult> DeleteAddress([FromForm] AddressModel addressModel)
        {
            User requestUser = await _userManager.FindByEmailAsync(User.Identity.Name);
            User targetUser = await _userManager.FindByIdAsync(addressModel.UserID);
            AddressResult addressResult = new AddressResult() { Success = true };

            if (string.IsNullOrEmpty(addressModel.UserID))
            {
                targetUser = await _userManager.FindByIdAsync(requestUser.Id);
            }

            if (targetUser == null)
            {
                addressResult.ErrorCodes.Add(AddressResultConstants.ERROR_USER_INVALID);
            }
            else if (requestUser.Id != targetUser.Id && !await _userManager.IsInRoleAsync(requestUser, "admin"))
            {
                addressResult.ErrorCodes.Add(AddressResultConstants.ERROR_ACCESS_DENIED);
            }

            if (addressResult.ErrorCodes.Count > 0)
            {
                addressResult.Success = false;
                return Json(addressResult);
            }

            List<UserAddress> userAddresses = _context.UserAddresses.Where(address => address.UserID.Equals(targetUser.Id)).ToList();

            foreach (UserAddress relation in userAddresses)
            {
                Address? address = _context.Addresses.FirstOrDefault(address => address.ID == relation.AddressID);
                if (address.AddressString.Equals(addressModel.AddressString))
                {
                    _context.Addresses.Remove(address);
                    _context.UserAddresses.Remove(relation);
                    await _context.SaveChangesAsync();
                    addressResult.Success = true;
                    return Json(addressResult);
                }
            }

            addressResult.ErrorCodes.Add(AddressResultConstants.ERROR_ADDRESS_NOT_FOUND);
            addressResult.Success = false;
            return Json(addressResult);
            
        }


        [HttpPost]
        public async Task<IActionResult> CreateAddressForUser([FromForm]AddressModel addressModel)
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
