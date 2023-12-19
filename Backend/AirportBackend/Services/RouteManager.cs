using AirportBackend.Models;
using AirportBackend.Services.Interfaces;
using AirportBackend.Services.Repositories;
using AirportBackend.Services.signalR;
namespace AirportBackend.Services
{
    public class RouteManager : IRouteManager
    {
        private readonly Queue<Flight> _queue67 = new();
        private readonly HubContextService _signalRService;
        private readonly SemaphoreSlim _sem = new(1);
        private readonly IMainRepository _repo;

        public event Action<Flight>? FlightLeftTheRoute;
        public Station[] Route { get; set; }

        public RouteManager(HubContextService service, IMainRepository repo)
        {
            _repo = repo;
            _signalRService = service;

            //Initialize Stations
            Route = new Station[8];
            for (int i = 1; i <= Route.Length; i++)
            {
                Route[i - 1] = new Station
                {
                    Name = $"Station {i}",
                    Id = i
                };
            }

            // Station 6 & 7 have the same queue
            Route[5].Queue = _queue67;
            Route[6].Queue = _queue67;
        }

        /// <summary>
        /// Resopnsible to check and handle the flight's next steps
        /// </summary>
        public async Task EnterStation(Flight flight)
        {
            var nextStation = FindNextStation(flight);

            // If the airplane is on its last station , we'll get -1
            if (nextStation == -1)
            {
                _ = _repo.UpdateRouteState(Route);
                await _signalRService.SendRouteState(Route);
                Console.WriteLine($"Flight {flight.FlightNumber} left the Terminal at time {DateTime.Now} Departing:{flight.IsDeparting}");
                FlightLeftTheRoute?.Invoke(flight);
                return;
            }
            // Becuase of the sempaphore, only 1 thread will can checking its next station's state
            await _sem.WaitAsync();

            if (Route![nextStation].Flight == null)
            {
                _sem.Release();
                Route[nextStation].Flight = flight;
                flight.CurrentStation = Route[nextStation];
                _ = _repo.UpdateRouteState(Route);
                await _signalRService.SendRouteState(Route);
                Console.WriteLine($"Flight {flight.FlightNumber} Entered {flight.CurrentStation.Name} at time {DateTime.Now} Departing:{flight.IsDeparting}  ");
                Console.WriteLine($"------------------------------------------------------------------------------------------------------------");
                Thread.Sleep(3000);
                await ExitStation(flight);
                await EnterStation(flight);
            }
            // Checks if to enter station 7
            else if (nextStation == 5 && Route[5].Flight != null && Route[6].Flight == null)
            {
                _sem.Release();
                Route[6].Flight = flight;
                flight.CurrentStation = Route[6];
                _ = _repo.UpdateRouteState(Route);
                await _signalRService.SendRouteState(Route);
                Console.WriteLine($"Flight {flight.FlightNumber} Entered {flight.CurrentStation.Name} at time {DateTime.Now} Departing:{flight.IsDeparting} ");
                Console.WriteLine($"------------------------------------------------------------------------------------------------------------");
                Thread.Sleep(3000);
                await ExitStation(flight);
                await EnterStation(flight);
            }
            //If the next station is not empty
            else
            {
                _sem.Release();
                Route[nextStation].Queue.Enqueue(flight);
                _ = _repo.UpdateRouteState(Route);
                await _signalRService.SendRouteState(Route);
                Console.WriteLine($"Flight {flight.FlightNumber} Put in {Route[nextStation].Name} queue at time {DateTime.Now} Departing:{flight.IsDeparting} ");
                Console.WriteLine($"------------------------------------------------------------------------------------------------------------");
            }
        }
        public async Task ExitStation(Flight flight)
        {
            flight.CurrentStation!.Flight = null;
            _ = _repo.UpdateRouteState(Route);
            await _signalRService.SendRouteState(Route);
            Console.WriteLine($"Flight {flight.FlightNumber} Exited {flight.CurrentStation!.Name} at time {DateTime.Now} Departing:{flight.IsDeparting}");
            Console.WriteLine($"------------------------------------------------------------------------------------------------------------");
            if (flight.CurrentStation.Queue.Count > 0)
            {
                Flight newFlight = flight.CurrentStation.Queue.Dequeue();
                Console.WriteLine($"Flight {newFlight.FlightNumber} Released from {flight.CurrentStation.Name} queue at time {DateTime.Now} Departing:{newFlight.IsDeparting} ");
                Console.WriteLine($"------------------------------------------------------------------------------------------------------------");
                _ = _repo.UpdateRouteState(Route);
                await _signalRService.SendRouteState(Route);
                await EnterStation(newFlight);
            }
        }
        private static int FindNextStation(Flight flight)
        {
            if (!flight.IsDeparting)
            {
                // If the airplane needs to get into its first station on the route
                if (flight.CurrentStation == null)
                    return 0;
                return flight.CurrentStation.Name switch
                {
                    "Station 1" => 1,
                    "Station 2" => 2,
                    "Station 3" => 3,
                    "Station 4" => 4,
                    "Station 5" => 5,
                    _ => -1,
                };
            }
            else
            {
                // If the airplane needs to get into its first station on the route
                if (flight.CurrentStation == null)
                    return 5;
                return flight.CurrentStation.Name switch
                {
                    "Station 6" or "Station 7" => 7,
                    "Station 8" => 3,
                    _ => -1,
                };
            }
        }
    }
}
