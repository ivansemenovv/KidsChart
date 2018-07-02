using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using kidsChart2.Models;

namespace kidsChart2.Controllers
{
    public class DayItemsController : Controller
    {
        private readonly MvcMovieContext _context;

        public DayItemsController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: DayItems
        public async Task<IActionResult> Index()
        {
            var todayList = await _context.DayItem.Where(dayItem => dayItem.ItemDay == DateTime.Today).ToListAsync();
            if (todayList.Count == 0)
            {
                var items = await _context.Items.Where(item => item.IsDaily || (!item.IsDaily && item.SpecificDate == DateTime.Today)).ToListAsync();
                foreach (var item in items)
                {
                    _context.DayItem.Add(new DayItem() { Name = item.Name, DueBy = item.DueBy, IsDone = false, ItemDay = DateTime.Today });
                }
                await _context.SaveChangesAsync();
            }

            return View(await _context.DayItem.Where(dayItem => dayItem.ItemDay == DateTime.Today).OrderBy(item => item.DueBy).ToListAsync());
        }

        // GET: DayItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dayItem = await _context.DayItem
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dayItem == null)
            {
                return NotFound();
            }

            return View(dayItem);
        }

        // GET: DayItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DayItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,DueBy,IsDone,StartDate")] DayItem dayItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dayItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dayItem);
        }

        // GET: DayItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dayItem = await _context.DayItem.FindAsync(id);
            if (dayItem == null)
            {
                return NotFound();
            }
            return View(dayItem);
        }

        // POST: DayItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,DueBy,IsDone,StartDate")] DayItem dayItem)
        {
            if (id != dayItem.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dayItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DayItemExists(dayItem.ID))
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
            return View(dayItem);
        }

        // GET: DayItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dayItem = await _context.DayItem
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dayItem == null)
            {
                return NotFound();
            }

            return View(dayItem);
        }

        // POST: DayItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dayItem = await _context.DayItem.FindAsync(id);
            _context.DayItem.Remove(dayItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DayItemExists(int id)
        {
            return _context.DayItem.Any(e => e.ID == id);
        }

        // GET: DayItems/Done/5
        public async Task<IActionResult> Done(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dayItem = await _context.DayItem.FindAsync(id);
            if (dayItem == null)
            {
                return NotFound();
            }

            dayItem.IsDone = !dayItem.IsDone;

            try
            {
                _context.Update(dayItem);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DayItemExists(dayItem.ID))
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
    }
}
