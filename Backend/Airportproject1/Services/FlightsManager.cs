using Airportproject.Client.Services.Interfaces;
using Airportproject.Client.Services.Repositories;
using Airportproject.Client.Services.signalR;
using Airportproject1.Models;

namespace Airportproject1.BL
{
    /// <summary>
    /// This class is responsible to the flight list
    /// </summary>
    public class FlightsManager : IFlightsManager
    {
        public List<Flight> Flights { get; private set; }
        private readonly IRouteManager _routeManager;
        private readonly HubContextService _hubService;
        private readonly IMainRepository _repo;
        public FlightsManager(IRouteManager routeManager, HubContextService hubService , IMainRepository repo)
        {
            _repo = repo;
            _routeManager = routeManager;
            Flights = new List<Flight>();
            _hubService = hubService;

            // Remove a flight from the list when the flight leaves the airport
            _routeManager.FlightLeftTheRoute += RemoveFlight;
        }
        public async Task AddFlight(Flight flight)
        {
            Flights.Add(flight);
                 await _hubService.SendFlight(Flights.ToList());
            _ = _repo.SaveFlight(flight);
            Fly(flight);
        }

        private void RemoveFlight(Flight flight)
        {
            Flights.Remove(flight);
            _ = _hubService.SendFlight(Flights.ToList());
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
