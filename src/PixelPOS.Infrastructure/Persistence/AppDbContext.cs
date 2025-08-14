using Microsoft.EntityFrameworkCore;
using PixelPOS.Domain.Entities;

namespace PixelPOS.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Plan> Plans => Set<Plan>();
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<Subscription> Subscriptions => Set<Subscription>();
    public DbSet<Branch> Branches => Set<Branch>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<PasswordResetToken> PasswordResetTokens => Set<PasswordResetToken>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Plan - Subscription: One to Many
        modelBuilder.Entity<Plan>()
            .HasMany(p => p.Subscriptions)
            .WithOne(s => s.Plan)
            .HasForeignKey(s => s.PlanId);

        // Company - Subscription: One to Many
        modelBuilder.Entity<Company>()
            .HasMany(c => c.Subscriptions)
            .WithOne(s => s.Company)
            .HasForeignKey(s => s.CompanyId);

        // Unique: Only one active subscription per company.
        modelBuilder.Entity<Subscription>()
            .HasIndex(s => new { s.CompanyId, s.IsActive })
            .HasFilter("[IsActive] = 1")
            .IsUnique();

        // Other configurations...
    }
}
