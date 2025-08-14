namespace PixelPOS.Domain.Entities;

/// <summary>
/// Company subscription to a plan, including payment and status tracking.
/// </summary>
public class Subscription
{
    public int Id { get; set; }

    // Foreign key: Company owning the subscription.
    public int CompanyId { get; set; }

    // Foreign key: Subscribed plan.
    public int PlanId { get; set; }

    // Subscription start date (UTC).
    public DateTime StartDate { get; set; } = DateTime.UtcNow;

    // Subscription end/expiration date (UTC, null if not set).
    public DateTime? EndDate { get; set; }

    // Indicates if this is the currently active subscription.
    public bool IsActive { get; set; } = true;

    // Payment status (Paid, Trial, Pending, Cancelled, etc.)
    public string? PaymentStatus { get; set; }

    // Navigation property: Company.
    public Company? Company { get; set; }

    // Navigation property: Plan.
    public Plan? Plan { get; set; }
}
