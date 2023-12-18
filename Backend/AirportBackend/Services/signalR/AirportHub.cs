using AirportBackend.Models;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
namespace AirportBackend.Services.signalR
{
    public class AirportHub : Hub
    {
        public async Task RouteState(Station[] route)
        {
            await Clients.All.SendAsync("GetRoute", route);
        }
        public async Task DeparturesState(List<Flight> departures)
        {
            await Clients.All.SendAsync("GetDeparture", departures);
        }
        public async Task ArrivalsState(List<Flight> arrivals)
        {
            await Clients.All.SendAsync("GetArrivale", arrivals);
        }
    }
}