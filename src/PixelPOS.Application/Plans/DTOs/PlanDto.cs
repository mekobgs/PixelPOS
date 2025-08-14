namespace PixelPOS.Application.Plans.DTOs;

public class PlanDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal? Price { get; set; }
    public int? MaxUsers { get; set; }
    public int? MaxBranches { get; set; }
    public decimal? MonthlyInvoiceLimit { get; set; }
    public bool EnableElectronicInvoicing { get; set; }
    public bool EnableReports { get; set; }
    public bool EnableIntegrations { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}
