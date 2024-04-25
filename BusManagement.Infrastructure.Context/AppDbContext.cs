using BusManagement.Core.Common.Enums;
using BusManagement.Core.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BusManagement.Infrastructure.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    #region DBSet

    public DbSet<Company> Companies { get; set; }
    public DbSet<School> Schools { get; set; }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<OffDuty> OffDuties { get; set; }
    public DbSet<Rent> Rents { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<VehicleBrand> VehicleBrands { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        foreach (var relation in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relation.DeleteBehavior = DeleteBehavior.Restrict;
        }

        builder.Entity<VehicleBrandTranslation>(b =>
        {
            b.HasKey(x => new { x.VehicleBrandId, x.Language });
        });
        builder
            .Entity<Vehicle>()
            .HasOne(v => v.Brand)
            .WithMany()
            .HasForeignKey(v => v.BrandId)
            .OnDelete(DeleteBehavior.Restrict);

        // Seed Data
        const string schoolTransportationManager = "fcdb4a01-a1be-4d5b-92e4-08b1163f47c7";
        const string companyTransportationManager = "0de8240e-0bfc-492d-9758-d041c1314812";
        builder
            .Entity<IdentityRole>()
            .HasData(
                new IdentityRole
                {
                    Id = schoolTransportationManager,
                    Name = RoleEnum.SchoolTransportationManager.ToString(),
                    NormalizedName = RoleEnum.SchoolTransportationManager.ToString().ToUpper(),
                    ConcurrencyStamp = "217ca5d6-29ce-4c73-8b92-de50c09f97f0"
                },
                new IdentityRole
                {
                    Id = companyTransportationManager,
                    Name = RoleEnum.CompanyTransportationManager.ToString(),
                    NormalizedName = RoleEnum.CompanyTransportationManager.ToString().ToUpper(),
                    ConcurrencyStamp = "678e1b6a-9a0c-4fdd-8bca-130ee693e2ae"
                }
            );
    }
}
