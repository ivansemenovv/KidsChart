using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace kidsChart2.Models
{
    public class TimeRangeAssignment
    {
        [Key]
        public int ItemId { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Дата начала")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Дата окончания")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Картинка")]
        public string Icon { get; set; }
    }
}
