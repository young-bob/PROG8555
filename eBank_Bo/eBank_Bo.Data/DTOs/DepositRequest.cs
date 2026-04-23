using System.ComponentModel.DataAnnotations;

namespace eBank_Bo.Data.DTOs;

/// <summary>
/// DTO for deposit requests.
/// </summary>
public class DepositRequest
{
    [Required]
    public int AccountId { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
    public decimal Amount { get; set; }
}
