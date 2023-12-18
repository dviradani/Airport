using AirportBackend.Services.Enums;
using AirportBackend.Services.Interfaces;
using AirportBackend.Models;
using Timer = System.Timers.Timer;

namespace AirportBackend.Services
{
    public class Simulator : ISimulator
    {
        private readonly IFlightsManager _flightManager;
        //private readonly AirportContext _context;
        Timer _timer = new();
        Random _random = new Random();
        public bool IsStarted { get; private set; }
        public Simulator(IFlightsManager flightManager)
        {

            _flightManager = flightManager;
            // _context = context;
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
            var flight = new Flight();
            flight.FlightNumber = _random.Next(10000);
            int totalCountries = Enum.GetNames(typeof(Countries)).Length;
            int randomIndex = _random.Next(0, totalCountries);
            flight.Name = Enum.GetName(typeof(Countries), randomIndex);
            flight.isDeparting = _random.Next(2) == 0;
            Console.WriteLine($"Flight {flight.FlightNumber} {flight.Name} Created at time {DateTime.Now} Departing:{flight.isDeparting}");
            Console.WriteLine($"------------------------------------------------------------------------------------------------------------");
            _flightManager.AddFlight(flight);
            //_context.Flights.Add(flight);
            //_context.SaveChangesAsync();
        }
    }
}
