using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WMA_2.Models;

namespace WMA_2.Controllers
{
    public class ClassViewController : Controller
    {
        private readonly WMAContext _context;

        public ClassViewController(WMAContext context)
        {
            _context = context;
        }

        // GET: ClassView
        public async Task<IActionResult> Index()
        {
            List<ClassView> lstCV = new List<ClassView>();
            List<WMA_Class> classes =  await _context.WmaClasses.ToListAsync();
            
            foreach(WMA_Class cl in classes)
            {
                ClassView cv = new ClassView();
                List<Class_Times> cstime  = await _context.WmaClass_Time.Where(cst => cst.ClassId == cl.ClassId).ToListAsync();
                foreach (var clt in cstime) {
                    ClassTimes ct = await _context.ClassTimes.SingleOrDefaultAsync(c => c.Id == clt.ClassTimeId);

                    cl.addClassTime(ct);
                }
                cl.Class_Times.OrderBy(t => t.DayOfWeek).ThenBy(t => t.StartTime).ToList();
                cv.Class = cl;

                lstCV.Add(cv);
            }
           
            return View(lstCV);
        }

        // GET: ClassView/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ClassView cv = new ClassView();
            WMA_Class c = await _context.WmaClasses.SingleOrDefaultAsync(m => m.ClassId == id);
            List<Class_Times> cstime = await _context.WmaClass_Time.Where(cst => cst.ClassId == c.ClassId).ToListAsync();
            foreach (var clt in cstime)
            {
                ClassTimes ct = await _context.ClassTimes.SingleOrDefaultAsync(cl => cl.Id == clt.ClassTimeId);

                c.addClassTime(ct);
            }

            c.Class_Times.OrderBy(t => t.DayOfWeek).ThenBy(t => t.StartTime).ToList();
            cv.Class = c;

            return View(cv);
        }

        // GET: ClassView/Create
        public async Task<IActionResult> Create()
        {
            ClassView cv = new ClassView();
            cv.ClassList = new List<WMA_Class>();
            WMA_Class mtCl = new WMA_Class();
            mtCl.ClassId = 0;
            mtCl.Description = "Select a Class";
            cv.ClassList.Add(mtCl);
            List<WMA_Class> classes = await _context.WmaClasses.ToListAsync();
            cv.ClassList.AddRange(classes);
            
            cv.ClassTimeList = new List<ClassTimes>();
            
            cv.ClassTimeList.AddRange(_context.WmaClassTime.ToList());
            return View(cv);
        }

