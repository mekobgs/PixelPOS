namespace PixelPOS.Application.Plans.DTOs;

public class PagedPlansDto
{
    public List<PlanDto> Data { get; set; } = new();
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
}
