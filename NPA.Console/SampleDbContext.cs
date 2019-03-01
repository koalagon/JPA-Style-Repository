using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPA.Console
{
    public class SampleDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        
        
    }
}
