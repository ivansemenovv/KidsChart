using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kidsChart2.Models
{
    public class HistoryItem
    {
        [Key]
        public int HistoryItemId { get; set; }

        public Item Item { get; set; }

        public DateTime DayItem { get; set; }

        [Display(Name = "Сделано")]
        public bool IsDone { get; set; }

        [Display(Name = "Иконка")]
        [DataType(DataType.ImageUrl)]
        public string IconPath { get; set; }
    }

    public class HistoryItemGroup : HistoryItem
    {
        public int Count { get; set; }
    }
}
