using Microsoft.AspNetCore.Mvc;
using eBank_Bo.Data.Repositories;

namespace eBank_Bo.Web.Controllers;

public class AccountController(IBankRepository bankRepository) : Controller
{
    public async Task<IActionResult> Index()
    {
        var accounts = await bankRepository.GetAllAccountsAsync();
        return View(accounts);
    }

    public async Task<IActionResult> Details(int id)
    {
        var account = await bankRepository.GetAccountByIdAsync(id);
        if (account == null)
        {
            return NotFound();
        }
        return View(account);
    }
}
