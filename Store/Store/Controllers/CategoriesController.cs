using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Entities;
using Store.Models;
using Store.Tools;

namespace Store.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            List<CategoryData> categoriesData = new List<CategoryData>();
            foreach (Category category in categories)
            {
                CategoryData categoryData = new CategoryData
                {
                    ID = category.ID,
                    Name = category.Name,
                    Description = category.Description,
                    ParentID = category.ParentID,
                };

                categoryData.ItemsIDs = GetCategoryItems(category.ID);
                categoryData.ChildCategoriesIDs = GetCategoryChilds(category.ID);

                categoriesData.Add(categoryData);
            }

            return Json(categoriesData);
        }

        // GET: api/Categories?id=
        [HttpGet]
        public async Task<ActionResult> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NoContent();
            }

            CategoryData categoryData = new CategoryData
            {
                ID = category.ID,
                Name = category.Name,
                Description = category.Description,
                ParentID = category.ParentID,
            };

            categoryData.ItemsIDs = GetCategoryItems(id);
            categoryData.ChildCategoriesIDs = GetCategoryChilds(id);

            return Json(categoryData);
        }

        [HttpGet]
        public async Task<IActionResult> GetImage(int id)
        {
            Category? category = _context.Categories.FirstOrDefault(category => category.ID == id);

            if (category == null)
            {
                return NoContent();
            }

            Image? image = _context.Images.FirstOrDefault(image => image.ID == category.ImageID);
            if (image == null)
            {
                return NoContent();
            }

            return File(ImageConverter.Base64ToImage(image.ImageData), "image/png");
        }

        [HttpGet]
        public async Task<IActionResult> GetImageBase64(int id)
        {
            Category? category = _context.Categories.FirstOrDefault(category => category.ID == id);

            if (category == null)
            {
                return NoContent();
            }

            Image? image = _context.Images.FirstOrDefault(image => image.ID == category.ImageID);
            if (image == null)
            {
                return NoContent();
            }

            return Json(image.ImageData);
        }

        [HttpGet]
        public async Task<IActionResult> GetInsideImage(int id)
        {
            Category? category = _context.Categories.FirstOrDefault(category => category.ID == id);
            if (category == null)
            {
                return NoContent();
            }

            Image? image = _context.Images.FirstOrDefault(image => image.ID == category.InsideImageID);
            if (image == null)
            {
                return NoContent();
            }

            return File(ImageConverter.Base64ToImage(image.ImageData), "image/png");
        }

        public async Task<IActionResult> GetInsideImageBase64(int id)
        {
            Category? category = _context.Categories.FirstOrDefault(category => category.ID == id);
            if (category == null)
            {
                return NoContent();
            }

            Image? image = _context.Images.FirstOrDefault(image => image.ID == category.InsideImageID);
            if (image == null)
            {
                return NoContent();
            }

            return Json(image.ImageData);
        }

        // POST: api/Categories/CreateCategory
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> CreateCategory([FromForm]CategoryModel categoryModel)
        {
            CategoryResult categoryResult = new CategoryResult() { Success = true };

            Category? parentCategory = await _context.Categories.FirstOrDefaultAsync(category => category.ID == categoryModel.ParentID);

            if(parentCategory == null)
            {
                categoryResult.ErrorCodes.Add(CategoryResultConstants.ERROR_PARENT_NOT_EXISTS);
            }
            else
            {
                List<Item> items = _context.Items.Where(item => item.CategoryID == parentCategory.ID).ToList();
                if (items.Count > 0)
                {
                    categoryResult.ErrorCodes.Add(CategoryResultConstants.ERROR_CHILD_CONFLICT);
                }
            }

            string nameRegex = @"^[а-яА-Яa-zA-Z -]+$";
            string descriptionRegex = @"^[а-яА-Яa-zA-Z0-9 -.,:]+$";

            if (string.IsNullOrEmpty(categoryModel.Name))
            {
                categoryResult.ErrorCodes.Add(CategoryResultConstants.ERROR_NAME_EMPTY);
            }
            else if(!Regex.IsMatch(categoryModel.Name, nameRegex))
            {
                categoryResult.ErrorCodes.Add(CategoryResultConstants.ERROR_NAME_VALIDATION_FAIL);
            }
            else if(categoryModel.Name.Length > 50)
            {
                categoryResult.ErrorCodes.Add(CategoryResultConstants.ERROR_NAME_TOO_LONG);
            }

            if (string.IsNullOrEmpty(categoryModel.Description))
            {
                categoryResult.ErrorCodes.Add(CategoryResultConstants.ERROR_DESCRIPTION_EMPTY);
            }
            else if (!Regex.IsMatch(categoryModel.Description, descriptionRegex))
            {
                categoryResult.ErrorCodes.Add(CategoryResultConstants.ERROR_DESCRIPTION_VALIDATION_FAIL);
            }
            else if (categoryModel.Description.Length > 500)
            {
                categoryResult.ErrorCodes.Add(CategoryResultConstants.ERROR_DESCRIPTION_TOO_LONG);
            }

            string image1 = ImageConverter.ImageToBase64(categoryModel.Image);
            string image2 = ImageConverter.ImageToBase64(categoryModel.InsideImage);

            if(image1 == "")
            {
                categoryResult.ErrorCodes.Add(CategoryResultConstants.ERROR_IMAGE_1);
            }
            if (image2 == "")
            {
                categoryResult.ErrorCodes.Add(CategoryResultConstants.ERROR_IMAGE_2);
            }

            if (categoryResult.ErrorCodes.Count > 0)
            {
                categoryResult.Success = false;
                return Json(categoryResult);
            }

            Image image = new Image() { ImageData = image1 };
            Image insideImage = new Image() { ImageData = image2 };
            _context.Images.Add(image);
            _context.Images.Add(insideImage);
            _context.SaveChanges();

            Category category = new Category()
            {
                Name = categoryModel.Name,
                Description = categoryModel.Description,
                ParentID = categoryModel.ParentID,
                ImageID = image.ID,
                InsideImageID = insideImage.ID,
            };

            _context.Categories.Add(category);
            _context.SaveChanges();

            CategoryData categoryData = new CategoryData()
            {
                ID = category.ID,
                Name = category.Name,
                Description = category.Description,
                ParentID = category.ParentID,
            };

            categoryResult.CategoryData = categoryData;
            
            return Json(categoryResult);
        }

        // DELETE: api/Categories/DeleteCategory/5
        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.ID == id);
        }

        private List<int> GetCategoryItems(int id)
        {
            return (from item in _context.Items
             where item.CategoryID == id
             select item.ID).ToList();
        }

        private List<int> GetCategoryChilds(int id)
        {
            return (from cat in _context.Categories
                    where cat.ParentID == id
                    select cat.ID).ToList();
        }
    }
}
