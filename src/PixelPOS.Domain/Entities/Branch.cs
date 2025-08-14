namespace PixelPOS.Domain.Entities
{
    /// <summary>
    /// Physical branch (outlet/store) for a company.
    /// </summary>
    public class Branch
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? PrinterName { get; set; }
        public string? OpeningHours { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation
        public Company? Company { get; set; }
    }
}
