using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace kidsChart2.Models
{
    public class Items
    {
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
    }
}
