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
    public class WMA_ConstantController : Controller
    {
        private readonly WMAContext _context;
        

        public WMA_ConstantController(WMAContext context)
        {
            _context = context;    
        }

        // GET: WMA_Constant
        public async Task<IActionResult> Index()
        {
            //constant.Categories = _context.WmaConstant_Category.ToList();

            var constants = _context.WmaConstants;
            var cats = _context.WmaConstant_Category.ToList();
            foreach(var c in constants)
            {
                c.Categories = cats;
                c.Category = cats.Where(ct => ct.Id == c.ConstantCategory).Single();
            }
            return View(await constants.ToListAsync());
        }

        // GET: WMA_Constant/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            WMA_Constant constant = await _context.WmaConstants.SingleOrDefaultAsync(m => m.ConstantId == id);
            if (constant == null)
            {
                return NotFound();
            }
            constant.Categories = _context.WmaConstant_Category.ToList();
            constant.Category = await _context.WmaConstant_Category.SingleOrDefaultAsync(m => m.Id == constant.ConstantCategory);


            return View(constant);
        }

        // GET: WMA_Constant/Create
        public IActionResult Create()
        {
            WMA_Constant constant = new WMA_Constant();
            constant.Categories =  _context.WmaConstant_Category.ToList();
                       
            return View(constant);
        }

        // POST: WMA_Constant/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConstantId,Description,ConstantCategory")] WMA_Constant wMA_Constant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wMA_Constant);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(wMA_Constant);
        }

        // GET: WMA_Constant/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            WMA_Constant constant = await _context.WmaConstants.SingleOrDefaultAsync(m => m.ConstantId == id);
            if (constant == null)
            {
                return NotFound();
            }
            constant.Categories = _context.WmaConstant_Category.ToList();

            return View(constant);
            /*var wMA_Constant = await _context.WmaConstants.SingleOrDefaultAsync(m => m.ConstantId == id);
            if (wMA_Constant == null)
            {
                return NotFound();
            }
            return View(wMA_Constant);
            */
        }

        // POST: WMA_Constant/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConstantId,Description,ConstantCategory")] WMA_Constant wMA_Constant)
        {
            if (id != wMA_Constant.ConstantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wMA_Constant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WMA_ConstantExists(wMA_Constant.ConstantId))
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
            return View(wMA_Constant);
        }

        // GET: WMA_Constant/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wMA_Constant = await _context.WmaConstants
                .SingleOrDefaultAsync(m => m.ConstantId == id);
            if (wMA_Constant == null)
            {
                return NotFound();
            }

            return View(wMA_Constant);
        }

        // POST: WMA_Constant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wMA_Constant = await _context.WmaConstants.SingleOrDefaultAsync(m => m.ConstantId == id);
            _context.WmaConstants.Remove(wMA_Constant);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool WMA_ConstantExists(int id)
        {
            return _context.WmaConstants.Any(e => e.ConstantId == id);
        }
    }
}
