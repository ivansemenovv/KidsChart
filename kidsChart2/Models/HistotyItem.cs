using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kidsChart2.Models
{
    public class HistotyItem
    {
        [Key]
        public int HistotyItemId { get; set; }

        public virtual Item Items { get; set; }

        [Display(Name = "Сделано")]
        public bool IsDone { get; set; }

        [Display(Name = "Иконка")]
        [DataType(DataType.ImageUrl)]
        public string IconPath { get; set; }
    }
}
