
using cSharpAdvanced_georgeWahba_s1185726.Models;
using Microsoft.EntityFrameworkCore;

namespace cSharpAdvanced_georgeWahba_s1185726.Data
{
    public class cSharpAdvanced_georgeWahba_s1185726Context : DbContext
    {
        public DbSet<cSharpAdvanced_georgeWahba_s1185726.Models.Image> Image { get; set; } = default!;
        public DbSet<cSharpAdvanced_georgeWahba_s1185726.Models.Customer> Customer { get; set; } = default!;
        public DbSet<cSharpAdvanced_georgeWahba_s1185726.Models.Landlord> Landlord { get; set; } = default!;
        public DbSet<cSharpAdvanced_georgeWahba_s1185726.Models.Location> Location { get; set; } = default!;
        public DbSet<cSharpAdvanced_georgeWahba_s1185726.Models.Reservation> Reservation { get; set; } = default!;

        public cSharpAdvanced_georgeWahba_s1185726Context(DbContextOptions<cSharpAdvanced_georgeWahba_s1185726Context> options)
            : base(options)
        {
            Database.EnsureCreated(); // Ensure database is created
            SeedDatabase(); // Seed the database
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>()
               .Property(e => e.Type)
               .HasConversion<string>();

            modelBuilder.Entity<Location>()
                .Property(e => e.Feature)
                .HasConversion<string>();

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.CustomerId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Location)
                .WithMany(l => l.Reservations)
                .HasForeignKey(r => r.LocationId);

            modelBuilder.Entity<Image>()
                .HasOne(i => i.Location)
                .WithMany(l => l.Images)
                .HasForeignKey(i => i.LocationId);

            modelBuilder.Entity<Location>()
                .HasOne(l => l.Landlord)
                .WithMany(ll => ll.Locations)
                .HasForeignKey(l => l.LandlordId);

            modelBuilder.Entity<Landlord>()
                .HasOne(l => l.Avatar)
                .WithMany()
                .HasForeignKey(l => l.AvatarId) // Correctly linking AvatarId as the foreign key
                .IsRequired(false); // If AvatarId is nullable

            modelBuilder.Entity<Location>()
               .Property(e => e.Feature)
               .HasConversion<int>();
        }

        private void SeedDatabase()
        {
            if (!Customer.Any())
            {
                DbSeeder.Seed(this);
            }
        }
    }
}