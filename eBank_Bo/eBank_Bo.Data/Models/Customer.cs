using System.ComponentModel.DataAnnotations;

namespace eBank_Bo.Data.Models;

/// <summary>
/// Represents a customer in the banking system.
/// </summary>
public class Customer
{
    [Key]
    public int CustomerId { get; set; }

    [Required(ErrorMessage = "First name is required.")]
    [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
    public required string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
    public required string LastName { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public required string Email { get; set; }

    public DateTime? DateOfBirth { get; set; }

    // Navigation property
    public ICollection<Account> Accounts { get; set; } = new List<Account>();
}
