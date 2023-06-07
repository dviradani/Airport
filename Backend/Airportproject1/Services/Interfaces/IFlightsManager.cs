using Airportproject1.Models;

namespace Airportproject.Client.Services.Interfaces
{
    public interface IFlightsManager
    {
        List<Flight> Flights { get; }

        Task AddFlight(Flight flight);
    }
}