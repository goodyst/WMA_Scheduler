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
    public class WMA_ClassController : Controller
    {
        private readonly WMAContext _context;

        public WMA_ClassController(WMAContext context)
        {
            _context = context;    
        }

        // GET: WMA_Class
        public async Task<IActionResult> Index()
        {
            return View(await _context.WmaClasses.ToListAsync());
        }

        // GET: WMA_Class/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wMA_Class = await _context.WmaClasses
                .SingleOrDefaultAsync(m => m.ClassId == id);
            if (wMA_Class == null)
            {
                return NotFound();
            }

            return View(wMA_Class);
        }

        // GET: WMA_Class/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WMA_Class/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClassId,Name,Description")] WMA_Class wMA_Class)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wMA_Class);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(wMA_Class);
        }

        // GET: WMA_Class/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wMA_Class = await _context.WmaClasses.SingleOrDefaultAsync(m => m.ClassId == id);
            if (wMA_Class == null)
            {
                return NotFound();
            }
            return View(wMA_Class);
        }

        // POST: WMA_Class/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClassId,Name,Description")] WMA_Class wMA_Class)
        {
            if (id != wMA_Class.ClassId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wMA_Class);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WMA_ClassExists(wMA_Class.ClassId))
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
            return View(wMA_Class);
        }

        // GET: WMA_Class/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wMA_Class = await _context.WmaClasses
                .SingleOrDefaultAsync(m => m.ClassId == id);
            if (wMA_Class == null)
            {
                return NotFound();
            }

            return View(wMA_Class);
        }

        // POST: WMA_Class/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wMA_Class = await _context.WmaClasses.SingleOrDefaultAsync(m => m.ClassId == id);
            _context.WmaClasses.Remove(wMA_Class);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool WMA_ClassExists(int id)
        {
            return _context.WmaClasses.Any(e => e.ClassId == id);
        }
    }
}
