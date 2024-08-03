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
    public class MenuModelsController : Controller
    {
        private readonly RecipeDbContext _context;

        public MenuModelsController(RecipeDbContext context)
        {
            _context = context;
        }

        // GET: MenuModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Menus.ToListAsync());
        }

        // GET: MenuModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuModel = await _context.Menus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuModel == null)
            {
                return NotFound();
            }

            return View(menuModel);
        }

        // GET: MenuModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MenuModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,Category")] MenuModel menuModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menuModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menuModel);
        }

        // GET: MenuModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuModel = await _context.Menus.FindAsync(id);
            if (menuModel == null)
            {
                return NotFound();
            }
            return View(menuModel);
        }

        // POST: MenuModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type,Category")] MenuModel menuModel)
        {
            if (id != menuModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuModelExists(menuModel.Id))
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
            return View(menuModel);
        }

        // GET: MenuModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuModel = await _context.Menus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuModel == null)
            {
                return NotFound();
            }

            return View(menuModel);
        }

        // POST: MenuModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuModel = await _context.Menus.FindAsync(id);
            if (menuModel != null)
            {
                _context.Menus.Remove(menuModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuModelExists(int id)
        {
            return _context.Menus.Any(e => e.Id == id);
        }
    }
}
