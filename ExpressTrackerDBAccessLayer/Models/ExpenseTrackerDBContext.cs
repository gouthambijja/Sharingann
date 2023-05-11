using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressTrackerDBAccessLayer.Models
{
    public class ExpenseTrackerDBContext:DbContext
    {
        public ExpenseTrackerDBContext() { }
        public ExpenseTrackerDBContext(DbContextOptions<ExpenseTrackerDBContext> options):base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>(x =>
             x.HasOne(u => u.User)
             .WithMany(t => t.Transactions)
             .HasForeignKey(u => u.UserId));
            //modelBuilder.Entity<User>(x =>
            // x.HasMany(u => u.Transactions)
            // .WithOne(t => t.User)
            // .HasForeignKey(u => u.UserId));
            modelBuilder.Entity<Category>(x =>
            x.HasOne(u => u.User)
            .WithMany(t => t.Categories)
            .HasForeignKey(u => u.UserId));
        }

    }
}
