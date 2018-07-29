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
    public class TravelItemsController : Controller
    {
        private readonly MvcMovieContext _context;

        public TravelItemsController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: TravelItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.TravelItems.OrderBy(item => item.Name).ToListAsync());
        }

        // GET: TravelItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelItem = await _context.TravelItems
                .FirstOrDefaultAsync(m => m.TravelItemId == id);
            if (travelItem == null)
            {
                return NotFound();
            }

            return View(travelItem);
        }

        // GET: TravelItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TravelItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TravelItemId,Name,IsDone")] TravelItem travelItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(travelItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(travelItem);
        }

        // GET: TravelItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelItem = await _context.TravelItems.FindAsync(id);
            if (travelItem == null)
            {
                return NotFound();
            }
            return View(travelItem);
        }

        // POST: TravelItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TravelItemId,Name,IsDone")] TravelItem travelItem)
        {
            if (id != travelItem.TravelItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(travelItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TravelItemExists(travelItem.TravelItemId))
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
            return View(travelItem);
        }

        // GET: TravelItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelItem = await _context.TravelItems
                .FirstOrDefaultAsync(m => m.TravelItemId == id);
            if (travelItem == null)
            {
                return NotFound();
            }

            return View(travelItem);
        }

        // POST: TravelItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var travelItem = await _context.TravelItems.FindAsync(id);
            _context.TravelItems.Remove(travelItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TravelItemExists(int id)
        {
            return _context.TravelItems.Any(e => e.TravelItemId == id);
        }

        public async Task<IActionResult> Done(int id)
        {
            var item = await _context.TravelItems.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            item.IsDone = !item.IsDone;

            _context.Update(item);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
