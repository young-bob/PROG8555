using Microsoft.EntityFrameworkCore;
using eBank_Bo.Data.Context;
using eBank_Bo.Data.Models;
using eBank_Bo.Data.Enums;

namespace eBank_Bo.Data.Repositories;

/// <summary>
/// Repository implementation for banking operations.
/// </summary>
public class BankRepository(BankingDbContext context) : IBankRepository
{
    public async Task<IEnumerable<Account>> GetAllAccountsAsync()
    {
        return await context.Accounts
            .Include(a => a.Customer)
            .ToListAsync();
    }

    public async Task<Account?> GetAccountByIdAsync(int accountId)
    {
        return await context.Accounts
            .Include(a => a.Customer)
            .FirstOrDefaultAsync(a => a.AccountId == accountId);
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId)
    {
        return await context.Transactions
            .Where(t => t.AccountId == accountId)
            .OrderByDescending(t => t.TransactionDate)
            .ToListAsync();
    }

    public async Task<(bool Success, string Message)> ProcessDepositAsync(int accountId, decimal amount)
    {
        if (amount <= 0)
            return (false, "Amount must be greater than zero");

        var account = await context.Accounts.FindAsync(accountId);
        if (account == null)
            return (false, "Account not found");

        account.Balance += amount;
        
        var transaction = new Transaction
        {
            AccountId = accountId,
            Amount = amount,
            TransactionType = TransactionType.Deposit,
            TransactionDate = DateTime.Now,
            Description = "Deposit"
        };

        context.Transactions.Add(transaction);
        await context.SaveChangesAsync();

        return (true, "Deposit successful");
    }

    public async Task<(bool Success, string Message)> ProcessWithdrawalAsync(int accountId, decimal amount)
    {
        if (amount <= 0)
            return (false, "Amount must be greater than zero");

        var account = await context.Accounts.FindAsync(accountId);
        if (account == null)
            return (false, "Account not found");

        if (account.Balance < amount)
            return (false, "Insufficient funds");

        account.Balance -= amount;
        
        var transaction = new Transaction
        {
            AccountId = accountId,
            Amount = amount,
            TransactionType = TransactionType.Withdrawal,
            TransactionDate = DateTime.Now,
            Description = "Withdrawal"
        };

        context.Transactions.Add(transaction);
        await context.SaveChangesAsync();

        return (true, "Withdrawal successful");
    }
}
