using AirportBackend.Models;
using AirportBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AirportBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly IFlightsListManager _flightsManager;
        private readonly ISimulator _simulator;

        public AirportController(IFlightsListManager flightManager, ISimulator simulator)
        {
            _flightsManager = flightManager;
            _simulator = simulator;
        }

        [HttpGet("departures")]
        public IEnumerable<Flight> GetDepartures()
        {
            return _flightsManager.Flights.Where(f => f.IsDeparting).ToList();
        }

        [HttpGet("arrivals")]
        public IEnumerable<Flight> GetArrivals()
        {
            return _flightsManager.Flights.Where(f => !f.IsDeparting).ToList();
        }

        [HttpPost("start")]
        // POST Method due to state changing
        public void Start()
        {
            _simulator.Start();
        }

        [HttpGet("status")]
        public bool SendStatus()
        {
            return _simulator.IsStarted;
        }

    }
}
