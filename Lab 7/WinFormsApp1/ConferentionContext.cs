using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Models;

namespace WinFormsApp1
{
    public class ConferentionContext : DbContext
    {
        public DbSet<Conferention> Conferentions { get; set; } = null!;
        public DbSet<Section> Sections { get; set; } = null!;
        public DbSet<Performance> Performances { get; set; } = null!;
        public DbSet<Performancer> Performancers { get; set; } = null!;
        public DbSet<Building> Buildings { get; set; } = null!;
        public DbSet<Equipment> Equipment { get; set; } = null!;
        public DbSet<Room> Rooms { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies()
                   .UseSqlServer(@"Server=.\SQLEXPRESS;Database=ConferentionDb;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Conferention>()
                .HasOne(b => b.Building)
                .WithOne(c => c.Conferention)
                .HasForeignKey<Conferention>(f => f.BuildingId);

            builder.Entity<Performancer>()
                .HasMany(p => p.Performances)
                .WithOne(p1 => p1.Performancer)
                .HasForeignKey(f => f.PerformancerId);

            builder.Entity<Section>()
                .HasOne(s => s.Room)
                .WithOne(r => r.Section)
                .HasForeignKey<Section>(f => f.RoomId);

            builder.Entity<Building>()
                .HasMany(r => r.Rooms)
                .WithOne(b => b.Building)
                .HasForeignKey(f => f.BuildingId);

            builder.Entity<Conferention>()
                .HasMany(r => r.Sections)
                .WithOne(b => b.Conferention)
                .HasForeignKey(f => f.ConferentionId);
        }
    }
}
