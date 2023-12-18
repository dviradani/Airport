using AirportBackend.Models;

namespace AirportBackend.Services.Interfaces
{
    public interface IFlightsManager
    {
        List<Flight> Flights { get; }

        Task AddFlight(Flight flight);
    }
}