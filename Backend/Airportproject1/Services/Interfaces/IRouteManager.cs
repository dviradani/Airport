using Airportproject1.Models;

namespace Airportproject.Client.Services.Interfaces
{
    public interface IRouteManager
    {
        Station[] Route { get; set; }
        public event Action<Flight> FlightLeftTheRoute;
        Task EnterStation(Flight flight);
        Task ExitStation(Flight flight);
    }
}