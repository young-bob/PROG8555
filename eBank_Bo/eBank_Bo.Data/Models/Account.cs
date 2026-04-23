using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eBank_Bo.Data.Enums;

namespace eBank_Bo.Data.Models;

/// <summary>
/// Represents a bank account belonging to a customer.
/// </summary>
public class Account
{
    [Key]
    public int AccountId { get; set; }

    [Required(ErrorMessage = "Account number is required.")]
    [RegularExpression(@"^ACC-\d{5}$", ErrorMessage = "Account number must follow the format 'ACC-XXXXX'")]
    public required string AccountNumber { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Balance cannot be negative.")]
    public decimal Balance { get; set; }

    [Required(ErrorMessage = "Account type is required.")]
    public AccountType AccountType { get; set; }

    [Required]
    public int CustomerId { get; set; }

    // Navigation properties
    [ForeignKey(nameof(CustomerId))]
    public Customer? Customer { get; set; }

    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
