using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kidsChart2.Models
{
    public class RoutineHistory
    {
        public int RoutineId { get; set; }
        public string RoutineName { get; set; }
        public List<HistoryItem> History { get; set; }
    }
}
