using AirportBackend.Services.Enums;
using AirportBackend.Services.Interfaces;
using AirportBackend.Models;
using Timer = System.Timers.Timer;

namespace AirportBackend.Services
{
    public class Simulator : ISimulator
    {
        private readonly IFlightsManager _flightManager;
        readonly Timer _timer = new();
        readonly Random _random = new();

        public bool IsStarted { get; private set; }

        public Simulator(IFlightsManager flightManager)
        {
            _flightManager = flightManager;
        }
        public void Start(int interval = 5000)
        {
            if (!IsStarted)
            {
                _timer.Interval = interval;
                _timer.Elapsed += CreateRandomFlight;
                _timer.AutoReset = true;
                _timer.Enabled = true;
                IsStarted = true;
            }
        }
        private void CreateRandomFlight(object? sender, System.Timers.ElapsedEventArgs e)
        {
            int totalCountries = Enum.GetNames(typeof(Countries)).Length;
            int randomIndex = _random.Next(0, totalCountries);
            var flight = new Flight
            {
                FlightNumber = _random.Next(10000),
                Destination = Enum.GetName(typeof(Countries), randomIndex),
                IsDeparting = _random.Next(2) == 0,
            };
            Console.WriteLine($"Flight {flight.FlightNumber} {flight.Destination} Created at time {DateTime.Now} Departing:{flight.IsDeparting}");
            Console.WriteLine($"------------------------------------------------------------------------------------------------------------");
            _flightManager.AddFlight(flight);
        }
    }
}
