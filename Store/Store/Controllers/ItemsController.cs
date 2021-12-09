using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Entities;
using Store.Data;
using Microsoft.AspNetCore.Authorization;
using Store.Models;
using System.Text.RegularExpressions;
using Store.Tools;
using Store.Extensions;

namespace Store.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Items/GetItems
        [HttpGet]
        public async Task<ActionResult> GetItems()
        {
            
            List<Item> items = await _context.Items.ToListAsync();
            List<ItemData> itemDatas = new List<ItemData>();

            foreach(Item item in items)
            {
                itemDatas.Add(new ItemData
                {
                    ID = item.ID,
                    Name = item.Name,
                    Description = item.Description,
                    CategoryID = item.CategoryID,
                });
            }

            return Json(itemDatas);
        }

        // GET: api/Items/GetPopularItems?count=
        [HttpGet]
        public async Task<ActionResult> GetPopularItems(int count)
        {
            count = Math.Max(count, 0);
            List<Item> items = await _context.Items.ToListAsync();
            List<ItemData> itemDatas = new List<ItemData>();

            foreach (Item item in items)
            {
                itemDatas.Add(new ItemData
                {
                    ID = item.ID,
                    Name = item.Name,
                    Description = item.Description,
                    CategoryID = item.CategoryID,
                });
            }

            TimeSpan time = DateTime.Now.TimeOfDay;

            Random rnd = new Random((int)time.TotalMinutes);
            itemDatas.Shuffle(rnd);
            List<ItemData> popularItems = itemDatas.GetRange(0, Math.Min(count, itemDatas.Count));

            return Json(popularItems);
        }


        // GET: api/Items/GetItem?id=
        [HttpGet]
        public async Task<ActionResult> GetItem(int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NoContent();
            }

            return Json(new ItemData()
            {
                ID = item.ID,
                Name = item.Name,
                Description = item.Description,
                CategoryID = item.CategoryID,
                Price = (float)item.Price,
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetImage(int id)
        {
            Item? item = _context.Items.FirstOrDefault(item => item.ID == id);
            

            if(item == null)
            {
                return NoContent();
            }

            Image? image = _context.Images.FirstOrDefault(image => image.ID == item.ImageID);
            if (image == null)
            {
                return NoContent();
            }

            return File(ImageConverter.Base64ToImage(image.ImageData), "image/png");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> CreateItem([FromForm] ItemModel itemModel)
        {
            ItemResult itemResult = new ItemResult() { Success = true };
            string nameRegex = @"^[a-zA-Zа-яА-Я .-:]+$";

            if (string.IsNullOrEmpty(itemModel.Name))
            {
                itemResult.ErrorCodes.Add(ItemResultConstants.ERROR_NAME_EMPTY);
            }
            else if(!Regex.IsMatch(itemModel.Name, nameRegex))
            {
                itemResult.ErrorCodes.Add(ItemResultConstants.ERROR_NAME_VALIDATION_FAIL);
            }
            else if(itemModel.Name.Length > 50)
            {
                itemResult.ErrorCodes.Add(ItemResultConstants.ERROR_NAME_TOO_LONG);
            }

            if (string.IsNullOrEmpty(itemModel.Description))
            {
                itemResult.ErrorCodes.Add(ItemResultConstants.ERROR_DESCRIPTION_EMPTY);
            }
            else if (!Regex.IsMatch(itemModel.Description, nameRegex))
            {
                itemResult.ErrorCodes.Add(ItemResultConstants.ERROR_DESCRIPTION_VALIDATION_FAIL);
            }
            else if (itemModel.Description.Length > 500)
            {
                itemResult.ErrorCodes.Add(ItemResultConstants.ERROR_DESCRIPTION_TOO_LONG);
            }

            string base64Image = ImageConverter.ImageToBase64(itemModel.Image);
            if(base64Image == "")
            {
                itemResult.ErrorCodes.Add(ItemResultConstants.ERROR_IMAGE);
            }

            Category? category = _context.Categories.FirstOrDefault(category => category.ID == itemModel.CategoryID);
            if(category == null)
            {
                itemResult.ErrorCodes.Add(ItemResultConstants.ERROR_CATEGORY_NOT_EXISTS);
            }

            List<Category> subcategories = _context.Categories.Where(cat => cat.ParentID == category.ID).ToList();
            if(subcategories.Count > 0)
            {
                itemResult.ErrorCodes.Add(ItemResultConstants.ERROR_CATEGORY_CHILD_CONFLICT);
            }

            if(itemModel.Price <= 0)
            {
                itemResult.ErrorCodes.Add(ItemResultConstants.ERROR_PRICE_VALUE);
            }

            if(itemResult.ErrorCodes.Count > 0)
            {
                itemResult.Success = false;
                return Json(itemResult);
            }

            Image image = new Image() { ImageData = base64Image };
            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            Item item = new Item()
            {
                Name = itemModel.Name,
                Description = itemModel.Description,
                CategoryID = itemModel.CategoryID,
                Price = ((decimal)itemModel.Price),
                ImageID = image.ID
            };

            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return Json(itemResult);
        }
    }
}
