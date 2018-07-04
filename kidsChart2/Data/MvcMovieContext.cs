using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using kidsChart2.Models;

namespace kidsChart2.Models
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext (DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }

        public DbSet<kidsChart2.Models.DayItem> DayItems { get; set; }
        public DbSet<kidsChart2.Models.Item> Items { get; set; }
        public DbSet<kidsChart2.Models.HistoryItem> HistoryItems { get; set; }
    }
}
