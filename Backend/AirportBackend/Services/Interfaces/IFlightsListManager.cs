using AirportBackend.Models;

namespace AirportBackend.Services.Interfaces
{
    public interface IFlightsListManager
    {
        List<Flight> Flights { get; }

        Task AddFlight(Flight flight);
    }
}