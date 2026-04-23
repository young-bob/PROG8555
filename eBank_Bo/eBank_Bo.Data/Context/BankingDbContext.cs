using Microsoft.EntityFrameworkCore;
using eBank_Bo.Data.Models;
using eBank_Bo.Data.Enums;

namespace eBank_Bo.Data.Context;

/// <summary>
/// Database context for the banking system.
/// </summary>
public class BankingDbContext(DbContextOptions<BankingDbContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure precision for decimal values
        modelBuilder.Entity<Account>()
            .Property(a => a.Balance)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Transaction>()
            .Property(t => t.Amount)
            .HasColumnType("decimal(18,2)");

        // Seed Data
        modelBuilder.Entity<Customer>().HasData(
            new Customer { CustomerId = 1, FirstName = "Yangbo", LastName = "User", Email = "yangbo@email.com" },
            new Customer { CustomerId = 2, FirstName = "Bob", LastName = "Smith", Email = "bob.smith@email.com" }
        );

        modelBuilder.Entity<Account>().HasData(
            new Account { AccountId = 1, AccountNumber = "ACC-10001", Balance = 5000, AccountType = AccountType.Checking, CustomerId = 1 },
            new Account { AccountId = 2, AccountNumber = "ACC-10002", Balance = 12000, AccountType = AccountType.Savings, CustomerId = 1 },
            new Account { AccountId = 3, AccountNumber = "ACC-10003", Balance = 3500, AccountType = AccountType.Checking, CustomerId = 2 },
            new Account { AccountId = 4, AccountNumber = "ACC-10004", Balance = 8000, AccountType = AccountType.Savings, CustomerId = 2 }
        );
    }
}
