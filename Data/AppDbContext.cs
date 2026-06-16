using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ExpenseTracker.Models;

namespace ExpenseTracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        public DbSet<ExpenseCategory> ExpenseCategories{ get; set; }

        protected override void OnModelCreating(
    ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExpenseCategory>()
        .HasData(
            new ExpenseCategory
            {
                Id = 1,
                Name = "Food"
            },
        new ExpenseCategory
        {
            Id = 2,
            Name = "Transport"
        },
        new ExpenseCategory
        {
            Id = 3,
            Name = "Party"
        }
        );
        }
    }
    }
