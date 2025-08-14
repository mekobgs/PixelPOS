namespace PixelPOS.Application.Branches.DTOs;

public class PagedBranchesDto
{
    public List<BranchDto> Data { get; set; } = new();
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
}