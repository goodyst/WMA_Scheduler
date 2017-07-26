using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WMA_2.Models;

namespace WMA_2.Controllers
{
    public class WMA_Constant_CategoryController : Controller
    {
        private readonly WMAContext _context;

        public WMA_Constant_CategoryController(WMAContext context)
        {
            _context = context;    
        }

        // GET: WMA_Constant_Category
        public async Task<IActionResult> Index()
        {
            return View(await _context.WmaConstant_Category.ToListAsync());
        }

        // GET: WMA_Constant_Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wMA_Constant_Category = await _context.WmaConstant_Category
                .SingleOrDefaultAsync(m => m.Id == id);
            if (wMA_Constant_Category == null)
            {
                return NotFound();
            }

            return View(wMA_Constant_Category);
        }

        // GET: WMA_Constant_Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WMA_Constant_Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description")] WMA_Constant_Category wMA_Constant_Category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wMA_Constant_Category);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(wMA_Constant_Category);
        }

        // GET: WMA_Constant_Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wMA_Constant_Category = await _context.WmaConstant_Category.SingleOrDefaultAsync(m => m.Id == id);
            if (wMA_Constant_Category == null)
            {
                return NotFound();
            }
            return View(wMA_Constant_Category);
        }

        // POST: WMA_Constant_Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description")] WMA_Constant_Category wMA_Constant_Category)
        {
            if (id != wMA_Constant_Category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wMA_Constant_Category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WMA_Constant_CategoryExists(wMA_Constant_Category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(wMA_Constant_Category);
        }

        // GET: WMA_Constant_Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wMA_Constant_Category = await _context.WmaConstant_Category
                .SingleOrDefaultAsync(m => m.Id == id);
            if (wMA_Constant_Category == null)
            {
                return NotFound();
            }

            return View(wMA_Constant_Category);
        }

        // POST: WMA_Constant_Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wMA_Constant_Category = await _context.WmaConstant_Category.SingleOrDefaultAsync(m => m.Id == id);
            _context.WmaConstant_Category.Remove(wMA_Constant_Category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool WMA_Constant_CategoryExists(int id)
        {
            return _context.WmaConstant_Category.Any(e => e.Id == id);
        }
    }
}
