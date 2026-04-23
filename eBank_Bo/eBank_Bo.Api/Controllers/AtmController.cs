using Microsoft.AspNetCore.Mvc;
using eBank_Bo.Data.Repositories;
using eBank_Bo.Data.DTOs;

namespace eBank_Bo.Api.Controllers;

/// <summary>
/// ATM Controller providing RESTful endpoints for ATM operations.
/// </summary>
[ApiController]
[Route("api/atm")]
public class AtmController(IBankRepository bankRepository) : ControllerBase
{
    [HttpGet("balance/{accountId}")]
    public async Task<IActionResult> GetBalance(int accountId)
    {
        var account = await bankRepository.GetAccountByIdAsync(accountId);
        if (account == null)
            return NotFound(new { error = "Account not found" });

        var response = new BalanceResponse
        {
            AccountId = account.AccountId,
            AccountNumber = account.AccountNumber,
            Balance = account.Balance
        };

        return Ok(response);
    }

    [HttpPost("withdraw")]
    public async Task<IActionResult> Withdraw([FromBody] WithdrawRequest request)
    {
        if (request.Amount <= 0)
            return BadRequest(new { error = "Amount must be greater than zero" });

        var account = await bankRepository.GetAccountByIdAsync(request.AccountId);
        if (account == null)
            return NotFound(new { error = "Account not found" });

        if (account.Balance < request.Amount)
            return BadRequest(new { error = "Insufficient funds", currentBalance = account.Balance });

        var (success, message) = await bankRepository.ProcessWithdrawalAsync(request.AccountId, request.Amount);
        if (!success)
            return BadRequest(new { error = message });

        // Refresh account to get new balance
        account = await bankRepository.GetAccountByIdAsync(request.AccountId);

        return Ok(new { message = "Withdrawal successful", newBalance = account!.Balance });
    }

    [HttpPost("deposit")]
    public async Task<IActionResult> Deposit([FromBody] DepositRequest request)
    {
        if (request.Amount <= 0)
            return BadRequest(new { error = "Amount must be greater than zero" });

        var account = await bankRepository.GetAccountByIdAsync(request.AccountId);
        if (account == null)
            return NotFound(new { error = "Account not found" });

        var (success, message) = await bankRepository.ProcessDepositAsync(request.AccountId, request.Amount);
        if (!success)
            return BadRequest(new { error = message });

        // Refresh account to get new balance
        account = await bankRepository.GetAccountByIdAsync(request.AccountId);

        return Ok(new { message = "Deposit successful", newBalance = account!.Balance });
    }

    [HttpGet("transactions/{accountId}")]
    public async Task<IActionResult> GetTransactions(int accountId)
    {
        var account = await bankRepository.GetAccountByIdAsync(accountId);
        if (account == null)
            return NotFound(new { error = "Account not found" });

        var transactions = await bankRepository.GetTransactionsByAccountIdAsync(accountId);

        var response = transactions.Select(t => new TransactionResponse
        {
            TransactionId = t.TransactionId,
            Amount = t.Amount,
            TransactionType = t.TransactionType.ToString(),
            TransactionDate = t.TransactionDate,
            Description = t.Description
        });

        return Ok(response);
    }
}
