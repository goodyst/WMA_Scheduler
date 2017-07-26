using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WMA_2.Models;

namespace WMA_2.Controllers
{
    public class ClassTimesController : Controller
    {
        private readonly WMAContext _context;

        public ClassTimesController(WMAContext context)
        {
            _context = context;    
        }

        // GET: ClassTimes
        public async Task<IActionResult> Index()
        {
            return View(await _context.WmaClassTime.OrderBy(t=>t.DayOfWeek).ThenBy(t=>t.StartTime).ToListAsync());
        }

        // GET: ClassTimes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classTimes = await _context.WmaClassTime
                .SingleOrDefaultAsync(m => m.Id == id);
            if (classTimes == null)
            {
                return NotFound();
            }

            return View(classTimes);
        }

        // GET: ClassTimes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClassTimes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DayOfWeek,StartTimeText,EndTimeText,ClassType")] ClassTimes classTimes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classTimes);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(classTimes);
        }

        // GET: ClassTimes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classTimes = await _context.WmaClassTime.SingleOrDefaultAsync(m => m.Id == id);
            if (classTimes == null)
            {
                return NotFound();
            }
            return View(classTimes);
        }

        // POST: ClassTimes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DayOfWeek,StartTimeText,EndTimeText,ClassType")] ClassTimes classTimes)
        {
            if (id != classTimes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classTimes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassTimesExists(classTimes.Id))
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
            return View(classTimes);
        }

        // GET: ClassTimes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classTimes = await _context.WmaClassTime
                .SingleOrDefaultAsync(m => m.Id == id);
            if (classTimes == null)
            {
                return NotFound();
            }

            return View(classTimes);
        }

        // POST: ClassTimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classTimes = await _context.WmaClassTime.SingleOrDefaultAsync(m => m.Id == id);
            _context.WmaClassTime.Remove(classTimes);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ClassTimesExists(int id)
        {
            return _context.WmaClassTime.Any(e => e.Id == id);
        }
    }
}
