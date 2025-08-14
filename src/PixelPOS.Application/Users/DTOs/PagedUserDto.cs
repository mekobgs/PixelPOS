namespace PixelPOS.Application.Users.DTOs;

public class PagedUsersDto
{
    public List<UserDto> Data { get; set; } = new();
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
}