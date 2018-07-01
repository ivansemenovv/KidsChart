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

        public string Name { get; set; }

        public DateTime? DueBy { get; set; }
    }
}
