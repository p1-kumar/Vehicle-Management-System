using Microsoft.AspNetCore.Mvc;
using Moq;
using VehicleManagement.Controllers;
using VehicleManagement.Models;
using VehicleManagement.Services;

namespace VehicleManagement.Tests.Controllers
{
    public class TripsControllerTests
    {
        [Fact]
        public void GetTrips_ReturnsOkWithTrips()
        {
            var mockService = new Mock<ITripService>();
            mockService.Setup(s => s.GetTripsByVehicleId(It.IsAny<int>())).Returns(new List<Trip> { new Trip { Id = 1, VehicleId = 1 } });
            var controller = new TripsController(mockService.Object);

            var result = controller.GetTrips(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var trips = Assert.IsType<List<Trip>>(okResult.Value);
            Assert.Single(trips);
        }

        [Fact]
        public void GetTrip_ReturnsNotFound_WhenTripDoesNotExist()
        {
            var mockService = new Mock<ITripService>();
            mockService.Setup(s => s.GetTrip(It.IsAny<int>())).Returns((Trip?)null);
            var controller = new TripsController(mockService.Object);

            var result = controller.GetTripDetails(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void AddTrip_ReturnsOk()
        {
            var mockService = new Mock<ITripService>();
            var controller = new TripsController(mockService.Object);
            var trip = new Trip { Id = 1, VehicleId = 1 };

            var result = controller.AddTrip(trip);

            Assert.IsType<OkResult>(result);
            mockService.Verify(s => s.AddTrip(trip), Times.Once);
        }

        [Fact]
        public void UpdateTrip_ReturnsOk()
        {
            var mockService = new Mock<ITripService>();
            var controller = new TripsController(mockService.Object);
            var trip = new Trip { Id = 1, VehicleId = 1 };

            var result = controller.UpdateTrip(1, trip);

            Assert.IsType<OkResult>(result);
            mockService.Verify(s => s.UpdateTrip(1, trip), Times.Once);
        }

        [Fact]
        public void DeleteTrip_ReturnsNotFound_WhenDeleteFails()
        {
            var mockService = new Mock<ITripService>();
            mockService.Setup(s => s.DeleteTrip(It.IsAny<int>())).Returns(false);
            var controller = new TripsController(mockService.Object);

            var result = controller.DeleteTrip(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteTrip_ReturnsOk_WhenDeleteSucceeds()
        {
            var mockService = new Mock<ITripService>();
            mockService.Setup(s => s.DeleteTrip(It.IsAny<int>())).Returns(true);
            var controller = new TripsController(mockService.Object);

            var result = controller.DeleteTrip(1);

            Assert.IsType<OkResult>(result);
        }
    }
}
