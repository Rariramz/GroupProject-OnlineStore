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
using Store.Models;

namespace Store.Controllers
{
    [Route("api/[controller]")]
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
        [Authorize("admin")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        [Authorize("admin")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize("admin")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            CategoryResult categoryResult = new CategoryResult() { Success = true };

            if (id != category.ID)
            {
                return BadRequest();
            }

            Category rootCategory = await _context.Categories.FirstAsync(c => c.ParentID == c.ID);

            if (rootCategory != null && rootCategory.ID != category.ID)
            {
                if (category.ID == category.ParentID)
                {
                    categoryResult.Success = false;
                    categoryResult.ErrorCodes.Add(CategoryResultConstants.ERROR_ROOT_ALREADY_EXISTS);
                }
            }

            if (!category.ParentID.HasValue || !CategoryExists(category.ParentID ?? 0))
            {
                categoryResult.Success = false;
                categoryResult.ErrorCodes.Add(CategoryResultConstants.ERROR_PARENT_INVALID);
            }

            if (category.ChildItems?.Count > 0 && category.ChildCategories?.Count > 0)
            {
                categoryResult.Success = false;
                categoryResult.ErrorCodes.Add(CategoryResultConstants.ERROR_CHILD_CONFLICT);
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Json(_context);
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize("admin")]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            CategoryResult categoryResult = new CategoryResult() { Success = true };


            Category rootCategory = await _context.Categories.FirstAsync(c => c.ParentID == c.ID);

            if (rootCategory != null)
            {
                if (category.ID == category.ParentID)
                {
                    categoryResult.Success = false;
                    categoryResult.ErrorCodes.Add(CategoryResultConstants.ERROR_ROOT_ALREADY_EXISTS);
                }
            }

            if (!category.ParentID.HasValue || !CategoryExists(category.ParentID ?? 0))
            {
                categoryResult.Success = false;
                categoryResult.ErrorCodes.Add(CategoryResultConstants.ERROR_PARENT_INVALID);
            }

            if (category.ChildItems?.Count > 0 && category.ChildCategories?.Count > 0)
            {
                categoryResult.Success = false;
                categoryResult.ErrorCodes.Add(CategoryResultConstants.ERROR_CHILD_CONFLICT);
            }

            if (categoryResult.Success)
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
            }

            return Json(categoryResult);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
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
