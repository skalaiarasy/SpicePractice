using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpicePractice.Data;
using SpicePractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpicePractice.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Category.ToListAsync());
        }

        //GET for CREATE
        public IActionResult Create()
        {
            return View();
        }

        //post -create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            else
            {
                _context.Category.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        //GET - Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var category = await _context.Category.FindAsync(id);
                if (category == null)
                {
                    return NotFound();
                }
                return View(category);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            _context.Category.Update(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        

        //GET
        public async Task<IActionResult> Details(int? id)
        {
        if (id == null)
            {
                return NotFound();
            }
            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }


        //GET DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return View();
            }
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
