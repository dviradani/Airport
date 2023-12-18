using AirportBackend.Data;
using AirportBackend.Models;

namespace AirportBackend.Services.Repositories
{
    public class MainRepository : IMainRepository
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public MainRepository(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;

        }

        public async Task SaveFlight(Flight flight)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var myScopedService = scope.ServiceProvider.GetService<AirportContext>();
                if (myScopedService != null)
                {
                    myScopedService.Flights.Add(flight);
                    await myScopedService.SaveChangesAsync();
                }
            }
        }

        public async Task UpdateRouteState(Station[] route)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var myScopedService = scope.ServiceProvider.GetService<AirportContext>();

                if (myScopedService != null)
                {
                    foreach (var station in route)
                    {
                        var existingStation = await myScopedService.Stations.FindAsync(station.Id);
                        if (existingStation != null)
                        {
                            if (station.Flight == null)
                                existingStation.FlightId = 0;
                            else
                                existingStation.FlightId = station.Flight.Id;
                        }
                    }
                    await myScopedService.SaveChangesAsync();
                }
            }
        }

    }
}
