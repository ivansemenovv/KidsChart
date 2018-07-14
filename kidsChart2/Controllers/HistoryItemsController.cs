using kidsChart2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kidsChart2.Controllers
{
    public class HistoryItemsController : Controller
    {
        private readonly MvcMovieContext _context;

        public HistoryItemsController(MvcMovieContext context)
        {
            _context = context;
        }

        class Pet
        {
            public string Name { get; set; }
            public double Age { get; set; }
        }


        // GET: HistoryItems
        public async Task<IActionResult> Index()
        {
            var pocket = _context.Pocket.First();
            var todayList = await _context.HistoryItems.Include("Item").Where(dayItem => dayItem.DayItem == DateTime.Today).OrderBy(HistoryItem => HistoryItem.Item.DueBy).ToListAsync();
            if(todayList.Count == 0)
            {
                var items = await _context.Items.Where(item => (item.IsDaily || (!item.IsDaily && item.SpecificDate == DateTime.Today)) && !item.IsOneTime).ToListAsync();
                foreach (var item in items)
                {
                    _context.HistoryItems.Add(new HistoryItem() { Item = item, DayItem = DateTime.Today});
                }

                // save balance for the day
                pocket.Balance += GetTotalCollectedStartsForLastDay();
                await _context.SaveChangesAsync();
            }
            
            todayList = await _context.HistoryItems.Include("Item").
                Where(dayItem => dayItem.DayItem == DateTime.Today && !dayItem.Item.IsOneTime).
                OrderBy(HistoryItem => HistoryItem.Item.DueBy).ToListAsync();
            var oneTimeItems = await _context.Items.Where(item => item.IsOneTime).ToListAsync();

            var oneDayItemsGroups = _context.HistoryItems.Include("Item").
                Where(dayItem => dayItem.DayItem >= DateTime.Today && dayItem.Item.IsOneTime).
                GroupBy(dayItem => dayItem.Item).Select(g => new { item = g.Key, count = g.Count() }).OrderByDescending(g => g.item.Weight).
                ToDictionary(k => k.item, i => i.count);

            var todayStars = _context.HistoryItems.Include("Item").
                Where(dayItem => dayItem.DayItem == DateTime.Today && (dayItem.Item.IsOneTime || (!dayItem.Item.IsOneTime && dayItem.IsDone)))
                .Sum(item => item.Item.Weight);

            var routinesHistory = GetRoutineHistory(50);

            var rewards = _context.Rewards.ToList();

            return View(new DayActions() {  HistoryItems = todayList, OneTimeItems = oneTimeItems,
                OneDayItemsGroups = oneDayItemsGroups,
                BalanceStars = pocket.Balance,
                TodayStars = todayStars,
                RoutinesHistory = routinesHistory,
                Rewards = rewards
            });
        }

        int GetTotalCollectedStartsForLastDay()
        {
            return _context.HistoryItems.Include("Item").
                Where(dayItem => dayItem.DayItem == DateTime.Today.AddDays(-1) && (dayItem.Item.IsOneTime || (!dayItem.Item.IsOneTime && dayItem.IsDone)))
                .Sum(item => item.Item.Weight);
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
        public async Task<IActionResult> Create([Bind("HistoryItemId,IsDone,IconPath")] HistoryItem histotyItem)
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
        public async Task<IActionResult> Edit(int id)
        {
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
        public async Task<IActionResult> Edit(int id, [Bind("HistoryItemId,IsDone,DayItem")] HistoryItem histotyItem)
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
        public async Task<IActionResult> DeleteConfirmed(int HistoryItemId)
        {
            var histotyItem = await _context.HistoryItems.FindAsync(HistoryItemId);
            _context.HistoryItems.Remove(histotyItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistotyItemExists(int id)
        {
            return _context.HistoryItems.Any(e => e.HistoryItemId == id);
        }

        // GET: DayItems/Done/5
        public async Task<IActionResult> Done(int id)
        {
            var dayItem = await _context.HistoryItems.FindAsync(id);
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
                if (!HistotyItemExists(dayItem.HistoryItemId))
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


        public async Task<IActionResult> AddOneTime(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.HistoryItems.Add(new HistoryItem() { Item = item, DayItem = DateTime.Today });
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> UseReward(int id)
        {
            var reward = await _context.Rewards.FindAsync(id);
            if (reward == null)
            {
                return NotFound();
            }

            var pocket = _context.Pocket.First();
            //if (pocket.Balance >= reward.Cost)
            {
                var rewardHistoryItem = new RewardsHistory()
                {
                    Date = DateTime.Today,
                    Reward = reward
                };

                _context.RewardsHistory.Add(rewardHistoryItem);
                await _context.SaveChangesAsync();
                pocket.Balance -= reward.Cost;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        private List<RoutineHistory> GetRoutineHistory(int numDays)
        {
            var historyRoutine = _context.HistoryItems.Include("Item").
                Where(dayItem => dayItem.DayItem < DateTime.Today && dayItem.DayItem >= DateTime.Today.AddDays(-1 * numDays) && dayItem.Item.IsDaily)
                .OrderBy(dayItem => dayItem.Item.ItemId);

            int routineId = -1;
            RoutineHistory routine = new RoutineHistory();
            var res = new List<RoutineHistory>();
            foreach (var item in historyRoutine)
            {
                if(routineId != item.Item.ItemId)
                {
                    routine = new RoutineHistory()
                    {
                        RoutineId = item.Item.ItemId,
                        RoutineName = item.Item.Name,
                        History = new List<bool>()
                    };

                    res.Add(routine);
                    routineId = routine.RoutineId;
                }
                routine.History.Add(item.IsDone);
            }

            return res;
        }
    }
}
