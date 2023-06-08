using Airportproject.Client.Services.Interfaces;
using Airportproject1.Controllers;
using Airportproject1.Models;
using Moq;

namespace Airportproject.Tests.Controllers
{
    [TestClass]
    public class AirportControllerTests
    {
        private Mock<IFlightsManager> _flightManagerMock = null!;
        private Mock<ISimulator> _simulatorMock = null!;
        private AirportController _controller= null!;

        [TestInitialize]
        public void Setup()
        {
            _flightManagerMock = new Mock<IFlightsManager>();
            _simulatorMock = new Mock<ISimulator>();
            _controller = new AirportController(_flightManagerMock.Object, _simulatorMock.Object, null!);
        }

        [TestMethod]
        public void GetDepatures_ShouldReturnDepartingFlights()
        {
            // Arrange
            var departingFlights = new List<Flight>
            {
                new Flight { isDeparting = true },
                new Flight { isDeparting = true },
                new Flight { isDeparting = false }
            };
            _flightManagerMock.Setup(fm => fm.Flights).Returns(departingFlights);

            // Act
            var result = _controller.GetDepatures();

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.All(f => f.isDeparting));
        }

        [TestMethod]
        public void GetArrivals_ShouldReturnArrivingFlights()
        {
            // Arrange
            var arrivingFlights = new List<Flight>
            {
                new Flight { isDeparting = true },
                new Flight { isDeparting = false },
                new Flight { isDeparting = false }
            };
            _flightManagerMock.Setup(fm => fm.Flights).Returns(arrivingFlights);

            // Act
            var result = _controller.GetArrivals();

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.IsFalse(result.Any(f => f.isDeparting));
        }

        [TestMethod]
        public void Start_ShouldCallSimulatorStartMethod()
        {
            // Act
            _controller.Start();

            // Assert
            _simulatorMock.Verify(s => s.Start(5000), Times.Once);
        }

        [TestMethod]
        public void SendStatus_ShouldReturnSimulatorStatus()
        {
            // Arrange
            _simulatorMock.Setup(s => s.IsStarted).Returns(true);

            // Act
            var result = _controller.SendStatus();

            // Assert
            Assert.IsTrue(result);
        }
    }
}
