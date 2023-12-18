using AirportBackend.Models;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
namespace AirportBackend.Services.signalR
{
    public class AirportHub : Hub
    {
        public async Task SendRouteState(Station[] route)
        {
            await Clients.All.SendAsync("GetRoute", route);
        }

        public async Task SendDepartingFlights(List<Flight> departures)
        {
            await Clients.All.SendAsync("GetDepartingFlights", departures);
        }

        public async Task SendArrivingFlights(List<Flight> arrivals)
        {
            await Clients.All.SendAsync("GetArrivingFlights", arrivals);
        }
    }
}