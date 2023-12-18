using AirportBackend.Models;

namespace AirportBackend.Services.Interfaces
{
    public interface IRouteManager
    {
        Station[] Route { get; set; }
        public event Action<Flight> FlightLeftTheRoute;
        Task EnterStation(Flight flight);
        Task ExitStation(Flight flight);
    }
}