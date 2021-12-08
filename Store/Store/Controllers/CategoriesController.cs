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
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        // GET: api/Categories?id=
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            CategoryData categoryData = new CategoryData
            {
                Name = category.Name,
                Description = category.Description,
                ParentID = category.ParentID,
                ImageID = category.ImageID,
                InsideImageID = category.InsideImageID
            };

            categoryData.Items = _context.Items.Where(item => item.CategoryID == id).ToList();
            categoryData.ChildCategoriesId = (from cat in _context.Categories
                                             where cat.ParentID == id
                                             select cat.ID).ToList();

            return Json(categoryData);
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //[Authorize(Roles = "admin")]
        //public async Task<IActionResult> PutCategory(int id, Category category)
        //{
        //    CategoryResult categoryResult = new CategoryResult() { Success = true };

        //    if (id != category.ID)
        //    {
        //        return BadRequest();
        //    }

        //    Category rootCategory = await _context.Categories.FirstAsync(c => c.ParentID == c.ID);

        //    if (rootCategory != null && rootCategory.ID != category.ID)
        //    {
        //        if (category.ID == category.ParentID)
        //        {
        //            categoryResult.Success = false;
        //            categoryResult.ErrorCodes.Add(CategoryResultConstants.ERROR_ROOT_ALREADY_EXISTS);
        //        }
        //    }

        //    if (!category.ParentID.HasValue || !CategoryExists(category.ParentID ?? 0))
        //    {
        //        categoryResult.Success = false;
        //        categoryResult.ErrorCodes.Add(CategoryResultConstants.ERROR_PARENT_INVALID);
        //    }

        //    if (category.ChildItems?.Count > 0 && category.ChildCategories?.Count > 0)
        //    {
        //        categoryResult.Success = false;
        //        categoryResult.ErrorCodes.Add(CategoryResultConstants.ERROR_CHILD_CONFLICT);
        //    }

        //    _context.Entry(category).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CategoryExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Json(_context);
        //}

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

        // POST: api/Categories/CreateCategory
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Category>> CreateCategory([FromForm]CategoryModel categoryModel)
        {
            CategoryResult categoryResult = new CategoryResult() { Success = true };

            Category? parentCategory = await _context.Categories.FirstOrDefaultAsync(category => category.ID == categoryModel.ParentID);

            if(parentCategory == null)
            {
                categoryResult.ErrorCodes.Add(CategoryResultConstants.ERROR_PARENT_NOT_EXISTS);
            }

            List<Item> items = _context.Items.Where(item => item.CategoryID == parentCategory.ID).ToList();
            if(items.Count > 0)
            {
                categoryResult.ErrorCodes.Add(CategoryResultConstants.ERROR_CHILD_CONFLICT);
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

            return Json(categoryResult);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
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
    }
}
