using Microsoft.AspNetCore.Mvc;
using eBank_Bo.Data.Repositories;
using eBank_Bo.Web.Models;
using eBank_Bo.Data.Enums;

namespace eBank_Bo.Web.Controllers;

public class TransactionController(IBankRepository bankRepository) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Deposit(int accountId)
    {
        var account = await bankRepository.GetAccountByIdAsync(accountId);
        if (account == null)
            return NotFound();

        var model = new DepositViewModel
        {
            AccountId = account.AccountId,
            AccountNumber = account.AccountNumber,
            CurrentBalance = account.Balance
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Deposit(DepositViewModel model)
    {
        if (!ModelState.IsValid)
        {
            // Restore details for the view
            var account = await bankRepository.GetAccountByIdAsync(model.AccountId);
            if (account != null)
            {
                model.AccountNumber = account.AccountNumber;
                model.CurrentBalance = account.Balance;
            }
            return View(model);
        }

        var (success, message) = await bankRepository.ProcessDepositAsync(model.AccountId, model.Amount);
        
        if (success)
        {
            TempData["SuccessMessage"] = message;
            return RedirectToAction("Index", "Account");
        }
        else
        {
            TempData["ErrorMessage"] = message;
            // Restore details for the view
            var account = await bankRepository.GetAccountByIdAsync(model.AccountId);
            if (account != null)
            {
                model.AccountNumber = account.AccountNumber;
                model.CurrentBalance = account.Balance;
            }
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Withdraw(int accountId)
    {
        var account = await bankRepository.GetAccountByIdAsync(accountId);
        if (account == null)
            return NotFound();

        var model = new WithdrawViewModel
        {
            AccountId = account.AccountId,
            AccountNumber = account.AccountNumber,
            CurrentBalance = account.Balance
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Withdraw(WithdrawViewModel model)
    {
        if (!ModelState.IsValid)
        {
            // Restore details for the view
            var account = await bankRepository.GetAccountByIdAsync(model.AccountId);
            if (account != null)
            {
                model.AccountNumber = account.AccountNumber;
                model.CurrentBalance = account.Balance;
            }
            return View(model);
        }

        var (success, message) = await bankRepository.ProcessWithdrawalAsync(model.AccountId, model.Amount);
        
        if (success)
        {
            TempData["SuccessMessage"] = message;
            return RedirectToAction("Index", "Account");
        }
        else
        {
            TempData["ErrorMessage"] = message;
            // Restore details for the view
            var account = await bankRepository.GetAccountByIdAsync(model.AccountId);
            if (account != null)
            {
                model.AccountNumber = account.AccountNumber;
                model.CurrentBalance = account.Balance;
            }
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> History(int accountId)
    {
        var account = await bankRepository.GetAccountByIdAsync(accountId);
        if (account == null)
            return NotFound();

        var transactions = (await bankRepository.GetTransactionsByAccountIdAsync(accountId)).ToList();

        var historyModel = new TransactionHistoryViewModel
        {
            AccountId = account.AccountId,
            AccountNumber = account.AccountNumber,
            CurrentBalance = account.Balance
        };

        decimal currentBalance = account.Balance;
        
        // transactions are ordered descending by date from the repository
        foreach (var t in transactions)
        {
            var item = new TransactionItemViewModel
            {
                Date = t.TransactionDate,
                Type = t.TransactionType.ToString(),
                Amount = t.Amount,
                Description = t.Description,
                BalanceAfter = currentBalance
            };
            historyModel.Transactions.Add(item);

            // Calculate previous balance for the next older transaction
            if (t.TransactionType == TransactionType.Deposit)
            {
                currentBalance -= t.Amount;
            }
            else if (t.TransactionType == TransactionType.Withdrawal)
            {
                currentBalance += t.Amount;
            }
        }

        return View(historyModel);
    }
}
