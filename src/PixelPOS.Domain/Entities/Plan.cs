namespace PixelPOS.Domain.Entities;

/// <summary>
/// Subscription plan entity for SaaS system.
/// </summary>
public class Plan
{
    /// <summary>
    /// Primary key (Identity).
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Unique plan name (e.g., Free, Basic, Premium).
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Plan price per period (e.g., monthly fee).
    /// </summary>
    public decimal? Price { get; set; }

    /// <summary>
    /// Maximum number of users allowed for this plan.
    /// </summary>
    public int? MaxUsers { get; set; }

    /// <summary>
    /// Maximum number of branches (outlets) allowed for this plan.
    /// </summary>
    public int? MaxBranches { get; set; }

    /// <summary>
    /// Monthly invoice/ticket emission limit for this plan.
    /// </summary>
    public decimal? MonthlyInvoiceLimit { get; set; }

    /// <summary>
    /// Enables electronic invoicing features for this plan.
    /// </summary>
    public bool EnableElectronicInvoicing { get; set; }

    /// <summary>
    /// Enables advanced reports for this plan.
    /// </summary>
    public bool EnableReports { get; set; }

    /// <summary>
    /// Enables third-party integrations for this plan.
    /// </summary>
    public bool EnableIntegrations { get; set; }

    /// <summary>
    /// Plan description (optional).
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Indicates if the plan is active (not deleted or deprecated).
    /// </summary>
    public bool IsActive { get; set; } = true;

    // Navigation property: All subscriptions to this plan.
    public ICollection<Subscription>? Subscriptions { get; set; }
}
