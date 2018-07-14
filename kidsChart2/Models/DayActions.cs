using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kidsChart2.Models
{
    public class DayActions
    {
        public IEnumerable<HistoryItem> HistoryItems { get; set; }
        public IEnumerable<Item> OneTimeItems { get; set; }
        public IDictionary<Item, int> OneDayItemsGroups { get; set; }
        public int BalanceStars { get; set; }
        public int TodayStars { get; set; }
        public List<RoutineHistory> RoutinesHistory { get; set; }
        public List<Reward> Rewards { get; set; }
    }
}
