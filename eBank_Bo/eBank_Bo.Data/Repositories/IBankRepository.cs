using eBank_Bo.Data.Models;

namespace eBank_Bo.Data.Repositories;

/// <summary>
/// Repository interface for banking operations.
/// </summary>
public interface IBankRepository
{
    Task<IEnumerable<Account>> GetAllAccountsAsync();
    Task<Account?> GetAccountByIdAsync(int accountId);
    Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId);
    Task<(bool Success, string Message)> ProcessDepositAsync(int accountId, decimal amount);
    Task<(bool Success, string Message)> ProcessWithdrawalAsync(int accountId, decimal amount);
}
