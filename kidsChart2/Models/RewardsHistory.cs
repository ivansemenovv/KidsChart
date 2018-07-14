using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kidsChart2.Models
{
    public class RewardsHistory
    {
        [Key]
        public int RewardHistoryId { get; set; }
        public DateTime Date { get; set; }
        public Reward Reward { get; set; }
    }
}
