namespace PixelPOS.Domain.Entities;

/// <summary>
/// User of the POS system.
/// </summary>
public class User
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public int? BranchId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? FullName { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLogin { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    // Navigation
    public Company? Company { get; set; }
    public Branch? Branch { get; set; }
}