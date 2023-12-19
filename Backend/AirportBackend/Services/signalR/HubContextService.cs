using AirportBackend.Models;
using Microsoft.AspNetCore.SignalR;
namespace AirportBackend.Services.signalR;

public class AirportHub : Hub { }

public class HubContextService
{
    private readonly IHubContext<AirportHub> _hubContext;

    public HubContextService(IHubContext<AirportHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendRouteState(Station[] route)
    {
        await _hubContext.Clients.All.SendAsync("GetRouteState", route);
    }

    public async Task SendFlights(List<Flight> flights)
    {
        await _hubContext.Clients.All.SendAsync("GetFlights", flights);
    }
}
