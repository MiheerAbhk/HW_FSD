using Microsoft.EntityFrameworkCore;
using Case_Study.Models;

namespace Case_Study.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightRoute> FlightRoutes { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Cancellation> Cancellations { get; set; }
        public DbSet<TokenStore> TokenStores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Seat>()
                .HasIndex(s => new { s.FlightRouteId, s.SeatNumber })
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Flight>()
                .HasIndex(f => f.FlightNumber)
                .IsUnique();

            modelBuilder.Entity<Airline>()
                .HasIndex(a => a.Code)
                .IsUnique();

            modelBuilder.Entity<Airline>()
                .HasOne(a => a.Owner)
                .WithMany(u => u.OwnedAirlines)
                .HasForeignKey(a => a.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                    .HasOne(b => b.Cancellation)
                    .WithOne(c => c.Booking)
                    .HasForeignKey<Cancellation>(c => c.BookingId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FlightRoute>()
                .HasOne(fr => fr.Owner)
                .WithMany(u => u.OwnedFlights)
                .HasForeignKey(fr => fr.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cancellation>()
                .HasOne(c => c.Admin)
                .WithMany()
                .HasForeignKey(c => c.ProcessedBy)
                .OnDelete(DeleteBehavior.Restrict);
        }
    
    }
}