using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeManagementSystem.Data;
using RecipeManagementSystem.Models;

namespace RecipeManagementSystem.Controllers
{
    public class RecipeController : Controller
    {
        private readonly RecipeDbContext _context;

        public RecipeController(RecipeDbContext context)
        {
            _context = context;
        }

        // GET: Recipe
        public async Task<IActionResult> Index()
        {
            return View(await _context.Recipes.ToListAsync());
        }

        // GET: Recipe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeModel = await _context.Recipes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipeModel == null)
            {
                return NotFound();
            }

            return View(recipeModel);
        }

        // GET: Recipe/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recipe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ImageUrl,Date,Desicription,Category")] RecipeModel recipeModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipeModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recipeModel);
        }

        // GET: Recipe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeModel = await _context.Recipes.FindAsync(id);
            if (recipeModel == null)
            {
                return NotFound();
            }
            return View(recipeModel);
        }

        // POST: Recipe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImageUrl,Date,Desicription,Category")] RecipeModel recipeModel)
        {
            if (id != recipeModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeModelExists(recipeModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(recipeModel);
        }

        // GET: Recipe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeModel = await _context.Recipes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipeModel == null)
            {
                return NotFound();
            }

            return View(recipeModel);
        }

        // POST: Recipe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipeModel = await _context.Recipes.FindAsync(id);
            if (recipeModel != null)
            {
                _context.Recipes.Remove(recipeModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeModelExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }
    }
}