        // POST: ClassView/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection)
        {
            try
            {
                var chosenClass = collection["ddlClasses"];
                int classId = int.Parse(collection["ddlClasses"][0]);
                var objClass = await _context.WmaClasses
                .SingleOrDefaultAsync(m => m.ClassId == classId);

                //WMA_Class objClass = await _context.ClassTimes.SingleOrDefaultAsync(m => m.Id == id); new WMA_Class(chosenClass[0]);
                // TODO: check to make sure ddlClasses isn't empty
                
                // TODO: check to make sure ClassTimeList isn't empty
                // foreach ClassTime in ClassTimeList
                foreach(string ClassTimeId in collection["ClassTimeList"])
                {
                    int id = 0; 
                    int.TryParse(ClassTimeId, out id);
                    Class_Times cts = await _context.WmaClass_Time.SingleOrDefaultAsync(m => m.ClassTimeId == id && m.ClassId == classId);
                    if (cts == null)
                    {
                        ClassTimes ct = await _context.ClassTimes.SingleOrDefaultAsync(m => m.Id == id);
                        if (ct.Id > 0)
                        {
                            Class_Times t = new Class_Times();
                            t.ClassId = objClass.ClassId;
                            t.ClassTimeId = ct.Id;
                            _context.Add(t);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                // add to class_times table
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ClassView/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ClassView cv = new ClassView();
            WMA_Class c = await _context.WmaClasses.SingleOrDefaultAsync(m => m.ClassId == id);
            List<WMA_Location> lstLocation = new List<WMA_Location>();
            
            List <Class_Times> cstime = await _context.WmaClass_Time.Where(cst => cst.ClassId == c.ClassId).ToListAsync();
            foreach (var clt in cstime)
            {
                ClassTimes ct = await _context.ClassTimes.SingleOrDefaultAsync(cl => cl.Id == clt.ClassTimeId);
                c.addClassTime(ct);
                WMA_Location loc = await _context.WmaLocation.SingleOrDefaultAsync(l => l.Id == clt.LocationId);

                if (loc == null) {
                    loc = new WMA_Location();
                    loc.Description = "No Location Assigned";
                }
                lstLocation.Add(loc);


            }

            c.Class_Times.OrderBy(t => t.DayOfWeek).ThenBy(t => t.StartTime).ToList();
            List<int> lstClassTimes = c.Class_Times.Select(cst => cst.Id).ToList();
            c.Full_Class_Time_Info = cstime.OrderBy(cst => lstClassTimes.IndexOf(cst.ClassTimeId)).ToList();
            cv.Class = c;
            
            cv.Class_Times = cstime;
            cv.Location = lstLocation;
            return View(cv);
        }

        // POST: ClassView/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                WMA_Class c = await _context.WmaClasses.SingleOrDefaultAsync(m => m.ClassId == id);
                if (c.Description != collection["Class.Description"]) {
                    c.Description = collection["Class.Description"];
                    _context.Update(c);
                    await _context.SaveChangesAsync();
                }
                // add classes
                // edit classes
                // remove classes
                // which classes are added
                // which classes are edited
                // which classes are removed
                string times = collection["tm"];
                string[] arrTimes;
                if (times != null) { 
                    arrTimes = times.Split(',');
                } else
                {
                    arrTimes  = new string[1];
                    arrTimes[0] = "";
                }
                List<Class_Times> cstime = await _context.WmaClass_Time.Where(cst => cst.ClassId == c.ClassId).ToListAsync();
                foreach (var clt in cstime)
                {
                    ClassTimes ct = await _context.ClassTimes.SingleOrDefaultAsync(cl => cl.Id == clt.ClassTimeId);

                    c.addClassTime(ct);
                }

                c.Class_Times.OrderBy(t => t.DayOfWeek).ThenBy(t => t.StartTime).ToList();
                List<ClassTimes> lstClassTimes = c.Class_Times_sorted();
                ClassTimes[] arrClassTimes = lstClassTimes.ToArray();
                for (int nTimes = 0; nTimes < arrTimes.Length; nTimes++)
                {

                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                // todo go to error page
                return RedirectToAction("Error", e);
            }
        }

        // GET: ClassView/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ClassView/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Error(Exception e)
        {
            return View(e);
        }
        public async Task<IActionResult> ClassTimesList() {
            var cv = new ClassView();
            List<ClassTimes> cstime = await _context.ClassTimes.ToListAsync();
            
            List<SelectListItem> ddlListClasses = new List<SelectListItem>();
            ddlListClasses.Add(new SelectListItem { Value = "", Text = "Select a Time" });
            foreach (var clt in cstime.OrderBy(t => t.DayOfWeek).ThenBy(t => t.StartTime).ToList())
            {
                ddlListClasses.Add( new SelectListItem { Value = clt.Id.ToString(), Text = clt.ToString() });


            }
            cv.DDLClassTimes = ddlListClasses ;
            return PartialView(cv);


        }
        [HttpPost]
        public async Task<IActionResult> ClassTimesList(ClassView_ClassTimes_DDLReturn ret)
        {
            var cv = new ClassView();
            List<ClassTimes> cstime = await _context.ClassTimes.ToListAsync();

            List<SelectListItem> ddlListClasses = new List<SelectListItem>();
            ddlListClasses.Add(new SelectListItem { Value = "", Text = "Select a Time", Selected = false });
            foreach (var clt in cstime.OrderBy(t => t.DayOfWeek).ThenBy(t => t.StartTime).ToList())
            {
                
                ddlListClasses.Add(new SelectListItem { Value = clt.Id.ToString(), Text = clt.ToString(), Selected = (ret.id == clt.Id) });


            }

            cv.DDLClassTimes = ddlListClasses;
            return PartialView(cv);


        }
        [HttpPost]
        public async Task<IActionResult> EditTimes(ClassView_Edit_Return prms)
        {
            WMA_Class c = await _context.WmaClasses.SingleOrDefaultAsync(m => m.ClassId == prms.Id);
            string error = "";
            if (c.Description != prms.Description)
            {
                c.Description = prms.Description;
                _context.Update(c);
                await _context.SaveChangesAsync();
            }
            if (prms.class_times != null) { 
                foreach (ClassView_ClassTimes_Edit_Return ct in prms.class_times)
                {
                    if (ct.mode == EditMode.Add)
                    {
                        ClassTimes cstime = await _context.ClassTimes.SingleOrDefaultAsync(tm => tm.Id == ct.classtime_id);
                        if (cstime != null && cstime.Id > 0) { 
                            Class_Times t = new Class_Times();
                            t.ClassId = c.ClassId;
                            t.ClassTimeId = ct.classtime_id;
                            _context.Add(t);
                            await _context.SaveChangesAsync();
                        }
                    }
                    if (ct.mode == EditMode.Edit)
                    {
                        Class_Times cstime = await _context.WmaClass_Time.SingleOrDefaultAsync(tm => tm.Id == ct.id);
                        if (cstime != null) { 
                            if (ct.classtime_id > 0) { 
                                cstime.ClassTimeId = ct.classtime_id;
                            }
                            if (ct.location_id > 0)
                            {
                                cstime.LocationId = ct.location_id;
                            }
                            _context.Update(cstime);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            error = "Sorry something occurred that shouldn't of. Class time does not exist for class id " + ct.id;
                        }
                    }
                }
            }
            return Json("{ \"Url\":\"/ClassView/Index\", \"Error\":\"" + error + "\"}");
        }
        public async Task<IActionResult> DeleteTimes(int id)
        {
            Class_Times ct = await _context.WmaClass_Time.SingleOrDefaultAsync(m => m.Id == id);
            _context.Remove(ct);
            await _context.SaveChangesAsync();
            return Json("{ \"Id\":" + id + ", \"Error\":\"\"}");
        }
        [HttpPost]
        public async Task<IActionResult> LocationList(Location_Return ret)
        {
            var cv = new ClassView();
            List<WMA_Location> csLocation = await _context.WmaLocation.ToListAsync();

            List<SelectListItem> ddlListLocation = new List<SelectListItem>();
            ddlListLocation.Add(new SelectListItem { Value = "", Text = "Select a Location", Selected = false });
            foreach (var loc in csLocation)
            {

                ddlListLocation.Add(new SelectListItem { Value = loc.Id.ToString(), Text = loc.Description, Selected = (ret.id == loc.Id) });


            }

            cv.DDLLocations = ddlListLocation;
            return PartialView(cv);


        }
        
    }
}