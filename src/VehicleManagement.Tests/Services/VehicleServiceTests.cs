using Moq;
using VehicleManagement.Models;
using VehicleManagement.Services;
using VehicleManagement.DataSource;

namespace VehicleManagement.Tests.Services
{
    public class VehicleServiceTests
    {
        [Fact]
        public void GetAllVehicles_ReturnsVehicles()
        {
            var mockStore = new Mock<IDataStore>();
            var dataStoreRecords = new DataStore { Vehicles = [new() { Id = 1, Name = "Car" }] };
            mockStore.Setup(s => s.Records).Returns(dataStoreRecords);
            var service = new VehicleService(mockStore.Object);

            var vehicles = service.GetAllVehicles();

            Assert.Single(vehicles);
            Assert.Equal("Car", vehicles[0].Name);
        }

        [Fact]
        public void AddVehicle_AddsVehicleAndSaves()
        {
            var mockStore = new Mock<IDataStore>();
            var dataStoreRecords = new DataStore { Vehicles = [] };
            mockStore.Setup(s => s.Records).Returns(dataStoreRecords);
            var service = new VehicleService(mockStore.Object);
            var vehicle = new Vehicle { Id = 2, Name = "Truck" };

            service.AddVehicle(vehicle);

            Assert.Single(dataStoreRecords.Vehicles);
            mockStore.Verify(s => s.SaveData(), Times.Once);
        }

        [Fact]
        public void DeleteVehicle_RemovesVehicleAndSaves()
        {
            var mockStore = new Mock<IDataStore>();
            var dataStoreRecords = new DataStore { Vehicles = [new() { Id = 3, Name = "Bus" }] };
            mockStore.Setup(s => s.Records).Returns(dataStoreRecords);
            var service = new VehicleService(mockStore.Object);

            var result = service.DeleteVehicle(3);

            Assert.True(result);
            Assert.Empty(dataStoreRecords.Vehicles);
            mockStore.Verify(s => s.SaveData(), Times.Once);
        }
    }
}
