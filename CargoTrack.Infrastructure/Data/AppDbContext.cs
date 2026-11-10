using Microsoft.EntityFrameworkCore;
using CargoTrack.Domain.Entities;

namespace CargoTrack.Infrastructure.Data;


public class AppDbContext : DbContext
{
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    public DbSet<Shipment> Shipments =>Set<Shipment>();
    public DbSet<User> Users=>Set<User>();

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

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(u=> u.Email).IsUnique();
            entity.Property(u=> u.Email).HasMaxLength(100);
            entity.Property(u=> u.Role).HasConversion<string>();
        });







    }



}