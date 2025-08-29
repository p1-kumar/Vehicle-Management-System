using Microsoft.AspNetCore.Mvc;
using Moq;
using VehicleManagement.Controllers;
using VehicleManagement.Models;
using VehicleManagement.Services;

namespace VehicleManagement.Tests.Controllers
{
    public class MaintenancesControllerTests
    {
        [Fact]
        public void GetMaintenance_ReturnsNotFound_WhenNoMaintenance()
        {
            var mockService = new Mock<IMaintenanceService>();
            mockService.Setup(s => s.GetMaintenanceByVehicleId(It.IsAny<int>())).Returns((Maintenance?)null);
            var controller = new MaintenancesController(mockService.Object);

            var result = controller.GetMaintenance(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetMaintenance_ReturnsOk_WhenMaintenanceExists()
        {
            var mockService = new Mock<IMaintenanceService>();
            mockService.Setup(s => s.GetMaintenanceByVehicleId(It.IsAny<int>())).Returns(new Maintenance { Id = 1, VehicleId = 1 });
            var controller = new MaintenancesController(mockService.Object);

            var result = controller.GetMaintenance(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<Maintenance>(okResult.Value);
        }

        [Fact]
        public void ScheduleMaintenance_ReturnsBadRequest_WhenFails()
        {
            var mockService = new Mock<IMaintenanceService>();
            mockService.Setup(s => s.ScheduleMaintenance(It.IsAny<int>(), It.IsAny<DateTime>())).Returns(false);
            var controller = new MaintenancesController(mockService.Object);
            var maintenance = new Maintenance { Id = 1, VehicleId = 1, ScheduledDate = DateTime.Now };

            var result = controller.ScheduleMaintenance(maintenance);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void ScheduleMaintenance_ReturnsOk_WhenSucceeds()
        {
            var mockService = new Mock<IMaintenanceService>();
            mockService.Setup(s => s.ScheduleMaintenance(It.IsAny<int>(), It.IsAny<DateTime>())).Returns(true);
            var controller = new MaintenancesController(mockService.Object);
            var maintenance = new Maintenance { Id = 1, VehicleId = 1, ScheduledDate = DateTime.Now };

            var result = controller.ScheduleMaintenance(maintenance);

            Assert.IsType<OkResult>(result);
        }
    }
}
