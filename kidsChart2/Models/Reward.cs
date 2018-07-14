using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kidsChart2.Models
{
    public class Reward
    {
        [Key]
        public int RewardId { get; set; }

        public string Name { get; set; }

        [Display(Name = "Иконка")]
        [DataType(DataType.ImageUrl)]
        public string IconPath { get; set; }

        public int Cost { get; set; }

    }
}
