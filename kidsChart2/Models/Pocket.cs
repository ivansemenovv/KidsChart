using System.ComponentModel.DataAnnotations;

namespace kidsChart2.Models
{
    public class Pocket
    {
        [Key]
        public int PocketId { get; set; }
        public int Balance { get; set; }
    }
}
