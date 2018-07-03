using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace kidsChart2.Models
{
    public class DayItem
    {
        [Key]
        public int ID { get; set; }

        public DateTime ItemDay { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Сделано")]
        public bool IsDone { get; set; }

        [Display(Name = "Время")]
        [DataType(DataType.Time)]
        public DateTime? DueBy { get; set; }

        [Display(Name = "Иконка")]
        [DataType(DataType.ImageUrl)]
        public string IconPath { get; set; }
    }
}
