namespace PixelPOS.Application.Companies.DTOs
{

    public class CreateCompanyDto
    {
        public string Name { get; set; } = string.Empty;
        public string? TaxId { get; set; }
        public string? Address { get; set; }
        public string? LogoUrl { get; set; }
        public int PlanId { get; set; }
        public string? PaymentStatus { get; set; } // Optional for subscription
    }
}
