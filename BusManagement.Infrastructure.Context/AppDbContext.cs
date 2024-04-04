using BusManagement.Core.Data;
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
        foreach (var relation in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relation.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}
