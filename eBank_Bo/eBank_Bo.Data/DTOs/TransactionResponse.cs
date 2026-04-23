namespace eBank_Bo.Data.DTOs;

/// <summary>
/// DTO for transaction response.
/// </summary>
public class TransactionResponse
{
    public int TransactionId { get; set; }
    public decimal Amount { get; set; }
    public required string TransactionType { get; set; }
    public DateTime TransactionDate { get; set; }
    public string? Description { get; set; }
}
