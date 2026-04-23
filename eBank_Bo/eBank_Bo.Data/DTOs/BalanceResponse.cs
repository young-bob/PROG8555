namespace eBank_Bo.Data.DTOs;

/// <summary>
/// DTO for balance response.
/// </summary>
public class BalanceResponse
{
    public int AccountId { get; set; }
    public required string AccountNumber { get; set; }
    public decimal Balance { get; set; }
}
