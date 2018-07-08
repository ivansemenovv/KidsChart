using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace kidsChart2.Models
{
    public class Item
    {
        public Item()
        {
            //HistoryItems = new HashSet<HistoryItem>();
        }

        [Key]
        public int ItemId { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Время")]
        [DataType(DataType.Time)]
        public DateTime? DueBy { get; set; }

        [Display(Name = "Выполнять каждый день")]
        public bool IsDaily { get; set; }

        [Display(Name = "Только в эту дату")]
        [DataType(DataType.Date)]
        public DateTime? SpecificDate { get; set; }

        [Display(Name = "Иконка")]
        [DataType(DataType.ImageUrl)]
        public string IconPath { get; set; }

        [Display(Name = "Хорошее дело")]
        public bool IsGood { get; set; }

        [Display(Name = "Дело на один раз")]
        public bool IsOneTime { get; set; }

        public int Weight { get; set; }

        //public ICollection<HistoryItem> HistoryItems { get; set; }
    }
}
