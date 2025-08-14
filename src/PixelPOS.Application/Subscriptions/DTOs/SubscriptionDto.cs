namespace PixelPOS.Application.Subscriptions.DTOs;

public class SubscriptionDto
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public int PlanId { get; set; }
    public string PlanName { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsActive { get; set; }
    public string? PaymentStatus { get; set; }
}