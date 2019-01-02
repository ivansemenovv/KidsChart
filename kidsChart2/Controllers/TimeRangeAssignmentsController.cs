using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using kidsChart2.Models;

namespace kidsChart2.Views
{
    public class TimeRangeAssignmentsController : Controller
    {
        private readonly MvcMovieContext _context;

        public TimeRangeAssignmentsController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: TimeRangeAssignments
        public async Task<IActionResult> Index()
        {
            return View(await _context.TimeRangeAssignment.ToListAsync());
        }

        // GET: TimeRangeAssignments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeRangeAssignment = await _context.TimeRangeAssignment
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (timeRangeAssignment == null)
            {
                return NotFound();
            }

            return View(timeRangeAssignment);
        }

        // GET: TimeRangeAssignments/Create
        public IActionResult Create()
        {
            var timeRangeAssignment = new TimeRangeAssignmentViewModel();
            return View(timeRangeAssignment);
        }

        // POST: TimeRangeAssignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,Name,StartDate,EndDate,Icon")] TimeRangeAssignment timeRangeAssignment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(timeRangeAssignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(timeRangeAssignment);
        }

        // GET: TimeRangeAssignments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeRangeAssignment = await _context.TimeRangeAssignment.FindAsync(id);
            if (timeRangeAssignment == null)
            {
                return NotFound();
            }
            return View(timeRangeAssignment);
        }

        // POST: TimeRangeAssignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,Name,StartDate,EndDate,Icon")] TimeRangeAssignment timeRangeAssignment)
        {
            if (id != timeRangeAssignment.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timeRangeAssignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimeRangeAssignmentExists(timeRangeAssignment.ItemId))
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
            return View(timeRangeAssignment);
        }

        // GET: TimeRangeAssignments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeRangeAssignment = await _context.TimeRangeAssignment
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (timeRangeAssignment == null)
            {
                return NotFound();
            }

            return View(timeRangeAssignment);
        }

        // POST: TimeRangeAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var timeRangeAssignment = await _context.TimeRangeAssignment.FindAsync(id);
            _context.TimeRangeAssignment.Remove(timeRangeAssignment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimeRangeAssignmentExists(int id)
        {
            return _context.TimeRangeAssignment.Any(e => e.ItemId == id);
        }
    }
}
