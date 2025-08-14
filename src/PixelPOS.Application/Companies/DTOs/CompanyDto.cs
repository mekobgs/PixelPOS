using PixelPOS.Application.Subscriptions.DTOs;

namespace PixelPOS.Application.Companies.DTOs
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? TaxId { get; set; }
        public string? Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public string? LogoUrl { get; set; }
        public List<SubscriptionDto> Subscriptions { get; set; } = new();
        public SubscriptionDto? ActiveSubscription { get; set; }
    }
}
