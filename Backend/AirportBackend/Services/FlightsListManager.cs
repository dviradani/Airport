using AirportBackend.Models;
using AirportBackend.Services.Interfaces;
using AirportBackend.Services.Repositories;
using AirportBackend.Services.signalR;

namespace AirportBackend.Services
{
    public class FlightsListManager : IFlightsListManager
    {
        public List<Flight> Flights { get; private set; } = new();
        private readonly IRouteManager _routeManager;
        private readonly HubContextService _hubService;
        private readonly IMainRepository _repo;
        public FlightsListManager(IRouteManager routeManager, HubContextService hubService, IMainRepository repo)
        {
            _repo = repo;
            _routeManager = routeManager;
            _hubService = hubService;

            // Remove a flight from the list when the flight leaves the airport
            _routeManager.FlightLeftTheRoute += RemoveFlight;
        }
        public async Task AddFlight(Flight flight)
        {
            Flights.Add(flight);
            await _hubService.SendFlights(Flights.ToList());
            _ = _repo.SaveFlight(flight);
            Fly(flight);
        }

        public void RemoveFlight(Flight flight)
        {
            Flights.Remove(flight);
            _ = _hubService.SendFlights(Flights.ToList());
        }

        private void Fly(Flight flight)
        {
            Task.Run(() =>
            {
                _routeManager.EnterStation(flight);
            });
        }
    }
}
