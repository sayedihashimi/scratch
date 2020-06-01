using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MyMovie.Models
{
    public class MyMovie01Context : DbContext
    {
        public MyMovie01Context (DbContextOptions<MyMovie01Context> options)
            : base(options)
        {
        }

        public DbSet<MyMovie.Models.Movie> Movie { get; set; }
    }
}
