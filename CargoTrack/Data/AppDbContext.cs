using Microsoft.EntityFrameworkCore;
using CargoTrack.Entities;

namespace CargoTrack.Data;


public class AppDbContext : DbContext
{
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    public DbSet<Shipment> Shipments =>Set<Shipment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Shipment>(entity =>
        {
            entity.HasIndex(s=>s.TrackingNumber).IsUnique();
            entity.Property(s=>s.TrackingNumber).HasMaxLength(20);
            entity.Property(s=>s.SenderCity).HasMaxLength(50);
            entity.Property(s=>s.ReceiverCity).HasMaxLength(50);
            entity.Property(s=>s.Status).HasConversion<string>();

        });
    }



}