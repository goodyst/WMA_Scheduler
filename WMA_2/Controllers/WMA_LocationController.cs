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
    public class WMA_LocationController : Controller
    {
        private readonly WMAContext _context;

        public WMA_LocationController(WMAContext context)
        {
            _context = context;    
        }

        // GET: WMA_Location
        public async Task<IActionResult> Index()
        {
            return View(await _context.WmaLocation.ToListAsync());
        }

        // GET: WMA_Location/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wMA_Location = await _context.WmaLocation
                .SingleOrDefaultAsync(m => m.Id == id);
            if (wMA_Location == null)
            {
                return NotFound();
            }

            return View(wMA_Location);
        }

        // GET: WMA_Location/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WMA_Location/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description")] WMA_Location wMA_Location)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wMA_Location);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(wMA_Location);
        }

        // GET: WMA_Location/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wMA_Location = await _context.WmaLocation.SingleOrDefaultAsync(m => m.Id == id);
            if (wMA_Location == null)
            {
                return NotFound();
            }
            return View(wMA_Location);
        }

        // POST: WMA_Location/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description")] WMA_Location wMA_Location)
        {
            if (id != wMA_Location.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wMA_Location);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WMA_LocationExists(wMA_Location.Id))
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
            return View(wMA_Location);
        }

        // GET: WMA_Location/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wMA_Location = await _context.WmaLocation
                .SingleOrDefaultAsync(m => m.Id == id);
            if (wMA_Location == null)
            {
                return NotFound();
            }

            return View(wMA_Location);
        }

        // POST: WMA_Location/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wMA_Location = await _context.WmaLocation.SingleOrDefaultAsync(m => m.Id == id);
            _context.WmaLocation.Remove(wMA_Location);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool WMA_LocationExists(int id)
        {
            return _context.WmaLocation.Any(e => e.Id == id);
        }
    }
}
