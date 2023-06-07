using Airportproject1.Models;

namespace Airportproject.Client.Services.Repositories
{
    public interface IMainRepository
    {
        Task SaveFlight(Flight flight);
        Task UpdateRouteState(Station[] route);
    }
}