using System.ComponentModel.DataAnnotations;

namespace eBank_Bo.Web.Models;

public class DepositViewModel
{
    [Required]
    public int AccountId { get; set; }
    
    public string? AccountNumber { get; set; }
    public decimal CurrentBalance { get; set; }

    [Required(ErrorMessage = "Amount is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
    public decimal Amount { get; set; }
}

public class WithdrawViewModel
{
    [Required]
    public int AccountId { get; set; }
    
    public string? AccountNumber { get; set; }
    public decimal CurrentBalance { get; set; }

    [Required(ErrorMessage = "Amount is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
    public decimal Amount { get; set; }
}

public class TransactionHistoryViewModel
{
    public int AccountId { get; set; }
    public string? AccountNumber { get; set; }
    public decimal CurrentBalance { get; set; }
    public List<TransactionItemViewModel> Transactions { get; set; } = new();
}

public class TransactionItemViewModel
{
    public DateTime Date { get; set; }
    public string? Type { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public decimal BalanceAfter { get; set; }
}
