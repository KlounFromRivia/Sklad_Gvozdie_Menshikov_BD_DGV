using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStory_Men_Cher.Models;

namespace UserStory_Men_Cher
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Tovar> TovarBD { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
         : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tovar>().HasKey(u => u.Id);
        }
    }
}
