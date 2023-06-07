using Airportproject1.Models;
using Microsoft.AspNetCore.SignalR;
namespace Airportproject.Client.Services.signalR;
public class HubContextService
{
    private readonly IHubContext<AirportHub> _hubContext;

    public HubContextService(IHubContext<AirportHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task RouteState(Station[] route)
    {
        await _hubContext.Clients.All.SendAsync("GetRoute", route);
    }

    public async Task SendFlight(List<Flight> flights)
    {
        await _hubContext.Clients.All.SendAsync("GetFlights", flights);
    }
}
