using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eBank_Bo.Data.Enums;

namespace eBank_Bo.Data.Models;

/// <summary>
/// Represents a financial transaction on an account.
/// </summary>
public class Transaction
{
    [Key]
    public int TransactionId { get; set; }

    [Required(ErrorMessage = "Amount is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "Transaction type is required.")]
    public TransactionType TransactionType { get; set; }

    [Required]
    public DateTime TransactionDate { get; set; } = DateTime.Now;

    [Required]
    public int AccountId { get; set; }

    [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters.")]
    public string? Description { get; set; }

    // Navigation property
    [ForeignKey(nameof(AccountId))]
    public Account? Account { get; set; }
}
