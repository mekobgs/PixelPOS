namespace PixelPOS.Domain.Entities;

/// <summary>
/// Company/Tenant entity for SaaS system.
/// </summary>
public class Company
{
    /// <summary>
    /// Primary key (Identity).
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Unique company legal/commercial name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Tax ID or fiscal identification (optional, country-specific).
    /// </summary>
    public string? TaxId { get; set; }

    /// <summary>
    /// Company address (optional).
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Date and time when the company was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Date and time when the company was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Indicates if the company is active (not deleted/disabled).
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// URL to the company's logo image (optional).
    /// </summary>
    public string? LogoUrl { get; set; }

    /// <summary>
    /// (Optionally) Navigation to audit or related entities in future.
    /// </summary>
    // public ICollection<AuditLog>? AuditLogs { get; set; }

    // Navigation property: All subscriptions (active and historic).
    public ICollection<Subscription>? Subscriptions { get; set; }

    // Helper: Get the active subscription (should be loaded via Include in EF Core).
    public Subscription? ActiveSubscription =>
        Subscriptions?.FirstOrDefault(s => s.IsActive && (s.EndDate == null || s.EndDate > DateTime.UtcNow));
}
