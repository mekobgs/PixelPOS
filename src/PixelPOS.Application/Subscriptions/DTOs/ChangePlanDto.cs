namespace PixelPOS.Application.Subscriptions.DTOs;

public class ChangePlanDto
{
    public int CompanyId { get; set; }
    public int NewPlanId { get; set; }
    public string? PaymentStatus { get; set; }
}