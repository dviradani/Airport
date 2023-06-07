using Airportproject.Client.Services.Interfaces;
using Airportproject.Client.Services.signalR;
using Airportproject1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Airportproject1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly IFlightsManager _flightManager;
        private readonly ISimulator _simulator;

        public AirportController(IFlightsManager flightManager, ISimulator simulator, HubContextService hubService)
        {
            _flightManager = flightManager;
            _simulator = simulator;
        }

        [HttpGet("depatures")]
        public IEnumerable<Flight> GetDepatures()
        {
            return _flightManager.Flights.Where(f => f.isDeparting).ToList();
        }
        [HttpGet("arrivals")]
        public IEnumerable<Flight> GetArrivals()
        {
            return _flightManager.Flights.Where(f => !f.isDeparting).ToList();
        }
        [HttpGet("start")]
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
