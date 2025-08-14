namespace PixelPOS.Application.Companies.DTOs;

public class PagedCompaniesDto
{
    public List<CompanyDto> Data { get; set; } = new();
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
}