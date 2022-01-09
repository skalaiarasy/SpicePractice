using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SpicePractice.Data;
using SpicePractice.Models;
using SpicePractice.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpicePractice.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        [TempData]
        public string StatusMessage { get; set; }
        public SubCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        //GET index
        public async Task<IActionResult> Index()
        {
            var subCategories = await _context.SubCategory.Include(s => s.Category).ToListAsync();
            return View(subCategories);
        }

        //GET - CREATE
        public async Task<IActionResult> Create()
        {
            SubCategoryAndCategoryViewModel model = new SubCategoryAndCategoryViewModel()
            {
                CategoryList = await _context.Category.ToListAsync(),
                SubCategory = new Models.SubCategory(),
                SubCategoryList=await _context.SubCategory.OrderBy(s=>s.Name).Select(s=>s.Name).Distinct().ToListAsync()
            };
            return View(model);
        }

        //post-CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubCategoryAndCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doesCategoryExists = _context.SubCategory.Include(s => s.Category).Where(s => s.Name == model.SubCategory.Name && s.CategoryId == model.SubCategory.CategoryId);
                if (doesCategoryExists.Count() > 0)
                {
                    //error
                    StatusMessage = "Error:Sub category exists under" + doesCategoryExists.First().Category.Name + "category. Please use another name";
                }
                else
                {
                    _context.SubCategory.Add(model.SubCategory);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            SubCategoryAndCategoryViewModel modelVM = new SubCategoryAndCategoryViewModel()
            {
                CategoryList = await _context.Category.ToListAsync(),
                SubCategory = model.SubCategory,
                SubCategoryList = await _context.SubCategory.OrderBy(p => p.Name).Select(p => p.Name).Distinct().ToListAsync(),
            StatusMessage = StatusMessage
        };
            return View(modelVM);
        }

        [ActionName("GetsubCategory")]
        public async Task<IActionResult> GetSubCategory(int id)
        {
            List<SubCategory> subCategories = new List<SubCategory>();
            subCategories = await (from subCategory in _context.SubCategory
                             where subCategory.CategoryId == id
                             select subCategory).ToListAsync();
            return Json(new SelectList(subCategories, "Id", "Name"));
        }

        //GET - EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            //I used FindAsync - But Brugan used Single or Default
            var subCategory = await _context.SubCategory.FindAsync(id);
            if (subCategory == null)
            {
                return NotFound();
            }
            SubCategoryAndCategoryViewModel model = new SubCategoryAndCategoryViewModel()
            {
                CategoryList = await _context.Category.ToListAsync(),
                SubCategory = subCategory,
                SubCategoryList = await _context.SubCategory.OrderBy(s => s.Name).Select(s => s.Name).Distinct().ToListAsync()
            };
            return View(model);
        }

        //post-EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SubCategoryAndCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doesCategoryExists = _context.SubCategory.Include(s => s.Category).Where(s => s.Name == model.SubCategory.Name && s.CategoryId == model.SubCategory.CategoryId);
                if (doesCategoryExists.Count() > 0)
                {
                    //error
                    StatusMessage = "Error:Sub category exists under" + doesCategoryExists.First().Category.Name + "category. Please use another name";
                }
                else
                {
                    var subCatFromDb = await _context.SubCategory.FindAsync(id); 
                    //var subCatFromDb = await _context.SubCategory.SingleOrDefaultAsync(c=>c.Id==id); Bhrugen used this
                    subCatFromDb.Name = model.SubCategory.Name;
                    
                    //if (subCatFromDb == null)
                    //{
                    //    return NotFound();
                    //}
                    //Why I didn't  use Update here?
                    //If you're using update it will change the entire properties. But here we're
                    //updating only one property-Name
                   // _context.SubCategory.Update(model.SubCategory);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            SubCategoryAndCategoryViewModel modelVM = new SubCategoryAndCategoryViewModel()
            {
                CategoryList = await _context.Category.ToListAsync(),
                SubCategory = model.SubCategory,
                SubCategoryList = await _context.SubCategory.OrderBy(p => p.Name).Select(p => p.Name).ToListAsync(),
                StatusMessage = StatusMessage
            };
            return View(modelVM);
        }

        //GET the Details of SubCategory
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var subCategory = await _context.SubCategory.FindAsync(id);
            if (subCategory == null)
            {
                return NotFound();
            }
            return View(subCategory);
        }

        //GET DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var subCategory = await _context.SubCategory.FindAsync(id);
            if (subCategory == null)
            {
                return NotFound();
            }
            return View(subCategory);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var subCategory = await _context.SubCategory.FindAsync(id);
            if (subCategory == null)
            {
                return View();
            }
            _context.SubCategory.Remove(subCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

    }
}
