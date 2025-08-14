namespace PixelPOS.Application.Users.DTOs;

public class UpdateUserDto
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public int? BranchId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? FullName { get; set; }
    public bool IsActive { get; set; }
    public List<int> RoleIds { get; set; } = new();
}
