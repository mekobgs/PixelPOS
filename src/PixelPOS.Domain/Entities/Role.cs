namespace PixelPOS.Domain.Entities;

/// <summary>
/// Role (permission group) for users.
/// </summary>
public class Role
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // Example: Admin, Cashier, Supervisor
    public string? Description { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}