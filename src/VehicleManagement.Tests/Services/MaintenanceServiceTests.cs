using Moq;
using VehicleManagement.Models;
using VehicleManagement.Services;
using VehicleManagement.DataSource;

namespace VehicleManagement.Tests.Services
{
    public class MaintenanceServiceTests
    {
        [Fact]
        public void GetMaintenanceByVehicleId_ReturnsMaintenance()
        {
            var mockStore = new Mock<IDataStore>();
            var maintenances = new List<Maintenance> { new() { Id = 1, VehicleId = 1 } };
            var dataStoreRecords = new DataStore { Maintenances = maintenances };
            mockStore.Setup(s => s.Records).Returns(dataStoreRecords);
            var service = new MaintenanceService(mockStore.Object);

            var result = service.GetMaintenanceByVehicleId(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.VehicleId);
        }

        [Fact]
        public void ScheduleMaintenance_UpdatesDateAndSaves()
        {
            var mockStore = new Mock<IDataStore>();
            var maintenance = new Maintenance { Id = 2, VehicleId = 2 };
            var dataStoreRecords = new DataStore { Maintenances = [maintenance] };
            mockStore.Setup(s => s.Records).Returns(dataStoreRecords);
            var service = new MaintenanceService(mockStore.Object);
            var newDate = new DateTime(2024, 6, 1);

            var result = service.ScheduleMaintenance(2, newDate);

            Assert.True(result);
            Assert.Equal(newDate, maintenance.ScheduledDate);
            mockStore.Verify(s => s.SaveData(), Times.Once);
        }

        [Fact]
        public void ScheduleMaintenance_ReturnsFalse_WhenNotFound()
        {
            var mockStore = new Mock<IDataStore>();
            var dataStoreRecords = new DataStore { Maintenances = [] };
            mockStore.Setup(s => s.Records).Returns(dataStoreRecords);
            var service = new MaintenanceService(mockStore.Object);

            var result = service.ScheduleMaintenance(99, DateTime.Now);

            Assert.False(result);
            mockStore.Verify(s => s.SaveData(), Times.Never);
        }
    }
}