using Moq;
using VehicleManagement.Models;
using VehicleManagement.Services;
using VehicleManagement.DataSource;

namespace VehicleManagement.Tests.Services
{
    public class TripServiceTests
    {
        [Fact]
        public void GetTripsByVehicleId_ReturnsTrips()
        {
            var mockStore = new Mock<IDataStore>();
            var dataStoreRecords = new DataStore { Trips = [new Trip { Id = 1, VehicleId = 1 }] };
            mockStore.Setup(s => s.Records).Returns(dataStoreRecords);
            var service = new TripService(mockStore.Object);

            var trips = service.GetTripsByVehicleId(1);

            Assert.Single(trips);
            Assert.Equal(1, trips[0].VehicleId);
        }

        [Fact]
        public void AddTrip_AddsTripAndSaves()
        {
            var mockStore = new Mock<IDataStore>();
            var dataStoreRecords = new DataStore { Trips = [] };
            mockStore.Setup(s => s.Records).Returns(dataStoreRecords);
            var service = new TripService(mockStore.Object);
            var trip = new Trip { Id = 2, VehicleId = 2 };

            service.AddTrip(trip);

            Assert.Single(dataStoreRecords.Trips);
            mockStore.Verify(s => s.SaveData(), Times.Once);
        }

        [Fact]
        public void DeleteTrip_RemovesTripAndSaves()
        {
            var mockStore = new Mock<IDataStore>();
            var dataStoreRecords = new DataStore { Trips = [new Trip { Id = 3, VehicleId = 3 }] };
            mockStore.Setup(s => s.Records).Returns(dataStoreRecords);
            var service = new TripService(mockStore.Object);

            var result = service.DeleteTrip(3);

            Assert.True(result);
            Assert.Empty(dataStoreRecords.Trips);
            mockStore.Verify(s => s.SaveData(), Times.Once);
        }
    }
}
