using AirportBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace AirportBackend.Data
{
    public class AirportContext : DbContext
    {
        public AirportContext(DbContextOptions<AirportContext> options) : base(options) { }

        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Station> Stations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Station>().HasData(
                new Station { Id = 1, Name = "Station 1", FlightId = 0 },
                new Station { Id = 2, Name = "Station 2", FlightId = 0 },
                new Station { Id = 3, Name = "Station 3", FlightId = 0 },
                new Station { Id = 4, Name = "Station 4", FlightId = 0 },
                new Station { Id = 5, Name = "Station 5", FlightId = 0 },
                new Station { Id = 6, Name = "Station 6", FlightId = 0 },
                new Station { Id = 7, Name = "Station 7", FlightId = 0 },
                new Station { Id = 8, Name = "Station 8", FlightId = 0 }
                );
        }
    }
}