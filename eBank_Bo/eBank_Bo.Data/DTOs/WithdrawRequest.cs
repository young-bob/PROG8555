using System.ComponentModel.DataAnnotations;

namespace eBank_Bo.Data.DTOs;

/// <summary>
/// DTO for withdrawal requests.
/// </summary>
public class WithdrawRequest
{
    [Required]
    public int AccountId { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
    public decimal Amount { get; set; }
}
