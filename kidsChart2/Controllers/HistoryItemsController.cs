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
    public class HistoryItemsController : Controller
    {
        private readonly MvcMovieContext _context;

        public HistoryItemsController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: HistoryItems
        public async Task<IActionResult> Index()
        {
            var list = await _context.HistoryItems.Include("Item").ToListAsync();
            return View(list);
        }

        // GET: HistoryItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var histotyItem = await _context.HistoryItems
                .FirstOrDefaultAsync(m => m.HistoryItemId == id);
            if (histotyItem == null)
            {
                return NotFound();
            }

            return View(histotyItem);
        }

        // GET: HistoryItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HistoryItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HistotyItemId,IsDone,IconPath")] HistoryItem histotyItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(histotyItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(histotyItem);
        }

        // GET: HistoryItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var histotyItem = await _context.HistoryItems.FindAsync(id);
            if (histotyItem == null)
            {
                return NotFound();
            }
            return View(histotyItem);
        }

        // POST: HistoryItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HistotyItemId,IsDone,IconPath")] HistoryItem histotyItem)
        {
            if (id != histotyItem.HistoryItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(histotyItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistotyItemExists(histotyItem.HistoryItemId))
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
            return View(histotyItem);
        }

        // GET: HistoryItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var histotyItem = await _context.HistoryItems
                .FirstOrDefaultAsync(m => m.HistoryItemId == id);
            if (histotyItem == null)
            {
                return NotFound();
            }

            return View(histotyItem);
        }

        // POST: HistoryItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var histotyItem = await _context.HistoryItems.FindAsync(id);
            _context.HistoryItems.Remove(histotyItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistotyItemExists(int id)
        {
            return _context.HistoryItems.Any(e => e.HistoryItemId == id);
        }
    }
}
