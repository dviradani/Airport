using AirportBackend.Models;

namespace AirportBackend.Services.Repositories
{
    public interface IMainRepository
    {
        Task SaveFlight(Flight flight);
        Task UpdateRouteState(Station[] route);
    }
}