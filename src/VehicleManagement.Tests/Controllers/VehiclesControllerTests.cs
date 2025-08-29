using Microsoft.AspNetCore.Mvc;
using Moq;
using VehicleManagement.Controllers;
using VehicleManagement.Models;
using VehicleManagement.Services;

namespace VehicleManagement.Tests.Controllers
{
    public class VehiclesControllerTests
    {
        [Fact]
        public void GetVehicles_ReturnsOkWithVehicles()
        {
            var mockService = new Mock<IVehicleService>();
            mockService.Setup(s => s.GetAllVehicles()).Returns([new Vehicle { Id = 1, Name = "Car" }]);
            var controller = new VehiclesController(mockService.Object);

            var result = controller.GetVehicles();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var vehicles = Assert.IsType<List<Vehicle>>(okResult.Value);
            Assert.Single(vehicles);
        }

        [Fact]
        public void GetVehicleDetails_ReturnsNotFound_WhenVehicleDoesNotExist()
        {
            var mockService = new Mock<IVehicleService>();
            mockService.Setup(s => s.GetVehicleById(It.IsAny<int>())).Returns((Vehicle?)null);
            var controller = new VehiclesController(mockService.Object);

            var result = controller.GetVehicleDetails(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void AddVehicle_ReturnsOk()
        {
            var mockService = new Mock<IVehicleService>();
            var controller = new VehiclesController(mockService.Object);
            var vehicle = new Vehicle { Id = 1, Name = "Car" };

            var result = controller.AddVehicle(vehicle);

            Assert.IsType<OkResult>(result);
            mockService.Verify(s => s.AddVehicle(vehicle), Times.Once);
        }

        [Fact]
        public void UpdateVehicle_ReturnsOk()
        {
            var mockService = new Mock<IVehicleService>();
            var controller = new VehiclesController(mockService.Object);
            var vehicle = new Vehicle { Id = 1, Name = "Car" };

            var result = controller.UpdateVehicle(1, vehicle);

            Assert.IsType<OkResult>(result);
            mockService.Verify(s => s.UpdateVehicle(1, vehicle), Times.Once);
        }

        [Fact]
        public void DeleteVehicle_ReturnsNotFound_WhenDeleteFails()
        {
            var mockService = new Mock<IVehicleService>();
            mockService.Setup(s => s.DeleteVehicle(It.IsAny<int>())).Returns(false);
            var controller = new VehiclesController(mockService.Object);

            var result = controller.DeleteVehicle(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteVehicle_ReturnsOk_WhenDeleteSucceeds()
        {
            var mockService = new Mock<IVehicleService>();
            mockService.Setup(s => s.DeleteVehicle(It.IsAny<int>())).Returns(true);
            var controller = new VehiclesController(mockService.Object);

            var result = controller.DeleteVehicle(1);

            Assert.IsType<OkResult>(result);
        }
    }
}
