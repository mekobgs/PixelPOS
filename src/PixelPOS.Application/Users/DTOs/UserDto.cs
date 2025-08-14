namespace PixelPOS.Application.Users.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public int? BranchId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? FullName { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLogin { get; set; }
    public List<string> Roles { get; set; } = new();
}